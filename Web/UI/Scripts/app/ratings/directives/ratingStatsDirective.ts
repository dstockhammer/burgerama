// <reference path="../../app.ts" />

module Burgerama.Ratings {
    export class RatingStatsDirective {
        constructor(private starRatingService: IStarRatingService) {}

        restrict = 'EA';
        template = '<p class="rating">{{ totalRating }}</p>';
        require = '^ngModel';
        scope = {
            ngModel: '='
        }
        link = (scope, element, attrs) => {
            scope.totalRating = scope.ngModel == null
                ? 'No ratings'
                : this.starRatingService.denormalizeRating(scope.ngModel);
        }
    }
}

Burgerama.app.directive('ratingStats', ['StarRatingService', starRatingService =>
    new Burgerama.Ratings.RatingStatsDirective(starRatingService)
]);
