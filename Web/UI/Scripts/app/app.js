/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />
'use strict';
var Burgerama;
(function (Burgerama) {
    Burgerama.app = angular.module('burgerama', [
        'ngResource',
        'ngRoute',
        'ngCookies',
        'ui.bootstrap',
        'LocalStorageModule',
        'toaster',
        'auth0'
    ]);

    Burgerama.app.config([
        '$httpProvider', '$routeProvider', 'authProvider', function ($httpProvider, $routeProvider, authProvider) {
            $httpProvider.interceptors.push('AuthHttpInterceptor');

            $routeProvider.when('/', {
                templateUrl: 'http://localhost/burgerama/Scripts/app/map/views/map.html',
                controller: 'MapController'
            }).otherwise({
                redirectTo: '/'
            });

            authProvider.init({
                domain: 'burgerama.auth0.com',
                clientID: 'xlaKo4Eqj5DbAJ44BmUGQhUF548TNc4Z',
                callbackURL: "http://localhost/burgerama/"
            });
        }]);
})(Burgerama || (Burgerama = {}));
//# sourceMappingURL=app.js.map
