/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenueScope extends ng.IScope {
        venues: Array<IVenue>;
    }

    export class VenueController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IVenueScope,
            private venueResource)
        {
            this.update();

            var unregisterVenueAdded = this.$rootScope.$on('VenueAdded', () => this.update());
            this.$scope.$on('$destroy', () => unregisterVenueAdded());
        }

        private update(): void {
            this.load();
        }

        private load(): ng.IPromise<any> {
            return this.venueResource.all(data => {
                this.$scope.venues = data;
            }, err => {

            });
        }
    }
}

Burgerama.app.controller('VenueController', ['$rootScope', '$scope', 'VenueResource', ($rootScope, $scope, venueResource) =>
    new Burgerama.Venues.VenueController($rootScope, $scope, venueResource)
]);
