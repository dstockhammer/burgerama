/// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Venues) {
        var Venue = (function () {
            function Venue() {
            }
            return Venue;
        })();
        Venues.Venue = Venue;

        var Location = (function () {
            function Location() {
            }
            return Location;
        })();
        Venues.Location = Location;
    })(Burgerama.Venues || (Burgerama.Venues = {}));
    var Venues = Burgerama.Venues;
})(Burgerama || (Burgerama = {}));

Burgerama.app.factory('VenueResource', [
    '$resource', function ($resource) {
        return $resource(Burgerama.Util.getApiUrl('venues') + '/:id', {
            id: '@id'
        }, {
            all: { method: 'GET', isArray: true },
            get: { method: 'GET' },
            create: { method: 'POST' }
        });
    }]);
//# sourceMappingURL=venueResource.js.map
