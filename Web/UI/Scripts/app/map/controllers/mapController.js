// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Map) {
        var MapController = (function () {
            function MapController($rootScope, $scope, $modal) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.options = {
                    center: new google.maps.LatLng(51.51, -0.11),
                    overviewMapControl: false,
                    scaleControl: true,
                    streetViewControl: true,
                    zoomControl: true,
                    mapTypeControl: false,
                    panControl: true,
                    zoom: 15
                };
                this.$scope.mapOptions = this.options;
                this.$scope.markers = new Array();
                this.$scope.places = new Array();

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
                    venues.forEach(function (venue) {
                        _this.addMarker(null, venue);
                    });
                });
                this.$scope.$on('$destroy', function () {
                    return unregisterVenuesLoaded();
                });

                // todo: not sure if this is really a good way to communicate between controller.
                // this is basically using an event as command, which seems very wrong.
                var unregisterPanClicked = this.$rootScope.$on('PanToClicked', function (event, latitude, longitude) {
                    _this.$scope.map.panTo(new google.maps.LatLng(latitude, longitude));
                });
                this.$scope.$on('$destroy', function () {
                    return unregisterPanClicked();
                });
            }
            MapController.prototype.clearSearch = function () {
                this.clearMarkers();
                this.$scope.currentSearchTerm = '';
                this.$scope.selectedVenue = null;
            };

            MapController.prototype.showAddVenueModal = function (venue) {
                this.$modal.open({
                    templateUrl: '/Scripts/app/venues/views/addVenue.modal.html',
                    controller: 'AddVenueController',
                    resolve: {
                        venue: function () {
                            return venue;
                        }
                    }
                });
            };

            MapController.prototype.addMarker = function (place, venue) {
                var _this = this;
                // if place is null, look it up using places api
                if (place == null) {
                    var request = {
                        reference: venue.location.reference
                    };

                    var service = new google.maps.places.PlacesService(this.$scope.map);
                    service.getDetails(request, function (placeResult, status) {
                        if (status == google.maps.places.PlacesServiceStatus.OK) {
                            _this.addMarker(placeResult, venue);
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
                        animation: google.maps.Animation.DROP
                    })
                };

                // todo: bug: this adds the listener multiple times. fix plx!
                google.maps.event.addListener(markerInfo.marker, 'click', function () {
                    _this.openMarkerInfo(markerInfo);
                });

                this.$scope.markers.push(markerInfo);
            };

            MapController.prototype.openMarkerInfo = function (markerInfo) {
                var _this = this;
                this.$scope.$apply(function () {
                    _this.$scope.selectedVenue = markerInfo.venue != null ? markerInfo.venue : {
                        id: '',
                        description: '',
                        title: markerInfo.place.name,
                        address: markerInfo.place.formatted_address,
                        url: markerInfo.place.url,
                        location: {
                            latitude: markerInfo.place.geometry.location.lat(),
                            longitude: markerInfo.place.geometry.location.lng(),
                            reference: markerInfo.place.reference
                        },
                        votes: 0,
                        rating: 0
                    };
                });

                this.$scope.venueInfoWindow.open(this.$scope.map, markerInfo.marker);
            };

            MapController.prototype.clearMarkers = function () {
                this.$scope.markers.forEach(function (marker) {
                    marker.marker.setMap(null);
                });

                this.$scope.markers = new Array();
            };

            MapController.prototype.searchPlaces = function () {
                var _this = this;
                this.$scope.places = this.searchBox.getPlaces();
                this.clearMarkers();

                var bounds = new google.maps.LatLngBounds();

                this.$scope.places.forEach(function (place) {
                    _this.addMarker(place);
                    bounds.extend(place.geometry.location);
                });

                this.$scope.map.fitBounds(bounds);
            };

            // todo: this is not very "angular"... refactor this if possible
            MapController.prototype.createMapSearchBox = function (inputId) {
                var input = document.getElementById(inputId);
                this.$scope.map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
                return new google.maps.places.SearchBox(input);
            };
            return MapController;
        })();
        Map.MapController = MapController;
    })(Burgerama.Map || (Burgerama.Map = {}));
    var Map = Burgerama.Map;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('MapController', [
    '$rootScope', '$scope', '$modal', function ($rootScope, $scope, $modal) {
        return new Burgerama.Map.MapController($rootScope, $scope, $modal);
    }
]);
//# sourceMappingURL=mapController.js.map
