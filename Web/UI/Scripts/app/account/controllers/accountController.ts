/// <reference path="../../app.ts" />

module Burgerama.Account {
    export interface IUserData {
        email: string;
    }

    export interface ILoginScope extends ng.IScope {
        signedIn: boolean;
        user: IUserData;

        showSignInModal: () => void;
        signOut: () => void;
    }

    export class AccountController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: ILoginScope,
            private $modal, // ng.ui.IModalService ?
            private toaster,
            private authService: IAuthService)
        {
            this.update();
            this.$scope.showSignInModal = () => this.showSignInModal();
            this.$scope.signOut = () => this.signOut();

            var unregisterSignIn = this.$rootScope.$on('SignIn', () => this.update());
            var unregisterSignOut = this.$rootScope.$on('SignOut', () => this.update());

            this.$scope.$on('$destroy', () => {
                unregisterSignIn();
                unregisterSignOut();
            });
        }

        private update() {
            this.$scope.signedIn = this.authService.checkAuth();
            this.$scope.user = {
                email: this.authService.email
            };
        }

        private showSignInModal(): void {
            this.$modal.open({
                templateUrl: 'http://localhost/burgerama/Scripts/app/account/views/login.html',
                controller: 'SignInController'
            });
        }

        private signOut(): void {
            this.authService.checkAuth();

            var email = this.authService.email;
            this.authService.signOut()
                .then(() => {
                    this.toaster.pop("success", "Success", "Bye, " + email + "!");
                });
        }
    }
}

Burgerama.app.controller('AccountController', ['$rootScope', '$scope', '$modal', 'toaster', 'AuthService', ($rootScope, $scope, $modal, toaster, authService) =>
    new Burgerama.Account.AccountController($rootScope, $scope, $modal, toaster, authService)
]);
