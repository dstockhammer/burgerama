/// <reference path="../../app.ts" />
Burgerama.app.factory('VoteResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('voting') + '/venue/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' }
        });
    }]);
//# sourceMappingURL=voteResource.js.map
