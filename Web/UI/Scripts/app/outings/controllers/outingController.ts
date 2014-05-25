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
            return this.outingResource.all(data => {
                this.$scope.outings = data;
                this.$rootScope.$broadcast('OutingsLoaded', this.$scope.outings);
            }, err => {
                this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
            });
        }

        private panTo(outing: IOuting) {
            this.$rootScope.$broadcast('PanToClicked', outing.venue);
        }
    }
}

Burgerama.app.controller('OutingController', ['$rootScope', '$scope', 'OutingResource', 'toaster', ($rootScope, $scope, outingResource, toaster) =>
    new Burgerama.Outings.OutingController($rootScope, $scope, outingResource, toaster)
]);
