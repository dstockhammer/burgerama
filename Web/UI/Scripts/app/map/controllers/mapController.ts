// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />

module Burgerama.Map {
    export interface IMapScope extends ng.IScope {
        mapOptions: google.maps.MapOptions;
        selectedVenue: Venues.IVenue;
        currentSearchTerm: any;

        setZoomMessage: (zoom: number) => void;
        clearSearch: () => void;
        showAddVenueModal: (venue: Venues.IVenue) => void;
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
            this.$scope.selectedVenue = null;
            this.$scope.currentSearchTerm = '';
            this.$scope.setZoomMessage = zoom => this.setZoomMessage(zoom);
            this.$scope.clearSearch = () => this.clearSearch();
            this.$scope.showAddVenueModal = venue => this.showAddVenueModal(venue);
            
            $scope.$watch('map', map => {
                // todo: add this to markup and only bind it here
                this.searchBox = this.createMapSearchBox(map, 'map-search-box');
                google.maps.event.addListener(this.searchBox, 'places_changed', () => {
                     this.searchPlaces(map);
                });

                // todo: this breaks the search?
                //google.maps.event.addListener(map, 'bounds_changed', () => {
                //     this.setSearchBound(map, this.searchBox);
                //});
            });
        }

        private clearSearch() {
            this.clearMarkersFromTheMap();
            this.$scope.currentSearchTerm = '';
            this.$scope.selectedVenue = null;
        }

        private showAddVenueModal(venue: Venues.IVenue) {
            this.$modal.open({
                templateUrl: '/Scripts/app/venues/views/addVenue.modal.html',
                controller: 'AddVenueController',
                resolve: {
                    venue: () => {
                        return venue;
                    }
                }
            });
        }

        private setZoomMessage(zoom) {
            // this is just a proof of concept. this is the way to add event listeners!
            //console.log(zoom);
        }

        private showMarkerInfo(marker: google.maps.Marker, place: google.maps.places.PlaceResult) {
            this.$scope.$apply(() => {
                this.$scope.selectedVenue = {
                    id: '',
                    description: '',
                    title: place.name,
                    address: place.formatted_address,
                    url: place.url,
                    location: {
                        latitude: place.geometry.location.lat(),
                        longitude: place.geometry.location.lng(),
                        reference: place.id,
                    }
                };
            });
        }

        private createMapSearchBox(map: google.maps.Map, inputId: string) {
            var input = <HTMLInputElement>document.getElementById(inputId);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            return new google.maps.places.SearchBox(input);
        }

        private searchPlaces(map: google.maps.Map) {
            this.places = this.searchBox.getPlaces();
            this.clearMarkersFromTheMap();
            var bounds = new google.maps.LatLngBounds();
            this.places.forEach((place) => {
                var marker = this.placeMarkerOnTheMap(map, place);
                google.maps.event.addDomListener(marker, 'click', () => {
                     this.showMarkerInfo(marker, place);
                });
                this.markers.push(marker);
                bounds.extend(place.geometry.location);
            });

            map.fitBounds(bounds);
            map.setZoom(this.options.zoom);
        }

        private setSearchBound(map: google.maps.Map) {
            var bounds = map.getBounds();
            this.searchBox.setBounds(bounds);
        }

        private setCurrentLocation(map: google.maps.Map) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(position => {
                    var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    map.setCenter(initialLocation);
                    map.setZoom(this.options.zoom);
                });
            }
        }

        private clearMarkersFromTheMap() {
            this.markers.forEach((marker) => {
                marker.setMap(null);
            });

            this.markers = new Array<google.maps.Marker>();
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
