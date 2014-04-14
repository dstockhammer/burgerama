/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var AuthHttpInterceptor = (function () {
            function AuthHttpInterceptor($rootScope, $q) {
                this.$rootScope = $rootScope;
                this.$q = $q;
                this.apiUrlStart = 'http://localhost/burgerama/api';
                this.tokenUrlEnd = '/token';
            }
            AuthHttpInterceptor.prototype.create = function () {
                var _this = this;
                return {
                    request: function (config) {
                        return _this.handleRequest(config);
                    },
                    response: function (response) {
                        return _this.handleResponse(response);
                    },
                    requestError: function (rejection) {
                        return _this.handleRequestError(rejection);
                    },
                    responseError: function (rejection) {
                        return _this.handleResponseError(rejection);
                    }
                };
            };

            AuthHttpInterceptor.prototype.isApiUrl = function (url) {
                return url.slice(0, this.apiUrlStart.length) == this.apiUrlStart;
            };

            AuthHttpInterceptor.prototype.isTokenUrl = function (url) {
                return url.slice(-this.tokenUrlEnd.length) == this.tokenUrlEnd;
            };

            AuthHttpInterceptor.prototype.handleRequest = function (config) {
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
            };

            AuthHttpInterceptor.prototype.handleResponse = function (response) {
                // Set progress of API calls to 100% and hide the indicator.
                if (this.isApiUrl(response.config.url)) {
                    //this.$rootScope.isLoading = false;
                    //this.progress.done();
                }

                // Return the response or promise.
                return response || this.$q.when(response);
            };

            AuthHttpInterceptor.prototype.handleRequestError = function (rejection) {
                // Set progress of API calls to 100% and hide the indicator.
                if (this.isApiUrl(rejection.config.url)) {
                    //this.$rootScope.isLoading = false;
                    //this.progress.done();
                }

                // Return the promise rejection.
                return this.$q.reject(rejection);
            };

            AuthHttpInterceptor.prototype.handleResponseError = function (rejection) {
                // Set progress of API calls to 100% and hide the indicator.
                if (this.isApiUrl(rejection.config.url)) {
                    //this.$rootScope.isLoading = false;
                    //this.progress.done();
                }

                // Return the promise rejection.
                return this.$q.reject(rejection);
            };
            return AuthHttpInterceptor;
        })();
        Account.AuthHttpInterceptor = AuthHttpInterceptor;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory("AuthHttpInterceptor", [
    '$rootScope', '$q', function ($rootScope, $q) {
        return new Burgerama.Account.AuthHttpInterceptor($rootScope, $q).create();
    }
]);
//# sourceMappingURL=authHttpInterceptor.js.map
