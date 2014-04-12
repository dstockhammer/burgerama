var Burgerama;
(function (Burgerama) {
    function createMapSearchBox(map, inputId) {
        var input = document.getElementById(inputId);
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
        return new google.maps.places.SearchBox(input);
    }
    Burgerama.createMapSearchBox = createMapSearchBox;

    function searchPlaces(map, searchBox, places, markers, options) {
        places = searchBox.getPlaces();
        clearMarkersFromTheMap(markers);
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var marker = placeMarkerOnTheMap(map, place);
            google.maps.event.addDomListener(marker, 'click', showMarkerInfo);
            markers.push(marker);
            bounds.extend(place.geometry.location);
        }

        map.fitBounds(bounds);
        map.setZoom(options.zoom);
    }
    Burgerama.searchPlaces = searchPlaces;

    function setSearchBound(map, searchBox) {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
    }
    Burgerama.setSearchBound = setSearchBound;

    function setCurrentLocation(map, options) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                map.setCenter(initialLocation);
                map.setZoom(options.zoom);
            });
        }
    }
    Burgerama.setCurrentLocation = setCurrentLocation;

    function showMarkerInfo(marker) {
        alert(marker);
    }
    Burgerama.showMarkerInfo = showMarkerInfo;

    function clearMarkersFromTheMap(markers) {
        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }

        markers = new Array();
    }
    Burgerama.clearMarkersFromTheMap = clearMarkersFromTheMap;

    function placeMarkerOnTheMap(map, place) {
        var image = new google.maps.MarkerImage(place.icon, new google.maps.Size(71, 71), new google.maps.Point(0, 0), new google.maps.Point(17, 34), new google.maps.Size(25, 25));

        var marker = new google.maps.Marker({
            map: map,
            icon: image,
            title: place.name,
            position: place.geometry.location,
            animation: google.maps.Animation.DROP
        });

        return marker;
    }
    Burgerama.placeMarkerOnTheMap = placeMarkerOnTheMap;
})(Burgerama || (Burgerama = {}));
//# sourceMappingURL=map.functions.js.map
