/// <reference path="../../app.ts" />

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
            private authEvents,
            private localStorageService)
        {
            var unregistersignOutSuccess = $rootScope.$on(this.authEvents.logout, () => this.signOutSuccess());
            var unregisterSignInSuccess = $rootScope.$on(this.authEvents.loginSuccess, () => this.signInSuccess());
            var unregisterSignInError = $rootScope.$on(this.authEvents.loginFailed, () => {
                this.toaster.pop('error', 'Error', 'Authentication failed.');
            });

            this.$rootScope.$on('$destroy', () => {
                unregistersignOutSuccess();
                unregisterSignInSuccess();
                unregisterSignInError();
            });
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
            this.auth.signin();
        }

        public signOut(): void {
            this.auth.signout();
        }

        private signOutSuccess(): void {
            this.localStorageService.remove('user');
            this.localStorageService.remove('token');

            this.$rootScope.$broadcast('SignOut');
            this.toaster.pop('success', 'Success', 'You signed out. Bye!');
        }

        private signInSuccess(): void {
            this.localStorageService.add('user', this.auth.profile);
            this.localStorageService.add('token', this.auth.idToken);

            this.$rootScope.$broadcast('SignIn');
            this.toaster.pop('success', 'Success', 'Signed in as ' + this.getUser().email + '.');
        }
    }
}

Burgerama.app.service('AuthService', ['$rootScope',  'toaster', 'auth', 'AUTH_EVENTS', 'localStorageService', ($rootScope, toaster, auth, authEvents, localStorageService) =>
    new Burgerama.Account.AuthService($rootScope, toaster, auth, authEvents, localStorageService)
]);
