/// <reference path="../../app.ts" />

module Burgerama.Account {
    export class AuthHttpInterceptor {
        private apiUrlStart = 'http://localhost/burgerama/api';
        private tokenUrlEnd = '/token';
        
        constructor(private $rootScope: IBurgeramaScope, private $q: ng.IQService) {

        }

        create() {
            return {
                request: config => this.handleRequest(config),
                response: response => this.handleResponse(response),
                requestError: rejection => this.handleRequestError(rejection),
                responseError: rejection => this.handleResponseError(rejection)
            };
        }

        private isApiUrl(url): boolean {
            return url.slice(0, this.apiUrlStart.length) == this.apiUrlStart;
        }

        private isTokenUrl(url): boolean {
            return url.slice(-this.tokenUrlEnd.length) == this.tokenUrlEnd;
        }

        private handleRequest(config) {
            // Add progress indicator to API calls.
            if (this.isApiUrl(config.url)) {
                //this.$rootScope.isLoading = true;
                //this.progress.start();
            }

            // Add bearer authentication token to API calls.
            if (typeof this.$rootScope.token != 'undefined' && this.$rootScope.token != null) {
                // Apply this only to calls to the API, but exclude the create token request.
                if (this.isApiUrl(config.url) && this.isTokenUrl(config.url) == false) {
                    config.headers.Authorization = 'Bearer ' + this.$rootScope.token;
                }
            }

            // Return the config or wrap it in a promise if blank.
            return config || this.$q.when(config);
        }

        private handleResponse(response) {
            // Set progress of API calls to 100% and hide the indicator.
            if (this.isApiUrl(response.config.url)) {
                //this.$rootScope.isLoading = false;
                //this.progress.done();
            }

            // Return the response or promise.
            return response || this.$q.when(response);
        }

        private handleRequestError(rejection) {
            // Set progress of API calls to 100% and hide the indicator.
            if (this.isApiUrl(rejection.config.url)) {
                //this.$rootScope.isLoading = false;
                //this.progress.done();
            }

            // Return the promise rejection.
            return this.$q.reject(rejection);
        }

        private handleResponseError(rejection) {
            // Set progress of API calls to 100% and hide the indicator.
            if (this.isApiUrl(rejection.config.url)) {
                //this.$rootScope.isLoading = false;
                //this.progress.done();
            }

            // Return the promise rejection.
            return this.$q.reject(rejection);
        }
    }
}

Burgerama.app.factory("AuthHttpInterceptor", ['$rootScope', '$q', ($rootScope, $q) =>
    new Burgerama.Account.AuthHttpInterceptor($rootScope, $q).create()
]);
 