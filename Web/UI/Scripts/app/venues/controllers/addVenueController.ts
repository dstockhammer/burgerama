module Burgerama.Venues {
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
            private venue,
            private closeCallback) {
            this.$scope.venue = venue;
            this.$scope.ok = () => this.ok();
            this.$scope.cancel = () => this.cancel();
        }

        private ok() {
            this.$modalInstance.close();
            this.closeCallback();

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
            this.closeCallback();
        }
    }
}

Burgerama.app.controller('AddVenueController', ['$rootScope', '$scope', '$modalInstance', 'VenueResource', 'toaster', 'venue', 'closeCallback', ($rootScope, $scope, $modalInstance, venueResource, toaster, venue, closeCallback) =>
    new Burgerama.Venues.AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster, venue, closeCallback)
]);
