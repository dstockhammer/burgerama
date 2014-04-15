/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        

        var AuthService = (function () {
            function AuthService($rootScope, $location, $http, $q, $resource, localStorageService) {
                this.$rootScope = $rootScope;
                this.$location = $location;
                this.$http = $http;
                this.$q = $q;
                this.$resource = $resource;
                this.localStorageService = localStorageService;
                this.email = this.$rootScope.email = this.localStorageService.get('email');
                this.token = this.$rootScope.token = this.localStorageService.get('token');

                this.TokenResource = this.$resource('http://localhost/burgerama/api/users/token', {}, {
                    create: {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        transformRequest: function (data) {
                            return 'grant_type=' + data.grant_type + '&username=' + data.username + '&password=' + data.password;
                        }
                    }
                });
            }
            AuthService.prototype.checkAuth = function () {
                if (this.email == null || this.token == null) {
                    //this.$location.path('/login').replace();
                    return false;
                }
                return true;
            };

            AuthService.prototype.signIn = function (email, password, persist) {
                var _this = this;
                var deferred = this.$q.defer();

                var reuqestParams = {
                    grant_type: 'password',
                    username: email,
                    password: password
                };

                var tokenRequest = new this.TokenResource(reuqestParams);
                tokenRequest.$create(function (token) {
                    _this.email = _this.$rootScope.email = token.userName;
                    _this.token = _this.$rootScope.token = token.access_token;

                    // todo: add expiration to localStorageService
                    if (persist) {
                        _this.localStorageService.add('email', _this.email);
                        _this.localStorageService.add('token', _this.token);
                        _this.localStorageService.cookie.set('email', _this.email);
                    } else {
                        _this.localStorageService.remove('token');
                    }

                    _this.$rootScope.$broadcast('SignIn');

                    deferred.resolve(token);
                }, function (err) {
                    deferred.reject(err.data);
                });

                return deferred.promise;
            };

            AuthService.prototype.signOut = function () {
                var _this = this;
                var deferred = this.$q.defer();

                this.$http.post('http://localhost/burgerama/api/users/account/logout', null, null).success(function () {
                    _this.token = _this.$rootScope.token = null;
                    _this.localStorageService.remove('token');

                    _this.$rootScope.$broadcast('SignOut');

                    deferred.resolve();
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            };
            return AuthService;
        })();
        Account.AuthService = AuthService;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.service("AuthService", [
    '$rootScope', '$location', '$http', '$q', '$resource', 'localStorageService', function ($rootScope, $location, $http, $q, $resource, localStorageService) {
        return new Burgerama.Account.AuthService($rootScope, $location, $http, $q, $resource, localStorageService);
    }
]);
//# sourceMappingURL=authService.js.map
