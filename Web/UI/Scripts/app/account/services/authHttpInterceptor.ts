/// <reference path="../../app.ts" />

module Burgerama.Account {
    export class AuthHttpInterceptor {
        private apiUrlStart = 'http://api.dev.burgerama.co.uk/';
        
        constructor(private $q: ng.IQService, private authService: IAuthService) {
        }

        create() {
            return {
                request: config => this.handleRequest(config),
            };
        }

        private handleRequest(config) {
            // Add bearer authentication token to API calls.
            if (this.authService.isAuthenticated() && this.isApiUrl(config.url)) {
                config.headers.Authorization = 'Bearer ' + this.authService.getToken();
            }

            // Return the config or wrap it in a promise if blank.
            return config || this.$q.when(config);
        }

        private isApiUrl(url): boolean {
            return url.slice(0, this.apiUrlStart.length) == this.apiUrlStart;
        }
    }
}

Burgerama.app.factory('AuthHttpInterceptor', ['$q', 'AuthService', ($q, authService) =>
    new Burgerama.Account.AuthHttpInterceptor($q, authService).create()
]);
 