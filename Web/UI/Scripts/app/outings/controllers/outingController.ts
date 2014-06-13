/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export interface IOutingScope extends ng.IScope {
        outings: Array<IOuting>;

        panTo: (outing: IOuting) => void;
        addRating: (venue: Venues.IVenue) => void;
    }

    export class OutingController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IOutingScope,
            private $modal,
            private outingResource,
            private toaster)
        {
            this.$scope.outings = null;
            this.$scope.panTo = outing => this.panTo(outing);
            this.$scope.addRating = venue => this.addRating(venue);

            this.load();
        }

        private load() {
            this.outingResource.all(data => {
                this.$scope.outings = data;
                this.$rootScope.$emit('OutingsLoaded', this.$scope.outings);
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private panTo(outing: IOuting) {
            this.$rootScope.$emit('VenueSelected', outing.venue);
        }

        private addRating(venue: Venues.IVenue) {
            this.$modal.open({
                templateUrl: '/Scripts/app/ratings/views/addRating.modal.html',
                controller: 'AddRatingController',
                resolve: {
                    context: (): Ratings.IRatingContext => {
                        return {
                            key: "venues",
                            reference: venue.id,
                            title: venue.name
                        };
                    }
                }
            });
        }
    }
}

Burgerama.app.controller('OutingController', ['$rootScope', '$scope', '$modal', 'OutingResource', 'toaster', ($rootScope, $scope, $modal, outingResource, toaster) =>
    new Burgerama.Outings.OutingController($rootScope, $scope, $modal, outingResource, toaster)
]);
