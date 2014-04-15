/// <reference path="../../app.ts" />

module Burgerama.Venues {
    export interface IVenue extends ng.resource.IResourceService {
        Id: string;
        Title: string;
    }
}

Burgerama.app.factory('VenueResource', ['$resource', $resource => {
    return $resource('http://localhost/burgerama/api/venues/venues/:id', {
        id: '@id'
    }, {
        all: { method: 'GET', isArray: true },
        save: { method: 'PUT' },
        create: { method: 'POST' },
        destroy: { method: 'DELETE' }
    });
}]);
