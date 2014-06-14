// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var StarRatingService = (function () {
            function StarRatingService() {
                this.starMin = 1;
                this.starMax = 1;
            }
            StarRatingService.prototype.denormalizeRating = function (value) {
                return value * (this.starMax - this.starMin) + this.starMin;
            };

            StarRatingService.prototype.normalizeRatin = function (value) {
                return (value - this.starMin) / (this.starMax - this.starMin);
            };

            StarRatingService.prototype.getTextForStar = function (star) {
                switch (star) {
                    case 1:
                        return 'Hate it!';
                    case 2:
                        return 'Dislike it';
                    case 3:
                        return "It's okay";
                    case 4:
                        return 'Like it';
                    case 5:
                        return 'Love it!';

                    default:
                        return null;
                }
            };
            return StarRatingService;
        })();
        Ratings.StarRatingService = StarRatingService;
    })(Burgerama.Ratings || (Burgerama.Ratings = {}));
    var Ratings = Burgerama.Ratings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.service('StarRatingService', function () {
    return new Burgerama.Ratings.StarRatingService();
});
//# sourceMappingURL=ratingService.js.map
