﻿/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenue {
        id: string;
        title: string;
        location: ILocation;
        url: string;
        description: string;
    }

    export interface ILocation {
        reference: string;
        latitude: number;
        longitude: number;
    }
}

Burgerama.app.factory('VenueResource', ['$resource', $resource => {
    return $resource('http://api.dev.burgerama.co.uk/venues/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true },
        save: { method: 'PUT' },
        create: { method: 'POST' },
        destroy: { method: 'DELETE' }
    });
}]);
