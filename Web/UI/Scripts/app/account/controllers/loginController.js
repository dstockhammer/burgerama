/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var LoginController = (function () {
            function LoginController($scope, $modal, $location, authService) {
                var _this = this;
                this.$scope = $scope;
                this.$modal = $modal;
                this.$location = $location;
                this.authService = authService;
                if (this.authService.email != null && this.authService.token != null) {
                    this.$location.path('/').replace();
                }

                this.$scope.email = this.authService.email;
                this.$scope.showModal = function () {
                    return _this.showModal();
                };
            }
            LoginController.prototype.showModal = function () {
                this.$modal.open({
                    templateUrl: 'http://localhost/burgerama/Scripts/app/account/views/login.html',
                    controller: 'LoginModalController'
                });
            };
            return LoginController;
        })();
        Account.LoginController = LoginController;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('LoginController', [
    '$scope', '$modal', '$location', 'AuthService', function ($scope, $modal, $location, authService) {
        return new Burgerama.Account.LoginController($scope, $modal, $location, authService);
    }
]);
//# sourceMappingURL=loginController.js.map
