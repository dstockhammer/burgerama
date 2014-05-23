// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />

module Burgerama.Map {
    export interface IMapScope extends ng.IScope {
        mapOptions: google.maps.MapOptions;
        setZoomMessage: (zoom: number) => void;
        showAddVenueModal: () => void;
    }

    export class MapController {
        private searchBox: google.maps.places.SearchBox;
        private markers: Array<google.maps.Marker> = new Array<google.maps.Marker>();
        private places: Array<google.maps.places.PlaceResult> = new Array<google.maps.places.PlaceResult>();
        private options: google.maps.MapOptions = {
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
            styles: [{
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
        
        constructor(private $scope: IMapScope, private $modal) {
            this.$scope.mapOptions = this.options;
            this.$scope.setZoomMessage = zoom => this.setZoomMessage(zoom);
            this.$scope.showAddVenueModal = () => this.showAddVenueModal();
            
            $scope.$watch('map', map => {
                console.log(map);

                this.searchBox = this.createMapSearchBox(map, 'map-search-box');
                google.maps.event.addListener(this.searchBox, 'places_changed', () => {
                     this.searchPlaces(map, this.searchBox, this.places, this.markers, this.options);
                });

                // todo: this breaks the search?
                //google.maps.event.addListener(map, 'bounds_changed', () => {
                //     this.setSearchBound(map, this.searchBox);
                //});
            });
        }

        private showAddVenueModal() {
            this.$modal.open({
                templateUrl: '/Scripts/app/venues/views/addVenue.modal.html',
                controller: 'AddVenueController'
            });
        }

        private setZoomMessage(zoom) {
            // this is just a proof of concept. this is the way to add event listeners!
            //console.log(zoom);
        }

        private createMapSearchBox(map: google.maps.Map, inputId: string) {
            var input = <HTMLInputElement>document.getElementById(inputId);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            return new google.maps.places.SearchBox(input);
        }

        private searchPlaces(map: google.maps.Map, searchBox: google.maps.places.SearchBox, places: Array<google.maps.places.PlaceResult>, markers: Array<google.maps.Marker>, options: google.maps.MapOptions) {
            places = searchBox.getPlaces();
            this.clearMarkersFromTheMap(markers);
            var bounds = new google.maps.LatLngBounds();
            places.forEach((place) => {
                var marker = this.placeMarkerOnTheMap(map, place);
                google.maps.event.addDomListener(marker, 'click', () => { this.showMarkerInfo(marker, place); });
                markers.push(marker);
                bounds.extend(place.geometry.location);
            });

            map.fitBounds(bounds);
            map.setZoom(options.zoom);
        }

        private setSearchBound(map: google.maps.Map, searchBox: google.maps.places.SearchBox) {
            var bounds = map.getBounds();
            searchBox.setBounds(bounds);
        }

        private setCurrentLocation(map: google.maps.Map, options: google.maps.MapOptions) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(position => {
                    var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    map.setCenter(initialLocation);
                    map.setZoom(options.zoom);
                });
            }
        }

        // todo: refactor this to use the angular way! model binding and stuff
        private showMarkerInfo(marker: google.maps.Marker, place: google.maps.places.PlaceResult) {
            var infoBox = document.getElementById('information-box');
            var innerHTML = '<span><b>';
            innerHTML += place.url ? '<a href = "' + place.url + '" target = "new">' : '<a>';
            innerHTML += place.name + '</a></b> | ' + place.formatted_address + ' | </span>';
            innerHTML += '<button type="button" class="btn btn-default" ng-click="showAddVenueModal();"><span class="glyphicon glyphicon-plus"></span> Add venue</button>';
            infoBox.innerHTML = innerHTML;
        }

        private clearMarkersFromTheMap(markers: Array<google.maps.Marker>) {
            markers.forEach((marker) => {
                marker.setMap(null);
            });

            markers = new Array<google.maps.Marker>();
        }

        private placeMarkerOnTheMap(map: google.maps.Map, place: google.maps.places.PlaceResult) {
            var image = new google.maps.MarkerImage(
                place.icon,
                new google.maps.Size(71, 71),
                new google.maps.Point(0, 0),
                new google.maps.Point(17, 34),
                new google.maps.Size(25, 25)
            );

            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                title: place.name,
                position: place.geometry.location,
                animation: google.maps.Animation.DROP
            });

            return marker;
        }
    }
}

Burgerama.app.controller('MapController', ['$scope', '$modal', ($scope, $modal) =>
    new Burgerama.Map.MapController($scope, $modal)
]);
