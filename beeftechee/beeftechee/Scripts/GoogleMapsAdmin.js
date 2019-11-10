$(document).ready(function () {

    //$(window).load(function () {
        var geocoder;
        var map;
        function initialize() {
            geocoder = new google.maps.Geocoder();
            var latlng = new google.maps.LatLng(37.9838096, 23.727538800000048);
            var mapOptions = {
                zoom: 12,
                center: latlng
            }
            map = new google.maps.Map(document.getElementById('map'), mapOptions);
        }

        function codeAddress(myaddress, city) {
            var address = myaddress + " " + city;
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == 'OK') {
                    map.setCenter(results[0].geometry.location);
                    var marker = new google.maps.Marker({
                        map: map,
                        position: results[0].geometry.location
                    });
                }
            });
        }

        $.ajax({ url: "/api/OrderApi" }).done(function (results) {
            initialize();
            for (var i = 0; i < results.length; i++) {
                codeAddress(results[i].Address, results[i].City);
            }

        });
    //});
    
});

