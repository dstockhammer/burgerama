var Burgerama;
(function (Burgerama) {
    (function (Map) {
        function createMapSearchBox(map, inputId) {
            var input = document.getElementById(inputId);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            return new google.maps.places.SearchBox(input);
        }
        Map.createMapSearchBox = createMapSearchBox;

        function searchPlaces(map, searchBox, places, markers, options) {
            places = searchBox.getPlaces();
            clearMarkersFromTheMap(markers);
            var bounds = new google.maps.LatLngBounds();
            places.forEach(function (place) {
                var marker = placeMarkerOnTheMap(map, place);
                google.maps.event.addDomListener(marker, 'click', function () {
                    showMarkerInfo(marker, place);
                });
                markers.push(marker);
                bounds.extend(place.geometry.location);
            });

            map.fitBounds(bounds);
            map.setZoom(options.zoom);
        }
        Map.searchPlaces = searchPlaces;

        function setSearchBound(map, searchBox) {
            var bounds = map.getBounds();
            searchBox.setBounds(bounds);
        }
        Map.setSearchBound = setSearchBound;

        function setCurrentLocation(map, options) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    map.setCenter(initialLocation);
                    map.setZoom(options.zoom);
                });
            }
        }
        Map.setCurrentLocation = setCurrentLocation;

        // refactor this to use angular
        function showMarkerInfo(marker, place) {
            var infoBox = document.getElementById('information-box');
            var innerHTML = '<span><b>';
            innerHTML += place.url ? '<a href = "' + place.url + '" target = "new">' : '<a>';
            innerHTML += place.name + '</a></b> | ' + place.formatted_address + ' | </span>';
            innerHTML += '<a>Add Venue</a>';
            infoBox.innerHTML = innerHTML;
        }
        Map.showMarkerInfo = showMarkerInfo;

        function clearMarkersFromTheMap(markers) {
            markers.forEach(function (marker) {
                marker.setMap(null);
            });

            markers = new Array();
        }
        Map.clearMarkersFromTheMap = clearMarkersFromTheMap;

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
        Map.placeMarkerOnTheMap = placeMarkerOnTheMap;
    })(Burgerama.Map || (Burgerama.Map = {}));
    var Map = Burgerama.Map;
})(Burgerama || (Burgerama = {}));
//# sourceMappingURL=map.functions.js.map
