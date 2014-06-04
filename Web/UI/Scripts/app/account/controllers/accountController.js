/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var AccountController = (function () {
            function AccountController($rootScope, $scope, authService) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.authService = authService;
                this.update();

                this.$scope.showSignInModal = function () {
                    return _this.showSignInModal();
                };
                this.$scope.signOut = function () {
                    return _this.signOut();
                };

                var unregisterSignIn = this.$rootScope.$on('UserSignedIn', function () {
                    return _this.update();
                });
                var unregisterSignOut = this.$rootScope.$on('UserSignedOut', function () {
                    return _this.update();
                });

                this.$scope.$on('$destroy', function () {
                    unregisterSignIn();
                    unregisterSignOut();
                });
            }
            AccountController.prototype.update = function () {
                this.$scope.signedIn = this.authService.isAuthenticated();
                this.$scope.user = this.authService.getUser();
            };

            AccountController.prototype.showSignInModal = function () {
                this.authService.signIn();
            };

            AccountController.prototype.signOut = function () {
                this.authService.signOut();
            };
            return AccountController;
        })();
        Account.AccountController = AccountController;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('AccountController', [
    '$rootScope', '$scope', 'AuthService', function ($rootScope, $scope, authService) {
        return new Burgerama.Account.AccountController($rootScope, $scope, authService);
    }
]);
//# sourceMappingURL=accountController.js.map
