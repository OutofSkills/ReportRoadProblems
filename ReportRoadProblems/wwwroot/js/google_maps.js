// Initialize and add the map
// In the following example, markers appear when the user clicks on the map.
// The markers are stored in an array.
// The user can then click an option to hide, show or delete the markers.
let map;
let markers = [];

function initMap() {
    const haightAshbury = { lat: 44.319305, lng: 23.800678 };
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 13,
        center: haightAshbury,
        mapTypeId: "roadmap",
    });
    //Address decoder
    const geocoder = new google.maps.Geocoder();
    // This event listener will call addMarker() when the map is clicked.
    map.addListener("click", (event) => {
        clearMarkers();
        addMarker(event.latLng);
        document.getElementById("lat").value = event.latLng.lat();
        document.getElementById("lng").value = event.latLng.lng();
        getAddress(geocoder, event.latLng);
    });
    // Adds a marker at the center of the map.
    addMarker(haightAshbury);
}

// Adds a marker to the map and push to the array.
function addMarker(location) {
    const marker = new google.maps.Marker({
        position: location,
        map: map,
    });
    markers.push(marker);
}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}

//Gets the address from the geolocation latlng wich are the coordinates of the marked location
function getAddress(geocoder, latlng) {
    geocoder
        .geocode({ location: latlng })
        .then((response) => {
            if (response.results[0]) {
                document.getElementById("address").value = response.results[0].formatted_address;
            } else {
                alert("No results found");
            }
        })
        .catch((e) => alert("Geocoder failed due to: " + e));
}