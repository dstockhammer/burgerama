// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var AddRatingController = (function () {
            function AddRatingController($rootScope, $scope, $modalInstance, ratingResource, toaster, starRatingService, context) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modalInstance = $modalInstance;
                this.ratingResource = ratingResource;
                this.toaster = toaster;
                this.starRatingService = starRatingService;
                this.context = context;
                this.$scope.rating = new Ratings.Rating();
                this.$scope.rating.context = context.key;
                this.$scope.rating.reference = context.reference;
                this.$scope.context = context;

                this.$scope.starValue = 0;
                this.$scope.starText = null;

                this.$scope.ok = function () {
                    return _this.ok();
                };
                this.$scope.cancel = function () {
                    return _this.cancel();
                };

                this.$scope.hoverStar = function (star) {
                    return _this.hoverStar(star);
                };
                this.$scope.leaveStar = function () {
                    return _this.leaveStar();
                };
            }
            AddRatingController.prototype.ok = function () {
                var _this = this;
                this.$scope.ratingError = typeof (this.$scope.rating.value) === 'undefined' || this.$scope.rating.value < 0 || this.$scope.rating.value > 1;

                if (this.$scope.ratingError)
                    return;

                this.$modalInstance.close();

                var resource = new this.ratingResource(this.$scope.rating);
                resource.$create(function (candidate) {
                    _this.toaster.pop('success', 'Success', 'Thanks for your contribution!');
                    _this.$rootScope.$emit('RatingAdded', candidate.userRating);
                }, function (err) {
                    if (err.status == 401) {
                        _this.toaster.pop('error', 'Unauthorized', 'You are not authorized to rate. Please log in or create an account.');
                    } else if (err.status == 409) {
                        _this.toaster.pop('error', 'Conflict', 'You have already rated this item.');
                    } else {
                        _this.toaster.pop('error', 'Error', 'An error has occurred: ' + err.statusText);
                    }
                });
            };

            AddRatingController.prototype.cancel = function () {
                this.$modalInstance.dismiss();
            };

            AddRatingController.prototype.hoverStar = function (star) {
                this.$scope.starText = this.starRatingService.getTextForStar(star);
            };

            AddRatingController.prototype.leaveStar = function () {
                this.$scope.starText = this.starRatingService.getTextForStar(this.$scope.starValue);
                this.$scope.rating.value = this.starRatingService.normalizeRating(this.$scope.starValue);
            };
            return AddRatingController;
        })();
        Ratings.AddRatingController = AddRatingController;
    })(Burgerama.Ratings || (Burgerama.Ratings = {}));
    var Ratings = Burgerama.Ratings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('AddRatingController', [
    '$rootScope', '$scope', '$modalInstance', 'RatingResource', 'toaster', 'StarRatingService', 'context', function ($rootScope, $scope, $modalInstance, ratingResource, toaster, starRatingService, context) {
        return new Burgerama.Ratings.AddRatingController($rootScope, $scope, $modalInstance, ratingResource, toaster, starRatingService, context);
    }
]);
//# sourceMappingURL=addRatingController.js.map
