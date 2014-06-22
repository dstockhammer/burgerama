/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var VenueController = (function () {
            function VenueController($rootScope, $scope, $modal, toaster, venueResource, voteResource, votingCandidateResource) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.toaster = toaster;
                this.venueResource = venueResource;
                this.voteResource = voteResource;
                this.votingCandidateResource = votingCandidateResource;
                this.venueContextKey = 'venues';
                this.$scope.venues = null;
                this.$scope.candidates = [];
                this.$scope.panTo = function (venue) {
                    return _this.panTo(venue);
                };
                this.$scope.addVote = function (venue) {
                    return _this.addVote(venue);
                };

                this.load();

                var unregisterVenueAdded = this.$rootScope.$on('VenueAdded', function (event, venue) {
                    _this.$scope.venues.push(venue);
                });
                this.$scope.$on('$destroy', function () {
                    return unregisterVenueAdded();
                });
            }
            VenueController.prototype.load = function () {
                var _this = this;
                this.venueResource.all(function (venues) {
                    _this.$scope.venues = venues;
                    _this.$rootScope.$emit('VenuesLoaded', _this.$scope.venues);

                    venues.forEach(function (venue) {
                        if (typeof (_this.$scope.venues[venue.id]) === 'undefined') {
                            _this.votingCandidateResource.get({ context: _this.venueContextKey, reference: venue.id }, function (candidate) {
                                _this.$scope.candidates[venue.id] = candidate;
                            });
                        }
                    });
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            VenueController.prototype.panTo = function (venue) {
                this.$rootScope.$emit('VenueSelected', venue);
            };

            VenueController.prototype.addVote = function (venue) {
                var _this = this;
                var resource = new this.voteResource({
                    context: this.venueContextKey,
                    reference: venue.id
                });

                resource.$create(function (candidate) {
                    _this.toaster.pop('success', 'Success', 'Thanks for your contribution!');
                    _this.$rootScope.$emit('VoteAdded', candidate.userVote);

                    _this.$scope.candidates[venue.id] = candidate;
                    _this.$scope.venues.forEach(function (v) {
                        if (v.id == venue.id) {
                            v.totalVotes++;
                        }
                    });
                }, function (err) {
                    if (err.status == 401) {
                        _this.toaster.pop('error', 'Unauthorized', 'You are not authorized to vote. Please log in or create an account.');
                    } else if (err.status == 409) {
                        _this.toaster.pop('error', 'Conflict', 'You have already voted for this item.');
                    } else {
                        _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                    }
                });
            };
            return VenueController;
        })();
        Venues.VenueController = VenueController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('VenueController', [
    '$rootScope', '$scope', '$modal', 'toaster', 'VenueResource', 'VoteResource', 'VotingCandidateResource',
    function ($rootScope, $scope, $modal, toaster, venueResource, voteResource, votingCandidateResource) {
        return new Burgerama.Venues.VenueController($rootScope, $scope, $modal, toaster, venueResource, voteResource, votingCandidateResource);
    }
]);
//# sourceMappingURL=venueController.js.map
