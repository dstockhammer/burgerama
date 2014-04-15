/// <reference path="../../app.ts" />

module Burgerama.Account {
    export interface ICredentials {
        email: string;
        password: string;
        persistent: boolean;
    }

    export class SignInController {
        constructor(
            private $scope,
            private $modalInstance, // ng.ui.IModalInstance ?
            private toaster,
            private authService: IAuthService)
        {
            this.$scope.credentials = {
                email: this.authService.email,
                password: "",
                persistent: false
            }

            this.$scope.submit = () => this.submit();
            this.$scope.cancel = () => this.cancel();
        }

        private cancel(): void {
            this.$modalInstance.dismiss();
        }

        private submit(): void {
            this.$modalInstance.close();

            this.authService.signIn(this.$scope.credentials.email, this.$scope.credentials.password, this.$scope.credentials.persistent)
                .then(token => {
                    this.toaster.pop("success", "Success", "Signed in as " + token.userName + ".");
                }, data => {
                    this.toaster.pop("error", "Error", "Authentication failed: " + data.error_description);
                });
        }
    }
}

Burgerama.app.controller('SignInController', ['$scope', '$modalInstance', 'toaster', 'AuthService', ($scope, $modalInstance, toaster, authService) =>
    new Burgerama.Account.SignInController($scope, $modalInstance, toaster, authService)
]);
