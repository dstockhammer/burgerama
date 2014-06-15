// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenueDetailsScope extends ng.IScope {
        venue: IVenue;
        ratings: Array<Ratings.Rating>;
        ratingStats: any;

        addVote: (venue: IVenue) => void;
    }

    export class VenueDetailsController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IVenueDetailsScope,
            private toaster,
            private starRatingService: Ratings.IStarRatingService,
            private venueResource,
            private voteResource,
            private ratingResource,
            private venueId)
        {
            this.$scope.venue = null;
            this.$scope.ratings = null;
            this.$scope.ratingStats = null;
            this.$scope.addVote = venue => this.addVote(venue);

            this.load();
        }

        private load() {
            this.venueResource.get({ id: this.venueId }, (venue: IVenue) => {
                if (venue == null) {
                    this.toaster.pop('error', 'Venue not found', 'The venue with id ' + this.venueId + ' was found.');
                    // todo: redirect to /venues
                    return;
                }

                this.$scope.venue = venue;
                this.$rootScope.$emit('VenuesLoaded', [this.$scope.venue]);

                if (venue.totalRating != null) {
                    this.ratingResource.all({ context: 'venues', reference: venue.id }, (ratings: Array<Ratings.Rating>) => {
                        this.$scope.ratings = ratings;
                        this.$scope.ratingStats = this.starRatingService.calculateRatingStats(ratings);
                    });
                }
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });

            //this.voteResource.get({ id: this.venueId }, data => {
            //    this.$scope.venue.votes = data.count();
            //}, err => {
            //    this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            //});
        }
        
        private addVote(venue: IVenue) {
            var resource = new this.voteResource(this.$scope.venue);
            resource.$create(() => {
                this.toaster.pop('success', 'Success', 'Added vote for venue: ' + this.$scope.venue.name);
                this.$rootScope.$emit('VenueVoted', this.$scope.venue);
                console.log('add vote clicked');
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

Burgerama.app.controller('VenueDetailsController', ['$rootScope', '$scope', 'toaster', 'StarRatingService', 'VenueResource', 'VoteResource', 'RatingResource', 'venueId',
    ($rootScope, $scope, toaster, starRatingService, venueResource, voteResource, ratingResource, venueId) =>
        new Burgerama.Venues.VenueDetailsController($rootScope, $scope, toaster, starRatingService, venueResource, voteResource, ratingResource, venueId)
]);
