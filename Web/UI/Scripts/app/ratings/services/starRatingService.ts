// <reference path="../../app.ts" />

module Burgerama.Ratings {
    export interface IStarRatingService {
        denormalizeRating: (value: number) => number;
        normalizeRating: (value: number) => number;

        getTextForStar: (star: number) => string;
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
    }
}

Burgerama.app.service('StarRatingService', () =>
    new Burgerama.Ratings.StarRatingService()
);
