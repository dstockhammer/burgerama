/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var SignInController = (function () {
            function SignInController($scope, $modalInstance, toaster, authService) {
                var _this = this;
                this.$scope = $scope;
                this.$modalInstance = $modalInstance;
                this.toaster = toaster;
                this.authService = authService;
                this.$scope.credentials = {
                    email: this.authService.email,
                    password: "",
                    persistent: false
                };

                this.$scope.submit = function () {
                    return _this.submit();
                };
                this.$scope.cancel = function () {
                    return _this.cancel();
                };
            }
            SignInController.prototype.cancel = function () {
                this.$modalInstance.dismiss();
            };

            SignInController.prototype.submit = function () {
                var _this = this;
                this.$modalInstance.close();

                this.authService.signIn(this.$scope.credentials.email, this.$scope.credentials.password, this.$scope.credentials.persistent).then(function (token) {
                    _this.toaster.pop("success", "Success", "Signed in as " + token.userName + ".");
                }, function (data) {
                    _this.toaster.pop("error", "Error", "Authentication failed: " + data.error_description);
                });
            };
            return SignInController;
        })();
        Account.SignInController = SignInController;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('SignInController', [
    '$scope', '$modalInstance', 'toaster', 'AuthService', function ($scope, $modalInstance, toaster, authService) {
        return new Burgerama.Account.SignInController($scope, $modalInstance, toaster, authService);
    }
]);
//# sourceMappingURL=signInController.js.map
