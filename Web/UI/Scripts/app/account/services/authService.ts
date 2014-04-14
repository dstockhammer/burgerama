/// <reference path="../../app.ts" />

module Burgerama.Account {
    interface ITokenRequest {
        grant_type: string;
        username: string;
        password: string;
    }

    // example token:
    // {"access_token":"xx","token_type":"bearer","expires_in":1209599,"userName":"admin",".issued":"Sun, 01 Dec 2014 13:37:00 GMT",".expires":"Sun, 15 Dec 2014 00:00:00 GMT"}
    interface IToken {
        access_token: string;
        token_type: string;
        expires_in: number;
        userName: string;
        issued: string;
        expires: string;
    }

    export interface IAuthService {
        email: string;
        token: string;
        login: (name: string, password: string, persist: boolean) => ng.IPromise<any>;
        logout: () => ng.IPromise<any>;
        checkAuth: () => boolean;
    }

    export class AuthService implements IAuthService {
        public email: string;
        public token: string;

        private TokenResource: any;

        constructor(
            private $rootScope: IBurgeramaScope,
            private $location: ng.ILocationService,
            private $http: ng.IHttpService,
            private $q: ng.IQService,
            private $resource: ng.resource.IResourceService,
            private localStorageService)
        {
            this.email = this.$rootScope.email = this.localStorageService.get('email');
            this.token = this.$rootScope.token = this.localStorageService.get('token');

            this.TokenResource = this.$resource('http://localhost/burgerama/api/users/token', {}, {
                create: {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    transformRequest: data => {
                        return 'grant_type=' + data.grant_type + '&username=' + data.username + '&password=' + data.password;
                    }
                }
            });
        }

        public checkAuth(): boolean {
            if (this.email == null || this.token == null) {
                //this.$location.path('/login').replace();
                return false;
            }
            return true;
        }

        public login(email: string, password: string, persist: boolean) {
            var deferred = this.$q.defer();

            var reuqestParams: ITokenRequest = {
                grant_type: 'password',
                username: email,
                password: password
            };

            var tokenRequest = new this.TokenResource(reuqestParams);
            tokenRequest.$create((token: IToken) => {
                this.email = this.$rootScope.email = token.userName;
                this.token = this.$rootScope.token = token.access_token;

                // todo: add expiration to localStorageService
                if (persist) {
                    this.localStorageService.add('email', this.email);
                    this.localStorageService.add('token', this.token);
                    this.localStorageService.cookie.set('email', this.email);
                } else {
                    this.localStorageService.remove('token');
                }

                this.$rootScope.$broadcast('login');

                deferred.resolve(token);
            }, err => {
                    deferred.reject(err.data);
                });

            return deferred.promise;
        }

        public logout() {
            var deferred = this.$q.defer();

            this.$http.post('http://localhost/burgerama/users/account/logout', null, null)
                .success(() => {
                    this.token = this.$rootScope.token = null;
                    this.localStorageService.remove('token');

                    this.$rootScope.$broadcast('logout');

                    deferred.resolve();
                }).error(err => {
                    deferred.reject(err);
                });

            return deferred.promise;
        }
    }
}

Burgerama.app.service("AuthService", ['$rootScope', '$location', '$http', '$q', '$resource', 'localStorageService', ($rootScope, $location, $http, $q, $resource, localStorageService) =>
    new Burgerama.Account.AuthService($rootScope, $location, $http, $q, $resource, localStorageService)
]);
