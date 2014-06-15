// <reference path="../../app.ts" />

module Burgerama.Ratings {
    export interface IStarRatingService {
        denormalizeRating: (value: number) => number;
        normalizeRating: (value: number) => number;
        formatTotalRating: (value: number) => string;
        getTextForStar: (star: number) => string;
        calculateRatingStats: (ratings: Array<Rating>) => any;
    }

    export class StarRatingService implements IStarRatingService {
        private starMin = 1; // todo: make const when the keyword becomes available
        private starMax = 5; // todo: make const when the keyword becomes available

        public denormalizeRating(value: number): number {
            return value * (this.starMax - this.starMin) + this.starMin;
        }

        public normalizeRating(value: number): number {
            return (value - this.starMin) / (this.starMax - this.starMin);
        }

        public formatTotalRating(value) {
            return value == null
                ? 'n/a'
                : this.denormalizeRating(value).toFixed(1);
        }

        public getTextForStar(star: number): string {
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
        }

        public calculateRatingStats(ratings: Array<Ratings.Rating>) {
            var stats = {
                1: { total: 0, percent: 0 },
                2: { total: 0, percent: 0 },
                3: { total: 0, percent: 0 },
                4: { total: 0, percent: 0 },
                5: { total: 0, percent: 0 }
            }

            ratings.forEach((rating: Ratings.Rating) => {
                var star = this.denormalizeRating(rating.value);
                stats[star].total++;
            });

            for (var i = 1; i <= 5; i++) {
                stats[i].percent = 100 / ratings.length * stats[i].total;
            }

            return stats;
        }
    }
}

Burgerama.app.service('StarRatingService', () =>
    new Burgerama.Ratings.StarRatingService()
);
