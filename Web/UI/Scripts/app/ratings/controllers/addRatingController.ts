// <reference path="../../app.ts" />

module Burgerama.Ratings {
    export interface IRatingContext {
        key: string;
        reference: string;
        title: string;
    }

    export interface IAddRatingScope extends ng.IScope {
        context: IRatingContext
        rating: Rating;
        ratingError: boolean;

        starValue: number;
        starText: string;

        ok: () => void;
        cancel: () => void;

        hoverStar: (star: number) => void;
        leaveStar: () => void;
    }

    export class AddRatingController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: IAddRatingScope,
            private $modalInstance,
            private ratingResource,
            private toaster,
            private starRatingService: IStarRatingService,
            private context: IRatingContext)
        {
            this.$scope.rating = new Rating();
            this.$scope.rating.context = context.key;
            this.$scope.rating.reference = context.reference;
            this.$scope.context = context;

            this.$scope.starValue = 0;
            this.$scope.starText = null;

            this.$scope.ok = () => this.ok();
            this.$scope.cancel = () => this.cancel();

            this.$scope.hoverStar = star => this.hoverStar(star);
            this.$scope.leaveStar = () => this.leaveStar();
        }

        private ok() {
            this.$scope.ratingError = typeof(this.$scope.rating.value) === 'undefined'
                || this.$scope.rating.value < 0
                || this.$scope.rating.value > 1;
            
            if (this.$scope.ratingError)
                return;

            this.$modalInstance.close();

            var resource = new this.ratingResource(this.$scope.rating);
            resource.$create(() => {
                this.toaster.pop('success', 'Success', 'Thanks for your contribution!');
                this.$rootScope.$emit('RatingAdded', this.$scope.rating);
            }, err => {
                if (err.status == 401) {
                    this.toaster.pop('error', 'Unauthorized', 'You are not authorized to rate. Please log in or create an account.');
                } else if (err.status == 409) {
                    this.toaster.pop('error', 'Conflict', 'You have already rated this item.');
                } else {
                    this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                }
            });
        }

        private cancel() {
            this.$modalInstance.dismiss();
        }

        private hoverStar(star: number) {
            this.$scope.starText = this.starRatingService.getTextForStar(star);
        }

        private leaveStar() {
            this.$scope.starText = this.starRatingService.getTextForStar(this.$scope.starValue);
            this.$scope.rating.value = this.starRatingService.normalizeRating(this.$scope.starValue);
        }
    }
}

Burgerama.app.controller('AddRatingController', ['$rootScope', '$scope', '$modalInstance', 'RatingResource', 'toaster', 'StarRatingService', 'context', ($rootScope, $scope, $modalInstance, ratingResource, toaster, starRatingService, context) =>
    new Burgerama.Ratings.AddRatingController($rootScope, $scope, $modalInstance, ratingResource, toaster, starRatingService, context)
]);
