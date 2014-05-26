/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var VenueController = (function () {
            function VenueController($rootScope, $scope, $modal, venueResource, toaster) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.venueResource = venueResource;
                this.toaster = toaster;
                this.$scope.venues = null;
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
                this.venueResource.all(function (data) {
                    _this.$scope.venues = data;
                    _this.$rootScope.$emit('VenuesLoaded', _this.$scope.venues);
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            VenueController.prototype.panTo = function (venue) {
                this.$rootScope.$emit('VenueSelected', venue);
            };

            VenueController.prototype.addVote = function (venue) {
                console.log('add vote clicked');
            };
            return VenueController;
        })();
        Venues.VenueController = VenueController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('VenueController', [
    '$rootScope', '$scope', '$modal', 'VenueResource', 'toaster', function ($rootScope, $scope, $modal, venueResource, toaster) {
        return new Burgerama.Venues.VenueController($rootScope, $scope, $modal, venueResource, toaster);
    }
]);
//# sourceMappingURL=venueController.js.map
