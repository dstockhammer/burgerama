/// <reference path="../../app.ts" />
/// <reference path='../../../typings/angular-local-storage/angular-local-storage.d.ts' />
/// <reference path='../../../typings/auth0/auth0.d.ts' />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var AuthService = (function () {
            function AuthService($rootScope, toaster, auth, localStorageService) {
                this.$rootScope = $rootScope;
                this.toaster = toaster;
                this.auth = auth;
                this.localStorageService = localStorageService;
            }
            AuthService.prototype.isAuthenticated = function () {
                return this.getUser() != null;
            };

            AuthService.prototype.getUser = function () {
                return this.localStorageService.get('user');
            };

            AuthService.prototype.getToken = function () {
                return this.localStorageService.get('token');
            };

            AuthService.prototype.signIn = function () {
                var _this = this;
                this.auth.signin({ popup: true }).then(function () {
                    return _this.signInSuccess();
                }, function () {
                    return _this.signInError();
                });
            };

            AuthService.prototype.signOut = function () {
                this.auth.signout();
                this.signOutSuccess();
            };

            AuthService.prototype.signOutSuccess = function () {
                this.localStorageService.remove('user');
                this.localStorageService.remove('token');

                this.$rootScope.$emit('UserSignedOut');
                this.toaster.pop('success', 'Success', 'You signed out. Bye!');
            };

            AuthService.prototype.signInSuccess = function () {
                this.localStorageService.set('user', this.auth.profile);
                this.localStorageService.set('token', this.auth.idToken);

                this.$rootScope.$emit('UserSignedIn');
                this.toaster.pop('success', 'Success', 'Signed in as ' + this.getUser().email + '.');
            };

            AuthService.prototype.signInError = function () {
                this.toaster.pop('error', 'Error', 'An error occurred :(');
            };
            return AuthService;
        })();
        Account.AuthService = AuthService;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.service('AuthService', [
    '$rootScope', 'toaster', 'auth', 'localStorageService', function ($rootScope, toaster, auth, localStorageService) {
        return new Burgerama.Account.AuthService($rootScope, toaster, auth, localStorageService);
    }
]);
//# sourceMappingURL=authService.js.map
