/// <reference path="../../app.ts" />

Burgerama.app.factory('OutingResource', [
    '$resource', 'configuration', function ($resource, config) {
        return $resource(config.url.outings + '/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true }
        });
    }]);
//# sourceMappingURL=outingResource.js.map
