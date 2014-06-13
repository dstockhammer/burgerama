/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Outings) {
        var OutingController = (function () {
            function OutingController($rootScope, $scope, $modal, outingResource, toaster) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.outingResource = outingResource;
                this.toaster = toaster;
                this.$scope.outings = null;
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
                this.outingResource.all(function (data) {
                    _this.$scope.outings = data;
                    _this.$rootScope.$emit('OutingsLoaded', _this.$scope.outings);
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
    '$rootScope', '$scope', '$modal', 'OutingResource', 'toaster', function ($rootScope, $scope, $modal, outingResource, toaster) {
        return new Burgerama.Outings.OutingController($rootScope, $scope, $modal, outingResource, toaster);
    }
]);
//# sourceMappingURL=outingController.js.map
