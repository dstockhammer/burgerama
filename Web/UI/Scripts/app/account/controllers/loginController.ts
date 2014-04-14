/// <reference path="../../app.ts" />

module Burgerama.Account {
    export interface ILoginScope extends ng.IScope {
        email: string;
        password: string;
        persistent: boolean;

        showModal: () => void;
        submit: () => void;
        cancel: () => void;
    }

    export class LoginController {
        constructor(
            private $scope: ILoginScope,
            private $modal, // ng.ui.IModalService ?
            private $location: ng.ILocationService,
            private authService: IAuthService)
        {
            if (this.authService.email != null && this.authService.token != null) {
                this.$location.path('/').replace();
            }

            this.$scope.email = this.authService.email;
            this.$scope.showModal = () => this.showModal();
        }

        private showModal() : void {
            this.$modal.open({
                templateUrl: 'http://localhost/burgerama/Scripts/app/account/views/login.html',
                controller: 'LoginModalController'
            });
        }
    }
}

Burgerama.app.controller('LoginController', ['$scope', '$modal', '$location', 'AuthService', ($scope, $modal, $location, authService) =>
    new Burgerama.Account.LoginController($scope, $modal, $location, authService)
]);
