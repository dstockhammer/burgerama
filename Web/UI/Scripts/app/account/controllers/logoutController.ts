/// <reference path="../../app.ts" />

module Burgerama.Account {
    export class LogoutController {
        constructor(private authService: IAuthService) {
            this.authService.checkAuth();

            var email = this.authService.email;
            this.authService.logout()
                .then(() => {
                    //this.alertService.success('Bye, ' + username, '/');
                });
        }
    }
}

Burgerama.app.controller('LogoutController', ['AuthService', authService =>
    new Burgerama.Account.LogoutController(authService)
]);
