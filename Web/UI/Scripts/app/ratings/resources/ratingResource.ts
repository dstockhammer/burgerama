/// <reference path="../../app.ts" />

module Burgerama.Ratings {
    export class Rating {
        context: string;
        reference: string;
        userId: string;
        createdOn: Date;
        value: number;
        text: string;
    }
}

Burgerama.app.factory('RatingResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('ratings') + '/:context/:reference', {
        reference: '@reference',
        context: '@context'
    }, {
        all: { method: 'GET', isArray: true },
        create: { method: 'POST' },
    });
}]);
