/// <reference path="../../app.ts" />

Burgerama.app.factory('VoteResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('voting') + '/venues/candidates/:id/votes', {
        id: '@id'
    }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' },
        });
}]);