/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenue extends ng.resource.IResourceService {
        id: string;
        title: string;
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
