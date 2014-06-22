/// <reference path="../../app.ts" />

module Burgerama.Voting {
    export class Candidate {
        context: string;
        reference: string;
        isValidated: boolean;
        openingDate: Date;
        closingDate: Date;
        votesCount: number;
        canUserVote: boolean;
        userVote: Vote;
    }
}

Burgerama.app.factory('VotingCandidateResource', ['$resource', $resource => {
    return $resource(Burgerama.Util.getApiUrl('voting') + '/:context/:reference', {
        reference: '@reference',
        context: '@context'
    }, {
        get: { method: 'GET' }
    });
}]); 