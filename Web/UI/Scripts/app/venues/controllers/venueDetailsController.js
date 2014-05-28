// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var VenueDetailsController = (function () {
            function VenueDetailsController($rootScope, $scope, venueResource, voteResource, toaster, venueId) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.venueResource = venueResource;
                this.voteResource = voteResource;
                this.toaster = toaster;
                this.venueId = venueId;
                this.$scope.venue = null;
                this.$scope.addVote = function (venue) {
                    return _this.addVote(venue);
                };

                this.load();
            }
            VenueDetailsController.prototype.load = function () {
                var _this = this;
                this.venueResource.get({ id: this.venueId }, function (data) {
                    if (data == null) {
                        _this.toaster.pop('error', 'Venue not found', 'The venue with id ' + _this.venueId + ' was found.');

                        // todo: redirect to /venues
                        return;
                    }

                    _this.$scope.venue = data;
                    _this.$rootScope.$emit('VenuesLoaded', [_this.$scope.venue]);
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });

                this.voteResource.get({ id: this.venueId }, function (data) {
                    _this.$scope.venue.votes = data.count();
                    _this.$rootScope.$emit('VenuesLoaded', [_this.$scope.venue]);
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            VenueDetailsController.prototype.addVote = function (venue) {
                var _this = this;
                var resource = new this.voteResource(this.$scope.venue);
                resource.$create(function () {
                    _this.toaster.pop('success', 'Success', 'Added vote for venue: ' + _this.$scope.venue.title);
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
            return VenueDetailsController;
        })();
        Venues.VenueDetailsController = VenueDetailsController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('VenueDetailsController', [
    '$rootScope', '$scope', 'VenueResource', 'toaster', 'venueId', function ($rootScope, $scope, venueResource, voteResource, toaster, venueId) {
        return new Burgerama.Venues.VenueDetailsController($rootScope, $scope, venueResource, voteResource, toaster, venueId);
    }
]);
//# sourceMappingURL=venueDetailsController.js.map
