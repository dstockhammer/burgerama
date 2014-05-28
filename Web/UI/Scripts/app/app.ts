/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />

'use strict';

declare var config: Burgerama.IBurgeramaConfig;

module Burgerama {
    export interface IBurgeramaConfig {
        url: IUrlConfig;
        auth0: IAuth0Config;
    }
    export interface IUrlConfig {
        frontend: string;
        venues: string;
        outings: string;
        voting: string;
        rating: string;
    }
    export interface IAuth0Config {
        domain: string;
        clientId: string;
    }

    export interface IBurgeramaScope extends ng.IRootScopeService {
        // nothing here yet
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
        $httpProvider.defaults.transformResponse.push(responseData => {
            Util.convertDateStringsToDates(responseData);
            return responseData;
        });

        $urlRouterProvider.otherwise('/venues');
        $stateProvider
        .state('search', {
            url: '/search',
            controller: 'SearchController',
            templateUrl: '/Scripts/app/map/views/search.results.html'
        })
        .state('venues', {
            url: '/venues',
            controller: 'VenueController',
            templateUrl: '/Scripts/app/venues/views/list.html'
        })
        .state('venue-details', {
            url: '/venues/:venueId',
            controller: 'VenueDetailsController',
            templateUrl: '/Scripts/app/venues/views/details.html',
            resolve: {
                venueId: ['$stateParams', $stateParams => {
                    return $stateParams.venueId;
                }]
            }
        })
        .state('outings', {
            url: '/outings',
            controller: 'OutingController',
            templateUrl: '/Scripts/app/outings/views/list.html'
        })
        .state('calendar', {
            url: '/calendar',
            controller: () => {
                console.log('not implemented yet');
            }
        });

        authProvider.init({
            domain: config.auth0.domain,
            clientID: config.auth0.clientId,
            callbackURL: config.url.frontend
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
