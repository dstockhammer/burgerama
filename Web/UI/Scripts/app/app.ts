﻿/// <reference path='../typings/angularjs/angular.d.ts' />
/// <reference path='../typings/angularjs/angular-resource.d.ts' />
/// <reference path='../typings/googlemaps/google.maps.d.ts' />

'use strict';

module Burgerama {
    export interface IModule {
        id: string;
    }

    // Create the module and define its dependencies.
    export var app: ng.IModule = angular.module('burgerama', [
        // Angular modules
        'ngResource',

        // Angular UI modules
        'ui.bootstrap'
    ]);

    export function initialize() {
        var mapOptions = {
            center: new google.maps.LatLng(-34.397, 150.644),
            overviewMapControl: false,
            scaleControl: false,
            streetViewControl: false,
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.LARGE,
                position: google.maps.ControlPosition.RIGHT_CENTER
            },
            mapTypeControl: false,
            panControl: false,
            zoom: 15
        };

        var map = new google.maps.Map(document.getElementById("map-canvas"),
            mapOptions);

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(position => {
                var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                map.setCenter(initialLocation);
                map.setZoom(mapOptions.zoom);
            });
        }

        var input = <HTMLInputElement>document.getElementById('central-search-box');
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
        var searchBox = new google.maps.places.SearchBox(input);
        var markers = [];

        // Listen for the event fired when the user selects an item from the
        // pick list. Retrieve the matching places for that item.
        google.maps.event.addListener(searchBox, 'places_changed', () => {
            var places = searchBox.getPlaces();


            for (var i = 0, marker; marker = markers[i]; i++) {
                marker.setMap(null);
            }

            // For each place, get the icon, place name, and location.
            var bounds = new google.maps.LatLngBounds();
            for (var i = 0, place; place = places[i]; i++) {
                var image = {
                    url: place.icon,
                    size: new google.maps.Size(71, 71),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(17, 34),
                    scaledSize: new google.maps.Size(25, 25)
                };

                // Create a marker for each place.
                markers.push(new google.maps.Marker({
                    map: map,
                    icon: image,
                    title: place.name,
                    position: place.geometry.location,
                    animation: google.maps.Animation.DROP
                }));

                bounds.extend(place.geometry.location);
            }

            map.fitBounds(bounds);
            map.setZoom(mapOptions.zoom);
        });

        // Bias the SearchBox results towards places that are within the bounds of the
        // current map's viewport.
        google.maps.event.addListener(map, 'bounds_changed', () => {
            var bounds = map.getBounds();
            searchBox.setBounds(bounds);
        });
    }
}

Burgerama.app.run(() => {
    Burgerama.initialize();
});
