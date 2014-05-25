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
                return this.venueResource.all(function (data) {
                    _this.$scope.venues = data;
                    _this.$rootScope.$broadcast('VenuesLoaded', _this.$scope.venues);
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            VenueController.prototype.panTo = function (venue) {
                this.$rootScope.$broadcast('PanToClicked', venue.location.latitude, venue.location.longitude);
            };

            VenueController.prototype.addVote = function (venue) {
                console.log('add vote clicked');
            };
            return VenueController;
        })();
        Venues.VenueController = VenueController;

        var AddVenueController = (function () {
            function AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster, venue) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modalInstance = $modalInstance;
                this.venueResource = venueResource;
                this.toaster = toaster;
                this.venue = venue;
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

                var resource = new this.venueResource(this.$scope.venue);
                resource.$create(function () {
                    _this.toaster.pop('success', 'Success', 'Added venue: ' + _this.$scope.venue.title);
                    _this.$rootScope.$broadcast('VenueAdded', _this.$scope.venue);
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
            };
            return AddVenueController;
        })();
        Venues.AddVenueController = AddVenueController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('VenueController', [
    '$rootScope', '$scope', '$modal', 'VenueResource', 'toaster', function ($rootScope, $scope, $modal, venueResource, toaster) {
        return new Burgerama.Venues.VenueController($rootScope, $scope, $modal, venueResource, toaster);
    }
]);
Burgerama.app.controller('AddVenueController', [
    '$rootScope', '$scope', '$modalInstance', 'VenueResource', 'toaster', 'venue', function ($rootScope, $scope, $modalInstance, venueResource, toaster, venue) {
        return new Burgerama.Venues.AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster, venue);
    }
]);
//# sourceMappingURL=venueController.js.map
