/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Account) {
        var AuthService = (function () {
            function AuthService($rootScope, toaster, auth, authEvents, localStorageService) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.toaster = toaster;
                this.auth = auth;
                this.authEvents = authEvents;
                this.localStorageService = localStorageService;
                var unregistersignOutSuccess = $rootScope.$on(this.authEvents.logout, function () {
                    return _this.signOutSuccess();
                });
                var unregisterSignInSuccess = $rootScope.$on(this.authEvents.loginSuccess, function () {
                    return _this.signInSuccess();
                });
                var unregisterSignInError = $rootScope.$on(this.authEvents.loginFailed, function () {
                    _this.toaster.pop('error', 'Error', 'Authentication failed.');
                });

                this.$rootScope.$on('$destroy', function () {
                    unregistersignOutSuccess();
                    unregisterSignInSuccess();
                    unregisterSignInError();
                });
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
                this.auth.signin();
            };

            AuthService.prototype.signOut = function () {
                this.auth.signout();
            };

            AuthService.prototype.signOutSuccess = function () {
                this.localStorageService.remove('user');
                this.localStorageService.remove('token');

                this.$rootScope.$emit('UserSignedOut');
                this.toaster.pop('success', 'Success', 'You signed out. Bye!');
            };

            AuthService.prototype.signInSuccess = function () {
                this.localStorageService.add('user', this.auth.profile);
                this.localStorageService.add('token', this.auth.idToken);

                this.$rootScope.$emit('UserSignedIn');
                this.toaster.pop('success', 'Success', 'Signed in as ' + this.getUser().email + '.');
            };
            return AuthService;
        })();
        Account.AuthService = AuthService;
    })(Burgerama.Account || (Burgerama.Account = {}));
    var Account = Burgerama.Account;
})(Burgerama || (Burgerama = {}));

Burgerama.app.service('AuthService', [
    '$rootScope', 'toaster', 'auth', 'AUTH_EVENTS', 'localStorageService', function ($rootScope, toaster, auth, authEvents, localStorageService) {
        return new Burgerama.Account.AuthService($rootScope, toaster, auth, authEvents, localStorageService);
    }
]);
//# sourceMappingURL=authService.js.map
