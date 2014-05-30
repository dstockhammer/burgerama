/// <reference path="../../app.ts" />

Burgerama.app.factory('VoteResource', ['$resource', $resource => {
    return $resource(config.url.voting + '/venue/:id', {
        id: '@id'
    }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' },
        });
}]);