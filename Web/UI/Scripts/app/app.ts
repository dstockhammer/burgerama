/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />

'use strict';

module Burgerama {
    export interface IBurgeramaScope extends ng.IRootScopeService {
        email: string;
        token: string;
    }

    export function initialize() {
        angular.bootstrap(document, ['burgerama']);
    }

    export var app: ng.IModule = angular.module('burgerama', [
        // Angular modules
        'ngResource',
        'ngCookies',
        'ngAnimate',

        // Angular UI modules
        'ui.bootstrap',
        'ui.event',
        'ui.router',
        'ui.map',

        // 3rd Party Modules
        'LocalStorageModule',
        'toaster',
        'truncate',
        'auth0'
    ]);

    app.config(['$httpProvider', '$stateProvider', '$urlRouterProvider', 'authProvider', ($httpProvider: ng.IHttpProvider, $stateProvider, $urlRouterProvider, authProvider) => {
        $httpProvider.interceptors.push('AuthHttpInterceptor');

        $urlRouterProvider.otherwise("/venues");
        $stateProvider
        .state('venues', {
            url: "/venues",
            controller: 'VenueController',
            templateUrl: '/Scripts/app/venues/views/list.html'
        })
        .state('outings', {
            url: "/outings",
            controller: 'OutingController',
            templateUrl: '/Scripts/app/outings/views/list.html'
        });

        authProvider.init({
            domain: 'burgerama.auth0.com',
            clientID: 'xlaKo4Eqj5DbAJ44BmUGQhUF548TNc4Z',
            callbackURL: "http://dev.burgerama.co.uk/"
        });
    }]);
}

Burgerama.app.run(['$rootScope', $rootScope => {
    $rootScope.loaded = true;
}]);

angular.element(document).ready(() => {
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyDMSKCEtbHIORL8DuKSxSEXxkBGB-mqf9c&sensor=true&libraries=places&callback=Burgerama.initialize';
    document.body.appendChild(script);
});
