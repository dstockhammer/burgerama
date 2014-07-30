/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Outings) {
        var OutingController = (function () {
            function OutingController($rootScope, $scope, $modal, toaster, outingResource, ratingCandidateResource) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.toaster = toaster;
                this.outingResource = outingResource;
                this.ratingCandidateResource = ratingCandidateResource;
                this.venueContextKey = 'venues';
                this.$scope.outings = null;
                this.$scope.candidates = [];
                this.$scope.panTo = function (outing) {
                    return _this.panTo(outing);
                };
                this.$scope.addRating = function (venue) {
                    return _this.addRating(venue);
                };

                this.load();
            }
            OutingController.prototype.load = function () {
                var _this = this;
                this.outingResource.all(function (outings) {
                    _this.$scope.outings = outings;
                    _this.$rootScope.$emit('OutingsLoaded', _this.$scope.outings);

                    outings.forEach(function (outing) {
                        if (typeof (_this.$scope.candidates[outing.venue.id]) === 'undefined') {
                            _this.ratingCandidateResource.get({ context: _this.venueContextKey, reference: outing.venue.id }, function (candidate) {
                                _this.$scope.candidates[outing.venue.id] = candidate;
                            });
                        }
                    });
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            OutingController.prototype.panTo = function (outing) {
                this.$rootScope.$emit('VenueSelected', outing.venue);
            };

            OutingController.prototype.addRating = function (venue) {
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
            return OutingController;
        })();
        Outings.OutingController = OutingController;
    })(Burgerama.Outings || (Burgerama.Outings = {}));
    var Outings = Burgerama.Outings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('OutingController', [
    '$rootScope', '$scope', '$modal', 'toaster', 'OutingResource', 'RatingCandidateResource', function ($rootScope, $scope, $modal, toaster, outingResource, ratingCandidateResource) {
        return new Burgerama.Outings.OutingController($rootScope, $scope, $modal, toaster, outingResource, ratingCandidateResource);
    }
]);
//# sourceMappingURL=outingController.js.map
