/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenueScope extends ng.IScope {
        venues: Array<Venue>;
        votingCandidates: Array<Voting.Candidate>;

        panTo: (venue: Venue) => void;
        addVote: (venue: Venue) => void;
    }

    export class VenueController {
        private venueContextKey = 'venues';

        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IVenueScope,
            private $modal,
            private toaster,
            private venueResource,
            private voteResource,
            private votingCandidateResource)
        {
            this.$scope.venues = null;
            this.$scope.votingCandidates = [];
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

                venues.forEach((venue: Venue) => {
                    if (typeof (this.$scope.votingCandidates[venue.id]) === 'undefined') {
                        this.votingCandidateResource.get({ context: this.venueContextKey, reference: venue.id }, (candidate: Voting.Candidate) => {
                            this.$scope.votingCandidates[venue.id] = candidate;
                        });
                    }
                });
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private panTo(venue: Venue) {
            this.$rootScope.$emit('VenueSelected', venue);
        }

        private addVote(venue: Venue) {
            var resource = new this.voteResource({
                context: this.venueContextKey,
                reference: venue.id
            });

            resource.$create((candidate: Voting.Candidate) => {
                this.toaster.pop('success', 'Success', 'Thanks for your contribution!');
                this.$rootScope.$emit('VoteAdded', candidate.userVote);

                this.$scope.votingCandidates[venue.id] = candidate;
                this.$scope.venues.forEach((v: Venue) => {
                    if (v.id == venue.id) {
                        v.totalVotes++;
                    }
                });
            }, err => {
                if (err.status == 401) {
                    this.toaster.pop('error', 'Unauthorized', 'You are not authorized to vote. Please log in or create an account.');
                } else if (err.status == 409) {
                    this.toaster.pop('error', 'Conflict', 'You have already voted for this item.');
                } else {
                    this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                }
            });
        }
    }
}

Burgerama.app.controller('VenueController', ['$rootScope', '$scope', '$modal', 'toaster', 'VenueResource', 'VoteResource', 'VotingCandidateResource',
    ($rootScope, $scope, $modal, toaster, venueResource, voteResource, votingCandidateResource) =>
        new Burgerama.Venues.VenueController($rootScope, $scope, $modal, toaster, venueResource, voteResource, votingCandidateResource)
]);
