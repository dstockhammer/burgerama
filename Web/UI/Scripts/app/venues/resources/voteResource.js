Burgerama.app.factory('VoteResource', [
    '$resource', 'configuration', function ($resource, config) {
        return $resource(config.url.voting + '/venue/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' }
        });
    }]);
//# sourceMappingURL=voteResource.js.map
