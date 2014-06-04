// <reference path="../../app.ts" />

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
            private voteResource,
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
                this.$rootScope.$emit('VenuesLoaded', [this.$scope.venue]);

                this.voteResource.all({ id: this.venueId }, data => {
                    this.$scope.venue.votes = data.length;
                }, err => {
                    this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });

            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });   
        }
        
        private addVote(venue: IVenue) {
            var resource = new this.voteResource(this.$scope.venue);
            resource.$create((data) => {
                this.toaster.pop('success', 'Success', 'Added vote for venue: ' + this.$scope.venue.title);
                this.$rootScope.$emit('VenueVoted', this.$scope.venue);
                this.$scope.venue.votes = data["0"];
            }, err => {
                if (err.status == 401) {
                    this.toaster.pop('error', 'Unauthorized', 'You are not authorized to vote on venues. Please log in or create an account.');
                } else if (err.status == 409) {
                    this.toaster.pop('error', 'Conflict', 'You have already voted on this venue.');
                } else {
                    this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                }
            });
        }
    }
}

Burgerama.app.controller('VenueDetailsController', ['$rootScope', '$scope', 'VenueResource', 'VoteResource', 'toaster', 'venueId', ($rootScope, $scope, venueResource, voteResource, toaster, venueId) =>
    new Burgerama.Venues.VenueDetailsController($rootScope, $scope, venueResource, voteResource, toaster, venueId)
]);
