var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var VenueDetailsController = (function () {
            function VenueDetailsController($rootScope, $scope, venueResource, toaster, venueId) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.venueResource = venueResource;
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
                    _this.$rootScope.$broadcast('VenuesLoaded', [_this.$scope.venue]);
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            VenueDetailsController.prototype.addVote = function (venue) {
                console.log('add vote clicked');
            };
            return VenueDetailsController;
        })();
        Venues.VenueDetailsController = VenueDetailsController;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('VenueDetailsController', [
    '$rootScope', '$scope', 'VenueResource', 'toaster', 'venueId', function ($rootScope, $scope, venueResource, toaster, venueId) {
        return new Burgerama.Venues.VenueDetailsController($rootScope, $scope, venueResource, toaster, venueId);
    }
]);
//# sourceMappingURL=venueDetailsController.js.map
