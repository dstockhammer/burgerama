/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Voting) {
        var Vote = (function () {
            function Vote() {
            }
            return Vote;
        })();
        Voting.Vote = Vote;
    })(Burgerama.Voting || (Burgerama.Voting = {}));
    var Voting = Burgerama.Voting;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory('VoteResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('voting') + '/:context/:reference/votes', {
            context: '@context',
            reference: '@reference'
        }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' }
        });
    }]);
//# sourceMappingURL=voteResource.js.map
