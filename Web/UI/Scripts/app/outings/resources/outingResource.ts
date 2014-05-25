/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export interface IOuting {
        id: string;
        venue: Venues.IVenue;
    }
}

Burgerama.app.factory('OutingResource', ['$resource', $resource => {
    return $resource('http://api.dev.burgerama.co.uk/outings/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true }
    });
}]);
