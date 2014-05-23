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
                this.$scope.setZoomMessage = function (zoom) {
                    return _this.setZoomMessage(zoom);
                };
                this.$scope.showAddVenueModal = function () {
                    return _this.showAddVenueModal();
                };

                $scope.$watch('map', function (map) {
                    console.log(map);

                    _this.searchBox = _this.createMapSearchBox(map, 'map-search-box');
                    google.maps.event.addListener(_this.searchBox, 'places_changed', function () {
                        _this.searchPlaces(map, _this.searchBox, _this.places, _this.markers, _this.options);
                    });
                    // todo: this breaks the search?
                    //google.maps.event.addListener(map, 'bounds_changed', () => {
                    //     this.setSearchBound(map, this.searchBox);
                    //});
                });
            }
            MapController.prototype.showAddVenueModal = function () {
                this.$modal.open({
                    templateUrl: '/Scripts/app/venues/views/addVenue.modal.html',
                    controller: 'AddVenueController'
                });
            };

            MapController.prototype.setZoomMessage = function (zoom) {
                // this is just a proof of concept. this is the way to add event listeners!
                //console.log(zoom);
            };

            MapController.prototype.createMapSearchBox = function (map, inputId) {
                var input = document.getElementById(inputId);
                map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
                return new google.maps.places.SearchBox(input);
            };

            MapController.prototype.searchPlaces = function (map, searchBox, places, markers, options) {
                var _this = this;
                places = searchBox.getPlaces();
                this.clearMarkersFromTheMap(markers);
                var bounds = new google.maps.LatLngBounds();
                places.forEach(function (place) {
                    var marker = _this.placeMarkerOnTheMap(map, place);
                    google.maps.event.addDomListener(marker, 'click', function () {
                        _this.showMarkerInfo(marker, place);
                    });
                    markers.push(marker);
                    bounds.extend(place.geometry.location);
                });

                map.fitBounds(bounds);
                map.setZoom(options.zoom);
            };

            MapController.prototype.setSearchBound = function (map, searchBox) {
                var bounds = map.getBounds();
                searchBox.setBounds(bounds);
            };

            MapController.prototype.setCurrentLocation = function (map, options) {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                        var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                        map.setCenter(initialLocation);
                        map.setZoom(options.zoom);
                    });
                }
            };

            // todo: refactor this to use the angular way! model binding and stuff
            MapController.prototype.showMarkerInfo = function (marker, place) {
                var infoBox = document.getElementById('information-box');
                var innerHTML = '<span><b>';
                innerHTML += place.url ? '<a href = "' + place.url + '" target = "new">' : '<a>';
                innerHTML += place.name + '</a></b> | ' + place.formatted_address + ' | </span>';
                innerHTML += '<button type="button" class="btn btn-default" ng-click="showAddVenueModal();"><span class="glyphicon glyphicon-plus"></span> Add venue</button>';
                infoBox.innerHTML = innerHTML;
            };

            MapController.prototype.clearMarkersFromTheMap = function (markers) {
                markers.forEach(function (marker) {
                    marker.setMap(null);
                });

                markers = new Array();
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
