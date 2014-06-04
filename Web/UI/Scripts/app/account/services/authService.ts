/// <reference path="../../app.ts" />
/// <reference path='../../../typings/angular-local-storage/angular-local-storage.d.ts' />
/// <reference path='../../../typings/auth0/auth0.d.ts' />

module Burgerama.Account {
    export interface IAuthService {
        isAuthenticated: () => boolean;
        getUser(): () => any;
        getToken(): () => string
        signIn: () => void;
        signOut: () => void;
    }

    export class AuthService implements IAuthService {
        constructor(
            private $rootScope: IBurgeramaScope,
            private toaster,
            private auth,
            private localStorageService: ng.localStorage.ILocalStorageService)
        {
        }

        public isAuthenticated(): boolean {
            return this.getUser() != null;
        }

        public getUser() {
            return this.localStorageService.get('user');
        }

        public getToken() {
            return this.localStorageService.get('token');
        }

        public signIn(): void {
            this.auth.signin({ popup: true })
                .then(() => this.signInSuccess(), () => this.signInError());
        }

        public signOut(): void {
            this.auth.signout();
            this.signOutSuccess();
        }

        private signOutSuccess(): void {
            this.localStorageService.remove('user');
            this.localStorageService.remove('token');

            this.$rootScope.$emit('UserSignedOut');
            this.toaster.pop('success', 'Success', 'You signed out. Bye!');
        }

        private signInSuccess(): void {
            this.localStorageService.set('user', this.auth.profile);
            this.localStorageService.set('token', this.auth.idToken);

            this.$rootScope.$emit('UserSignedIn');
            this.toaster.pop('success', 'Success', 'Signed in as ' + this.getUser().email + '.');
        }

        private signInError(): void {
            this.toaster.pop('error', 'Error', 'An error occurred :(');
        }
    }
}

Burgerama.app.service('AuthService', ['$rootScope', 'toaster', 'auth', 'localStorageService', ($rootScope, toaster, auth, localStorageService) =>
    new Burgerama.Account.AuthService($rootScope, toaster, auth, localStorageService)
]);
