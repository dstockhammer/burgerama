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
        'ngRoute',
        'ngCookies',

        // Angular UI modules
        'ui.bootstrap',
        'ui.event',
        'ui.map',

        // 3rd Party Modules
        'LocalStorageModule',
        'toaster',
        'auth0',
        'truncate'
    ]);

    app.config(['$httpProvider', '$routeProvider', 'authProvider', ($httpProvider: ng.IHttpProvider, $routeProvider: ng.route.IRouteProvider, authProvider) => {
        $httpProvider.interceptors.push('AuthHttpInterceptor');

        $routeProvider
            .when('/', {
                // nothing to do for now
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

Burgerama.app.run(() => {
    // todo: add a loading screen and hide it here
});

angular.element(document).ready(() => {
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyDMSKCEtbHIORL8DuKSxSEXxkBGB-mqf9c&sensor=true&libraries=places&callback=Burgerama.initialize';
    document.body.appendChild(script);
});
