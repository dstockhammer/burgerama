/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var Rating = (function () {
            function Rating() {
            }
            return Rating;
        })();
        Ratings.Rating = Rating;
    })(Burgerama.Ratings || (Burgerama.Ratings = {}));
    var Ratings = Burgerama.Ratings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory('RatingResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('ratings') + '/:context/:reference/ratings', {
            context: '@context',
            reference: '@reference'
        }, {
            all: { method: 'GET', isArray: true },
            create: { method: 'POST' }
        });
    }]);
//# sourceMappingURL=ratingResource.js.map
