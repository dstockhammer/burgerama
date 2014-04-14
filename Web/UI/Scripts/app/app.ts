/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />

'use strict';

module Burgerama {
    export interface IBurgeramaScope extends ng.IRootScopeService {
        email: string;
        token: string;
    }

    export var app: ng.IModule = angular.module('burgerama', [
        // Angular modules
        'ngResource',
        'ngRoute',

        // Angular UI modules
        'ui.bootstrap',

        // 3rd Party Modules
        'LocalStorageModule'
    ]);

    app.config(['$httpProvider', '$routeProvider', ($httpProvider: ng.IHttpProvider, $routeProvider: ng.route.IRouteProvider) => {
        $httpProvider.interceptors.push('AuthHttpInterceptor');

        $routeProvider
            .when('/', {
                templateUrl: 'http://localhost/burgerama/Scripts/app/map/views/map.html',
                controller: 'MapController'
            })
            .when('/logout', {
                controller: 'LogoutController'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);
}
