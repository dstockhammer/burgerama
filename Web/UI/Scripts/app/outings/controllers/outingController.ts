﻿/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export interface IOutingScope extends ng.IScope {
        outings: Array<Outing>;
        candidates: Array<Ratings.Candidate>;

        panTo: (outing: Outing) => void;
        addRating: (venue: Venues.Venue) => void;
    }

    export class OutingController {
        private venueContextKey = 'venues';

        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IOutingScope,
            private $modal,
            private toaster,
            private outingResource,
            private ratingCandidateResource)
        {
            this.$scope.outings = null;
            this.$scope.candidates = [];
            this.$scope.panTo = outing => this.panTo(outing);
            this.$scope.addRating = venue => this.addRating(venue);

            this.load();
        }

        private load() {
            this.outingResource.all((outings: Array<Outing>) => {
                this.$scope.outings = outings;
                this.$rootScope.$emit('OutingsLoaded', this.$scope.outings);

                outings.forEach((outing: Outing) => {
                    if (typeof(this.$scope.candidates[outing.venue.id]) === 'undefined') {
                        this.ratingCandidateResource.get({ context: this.venueContextKey, reference: outing.venue.id }, (candidate: Ratings.Candidate) => {
                            this.$scope.candidates[outing.venue.id] = candidate;
                        });
                    }
                });
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private panTo(outing: Outing) {
            this.$rootScope.$emit('VenueSelected', outing.venue);
        }

        private addRating(venue: Venues.Venue) {
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

Burgerama.app.controller('OutingController', ['$rootScope', '$scope', '$modal', 'toaster', 'OutingResource', 'RatingCandidateResource', ($rootScope, $scope, $modal, toaster, outingResource, ratingCandidateResource) =>
    new Burgerama.Outings.OutingController($rootScope, $scope, $modal, toaster, outingResource, ratingCandidateResource)
]);
