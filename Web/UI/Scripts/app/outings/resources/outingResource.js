/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Outings) {
        var Outing = (function () {
            function Outing() {
            }
            return Outing;
        })();
        Outings.Outing = Outing;
    })(Burgerama.Outings || (Burgerama.Outings = {}));
    var Outings = Burgerama.Outings;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory('OutingResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('outings') + '/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true }
        });
    }]);
//# sourceMappingURL=outingResource.js.map
