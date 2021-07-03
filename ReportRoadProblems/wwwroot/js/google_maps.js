// Initialize and add the map
// In the following example, markers appear when the user clicks on the map.
// The markers are stored in an array.
// The user can then click an option to hide, show or delete the markers.
let map, infoWindow;
let markers = [];

function initMap() {
    const Craiova = { lat: 44.319305, lng: 23.800678 };
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 13,
        center: Craiova,
        mapTypeId: "roadmap",
    });
    //Address decoder
    const geocoder = new google.maps.Geocoder();
    //Create a button for current location and center it to the top of the map
    infoWindow = new google.maps.InfoWindow();
    const locationButton = document.createElement("button");
    locationButton.textContent = "Current Location";
    locationButton.classList.add("custom-map-control-button");
    locationButton.type = "button";
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);

    locationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };
                    infoWindow.setPosition(pos);
                    infoWindow.setContent("Location found.");
                    infoWindow.open(map);
                    map.setCenter(pos);

                    document.getElementById("lat").value = pos.lat;
                    document.getElementById("lng").value = pos.lng;
                    getAddress(geocoder, pos);
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    });
    // This event listener will call addMarker() when the map is clicked.
    map.addListener("click", (event) => {
        clearMarkers();
        addMarker(event.latLng);
        document.getElementById("lat").value = event.latLng.lat();
        document.getElementById("lng").value = event.latLng.lng();
        getAddress(geocoder, event.latLng);
    });
    // Adds a marker at the center of the map.
    addMarker(Craiova);
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
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
                document.getElementById("addr").value = response.results[0].formatted_address;
            } else {
                alert("No results found");
            }
        })
        .catch((e) => alert("Geocoder failed due to: " + e));
}

//handle geolocation error
function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}
