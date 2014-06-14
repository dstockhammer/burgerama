/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenue {
        id: string;
        name: string;
        location: ILocation;
        url: string;
        description: string;
        address: string;
        totalRating: number;
        totalVotes: number;
    }

    export interface ILocation {
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
