/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />
'use strict';
var Burgerama;
(function (Burgerama) {
    function initialize() {
        angular.bootstrap(document, ['burgerama']);
    }
    Burgerama.initialize = initialize;

    Burgerama.app = angular.module('burgerama', [
        'ngResource',
        'ngCookies',
        'ngAnimate',
        'ui.bootstrap',
        'ui.event',
        'ui.router',
        'ui.map',
        'LocalStorageModule',
        'toaster',
        'truncate',
        'auth0'
    ]);

    Burgerama.app.config([
        '$httpProvider', '$stateProvider', '$urlRouterProvider', 'authProvider', function ($httpProvider, $stateProvider, $urlRouterProvider, authProvider) {
            $httpProvider.interceptors.push('AuthHttpInterceptor');
            $httpProvider.defaults.transformResponse.push(function (responseData) {
                Burgerama.Util.convertDateStringsToDates(responseData);
                return responseData;
            });

            $urlRouterProvider.otherwise('/venues');
            $stateProvider.state('search', {
                url: '/search',
                controller: 'SearchController',
                templateUrl: '/Scripts/app/map/views/search.results.html'
            }).state('venues', {
                url: '/venues',
                controller: 'VenueController',
                templateUrl: '/Scripts/app/venues/views/list.html'
            }).state('venue-details', {
                url: '/venues/:venueId',
                controller: 'VenueDetailsController',
                templateUrl: '/Scripts/app/venues/views/details.html',
                resolve: {
                    venueId: [
                        '$stateParams', function ($stateParams) {
                            return $stateParams.venueId;
                        }]
                }
            }).state('outings', {
                url: '/outings',
                controller: 'OutingController',
                templateUrl: '/Scripts/app/outings/views/list.html'
            }).state('calendar', {
                url: '/calendar',
                controller: function () {
                    console.log('not implemented yet');
                }
            });

            authProvider.init({
                domain: config.auth0.domain,
                clientID: config.auth0.clientId,
                callbackURL: config.url.frontend,
                callbackOnLocationHash: true,
                showIcon: false
            });
        }]);
})(Burgerama || (Burgerama = {}));

Burgerama.app.run([
    '$rootScope', function ($rootScope) {
        // give people a second to admire the loading screen
        setTimeout(function () {
            $rootScope.loaded = true;
        }, 1000);
    }]);

angular.element(document).ready(function () {
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyDMSKCEtbHIORL8DuKSxSEXxkBGB-mqf9c&sensor=true&libraries=places&callback=Burgerama.initialize';
    document.body.appendChild(script);
});
//# sourceMappingURL=app.js.map
