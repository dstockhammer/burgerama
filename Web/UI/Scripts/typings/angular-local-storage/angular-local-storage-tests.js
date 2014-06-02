///<reference path="../angularjs/angular.d.ts" />
///<reference path="./angular-local-storage.d.ts" />

var ng;
(function (ng) {
    (function (LocalStorageTests) {
        var TestController = (function () {
            function TestController($scope, storage) {
                this.$scope = $scope;
                this.storage = storage;
                storage.bind($scope, 'varName');
                storage.bind($scope, 'varName', { defaultValue: 'randomValue123', storeName: 'customStoreKey' });
                $scope.viewType = 'ANYTHING';
                storage.unbind($scope, 'viewType');

                storage.set('key', 'value');
                storage.get('key');
                storage.remove('key');

                storage.clearAll();
            }
            return TestController;
        })();
        LocalStorageTests.TestController = TestController;
    })(ng.LocalStorageTests || (ng.LocalStorageTests = {}));
    var LocalStorageTests = ng.LocalStorageTests;
})(ng || (ng = {}));

var app = angular.module('angularLocalStorageTests', ['angularLocalStorage']);
app.controller('testCtrl', ['$scope', 'storage', function ($scope, storage) {
        return new ng.LocalStorageTests.TestController($scope, storage);
    }]);
//# sourceMappingURL=angular-local-storage-tests.js.map
