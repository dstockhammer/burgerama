Burgerama.app.factory('VoteResource', ['$resource', $resource => {
    return $resource('http://api.dev.burgerama.co.uk/voting/venue/:id', {
        id: '@id'
    }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' },
        });
}]);