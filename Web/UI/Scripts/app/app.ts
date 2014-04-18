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
        'ngCookies',

        // Angular UI modules
        'ui.bootstrap',

        // 3rd Party Modules
        'LocalStorageModule',
        'toaster',
        'auth0'
    ]);

    app.config(['$httpProvider', '$routeProvider', 'authProvider', ($httpProvider: ng.IHttpProvider, $routeProvider: ng.route.IRouteProvider, authProvider) => {
        $httpProvider.interceptors.push('AuthHttpInterceptor');

        $routeProvider
            .when('/', {
                templateUrl: 'http://dev.burgerama.co.uk/Scripts/app/map/views/map.html',
                controller: 'MapController'
            })
            .otherwise({
                redirectTo: '/'
            });

        authProvider.init({
            domain: 'burgerama.auth0.com',
            clientID: 'xlaKo4Eqj5DbAJ44BmUGQhUF548TNc4Z',
            callbackURL: "http://dev.burgerama.co.uk/"
        });
    }]);
}
