/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export interface IOuting {
        id: string;
        date: Date;
        venueId: string;
        venue: Venues.IVenue;
    }
}

Burgerama.app.factory('OutingResource', ['$resource', 'configuration', ($resource, config) => {
    return $resource(config.url.outings + '/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true }
    });
}]);
