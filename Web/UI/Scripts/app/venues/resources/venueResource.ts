/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export class Venue {
        id: string;
        name: string;
        location: Location;
        url: string;
        description: string;
        address: string;
        totalRating: number;
        totalVotes: number;
    }

    export class Location {
        reference: string;
        latitude: number;
        longitude: number;
    }
}

Burgerama.app.factory('VenueResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('venues') + '/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true },
        get: { method: 'GET' },
        create: { method: 'POST' }
    });
}]);
