﻿/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var AuthHttpInterceptor = (function () {
            function AuthHttpInterceptor($q, authService) {
                this.$q = $q;
                this.authService = authService;
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
                if (this.authService.isAuthenticated() && Burgerama.Util.isApiUrl(config.url)) {
                    config.headers.Authorization = 'Bearer ' + this.authService.getToken();
                }

                // Return the config or wrap it in a promise if blank.
                return config || this.$q.when(config);
            };
            return AuthHttpInterceptor;
        })();
        Account.AuthHttpInterceptor = AuthHttpInterceptor;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory('AuthHttpInterceptor', [
    '$q', 'AuthService', function ($q, authService) {
        return new Burgerama.Account.AuthHttpInterceptor($q, authService).create();
    }
]);
//# sourceMappingURL=authHttpInterceptor.js.map
