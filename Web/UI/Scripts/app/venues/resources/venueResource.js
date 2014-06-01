﻿/// <reference path="../../app.ts" />

Burgerama.app.factory('VenueResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('venues') + '/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            get: { method: 'GET' },
            create: { method: 'POST' }
        });
    }]);
//# sourceMappingURL=venueResource.js.map
