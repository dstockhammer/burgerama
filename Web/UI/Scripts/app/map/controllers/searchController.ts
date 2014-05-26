// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />

module Burgerama.Map {
    export interface ISearchScope extends ng.IScope {
        places: Array<google.maps.places.PlaceResult>;

        panTo: (place: google.maps.places.PlaceResult) => void;
    }

    export class SearchController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: ISearchScope)
        {
            this.$scope.places = [];
            this.$scope.panTo = place => this.panTo(place);

            var unregisterSearchedForPlaces = this.$rootScope.$on('CurrentSearchUpdated', (event, results: Array<google.maps.places.PlaceResult>) => {
                this.$scope.places = results;
            });
            this.$scope.$on('$destroy', () => unregisterSearchedForPlaces());

            this.$rootScope.$emit('SearchResultsLoaded');
        }

        private panTo(place: google.maps.places.PlaceResult) {
            this.$rootScope.$emit('PlaceSelected', place);
        }
    }
} 

Burgerama.app.controller('SearchController', ['$rootScope', '$scope', ($rootScope, $scope) =>
    new Burgerama.Map.SearchController($rootScope, $scope)
]);
