/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export interface IOutingScope extends ng.IScope {
        outings: Array<IOuting>;
        candidates: Array<Ratings.Candidate>;

        panTo: (outing: IOuting) => void;
        addRating: (venue: Venues.IVenue) => void;
    }

    export class OutingController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IOutingScope,
            private $modal,
            private toaster,
            private outingResource,
            private candidateResource)
        {
            this.$scope.outings = null;
            this.$scope.candidates = [];
            this.$scope.panTo = outing => this.panTo(outing);
            this.$scope.addRating = venue => this.addRating(venue);

            this.load();
        }

        private load() {
            this.outingResource.all((outings: Array<IOuting>) => {
                this.$scope.outings = outings;
                this.$rootScope.$emit('OutingsLoaded', this.$scope.outings);

                outings.forEach((outing: IOuting) => {
                    if (typeof(this.$scope.candidates[outing.venue.id]) === 'undefined') {
                        this.candidateResource.get({ context: 'venues', reference: outing.venue.id }, (candidate: Ratings.Candidate) => {
                            this.$scope.candidates[outing.venue.id] = candidate;
                        });
                    }
                });
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

Burgerama.app.controller('OutingController', ['$rootScope', '$scope', '$modal', 'toaster', 'OutingResource', 'CandidateResource', ($rootScope, $scope, $modal, toaster, outingResource, candidateResource) =>
    new Burgerama.Outings.OutingController($rootScope, $scope, $modal, toaster, outingResource, candidateResource)
]);
