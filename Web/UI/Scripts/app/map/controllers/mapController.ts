// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />

module Burgerama.Map {
    enum MarkerType {
        Search,
        Venue,
        Outing
    };

    export interface IMarkerInfo {
        marker: google.maps.Marker;
        place: google.maps.places.PlaceResult;
        venue: Venues.IVenue;
    }

    export interface IMapScope extends ng.IScope {
        map: google.maps.Map;
        options: google.maps.MapOptions;
        markers: Array<IMarkerInfo>;

        selectedVenue: Venues.IVenue;
        currentSearchTerm: any;

        clearSearch: () => void;
        showAddVenueModal: (venue: Venues.IVenue) => void;
        openMarkerInfo: (markerInfo: IMarkerInfo) => void;
        
        venueInfoWindow: any;
    }

    export class MapController {
        private searchBox: google.maps.places.SearchBox;
        private currentSearchResults: Array<google.maps.places.PlaceResult>;

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

        // todo: this is used in a workaround. see showAddVenueModal() for details.
        private openModals = 0;

        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IMapScope,
            private $location: ng.ILocationService,
            private $modal)
        {
            this.currentSearchResults = [];

            this.$scope.options = this.options;
            this.$scope.markers = new Array<IMarkerInfo>();

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
                this.addMarkersForAllVenues(venues);
            });
            var unregisterOutingsLoaded = this.$rootScope.$on('OutingsLoaded', (event, outings: Array<Outings.IOuting>) => {
                this.clearMarkers();
                this.addMarkersForAllOutings(outings);
            });
            var unregisterVenueSelected = this.$rootScope.$on('VenueSelected', (event, venue: Venues.IVenue) => {
                var markerInfos = this.$scope.markers.filter(element => {
                    return element.venue != null && element.venue.id == venue.id;
                });

                if (markerInfos.length > 0) 
                    this.openMarkerInfo(markerInfos[0]);

                this.$scope.map.panTo(new google.maps.LatLng(venue.location.latitude, venue.location.longitude));
                this.$scope.map.setZoom(this.options.zoom);
            });
            var unregisterPlaceSelected = this.$rootScope.$on('PlaceSelected', (event, place: google.maps.places.PlaceResult) => {
                var markerInfos = this.$scope.markers.filter(element => {
                    return element.place != null && element.place.reference == place.reference;
                });

                if (markerInfos.length > 0)
                    this.openMarkerInfo(markerInfos[0]);

                this.$scope.map.panTo(place.geometry.location);
                this.$scope.map.setZoom(this.options.zoom);
            });
            var unregisterSearchResultsLoaded = this.$rootScope.$on('SearchResultsLoaded', event => {
                if (this.currentSearchResults.length > 0) {
                    this.clearMarkers();
                    this.addMarkersForAllPlaces(this.currentSearchResults);
                }

                // todo: add a check to ensure this is only emitted if the results actually changed. search id maybe.
                this.$rootScope.$emit('CurrentSearchUpdated', this.currentSearchResults);
            });
            this.$scope.$on('$destroy', () => {
                unregisterVenuesLoaded();
                unregisterOutingsLoaded();
                unregisterVenueSelected();
                unregisterPlaceSelected();
                unregisterSearchResultsLoaded();
            });
        }

        private clearSearch() {
            this.clearMarkers();
            this.$scope.currentSearchTerm = '';
            this.$scope.selectedVenue = null;
        }

        private showAddVenueModal(venue: Venues.IVenue) {
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
                    venue: () => {
                        return venue;
                    },
                    closeCallback: () => {
                        return () => { this.openModals--; };
                    }
                }
            });
        }

        private addMarkersForAllPlaces(places: Array<google.maps.places.PlaceResult>) {
            var bounds = new google.maps.LatLngBounds();

            places.forEach(place => {
                this.addMarker(MarkerType.Search, place);
                bounds.extend(place.geometry.location);
            });
            
            this.$scope.map.fitBounds(bounds);
        }

        private addMarkersForAllVenues(venues: Array<Venues.IVenue>) {
            var bounds = new google.maps.LatLngBounds();

            venues.forEach(venue => {
                this.addMarker(MarkerType.Venue, null, venue);
                bounds.extend(new google.maps.LatLng(venue.location.latitude, venue.location.longitude));
            });

            this.$scope.map.fitBounds(bounds);
        }

        private addMarkersForAllOutings(outings: Array<Outings.IOuting>) {
            var bounds = new google.maps.LatLngBounds();

            outings.forEach(outing => {
                this.addMarker(MarkerType.Outing, null, outing.venue);
                bounds.extend(new google.maps.LatLng(outing.venue.location.latitude, outing.venue.location.longitude));
            });

            this.$scope.map.fitBounds(bounds);
        }

        private addMarker(markerType: MarkerType, place: google.maps.places.PlaceResult, venue?: Venues.IVenue) {
            // if place is null, look it up using places api
            if (place == null) {
                var request = {
                    reference: venue.location.reference
                };

                var service = new google.maps.places.PlacesService(this.$scope.map);
                service.getDetails(request, (placeResult, status) => {
                    if (status == google.maps.places.PlacesServiceStatus.OK) {
                        this.addMarker(markerType, placeResult, venue);
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
                    icon: this.getPinForMarkerType(markerType),
                    animation: google.maps.Animation.DROP
                })
            }
            
            this.$scope.markers.push(markerInfo);
        }

        private openMarkerInfo(markerInfo: IMarkerInfo) {
            this.$scope.selectedVenue = markerInfo.venue != null
                ? markerInfo.venue
                : {
                    id: '',
                    description: '',
                    name:  markerInfo.place.name,
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

            this.$scope.venueInfoWindow.open(this.$scope.map, markerInfo.marker);
        }

        private clearMarkers() {
            this.$scope.markers.forEach((marker) => {
                marker.marker.setMap(null);
            });

            this.$scope.markers = new Array<IMarkerInfo>();
        }

        private searchPlaces() {
            this.$location.path('search');
            this.currentSearchResults = this.searchBox.getPlaces();

            this.clearMarkers();
            this.addMarkersForAllPlaces(this.currentSearchResults);

            this.$rootScope.$emit('CurrentSearchUpdated', this.currentSearchResults);
        }
        
        // todo: this is not very "angular"... refactor this if possible
        private createMapSearchBox(inputId: string) {
            var input = <HTMLInputElement>document.getElementById(inputId);
            this.$scope.map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            return new google.maps.places.SearchBox(input);
        }

        private getPinForMarkerType(markerType: MarkerType) {
            switch (markerType) {
                case MarkerType.Venue:
                    return '/Content/img/map/pin-blue-hole.png';

                case MarkerType.Outing:
                    return '/Content/img/map/pin-orange-hole.png';

                default:
                case MarkerType.Search:
                    return '/Content/img/map/pin-grey-hole.png';
            }
        }
    }
}

Burgerama.app.controller('MapController', ['$rootScope', '$scope', '$location', '$modal', ($rootScope, $scope, $location, $modal) =>
    new Burgerama.Map.MapController($rootScope, $scope, $location, $modal)
]);
