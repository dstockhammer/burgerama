/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenueScope extends ng.IScope {
        venues: Array<Venue>;

        panTo: (venue: Venue) => void;
        addVote: (venue: Venue) => void;
    }

    export class VenueController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IVenueScope,
            private $modal,
            private venueResource,
            private toaster)
        {
            this.$scope.venues = null;
            this.$scope.panTo = venue => this.panTo(venue);
            this.$scope.addVote = venue => this.addVote(venue);

            this.load();

            var unregisterVenueAdded = this.$rootScope.$on('VenueAdded', (event, venue: Venue) => {
                this.$scope.venues.push(venue);
            });
            this.$scope.$on('$destroy', () => unregisterVenueAdded());
        }

        private load() {
            this.venueResource.all((venues: Array<Venue>) => {
                this.$scope.venues = venues;
                this.$rootScope.$emit('VenuesLoaded', this.$scope.venues);
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private panTo(venue: Venue) {
            this.$rootScope.$emit('VenueSelected', venue);
        }

        private addVote(venue: Venue) {
            console.log('add vote clicked');
        }
    }
}

Burgerama.app.controller('VenueController', ['$rootScope', '$scope', '$modal', 'VenueResource', 'toaster', ($rootScope, $scope, $modal, venueResource, toaster) =>
    new Burgerama.Venues.VenueController($rootScope, $scope, $modal, venueResource, toaster)
]);
