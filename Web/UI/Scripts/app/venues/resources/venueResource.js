/// <reference path="../../app.ts" />

Burgerama.app.factory('VenueResource', [
    '$resource', function ($resource) {
        return $resource('http://localhost/burgerama/api/venues/venues/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            save: { method: 'PUT' },
            create: { method: 'POST' },
            destroy: { method: 'DELETE' }
        });
    }]);
//# sourceMappingURL=venueResource.js.map
