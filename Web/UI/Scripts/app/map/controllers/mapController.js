// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Map) {
        var MarkerType;
        (function (MarkerType) {
            MarkerType[MarkerType["Search"] = 0] = "Search";
            MarkerType[MarkerType["Venue"] = 1] = "Venue";
            MarkerType[MarkerType["Outing"] = 2] = "Outing";
        })(MarkerType || (MarkerType = {}));
        ;

        var MapController = (function () {
            function MapController($rootScope, $scope, $location, $modal) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$location = $location;
                this.$modal = $modal;
                this.options = {
                    center: new google.maps.LatLng(51.51, -0.11),
                    overviewMapControl: false,
                    scaleControl: true,
                    streetViewControl: true,
                    zoomControl: true,
                    mapTypeControl: false,
                    panControl: true,
                    zoom: 16
                };
                // todo: this is used in a workaround. see showAddVenueModal() for details.
                this.openModals = 0;
                this.currentSearchResults = [];

                this.$scope.options = this.options;
                this.$scope.markers = new Array();

                this.$scope.selectedVenue = null;
                this.$scope.currentSearchTerm = '';

                this.$scope.clearSearch = function () {
                    return _this.clearSearch();
                };
                this.$scope.showAddVenueModal = function (venue) {
                    return _this.showAddVenueModal(venue);
                };
                this.$scope.openMarkerInfo = function (markerInfo) {
                    return _this.openMarkerInfo(markerInfo);
                };

                this.$scope.$watch('map', function (map) {
                    _this.$scope.map = map;

                    // todo: add this to markup and only bind it here
                    _this.searchBox = _this.createMapSearchBox('map-search-box');
                    google.maps.event.addListener(_this.searchBox, 'places_changed', function () {
                        _this.searchPlaces();
                    });
                });

                var unregisterVenuesLoaded = this.$rootScope.$on('VenuesLoaded', function (event, venues) {
                    _this.clearMarkers();
                    _this.addMarkersForAllVenues(venues);
                });
                var unregisterOutingsLoaded = this.$rootScope.$on('OutingsLoaded', function (event, outings) {
                    _this.clearMarkers();
                    _this.addMarkersForAllOutings(outings);
                });
                var unregisterVenueSelected = this.$rootScope.$on('VenueSelected', function (event, venue) {
                    var markerInfos = _this.$scope.markers.filter(function (element) {
                        return element.venue != null && element.venue.id == venue.id;
                    });

                    if (markerInfos.length > 0)
                        _this.openMarkerInfo(markerInfos[0]);

                    _this.$scope.map.panTo(new google.maps.LatLng(venue.location.latitude, venue.location.longitude));
                    _this.$scope.map.setZoom(_this.options.zoom);
                });
                var unregisterPlaceSelected = this.$rootScope.$on('PlaceSelected', function (event, place) {
                    var markerInfos = _this.$scope.markers.filter(function (element) {
                        return element.place != null && element.place.reference == place.reference;
                    });

                    if (markerInfos.length > 0)
                        _this.openMarkerInfo(markerInfos[0]);

                    _this.$scope.map.panTo(place.geometry.location);
                    _this.$scope.map.setZoom(_this.options.zoom);
                });
                var unregisterSearchResultsLoaded = this.$rootScope.$on('SearchResultsLoaded', function (event) {
                    if (_this.currentSearchResults.length > 0) {
                        _this.clearMarkers();
                        _this.addMarkersForAllPlaces(_this.currentSearchResults);
                    }

                    // todo: add a check to ensure this is only emitted if the results actually changed. search id maybe.
                    _this.$rootScope.$emit('CurrentSearchUpdated', _this.currentSearchResults);
                });
                this.$scope.$on('$destroy', function () {
                    unregisterVenuesLoaded();
                    unregisterOutingsLoaded();
                    unregisterVenueSelected();
                    unregisterPlaceSelected();
                    unregisterSearchResultsLoaded();
                });
            }
            MapController.prototype.clearSearch = function () {
                this.clearMarkers();
                this.$scope.currentSearchTerm = '';
                this.$scope.selectedVenue = null;
            };

            MapController.prototype.showAddVenueModal = function (venue) {
                var _this = this;
                // todo: the whole openModals and closeCallback thing is a workaround for an issue
                // with ng-click in the info window. the handler is registered every time a window
                // is opened and is never unregistered, so the number increases every time. hopefully
                // there is a better(proper?) way to resolve this issue, so please investigate.
                if (this.openModals > 0)
                    return;

                this.openModals++;
                this.$modal.open({
                    templateUrl: '/Scripts/app/venues/views/addVenue.modal.html',
                    controller: 'AddVenueController',
                    resolve: {
                        venue: function () {
                            return venue;
                        },
                        closeCallback: function () {
                            return function () {
                                _this.openModals--;
                            };
                        }
                    }
                });
            };

            MapController.prototype.addMarkersForAllPlaces = function (places) {
                var _this = this;
                var bounds = new google.maps.LatLngBounds();

                places.forEach(function (place) {
                    _this.addMarker(0 /* Search */, place);
                    bounds.extend(place.geometry.location);
                });

                if (places.length == 1) {
                    this.$scope.map.panTo(places[0].geometry.location);
                    this.$scope.map.setZoom(this.options.zoom);
                } else {
                    this.$scope.map.fitBounds(bounds);
                }
            };

            MapController.prototype.addMarkersForAllVenues = function (venues) {
                var _this = this;
                var bounds = new google.maps.LatLngBounds();

                venues.forEach(function (venue) {
                    _this.addMarker(1 /* Venue */, null, venue);
                    bounds.extend(new google.maps.LatLng(venue.location.latitude, venue.location.longitude));
                });

                if (venues.length == 1) {
                    var location = new google.maps.LatLng(venues[0].location.latitude, venues[0].location.longitude);
                    this.$scope.map.panTo(location);
                    this.$scope.map.setZoom(this.options.zoom);
                } else {
                    this.$scope.map.fitBounds(bounds);
                }
            };

            MapController.prototype.addMarkersForAllOutings = function (outings) {
                var _this = this;
                var bounds = new google.maps.LatLngBounds();

                outings.forEach(function (outing) {
                    _this.addMarker(2 /* Outing */, null, outing.venue);
                    bounds.extend(new google.maps.LatLng(outing.venue.location.latitude, outing.venue.location.longitude));
                });

                if (outings.length == 1) {
                    var location = new google.maps.LatLng(outings[0].venue.location.latitude, outings[0].venue.location.longitude);
                    this.$scope.map.panTo(location);
                    this.$scope.map.setZoom(this.options.zoom);
                } else {
                    this.$scope.map.fitBounds(bounds);
                }
            };

            MapController.prototype.addMarker = function (markerType, place, venue) {
                var _this = this;
                // if place is null, look it up using places api
                if (place == null) {
                    var request = {
                        reference: venue.location.reference
                    };

                    var service = new google.maps.places.PlacesService(this.$scope.map);
                    service.getDetails(request, function (placeResult, status) {
                        if (status == google.maps.places.PlacesServiceStatus.OK) {
                            _this.addMarker(markerType, placeResult, venue);
                        } else {
                            console.error('an error occurred while retrieving place details');
                        }
                    });

                    return;
                }

                var markerInfo = {
                    venue: venue,
                    place: place,
                    marker: new google.maps.Marker({
                        map: this.$scope.map,
                        position: new google.maps.LatLng(place.geometry.location.lat(), place.geometry.location.lng()),
                        icon: this.getPinForMarkerType(markerType),
                        animation: google.maps.Animation.DROP
                    })
                };

                this.$scope.markers.push(markerInfo);
            };

            MapController.prototype.openMarkerInfo = function (markerInfo) {
                this.$scope.selectedVenue = markerInfo.venue != null ? markerInfo.venue : {
                    id: '',
                    description: '',
                    name: markerInfo.place.name,
                    address: markerInfo.place.formatted_address,
                    url: markerInfo.place.url,
                    location: {
                        latitude: markerInfo.place.geometry.location.lat(),
                        longitude: markerInfo.place.geometry.location.lng(),
                        reference: markerInfo.place.reference
                    },
                    outings: [],
                    totalVotes: 0,
                    totalRating: null
                };

                this.$scope.venueInfoWindow.open(this.$scope.map, markerInfo.marker);
            };

            MapController.prototype.clearMarkers = function () {
                this.$scope.markers.forEach(function (marker) {
                    marker.marker.setMap(null);
                });

                this.$scope.markers = new Array();
            };

            MapController.prototype.searchPlaces = function () {
                this.$location.path('search');
                this.currentSearchResults = this.searchBox.getPlaces();

                this.clearMarkers();
                this.addMarkersForAllPlaces(this.currentSearchResults);

                this.$rootScope.$emit('CurrentSearchUpdated', this.currentSearchResults);
            };

            // todo: this is not very "angular"... refactor this if possible
            MapController.prototype.createMapSearchBox = function (inputId) {
                var input = document.getElementById(inputId);
                this.$scope.map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
                return new google.maps.places.SearchBox(input);
            };

            MapController.prototype.getPinForMarkerType = function (markerType) {
                switch (markerType) {
                    case 1 /* Venue */:
                        return '/Content/img/map/pin-blue-hole.png';

                    case 2 /* Outing */:
                        return '/Content/img/map/pin-orange-hole.png';

                    default:
                    case 0 /* Search */:
                        return '/Content/img/map/pin-grey-hole.png';
                }
            };
            return MapController;
        })();
        Map.MapController = MapController;
    })(Burgerama.Map || (Burgerama.Map = {}));
    var Map = Burgerama.Map;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('MapController', [
    '$rootScope', '$scope', '$location', '$modal', function ($rootScope, $scope, $location, $modal) {
        return new Burgerama.Map.MapController($rootScope, $scope, $location, $modal);
    }
]);
//# sourceMappingURL=mapController.js.map
