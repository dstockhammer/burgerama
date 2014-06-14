// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var RatingStatsDirective = (function () {
            function RatingStatsDirective(starRatingService) {
                var _this = this;
                this.starRatingService = starRatingService;
                this.restrict = 'EA';
                this.template = '<p class="rating">{{ totalRating }}</p>';
                this.require = '^ngModel';
                this.scope = {
                    ngModel: '='
                };
                this.link = function (scope, element, attrs) {
                    scope.totalRating = scope.ngModel == null ? 'No ratings' : _this.starRatingService.denormalizeRating(scope.ngModel);
                };
            }
            return RatingStatsDirective;
        })();
        Ratings.RatingStatsDirective = RatingStatsDirective;
    })(Burgerama.Ratings || (Burgerama.Ratings = {}));
    var Ratings = Burgerama.Ratings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.directive('ratingStats', [
    'StarRatingService', function (starRatingService) {
        return new Burgerama.Ratings.RatingStatsDirective(starRatingService);
    }
]);
//# sourceMappingURL=ratingStatsDirective.js.map
