// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var AddVenueController = (function () {
            function AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster, venue, closeCallback) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modalInstance = $modalInstance;
                this.venueResource = venueResource;
                this.toaster = toaster;
                this.venue = venue;
                this.closeCallback = closeCallback;
                this.$scope.venue = venue;
                this.$scope.ok = function () {
                    return _this.ok();
                };
                this.$scope.cancel = function () {
                    return _this.cancel();
                };
            }
            AddVenueController.prototype.ok = function () {
                var _this = this;
                this.$modalInstance.close();
                this.closeCallback();

                var resource = new this.venueResource(this.$scope.venue);
                resource.$create(function () {
                    _this.toaster.pop('success', 'Success', 'Added venue: ' + _this.$scope.venue.name);
                    _this.$rootScope.$emit('VenueAdded', _this.$scope.venue);
                }, function (err) {
                    if (err.status == 401) {
                        _this.toaster.pop('error', 'Unauthorized', 'You are not authorized to suggest venues. Please log in or create an account.');
                    } else if (err.status == 409) {
                        _this.toaster.pop('error', 'Conflict', 'This venue has already been suggested.');
                    } else {
                        _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                    }
                });
            };

            AddVenueController.prototype.cancel = function () {
                this.$modalInstance.dismiss();
                this.closeCallback();
            };
            return AddVenueController;
        })();
        Venues.AddVenueController = AddVenueController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('AddVenueController', [
    '$rootScope', '$scope', '$modalInstance', 'VenueResource', 'toaster', 'venue', 'closeCallback', function ($rootScope, $scope, $modalInstance, venueResource, toaster, venue, closeCallback) {
        return new Burgerama.Venues.AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster, venue, closeCallback);
    }
]);
//# sourceMappingURL=addVenueController.js.map
