/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />
/// <reference path='../typings/googlemaps/google.maps.d.ts' />
'use strict';
var Burgerama;
(function (Burgerama) {
    // Create the module and define its dependencies.
    Burgerama.app = angular.module('burgerama', [
        'ngResource',
        'ui.bootstrap'
    ]);

    Burgerama.map;
    Burgerama.searchBox;
    Burgerama.markers = new Array();
    Burgerama.places = new Array();

    function initialize() {
        Burgerama.map = new google.maps.Map(document.getElementById('map-canvas'), Burgerama.mapOptions);
        Burgerama.searchBox = Burgerama.createMapSearchBox(Burgerama.map, 'map-search-box');
        Burgerama.setCurrentLocation(Burgerama.map, Burgerama.mapOptions);
        google.maps.event.addListener(Burgerama.searchBox, 'places_changed', function () {
            Burgerama.searchPlaces(Burgerama.map, Burgerama.searchBox, Burgerama.places, Burgerama.markers, Burgerama.mapOptions);
        });
        google.maps.event.addListener(Burgerama.map, 'bounds_changed', function () {
            Burgerama.setSearchBound(Burgerama.map, Burgerama.searchBox);
        });
    }
    Burgerama.initialize = initialize;
})(Burgerama || (Burgerama = {}));

Burgerama.app.run(function () {
    Burgerama.initialize();
});
//# sourceMappingURL=app.js.map
