// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />

module Burgerama.Map {
    export interface IMarkerInfo {
        marker: google.maps.Marker;
        place: google.maps.places.PlaceResult;
        venue: Venues.IVenue;
    }

    export interface IMapScope extends ng.IScope {
        map: google.maps.Map;
        mapOptions: google.maps.MapOptions;
        markers: Array<IMarkerInfo>;
        places: Array < google.maps.places.PlaceResult>;

        selectedVenue: Venues.IVenue;
        currentSearchTerm: any;

        clearSearch: () => void;
        showAddVenueModal: (venue: Venues.IVenue) => void;
        openMarkerInfo: (markerInfo: IMarkerInfo) => void;

        venueInfoWindow: any;
    }

    export class MapController {
        private searchBox: google.maps.places.SearchBox;
        private options: google.maps.MapOptions = {
            center: new google.maps.LatLng(51.51, -0.11),
            overviewMapControl: false,
            scaleControl: true,
            streetViewControl: true,
            zoomControl: true,
            mapTypeControl: false,
            panControl: true,
            zoom: 15
        };
        
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IMapScope,
            private $modal)
        {
            this.$scope.mapOptions = this.options;
            this.$scope.markers = new Array<IMarkerInfo>();
            this.$scope.places = new Array<google.maps.places.PlaceResult>();

            this.$scope.selectedVenue = null;
            this.$scope.currentSearchTerm = '';

            this.$scope.clearSearch = () => this.clearSearch();
            this.$scope.showAddVenueModal = venue => this.showAddVenueModal(venue);
            this.$scope.openMarkerInfo = markerInfo => this.openMarkerInfo(markerInfo);
            
            this.$scope.$watch('map', map => {
                this.$scope.map = map;

                // todo: add this to markup and only bind it here
                this.searchBox = this.createMapSearchBox('map-search-box');
                google.maps.event.addListener(this.searchBox, 'places_changed', () => {
                     this.searchPlaces();
                });
            });

            var unregisterVenuesLoaded = this.$rootScope.$on('VenuesLoaded', (event, venues: Array<Venues.IVenue>) => {
                this.clearMarkers();
                venues.forEach(venue => {
                    this.addMarker(null, venue);
                });
            });
            this.$scope.$on('$destroy', () => unregisterVenuesLoaded());

            // todo: not sure if this is really a good way to communicate between controller.
            // this is basically using an event as command, which seems very wrong.
            var unregisterPanClicked = this.$rootScope.$on('PanToClicked', (event, venue: Venues.IVenue) => {
                this.$scope.map.panTo(new google.maps.LatLng(venue.location.latitude, venue.location.longitude));
            });
            this.$scope.$on('$destroy', () => unregisterPanClicked());
        }

        private clearSearch() {
            this.clearMarkers();
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

        private addMarker(place: google.maps.places.PlaceResult, venue?: Venues.IVenue) {
            // if place is null, look it up using places api
            if (place == null) {
                var request = {
                    reference: venue.location.reference
                };

                var service = new google.maps.places.PlacesService(this.$scope.map);
                service.getDetails(request, (placeResult, status) => {
                    if (status == google.maps.places.PlacesServiceStatus.OK) {
                        this.addMarker(placeResult, venue);
                    } else {
                        console.error('an error occurred while retrieving place details');
                    }
                });

                return;
            }

            var markerInfo: IMarkerInfo = {
                venue: venue,
                place: place,
                marker: new google.maps.Marker({
                    map: this.$scope.map,
                    position: new google.maps.LatLng(place.geometry.location.lat(), place.geometry.location.lng()),
                    animation: google.maps.Animation.DROP
                })
            }

            // todo: bug: this adds the listener multiple times. fix plx!
            google.maps.event.addListener(markerInfo.marker, 'click', () => {
                this.openMarkerInfo(markerInfo);
            });

            this.$scope.markers.push(markerInfo);
        }

        private openMarkerInfo(markerInfo: IMarkerInfo) {
            this.$scope.$apply(() => {
                this.$scope.selectedVenue = markerInfo.venue != null
                    ? markerInfo.venue
                    : {
                        id: '',
                        description: '',
                        title:  markerInfo.place.name,
                        address:  markerInfo.place.formatted_address,
                        url: markerInfo.place.url,
                        location: {
                            latitude: markerInfo.place.geometry.location.lat(),
                            longitude: markerInfo.place.geometry.location.lng(),
                            reference: markerInfo.place.reference,
                        },
                        votes: 0,
                        rating: 0
                    };
            });

            this.$scope.venueInfoWindow.open(this.$scope.map, markerInfo.marker);
        }

        private clearMarkers() {
            this.$scope.markers.forEach((marker) => {
                marker.marker.setMap(null);
            });

            this.$scope.markers = new Array<IMarkerInfo>();
        }

        private searchPlaces() {
            this.$scope.places = this.searchBox.getPlaces();
            this.clearMarkers();

            var bounds = new google.maps.LatLngBounds();

            this.$scope.places.forEach(place => {
                this.addMarker(place);
                bounds.extend(place.geometry.location);
            });

            this.$scope.map.fitBounds(bounds);
        }
        
        // todo: this is not very "angular"... refactor this if possible
        private createMapSearchBox(inputId: string) {
            var input = <HTMLInputElement>document.getElementById(inputId);
            this.$scope.map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            return new google.maps.places.SearchBox(input);
        }
    }
}

Burgerama.app.controller('MapController', ['$rootScope', '$scope', '$modal', ($rootScope, $scope, $modal) =>
    new Burgerama.Map.MapController($rootScope, $scope, $modal)
]);
