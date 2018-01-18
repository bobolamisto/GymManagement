﻿$(document).ready(function () {
    $(".clickable").click(function () {
        var id = Number(this.id);
        var building;
        for (var i = 0; i < buildings.length; i++)
            if (buildings[i].Id === id)
            {
                building = buildings[i];
                building.Street = building.Street + " " + building.StreetNumber;
                break;
            }
        window.latitude = building.Latitude;
        window.longitude = building.Longitude;
        window.street = building.Street;
        $("#map").empty();
        initMap();
    });
});

function initMap() {
    var loc = { lat: window.latitude, lng: window.longitude };
    window.map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: loc,
        title: window.street,
    });

    var me = new google.maps.Marker({
        position: loc,
        map: window.map,
        animation: google.maps.Animation.BOUNCE,
    });

}