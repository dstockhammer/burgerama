/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var VenueController = (function () {
            function VenueController($rootScope, $scope, venueResource) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.venueResource = venueResource;
                this.update();

                var unregisterVenueAdded = this.$rootScope.$on('VenueAdded', function () {
                    return _this.update();
                });
                this.$scope.$on('$destroy', function () {
                    return unregisterVenueAdded();
                });
            }
            VenueController.prototype.update = function () {
                this.load();
            };

            VenueController.prototype.load = function () {
                var _this = this;
                return this.venueResource.all(function (data) {
                    _this.$scope.venues = data;
                }, function (err) {
                });
            };
            return VenueController;
        })();
        Venues.VenueController = VenueController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('VenueController', [
    '$rootScope', '$scope', 'VenueResource', function ($rootScope, $scope, venueResource) {
        return new Burgerama.Venues.VenueController($rootScope, $scope, venueResource);
    }
]);
//# sourceMappingURL=venueController.js.map
