// <reference path="../../app.ts" />

module Burgerama.Ratings {
    export class RatingStatsDirective {
        constructor(private starRatingService: IStarRatingService) {}

        restrict = 'EA';
        templateUrl = '/Scripts/app/ratings/views/ratingStats.directive.html';
        require = '^ngModel';
        scope = {
            ngModel: '=',
            ratingsCount: '@',
            extended: '@'
        }
        link = (scope, element, attrs) => {
            scope.totalRating = this.starRatingService.formatTotalRating(scope.ngModel);

            scope.$watch('ngModel', value => {
                scope.totalRating = this.starRatingService.formatTotalRating(value);
            });
        }
    }
}

Burgerama.app.directive('ratingStats', ['StarRatingService', starRatingService =>
    new Burgerama.Ratings.RatingStatsDirective(starRatingService)
]);
