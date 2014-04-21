/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenueScope extends ng.IScope {
        venues: Array<IVenue>;

        showAddVenueModal: () => void;
    }

    export class VenueController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IVenueScope,
            private $modal,
            private venueResource,
            private toaster)
        {
            this.load();

            this.$scope.showAddVenueModal = () => this.showAddVenueModal();

            var unregisterVenueAdded = this.$rootScope.$on('VenueAdded', (event, venue) => {
                this.$scope.venues.push(venue);
            });
            this.$scope.$on('$destroy', () => unregisterVenueAdded());
        }

        private load() {
            return this.venueResource.all(data => {
                this.$scope.venues = data;
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private showAddVenueModal() {
            this.$modal.open({
                templateUrl: '/Scripts/app/venues/views/addVenue.modal.html',
                controller: 'AddVenueController'
            });
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
            private $modalInstance, // ng.ui.IModalInstance ?
            private venueResource,
            private toaster) {

            this.$scope.venue = {
                id: null,
                title: '',
                location: {
                    reference: 'no reference',
                    latitude: 0,
                    longitude: 0
                },
                description: '',
                url: ''
            };

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
                    this.toaster.pop('error', 'Unauthorized', 'You are not authorized to perform this action.');
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
Burgerama.app.controller('AddVenueController', ['$rootScope', '$scope', '$modalInstance', 'VenueResource', 'toaster', ($rootScope, $scope, $modalInstance, venueResource, toaster) =>
    new Burgerama.Venues.AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster)
]);
