/// <reference path="../../app.ts" />

module Burgerama.Ratings {
    export class Candidate {
        context: string;
        reference: string;
        isValidated: boolean;
        openingDate: Date;
        closingDate: Date;
        ratingsCount: number;
        totalRating: number;
        canUserRate: boolean;
        userRating: Rating;
    }
}

Burgerama.app.factory('CandidateResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('ratings') + '/:context/:reference', {
        reference: '@reference',
        context: '@context'
    }, {
        get: { method: 'GET' }
    });
}]);
