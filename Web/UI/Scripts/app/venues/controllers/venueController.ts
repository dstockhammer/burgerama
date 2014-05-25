/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenueScope extends ng.IScope {
        venues: Array<IVenue>;

        panTo: (venue: IVenue) => void;
        addVote: (venue: IVenue) => void;
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

            var unregisterVenueAdded = this.$rootScope.$on('VenueAdded', (event, venue) => {
                this.$scope.venues.push(venue);
            });
            this.$scope.$on('$destroy', () => unregisterVenueAdded());
        }

        private load() {
            return this.venueResource.all(data => {
                this.$scope.venues = data;
                this.$rootScope.$broadcast('VenuesLoaded', this.$scope.venues);
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private panTo(venue: IVenue) {
            this.$rootScope.$broadcast('PanToClicked', venue.location.latitude, venue.location.longitude);
        }

        private addVote(venue: IVenue) {
            console.log('add vote clicked');
        }
    }

    export interface IAddVenueScope extends ng.IScope {
        venue: IVenue;

        ok: () => void;
        cancel: () => void;
    }

    export class AddVenueController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IAddVenueScope,
            private $modalInstance,
            private venueResource,
            private toaster,
            private venue)
        {
            this.$scope.venue = venue;
            this.$scope.ok = () => this.ok();
            this.$scope.cancel = () => this.cancel();
        }

        private ok() {
            this.$modalInstance.close();

            var resource = new this.venueResource(this.$scope.venue);
            resource.$create(() => {
                this.toaster.pop('success', 'Success', 'Added venue: ' + this.$scope.venue.title);
                this.$rootScope.$broadcast('VenueAdded', this.$scope.venue);
            }, err => {
                if (err.status == 401) {
                    this.toaster.pop('error', 'Unauthorized', 'You are not authorized to suggest venues. Please log in or create an account.');
                } else if (err.status == 409) {
                    this.toaster.pop('error', 'Conflict', 'This venue has already been suggested.');
                } else {
                    this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                }
            });
        }

        private cancel() {
            this.$modalInstance.dismiss();
        }
    }
}

Burgerama.app.controller('VenueController', ['$rootScope', '$scope', '$modal', 'VenueResource', 'toaster', ($rootScope, $scope, $modal, venueResource, toaster) =>
    new Burgerama.Venues.VenueController($rootScope, $scope, $modal, venueResource, toaster)
]);
Burgerama.app.controller('AddVenueController', ['$rootScope', '$scope', '$modalInstance', 'VenueResource', 'toaster', 'venue', ($rootScope, $scope, $modalInstance, venueResource, toaster, venue) =>
    new Burgerama.Venues.AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster, venue)
]);
