/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var AccountController = (function () {
            function AccountController($rootScope, $scope, $modal, toaster, authService) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modal = $modal;
                this.toaster = toaster;
                this.authService = authService;
                this.update();
                this.$scope.showSignInModal = function () {
                    return _this.showSignInModal();
                };
                this.$scope.signOut = function () {
                    return _this.signOut();
                };

                var unregisterSignIn = this.$rootScope.$on('SignIn', function () {
                    return _this.update();
                });
                var unregisterSignOut = this.$rootScope.$on('SignOut', function () {
                    return _this.update();
                });

                this.$scope.$on('$destroy', function () {
                    unregisterSignIn();
                    unregisterSignOut();
                });
            }
            AccountController.prototype.update = function () {
                this.$scope.signedIn = this.authService.checkAuth();
                this.$scope.user = {
                    email: this.authService.email
                };
            };

            AccountController.prototype.showSignInModal = function () {
                this.$modal.open({
                    templateUrl: 'http://localhost/burgerama/Scripts/app/account/views/login.html',
                    controller: 'SignInController'
                });
            };

            AccountController.prototype.signOut = function () {
                var _this = this;
                this.authService.checkAuth();

                var email = this.authService.email;
                this.authService.signOut().then(function () {
                    _this.toaster.pop("success", "Success", "Bye, " + email + "!");
                });
            };
            return AccountController;
        })();
        Account.AccountController = AccountController;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('AccountController', [
    '$rootScope', '$scope', '$modal', 'toaster', 'AuthService', function ($rootScope, $scope, $modal, toaster, authService) {
        return new Burgerama.Account.AccountController($rootScope, $scope, $modal, toaster, authService);
    }
]);
//# sourceMappingURL=accountController.js.map
