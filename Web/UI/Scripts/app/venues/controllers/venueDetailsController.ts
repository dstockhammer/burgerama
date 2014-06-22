// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenueDetailsScope extends ng.IScope {
        venue: Venue;

        votes: Array<Voting.Vote>;
        votingCandidate: Voting.Candidate;

        ratings: Array<Ratings.Rating>;
        ratingStats: any;
        ratingOrder: boolean;
        ratingCandidate: Ratings.Candidate;

        addVote: (venue: Venue) => void;
        addRating: (venue: Venue) => void;
    }

    export class VenueDetailsController {
        private venuesContextKey = 'venues';

        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IVenueDetailsScope,
            private $modal,
            private toaster,
            private starRatingService: Ratings.IStarRatingService,
            private venueResource,
            private voteResource,
            private ratingResource,
            private votingCandidateResource,
            private ratingCandidateResource,
            private venueId)
        {
            this.$scope.venue = null;
            this.$scope.votingCandidate = null;
            this.$scope.ratingCandidate = null;
            this.$scope.ratings = null;
            this.$scope.ratingStats = null;
            this.$scope.ratingOrder = true;
            this.$scope.addVote = venue => this.addVote(venue);
            this.$scope.addRating = venue => this.addRating(venue);

            this.load();
        }

        private load() {
            this.venueResource.get({ id: this.venueId }, (venue: Venue) => {
                if (venue == null) {
                    this.toaster.pop('error', 'Venue not found', 'The venue with id ' + this.venueId + ' was found.');
                    // todo: redirect to /venues
                    return;
                }

                this.loadVotes(venue);

                if (venue.outings.length > 0) {
                    this.loadRatings(venue);
                }

                this.$scope.venue = venue;
                this.$rootScope.$emit('VenuesLoaded', [this.$scope.venue]);
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });

        }

        private loadRatings(venue: Venue) {
            this.ratingCandidateResource.get({ context: this.venuesContextKey, reference: venue.id }, (candidate: Ratings.Candidate) => {
                this.$scope.ratingCandidate = candidate;
            });

            this.ratingResource.all({ context: this.venuesContextKey, reference: venue.id }, (ratings: Array<Ratings.Rating>) => {
                this.$scope.ratings = ratings;
                this.$scope.ratingStats = this.starRatingService.calculateRatingStats(ratings);
            });
        }

        private loadVotes(venue: Venue) {
            this.votingCandidateResource.get({ context: this.venuesContextKey, reference: venue.id }, (candidate: Voting.Candidate) => {
                this.$scope.votingCandidate = candidate;
            });

            this.voteResource.all({ context: this.venuesContextKey, reference: venue.id }, (votes: Array<Voting.Vote>) => {
                this.$scope.votes = votes;
            });
        }
        
        private addVote(venue: Venue) {
            //var resource = new this.voteResource(this.$scope.venue);
            //resource.$create((data) => {

            //    // todo !!

            //    this.toaster.pop('success', 'Success', 'Added vote for venue: ' + this.$scope.venue.name);
            //    this.$rootScope.$emit('VenueVoted', this.$scope.venue);
            //    this.$scope.venue.totalVotes = data["0"];
            //}, err => {
            //    if (err.status == 401) {
            //        this.toaster.pop('error', 'Unauthorized', 'You are not authorized to vote on venues. Please log in or create an account.');
            //    } else if (err.status == 409) {
            //        this.toaster.pop('error', 'Conflict', 'You have already voted on this venue.');
            //    } else {
            //        this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            //    }
            //});
        }

        private addRating(venue: Venue) {
            this.$modal.open({
                templateUrl: '/Scripts/app/ratings/views/addRating.modal.html',
                controller: 'AddRatingController',
                resolve: {
                    context: (): Ratings.IRatingContext => {
                        return {
                            key: this.venuesContextKey,
                            reference: venue.id,
                            title: venue.name
                        };
                    }
                }
            });
        }
    }
}

Burgerama.app.controller('VenueDetailsController', ['$rootScope', '$scope', '$modal', 'toaster', 'StarRatingService', 'VenueResource', 'VoteResource', 'RatingResource', 'VotingCandidateResource', 'RatingCandidateResource', 'venueId',
    ($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, votingCandidateResource, ratingCandidateResource, venueId) =>
        new Burgerama.Venues.VenueDetailsController($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, votingCandidateResource, ratingCandidateResource, venueId)
]);
