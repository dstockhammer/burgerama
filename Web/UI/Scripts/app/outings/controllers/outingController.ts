/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export interface IOutingScope extends ng.IScope {
        outings: Array<IOuting>;

        panTo: (outing: IOuting) => void;
    }

    export class OutingController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IOutingScope,
            private outingResource,
            private toaster)
        {
            this.$scope.outings = null;
            this.$scope.panTo = outing => this.panTo(outing);

            this.load();
        }

        private load() {
            this.outingResource.all(data => {
                this.$scope.outings = data;
                this.$rootScope.$emit('OutingsLoaded', this.$scope.outings);
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private panTo(outing: IOuting) {
            this.$rootScope.$emit('VenueSelected', outing.venue);
        }
    }
}

Burgerama.app.controller('OutingController', ['$rootScope', '$scope', 'OutingResource', 'toaster', ($rootScope, $scope, outingResource, toaster) =>
    new Burgerama.Outings.OutingController($rootScope, $scope, outingResource, toaster)
]);
