// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Map) {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // todo: move google maps into directives and controllers (-> the angular way)
        // or maybe better, use http://angular-google-maps.org/.
        //
        Map.map;
        Map.searchBox;
        Map.markers = new Array();
        Map.places = new Array();

        function initialize() {
            Map.map = new google.maps.Map(document.getElementById('map-canvas'), Map.mapOptions);
            Map.searchBox = Map.createMapSearchBox(Map.map, 'map-search-box');
            Map.setCurrentLocation(Map.map, Map.mapOptions);
            google.maps.event.addListener(Map.searchBox, 'places_changed', function () {
                Map.searchPlaces(Map.map, Map.searchBox, Map.places, Map.markers, Map.mapOptions);
            });
            google.maps.event.addListener(Map.map, 'bounds_changed', function () {
                Map.setSearchBound(Map.map, Map.searchBox);
            });
        }
        Map.initialize = initialize;

        //
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        var MapController = (function () {
            function MapController() {
                initialize();
            }
            return MapController;
        })();
        Map.MapController = MapController;
    })(Burgerama.Map || (Burgerama.Map = {}));
    var Map = Burgerama.Map;
})(Burgerama || (Burgerama = {}));

Burgerama.app.controller('MapController', function () {
    return new Burgerama.Map.MapController();
});
//# sourceMappingURL=mapController.js.map
