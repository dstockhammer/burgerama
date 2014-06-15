// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var VenueDetailsController = (function () {
            function VenueDetailsController($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, venueId) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.toaster = toaster;
                this.starRatingService = starRatingService;
                this.venueResource = venueResource;
                this.voteResource = voteResource;
                this.ratingResource = ratingResource;
                this.venueId = venueId;
                this.$scope.venue = null;
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
                        _this.ratingResource.all({ context: 'venues', reference: venue.id }, function (ratings) {
                            _this.$scope.ratings = ratings;
                            _this.$scope.ratingStats = _this.starRatingService.calculateRatingStats(ratings);
                        });
                    }
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
                //this.voteResource.get({ id: this.venueId }, data => {
                //    this.$scope.venue.votes = data.count();
                //}, err => {
                //    this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                //});
            };

            VenueDetailsController.prototype.addVote = function (venue) {
                var _this = this;
                var resource = new this.voteResource(this.$scope.venue);
                resource.$create(function () {
                    _this.toaster.pop('success', 'Success', 'Added vote for venue: ' + _this.$scope.venue.name);
                    _this.$rootScope.$emit('VenueVoted', _this.$scope.venue);
                    console.log('add vote clicked');
                }, function (err) {
                    if (err.status == 401) {
                        _this.toaster.pop('error', 'Unauthorized', 'You are not authorized to vote on venues. Please log in or create an account.');
                    } else if (err.status == 409) {
                        _this.toaster.pop('error', 'Conflict', 'You have already voted on this venue.');
                    } else {
                        _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                    }
                });
            };

            VenueDetailsController.prototype.addRating = function (venue) {
                this.$modal.open({
                    templateUrl: '/Scripts/app/ratings/views/addRating.modal.html',
                    controller: 'AddRatingController',
                    resolve: {
                        context: function () {
                            return {
                                key: "venues",
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
    '$rootScope', '$scope', '$modal', 'toaster', 'StarRatingService', 'VenueResource', 'VoteResource', 'RatingResource', 'venueId',
    function ($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, venueId) {
        return new Burgerama.Venues.VenueDetailsController($rootScope, $scope, $modal, toaster, starRatingService, venueResource, voteResource, ratingResource, venueId);
    }
]);
//# sourceMappingURL=venueDetailsController.js.map
