// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Map) {
        var SearchController = (function () {
            function SearchController($rootScope, $scope) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$scope.places = [];
                this.$scope.panTo = function (place) {
                    return _this.panTo(place);
                };

                var unregisterSearchedForPlaces = this.$rootScope.$on('CurrentSearchUpdated', function (event, results) {
                    _this.$scope.places = results;
                });
                this.$scope.$on('$destroy', function () {
                    return unregisterSearchedForPlaces();
                });

                this.$rootScope.$emit('SearchResultsLoaded');
            }
            SearchController.prototype.panTo = function (place) {
                this.$rootScope.$emit('PlaceSelected', place);
            };
            return SearchController;
        })();
        Map.SearchController = SearchController;
    })(Burgerama.Map || (Burgerama.Map = {}));
    var Map = Burgerama.Map;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('SearchController', [
    '$rootScope', '$scope', function ($rootScope, $scope) {
        return new Burgerama.Map.SearchController($rootScope, $scope);
    }
]);
//# sourceMappingURL=searchController.js.map
