/// <reference path="../../app.ts" />

module Burgerama.Account {
    export interface ILoginScope extends ng.IScope {
        signedIn: boolean;
        user: any;

        showSignInModal: () => void;
        signOut: () => void;
    }

    export class AccountController {
        constructor(
            private $rootScope: IBurgeramaScope,
            private $scope: ILoginScope,
            private authService: IAuthService)
        {
            this.update();
            
            this.$scope.showSignInModal = () => this.showSignInModal();
            this.$scope.signOut = () => this.signOut();

            var unregisterSignIn = this.$rootScope.$on('UserSignedIn', () => this.update());
            var unregisterSignOut = this.$rootScope.$on('UserSignedOut', () => this.update());

            this.$scope.$on('$destroy', () => {
                unregisterSignIn();
                unregisterSignOut();
            });
        }

        private update(): void {
            this.$scope.signedIn = this.authService.isAuthenticated();
            this.$scope.user = this.authService.getUser();
        }

        private showSignInModal(): void {
            this.authService.signIn();
        }

        private signOut(): void {
            this.authService.signOut();
        }
    }
}

Burgerama.app.controller('AccountController', ['$rootScope', '$scope', 'AuthService', ($rootScope, $scope, authService) =>
    new Burgerama.Account.AccountController($rootScope, $scope, authService)
]);
