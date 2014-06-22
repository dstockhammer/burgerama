/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Voting) {
        var Candidate = (function () {
            function Candidate() {
            }
            return Candidate;
        })();
        Voting.Candidate = Candidate;
    })(Burgerama.Voting || (Burgerama.Voting = {}));
    var Voting = Burgerama.Voting;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory('VotingCandidateResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('voting') + '/:context/:reference', {
            reference: '@reference',
            context: '@context'
        }, {
            get: { method: 'GET' }
        });
    }]);
//# sourceMappingURL=candidateResource.js.map
