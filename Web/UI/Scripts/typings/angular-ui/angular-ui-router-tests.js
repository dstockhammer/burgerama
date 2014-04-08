/// <reference path="angular-ui-router.d.ts" />
var myApp = angular.module('testModule');

myApp.config(function ($stateProvider, $urlRouterProvider, $urlMatcherFactory) {
    var matcher = $urlMatcherFactory.compile("/foo/:bar?param1");

    $urlRouterProvider.when('/test', '/list').when('/test', '/list').when('/test', '/list').when(/\/test\d/, '/list').when(/\/test\d/, function ($injector, $location) {
        return '/list';
    }).when(/\/test\d/, ['$injector', '$location', function ($injector, $location) {
            return '/list';
        }]).when(matcher, '/list').when(matcher, function ($injector, $location) {
        return '/list';
    }).when(matcher, ['$injector', '$location', function ($injector, $location) {
            return '/list';
        }]).otherwise("/state1");

    // Now set up the states
    $stateProvider.state('state1', {
        url: "/state1",
        templateUrl: "partials/state1.html"
    }).state('state1.list', {
        url: "/list",
        templateUrl: "partials/state1.list.html",
        controller: function ($scope) {
            $scope.items = ["A", "List", "Of", "Items"];
        }
    }).state('state2', {
        url: "/state2",
        templateUrl: "partials/state2.html"
    }).state('state2.list', {
        url: "/list",
        templateUrl: "partials/state2.list.html",
        controller: function ($scope) {
            $scope.things = ["A", "Set", "Of", "Things"];
        }
    }).state('index', {
        url: "",
        views: {
            "viewA": { template: "index.viewA" },
            "viewB": { template: "index.viewB" }
        }
    }).state('route1', {
        url: "/route1",
        views: {
            "viewA": { template: "route1.viewA" },
            "viewB": { template: "route1.viewB" }
        }
    }).state('route2', {
        url: "/route2",
        views: {
            "viewA": { template: "route2.viewA" },
            "viewB": { template: "route2.viewB" }
        }
    });
});
//# sourceMappingURL=angular-ui-router-tests.js.map
