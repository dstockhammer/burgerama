/// <reference path='../typings/angularjs/angular.d.ts'/>
/// <reference path='../typings/angularjs/angular-resource.d.ts'/>

'use strict';

module Burgerama {
    export interface IModule {
        Id: string;
    }

    // Create the module and define its dependencies.
    export var App: ng.IModule = angular.module('burgerama', [
        // Angular modules
        'ngResource',

        // Angular UI modules
        'ui.bootstrap'
    ]);
}

Burgerama.App.run(() => {
    // todo
});