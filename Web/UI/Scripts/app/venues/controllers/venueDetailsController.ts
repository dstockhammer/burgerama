module Burgerama.Venues {
    export interface IVenueDetailsScope extends ng.IScope {
        venue: IVenue;

        addVote: (venue: IVenue) => void;
    }

    export class VenueDetailsController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IVenueDetailsScope,
            private venueResource,
            private toaster,
            private venueId)
        {
            this.$scope.venue = null;
            this.$scope.addVote = venue => this.addVote(venue);

            this.load();
        }

        private load() {
            this.venueResource.get({ id: this.venueId }, data => {
                if (data == null) {
                    this.toaster.pop('error', 'Venue not found', 'The venue with id ' + this.venueId + ' was found.');
                    // todo: redirect to /venues
                    return;
                }

                this.$scope.venue = data;
                this.$rootScope.$broadcast('VenuesLoaded', [this.$scope.venue]);
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }
        
        private addVote(venue: IVenue) {
            console.log('add vote clicked');
        }
    }
}

Burgerama.app.controller('VenueDetailsController', ['$rootScope', '$scope', 'VenueResource', 'toaster', 'venueId', ($rootScope, $scope, venueResource, toaster, venueId) =>
    new Burgerama.Venues.VenueDetailsController($rootScope, $scope, venueResource, toaster, venueId)
]);
