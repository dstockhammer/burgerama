/// <reference path="../../app.ts" />

Burgerama.app.factory('VenueResource', [
    '$resource', 'configuration', function ($resource, config) {
        return $resource(config.url.voting + '/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            get: { method: 'GET' }
        });
    }]);
//# sourceMappingURL=venueResource.js.map
