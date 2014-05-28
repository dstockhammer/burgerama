/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenue {
        id: string;
        title: string;
        location: ILocation;
        url: string;
        description: string;
        address: string;
        rating: number;
        votes: number;
    }

    export interface ILocation {
        reference: string;
        latitude: number;
        longitude: number;
    }
}

Burgerama.app.factory('VenueResource', ['$resource', 'configuration', ($resource, config) => {
    return $resource(config.url.voting + '/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true },
        get: { method: 'GET' }
    });
}]);
