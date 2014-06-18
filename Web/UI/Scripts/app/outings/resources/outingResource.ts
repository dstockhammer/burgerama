/// <reference path="../../app.ts" />

module Burgerama.Outings {
    export class Outing {
        id: string;
        date: Date;
        venue: Venues.Venue;
    }
}

Burgerama.app.factory('OutingResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('outings') + '/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true }
    });
}]);
