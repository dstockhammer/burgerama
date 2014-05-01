// <reference path="../../app.ts" />
// <reference path="../../../typings/googlemaps/google.maps.d.ts" />

module Burgerama.Map {

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // todo: move google maps into directives and controllers (-> the angular way)
    // or maybe better, use http://angular-google-maps.org/.
    //
    export var map: google.maps.Map;
    export var searchBox: google.maps.places.SearchBox;
    export var markers: Array<google.maps.Marker> =
        new Array<google.maps.Marker>();
    export var places: Array<google.maps.places.PlaceResult> =
        new Array<google.maps.places.PlaceResult>();

    export function initialize() {
        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        searchBox = createMapSearchBox(map, 'map-search-box');
        setCurrentLocation(map, mapOptions);
        google.maps.event.addListener(
            searchBox, 'places_changed', () => { searchPlaces(map, searchBox, places, markers, mapOptions); });
        google.maps.event.addListener(
            map, 'bounds_changed', () => { setSearchBound(map, searchBox); });

        // This is ugly as hell, must be translated to proper angular solution
        var logoContainer = document.getElementById('logo-container');
        logoContainer.className = 'hide-logo';
        window.setTimeout(() => { logoContainer.parentNode.removeChild(logoContainer); }, 9000);
    }
    //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    export class MapController {
        constructor() {
            initialize();
        }
    }
}

Burgerama.app.controller('MapController', () =>
    new Burgerama.Map.MapController()
);
 