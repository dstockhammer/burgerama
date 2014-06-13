// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Ratings) {
        var AddRatingController = (function () {
            function AddRatingController($rootScope, $scope, $modalInstance, ratingResource, toaster, context) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$modalInstance = $modalInstance;
                this.ratingResource = ratingResource;
                this.toaster = toaster;
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
                this.$modalInstance.close();

                var resource = new this.ratingResource(this.$scope.rating);
                resource.$create(function () {
                    _this.toaster.pop('success', 'Success', 'Thanks for your contribution!');
                    _this.$rootScope.$emit('RatingAdded', _this.$scope.rating);
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
                this.$scope.starText = this.getTextForStar(star);
            };

            AddRatingController.prototype.leaveStar = function () {
                this.$scope.starText = this.getTextForStar(this.$scope.starValue);
                this.$scope.rating.value = this.normalizeRatingForStar(this.$scope.starValue);

                console.log('leave', this.$scope.starValue, this.$scope.starText);
            };

            AddRatingController.prototype.normalizeRatingForStar = function (star) {
                var min = 1;
                var max = 5;

                return (star - min) / (max - min);
            };

            AddRatingController.prototype.getTextForStar = function (star) {
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
            return AddRatingController;
        })();
        Ratings.AddRatingController = AddRatingController;
    })(Burgerama.Ratings || (Burgerama.Ratings = {}));
    var Ratings = Burgerama.Ratings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('AddRatingController', [
    '$rootScope', '$scope', '$modalInstance', 'RatingResource', 'toaster', 'context', function ($rootScope, $scope, $modalInstance, ratingResource, toaster, context) {
        return new Burgerama.Ratings.AddRatingController($rootScope, $scope, $modalInstance, ratingResource, toaster, context);
    }
]);
//# sourceMappingURL=addRatingController.js.map
