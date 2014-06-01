/// <reference path="../../app.ts" />

module Burgerama.Account {
    export class AuthHttpInterceptor {
        constructor(private $q: ng.IQService, private authService: IAuthService) {
        }

        create() {
            return {
                request: config => this.handleRequest(config),
            };
        }

        private handleRequest(config) {
            // Add bearer authentication token to API calls.
            if (this.authService.isAuthenticated() && Util.isApiUrl(config.url)) {
                config.headers.Authorization = 'Bearer ' + this.authService.getToken();
            }

            // Return the config or wrap it in a promise if blank.
            return config || this.$q.when(config);
        }
    }
}

Burgerama.app.factory('AuthHttpInterceptor', ['$q', 'AuthService', ($q, authService) =>
    new Burgerama.Account.AuthHttpInterceptor($q, authService).create()
]);
 