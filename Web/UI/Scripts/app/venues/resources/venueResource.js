/// <reference path="../../app.ts" />

Burgerama.app.factory('VenueResource', [
    '$resource', function ($resource) {
        return $resource('http://api.dev.burgerama.co.uk/voting/venue/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            get: { method: 'GET' },
            save: { method: 'PUT' },
            create: { method: 'POST' },
            destroy: { method: 'DELETE' }
        });
    }]);
//# sourceMappingURL=venueResource.js.map
