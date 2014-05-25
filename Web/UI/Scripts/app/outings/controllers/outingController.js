/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Outings) {
        var OutingController = (function () {
            function OutingController($rootScope, $scope, outingResource, toaster) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.outingResource = outingResource;
                this.toaster = toaster;
                this.$scope.outings = null;
                this.$scope.panTo = function (outing) {
                    return _this.panTo(outing);
                };

                this.load();
            }
            OutingController.prototype.load = function () {
                var _this = this;
                return this.outingResource.all(function (data) {
                    _this.$scope.outings = data;
                    _this.$rootScope.$broadcast('OutingsLoaded', _this.$scope.outings);
                }, function (err) {
                    _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                });
            };

            OutingController.prototype.panTo = function (outing) {
                this.$rootScope.$broadcast('PanToClicked', outing.venue);
            };
            return OutingController;
        })();
        Outings.OutingController = OutingController;
    })(Burgerama.Outings || (Burgerama.Outings = {}));
    var Outings = Burgerama.Outings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('OutingController', [
    '$rootScope', '$scope', 'OutingResource', 'toaster', function ($rootScope, $scope, outingResource, toaster) {
        return new Burgerama.Outings.OutingController($rootScope, $scope, outingResource, toaster);
    }
]);
//# sourceMappingURL=outingController.js.map
