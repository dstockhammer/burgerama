/// <reference path="../../app.ts" />

Burgerama.app.factory('OutingResource', [
    '$resource', function ($resource) {
        return $resource('http://api.dev.burgerama.co.uk/outings/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true }
        });
    }]);
//# sourceMappingURL=outingResource.js.map
