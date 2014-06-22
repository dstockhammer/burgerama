/// <reference path="../../app.ts" />

module Burgerama.Voting {
    export class Vote {
        createdOn: Date;
        userId: string;
    }
}

Burgerama.app.factory('VoteResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('voting') + '/:context/:reference/votes', {
        context: '@context',
        reference: '@reference'
    }, {
        all: { method: 'GET', isArray: true },
        create: { method: 'POST' },
    });
}]);
