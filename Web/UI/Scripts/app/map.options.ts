module Burgerama {
    export var mapOptions = {
        center: new google.maps.LatLng(51.51, -0.11),
        overviewMapControl: false,
        scaleControl: false,
        streetViewControl: false,
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.LARGE,
            position: google.maps.ControlPosition.LEFT_CENTER
        },
        mapTypeControl: false,
        panControl: false,
        zoom: 15,
        styles: [{
            stylers: [
                { hue: "#ffa200" },
                { saturation: 20 },
                { lightness: 10 },
                { gamma: 0.75 }
            ]
        },
            {
                featureType: "water",
                stylers: [
                    { hue: "#0044ff" },
                    { lightness: 40 }
                ]
            }
        ]
    };
}
