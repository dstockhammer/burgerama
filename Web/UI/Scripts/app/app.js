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
                controller: function () {
                    console.log('not implemented yet');
                }
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
                domain: 'burgerama.auth0.com',
                clientID: 'xlaKo4Eqj5DbAJ44BmUGQhUF548TNc4Z',
                callbackURL: "http://dev.burgerama.co.uk/"
            });
        }]);
})(Burgerama || (Burgerama = {}));

Burgerama.app.run([
    '$rootScope', function ($rootScope) {
        $rootScope.loaded = true;
    }]);

angular.element(document).ready(function () {
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyDMSKCEtbHIORL8DuKSxSEXxkBGB-mqf9c&sensor=true&libraries=places&callback=Burgerama.initialize';
    document.body.appendChild(script);
});
//# sourceMappingURL=app.js.map
