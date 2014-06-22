// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var VenueDetailsController = (function () {
            function VenueDetailsController($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, candidateResource, venueId) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.toaster = toaster;
                this.starRatingService = starRatingService;
                this.venueResource = venueResource;
                this.voteResource = voteResource;
                this.ratingResource = ratingResource;
                this.candidateResource = candidateResource;
                this.venueId = venueId;
                this.venuesContextKey = 'venues';
                this.$scope.venue = null;
                this.$scope.candidate = null;
                this.$scope.ratings = null;
                this.$scope.ratingStats = null;
                this.$scope.ratingOrder = true;
                this.$scope.addVote = function (venue) {
                    return _this.addVote(venue);
                };
                this.$scope.addRating = function (venue) {
                    return _this.addRating(venue);
                };

                this.load();
            }
            VenueDetailsController.prototype.load = function () {
                var _this = this;
                this.venueResource.get({ id: this.venueId }, function (venue) {
                    if (venue == null) {
                        _this.toaster.pop('error', 'Venue not found', 'The venue with id ' + _this.venueId + ' was found.');

                        // todo: redirect to /venues
                        return;
                    }

                    _this.$scope.venue = venue;
                    _this.$rootScope.$emit('VenuesLoaded', [_this.$scope.venue]);

                    if (venue.totalRating != null) {
                        _this.candidateResource.get({ context: _this.venuesContextKey, reference: venue.id }, function (candidate) {
                            _this.$scope.candidate = candidate;
                        });

                        _this.ratingResource.all({ context: _this.venuesContextKey, reference: venue.id }, function (ratings) {
                            _this.$scope.ratings = ratings;
                            _this.$scope.ratingStats = _this.starRatingService.calculateRatingStats(ratings);
                        });
                    }
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });

                this.voteResource.all({ id: this.venueId }, function (data) {
                    _this.$scope.venue.totalVotes = data.length;
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            VenueDetailsController.prototype.addVote = function (venue) {
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
            };

            VenueDetailsController.prototype.addRating = function (venue) {
                var _this = this;
                this.$modal.open({
                    templateUrl: '/Scripts/app/ratings/views/addRating.modal.html',
                    controller: 'AddRatingController',
                    resolve: {
                        context: function () {
                            return {
                                key: _this.venuesContextKey,
                                reference: venue.id,
                                title: venue.name
                            };
                        }
                    }
                });
            };
            return VenueDetailsController;
        })();
        Venues.VenueDetailsController = VenueDetailsController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('VenueDetailsController', [
    '$rootScope', '$scope', '$modal', 'toaster', 'StarRatingService', 'VenueResource', 'VoteResource', 'RatingResource', 'CandidateResource', 'venueId',
    function ($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, candidateResource, venueId) {
        return new Burgerama.Venues.VenueDetailsController($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, candidateResource, venueId);
    }
]);
//# sourceMappingURL=venueDetailsController.js.map
