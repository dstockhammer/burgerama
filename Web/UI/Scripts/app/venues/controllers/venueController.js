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
                this.load();

                this.$scope.showAddVenueModal = function () {
                    return _this.showAddVenueModal();
                };

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
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            VenueController.prototype.showAddVenueModal = function () {
                this.$modal.open({
                    templateUrl: '/Scripts/app/venues/views/addVenue.modal.html',
                    controller: 'AddVenueController'
                });
            };
            return VenueController;
        })();
        Venues.VenueController = VenueController;

        var AddVenueController = (function () {
            function AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modalInstance = $modalInstance;
                this.venueResource = venueResource;
                this.toaster = toaster;
                this.$scope.venue = {
                    id: null,
                    title: '',
                    location: {
                        reference: 'no reference',
                        latitude: 0,
                        longitude: 0
                    },
                    description: '',
                    url: ''
                };

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
                        _this.toaster.pop('error', 'Unauthorized', 'You are not authorized to perform this action.');
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
    '$rootScope', '$scope', '$modalInstance', 'VenueResource', 'toaster', function ($rootScope, $scope, $modalInstance, venueResource, toaster) {
        return new Burgerama.Venues.AddVenueController($rootScope, $scope, $modalInstance, venueResource, toaster);
    }
]);
//# sourceMappingURL=venueController.js.map
