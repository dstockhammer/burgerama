// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var StarRatingService = (function () {
            function StarRatingService() {
                this.starMin = 1;
                this.starMax = 5;
            }
            StarRatingService.prototype.denormalizeRating = function (value) {
                return value * (this.starMax - this.starMin) + this.starMin;
            };

            StarRatingService.prototype.normalizeRating = function (value) {
                return (value - this.starMin) / (this.starMax - this.starMin);
            };

            StarRatingService.prototype.formatTotalRating = function (value) {
                return value == null ? 'No ratings' : this.denormalizeRating(value).toFixed(1);
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

            StarRatingService.prototype.calculateRatingStats = function (ratings) {
                var _this = this;
                var stats = {
                    1: { total: 0, percent: 0 },
                    2: { total: 0, percent: 0 },
                    3: { total: 0, percent: 0 },
                    4: { total: 0, percent: 0 },
                    5: { total: 0, percent: 0 }
                };

                ratings.forEach(function (rating) {
                    var star = _this.denormalizeRating(rating.value);
                    stats[star].total++;
                });

                for (var i = 1; i <= 5; i++) {
                    stats[i].percent = 100 / ratings.length * stats[i].total;
                }

                return stats;
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
//# sourceMappingURL=starRatingService.js.map
