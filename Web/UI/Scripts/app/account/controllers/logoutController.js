/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var LogoutController = (function () {
            function LogoutController(authService) {
                this.authService = authService;
                this.authService.checkAuth();

                var email = this.authService.email;
                this.authService.logout().then(function () {
                    //this.alertService.success('Bye, ' + username, '/');
                });
            }
            return LogoutController;
        })();
        Account.LogoutController = LogoutController;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('LogoutController', [
    'AuthService', function (authService) {
        return new Burgerama.Account.LogoutController(authService);
    }
]);
//# sourceMappingURL=logoutController.js.map
