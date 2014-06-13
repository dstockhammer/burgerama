/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export interface IOuting {
        id: string;
        date: Date;
        venue: Venues.IVenue;
    }
}

Burgerama.app.factory('OutingResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('outings') + '/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true }
    });
}]);
