/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var LoginModalController = (function () {
            function LoginModalController($scope, $modalInstance, authService) {
                var _this = this;
                this.$scope = $scope;
                this.$modalInstance = $modalInstance;
                this.authService = authService;
                this.$scope.submit = function () {
                    return _this.submit();
                };
                this.$scope.cancel = function () {
                    return _this.cancel();
                };
            }
            LoginModalController.prototype.cancel = function () {
                console.log("cancel");
                this.$modalInstance.dismiss();
            };

            LoginModalController.prototype.submit = function () {
                console.log("submit");
                this.$modalInstance.close();

                this.authService.login(this.$scope.email, this.$scope.password, this.$scope.persistent).then(function (token) {
                    //this.alertService.success('Logged in as ' + token.userName, "/");
                }, function (data) {
                    //this.alertService.error(data.error_description);
                });
            };
            return LoginModalController;
        })();
        Account.LoginModalController = LoginModalController;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('LoginModalController', [
    '$scope', '$modalInstance', 'AuthService', function ($scope, $modalInstance, authService) {
        return new Burgerama.Account.LoginModalController($scope, $modalInstance, authService);
    }
]);
//# sourceMappingURL=loginModalController.js.map
