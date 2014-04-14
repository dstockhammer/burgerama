/// <reference path="../../app.ts" />

module Burgerama.Account {
    export class LoginModalController {
        constructor(
            private $scope: ILoginScope,
            private $modalInstance, // ng.ui.IModalInstance ?
            private authService: IAuthService)
        {
            this.$scope.submit = () => this.submit();
            this.$scope.cancel = () => this.cancel();
        }

        private cancel(): void {
            console.log("cancel");
            this.$modalInstance.dismiss();
        }

        private submit(): void {
            console.log("submit");
            this.$modalInstance.close();

            this.authService.login(this.$scope.email, this.$scope.password, this.$scope.persistent)
                .then(token => {
                    //this.alertService.success('Logged in as ' + token.userName, "/");
                }, data => {
                    //this.alertService.error(data.error_description);
                });
        }
    }
}

Burgerama.app.controller('LoginModalController', ['$scope', '$modalInstance', 'AuthService', ($scope, $modalInstance, authService) =>
    new Burgerama.Account.LoginModalController($scope, $modalInstance, authService)
]);
