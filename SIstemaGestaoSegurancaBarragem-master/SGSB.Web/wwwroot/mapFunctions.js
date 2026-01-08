// wwwroot/mapFunctions.js

function initGoogleMap(divId) {
    var map = new google.maps.Map(document.getElementById(divId), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 8
    });
    return map;
}

function loadKml(map, kmlPath) {
    var kmlLayer = new google.maps.KmlLayer({
        url: kmlPath,
        map: map
    });
}
