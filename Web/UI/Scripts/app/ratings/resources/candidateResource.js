/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var Candidate = (function () {
            function Candidate() {
            }
            return Candidate;
        })();
        Ratings.Candidate = Candidate;
    })(Burgerama.Ratings || (Burgerama.Ratings = {}));
    var Ratings = Burgerama.Ratings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory('CandidateResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('ratings') + '/:context/:reference', {
            reference: '@reference',
            context: '@context'
        }, {
            get: { method: 'GET' }
        });
    }]);
//# sourceMappingURL=candidateResource.js.map
