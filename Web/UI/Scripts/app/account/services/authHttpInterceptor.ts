/// <reference path="../../app.ts" />

module Burgerama.Account {
    export class AuthHttpInterceptor {
        private apiUrlStart = 'http://localhost/burgerama/api';
        
        constructor(private $rootScope: IBurgeramaScope, private $q: ng.IQService, private authService: IAuthService) {
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

Burgerama.app.factory("AuthHttpInterceptor", ['$rootScope', '$q', 'AuthService', ($rootScope, $q, authService) =>
    new Burgerama.Account.AuthHttpInterceptor($rootScope, $q, authService).create()
]);
 