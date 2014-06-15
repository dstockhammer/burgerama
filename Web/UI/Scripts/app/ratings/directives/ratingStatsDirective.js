// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var RatingStatsDirective = (function () {
            function RatingStatsDirective(starRatingService) {
                var _this = this;
                this.starRatingService = starRatingService;
                this.restrict = 'EA';
                this.templateUrl = '/Scripts/app/ratings/views/ratingStats.directive.html';
                this.require = '^ngModel';
                this.scope = {
                    ngModel: '=',
                    ratingsCount: '@',
                    extended: '@'
                };
                this.link = function (scope, element, attrs) {
                    scope.totalRating = _this.starRatingService.formatTotalRating(scope.ngModel);

                    scope.$watch('ngModel', function (value) {
                        scope.totalRating = _this.starRatingService.formatTotalRating(value);
                    });
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
