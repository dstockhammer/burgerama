/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var AuthHttpInterceptor = (function () {
            function AuthHttpInterceptor($rootScope, $q, authService) {
                this.$rootScope = $rootScope;
                this.$q = $q;
                this.authService = authService;
                this.apiUrlStart = 'http://api.dev.burgerama.co.uk/';
            }
            AuthHttpInterceptor.prototype.create = function () {
                var _this = this;
                return {
                    request: function (config) {
                        return _this.handleRequest(config);
                    }
                };
            };

            AuthHttpInterceptor.prototype.handleRequest = function (config) {
                // Add bearer authentication token to API calls.
                if (this.authService.isAuthenticated() && this.isApiUrl(config.url)) {
                    config.headers.Authorization = 'Bearer ' + this.authService.getToken();
                }

                // Return the config or wrap it in a promise if blank.
                return config || this.$q.when(config);
            };

            AuthHttpInterceptor.prototype.isApiUrl = function (url) {
                return url.slice(0, this.apiUrlStart.length) == this.apiUrlStart;
            };
            return AuthHttpInterceptor;
        })();
        Account.AuthHttpInterceptor = AuthHttpInterceptor;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory("AuthHttpInterceptor", [
    '$rootScope', '$q', 'AuthService', function ($rootScope, $q, authService) {
        return new Burgerama.Account.AuthHttpInterceptor($rootScope, $q, authService).create();
    }
]);
//# sourceMappingURL=authHttpInterceptor.js.map
