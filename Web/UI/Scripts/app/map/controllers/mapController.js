// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Map) {
        var MapController = (function () {
            function MapController($scope, $modal) {
                var _this = this;
                this.$scope = $scope;
                this.$modal = $modal;
                this.markers = new Array();
                this.places = new Array();
                this.options = {
                    center: new google.maps.LatLng(51.51, -0.11),
                    overviewMapControl: false,
                    scaleControl: false,
                    streetViewControl: false,
                    zoomControl: true,
                    zoomControlOptions: {
                        style: google.maps.ZoomControlStyle.LARGE,
                        position: google.maps.ControlPosition.LEFT_CENTER
                    },
                    mapTypeControl: false,
                    panControl: false,
                    zoom: 15,
                    styles: [
                        {
                            stylers: [
                                { hue: "#ffa200" },
                                { saturation: 20 },
                                { lightness: 10 },
                                { gamma: 0.75 }
                            ]
                        },
                        {
                            featureType: "water",
                            stylers: [
                                { hue: "#0044ff" },
                                { lightness: 40 }
                            ]
                        }
                    ]
                };
                this.$scope.mapOptions = this.options;
                this.$scope.selectedVenue = null;
                this.$scope.currentSearchTerm = '';
                this.$scope.setZoomMessage = function (zoom) {
                    return _this.setZoomMessage(zoom);
                };
                this.$scope.clearSearch = function () {
                    return _this.clearSearch();
                };
                this.$scope.showAddVenueModal = function (venue) {
                    return _this.showAddVenueModal(venue);
                };

                $scope.$watch('map', function (map) {
                    // todo: add this to markup and only bind it here
                    _this.searchBox = _this.createMapSearchBox(map, 'map-search-box');
                    google.maps.event.addListener(_this.searchBox, 'places_changed', function () {
                        _this.searchPlaces(map);
                    });
                    // todo: this breaks the search?
                    //google.maps.event.addListener(map, 'bounds_changed', () => {
                    //     this.setSearchBound(map, this.searchBox);
                    //});
                });
            }
            MapController.prototype.clearSearch = function () {
                this.clearMarkersFromTheMap();
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

            MapController.prototype.setZoomMessage = function (zoom) {
                // this is just a proof of concept. this is the way to add event listeners!
                //console.log(zoom);
            };

            MapController.prototype.showMarkerInfo = function (marker, place) {
                var _this = this;
                this.$scope.$apply(function () {
                    _this.$scope.selectedVenue = {
                        id: '',
                        description: '',
                        title: place.name,
                        address: place.formatted_address,
                        url: place.url,
                        location: {
                            latitude: place.geometry.location.lat(),
                            longitude: place.geometry.location.lng(),
                            reference: place.id
                        }
                    };
                });
            };

            MapController.prototype.createMapSearchBox = function (map, inputId) {
                var input = document.getElementById(inputId);
                map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
                return new google.maps.places.SearchBox(input);
            };

            MapController.prototype.searchPlaces = function (map) {
                var _this = this;
                this.places = this.searchBox.getPlaces();
                this.clearMarkersFromTheMap();
                var bounds = new google.maps.LatLngBounds();
                this.places.forEach(function (place) {
                    var marker = _this.placeMarkerOnTheMap(map, place);
                    google.maps.event.addDomListener(marker, 'click', function () {
                        _this.showMarkerInfo(marker, place);
                    });
                    _this.markers.push(marker);
                    bounds.extend(place.geometry.location);
                });

                map.fitBounds(bounds);
                map.setZoom(this.options.zoom);
            };

            MapController.prototype.setSearchBound = function (map) {
                var bounds = map.getBounds();
                this.searchBox.setBounds(bounds);
            };

            MapController.prototype.setCurrentLocation = function (map) {
                var _this = this;
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                        var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                        map.setCenter(initialLocation);
                        map.setZoom(_this.options.zoom);
                    });
                }
            };

            MapController.prototype.clearMarkersFromTheMap = function () {
                this.markers.forEach(function (marker) {
                    marker.setMap(null);
                });

                this.markers = new Array();
            };

            MapController.prototype.placeMarkerOnTheMap = function (map, place) {
                var image = new google.maps.MarkerImage(place.icon, new google.maps.Size(71, 71), new google.maps.Point(0, 0), new google.maps.Point(17, 34), new google.maps.Size(25, 25));

                var marker = new google.maps.Marker({
                    map: map,
                    icon: image,
                    title: place.name,
                    position: place.geometry.location,
                    animation: google.maps.Animation.DROP
                });

                return marker;
            };
            return MapController;
        })();
        Map.MapController = MapController;
    })(Burgerama.Map || (Burgerama.Map = {}));
    var Map = Burgerama.Map;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('MapController', [
    '$scope', '$modal', function ($scope, $modal) {
        return new Burgerama.Map.MapController($scope, $modal);
    }
]);
//# sourceMappingURL=mapController.js.map
