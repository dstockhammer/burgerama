/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />
/// <reference path='../typings/googlemaps/google.maps.d.ts' />

'use strict';

module Burgerama {
    // Create the module and define its dependencies.
    export var app: ng.IModule = angular.module('burgerama', [
        // Angular modules
        'ngResource',
        // Angular UI modules
        'ui.bootstrap'
    ]);

    export var map: google.maps.Map;
    export var searchBox: google.maps.places.SearchBox;
    export var markers: Array<google.maps.Marker> =
        new Array<google.maps.Marker>();
    export var places: Array<google.maps.places.PlaceResult> =
        new Array<google.maps.places.PlaceResult>();

    export function initialize() {
        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        searchBox = createMapSearchBox(map,'map-search-box');
        setCurrentLocation(map, mapOptions);
        google.maps.event.addListener(
            searchBox, 'places_changed', () => { searchPlaces(map, searchBox, places, markers, mapOptions); });
        google.maps.event.addListener(
            map, 'bounds_changed', () => { setSearchBound(map, searchBox); });
    }
}

Burgerama.app.run(() => {
    Burgerama.initialize();
});
