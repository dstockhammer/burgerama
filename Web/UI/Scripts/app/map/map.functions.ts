module Burgerama.Map {
    export function createMapSearchBox(
        map: google.maps.Map,
        inputId: string)
    {
        var input = <HTMLInputElement>document.getElementById(inputId);
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
        return new google.maps.places.SearchBox(input);
    }

    export function searchPlaces(
        map: google.maps.Map,
        searchBox: google.maps.places.SearchBox,
        places: Array<google.maps.places.PlaceResult>,
        markers: Array<google.maps.Marker>,
        options: google.maps.MapOptions)
    {
        places = searchBox.getPlaces();
        clearMarkersFromTheMap(markers);
        var bounds = new google.maps.LatLngBounds();
        places.forEach((place) => {
            var marker = placeMarkerOnTheMap(map, place);
            google.maps.event.addDomListener(marker, 'click', () => { showMarkerInfo(marker, place); });
            markers.push(marker);
            bounds.extend(place.geometry.location);
        });

        map.fitBounds(bounds);
        map.setZoom(options.zoom);
    }

    export function setSearchBound(
        map: google.maps.Map,
        searchBox: google.maps.places.SearchBox)
    {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
    }

    export function setCurrentLocation(
        map: google.maps.Map,
        options: google.maps.MapOptions)
    {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(position => {
                var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                map.setCenter(initialLocation);
                map.setZoom(options.zoom);
            });
        }
    }

    export function showMarkerInfo(
        marker: google.maps.Marker,
        place: google.maps.places.PlaceResult)
    {
        alert(place.name);
    }

    export function clearMarkersFromTheMap(
        markers: Array<google.maps.Marker>)
    {
        markers.forEach((marker) => {
            marker.setMap(null);
        });

        markers = new Array<google.maps.Marker>();
    }

    export function placeMarkerOnTheMap(
        map: google.maps.Map,
        place: google.maps.places.PlaceResult)
    {
        var image = new google.maps.MarkerImage (
            place.icon,
            new google.maps.Size(71, 71),
            new google.maps.Point(0, 0),
            new google.maps.Point(17, 34),
            new google.maps.Size(25, 25)
        );

        
        var marker = new google.maps.Marker({
            map: map,
            icon: image,
            title: place.name,
            position: place.geometry.location,
            animation: google.maps.Animation.DROP
        });

        return marker;
    }
}