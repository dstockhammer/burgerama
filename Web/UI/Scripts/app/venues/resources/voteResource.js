Burgerama.app.factory('VoteResource', [
    '$resource', function ($resource) {
        return $resource('http://api.dev.burgerama.co.uk/voting/venue/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' }
        });
    }]);
//# sourceMappingURL=voteResource.js.map
