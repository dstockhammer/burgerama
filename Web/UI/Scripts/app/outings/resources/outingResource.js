/// <reference path="../../app.ts" />

Burgerama.app.factory('OutingResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('outings') + '/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true }
        });
    }]);
//# sourceMappingURL=outingResource.js.map
