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

// Service for determining who the currently logged on user is.
var UrlLocatorTestService = (function () {
    function UrlLocatorTestService($http, $rootScope, $urlRouter) {
        var _this = this;
        this.$http = $http;
        this.$rootScope = $rootScope;
        this.$urlRouter = $urlRouter;
        $rootScope.$on("$locationChangeSuccess", function (event) {
            return _this.onLocationChangeSuccess(event);
        });
    }
    UrlLocatorTestService.prototype.onLocationChangeSuccess = function (event) {
        var _this = this;
        if (!this.currentUser) {
            // If the current user is unknown, halt the state change and request current
            // user details from the server
            event.preventDefault();

            // Note that we do not concern ourselves with what to do if this request fails,
            // because if it fails, the web page will be redirected away to the login screen.
            this.$http({ url: "/api/me", method: "GET" }).success(function (user) {
                _this.currentUser = user;

                // sync the ui-state with the location in the browser, which effectively
                // restarts the state change that was stopped previously
                _this.$urlRouter.sync();
            });
        }
    };
    UrlLocatorTestService.$inject = ["$http", "$rootScope", "$urlRouter"];
    return UrlLocatorTestService;
})();

myApp.service("urlLocatorTest", UrlLocatorTestService);

var UiViewScrollProviderTests;
(function (UiViewScrollProviderTests) {
    var app = angular.module("uiViewScrollProviderTests", ["ui.router"]);

    app.config([
        '$uiViewScrollProvider', function ($uiViewScrollProvider) {
            // This prevents unwanted scrolling to the active nested state view.
            // Use this when you have nested states, but you don't want the browser to scroll down the page
            // to the nested state view.
            //
            // See https://github.com/angular-ui/ui-router/issues/848
            // And https://github.com/angular-ui/ui-router/releases/tag/0.2.8
            $uiViewScrollProvider.useAnchorScroll();
        }]);
})(UiViewScrollProviderTests || (UiViewScrollProviderTests = {}));
//# sourceMappingURL=angular-ui-router-tests.js.map
