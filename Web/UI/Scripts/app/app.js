/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />
'use strict';
var Burgerama;
(function (Burgerama) {
    Burgerama.app = angular.module('burgerama', [
        'ngResource',
        'ngRoute',
        'ui.bootstrap',
        'LocalStorageModule'
    ]);

    Burgerama.app.config([
        '$httpProvider', '$routeProvider', function ($httpProvider, $routeProvider) {
            $httpProvider.interceptors.push('AuthHttpInterceptor');

            $routeProvider.when('/', {
                templateUrl: 'http://localhost/burgerama/Scripts/app/map/views/map.html',
                controller: 'MapController'
            }).when('/logout', {
                controller: 'LogoutController'
            }).otherwise({
                redirectTo: '/'
            });
        }]);
})(Burgerama || (Burgerama = {}));
//# sourceMappingURL=app.js.map
