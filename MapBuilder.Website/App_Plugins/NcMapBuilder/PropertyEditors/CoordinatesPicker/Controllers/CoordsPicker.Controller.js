angular.module("umbraco")
    .controller("Novicell.CoordsPickerController", function ($scope, assetsService, mapBuilderResource) {
        $scope.searchField = "";
        var marker = null;
        var map, latLng;

        assetsService.loadJs('http://www.google.com/jsapi')
            .then(function () {
                google.load("maps", "3", {
                    callback: initMap,
                    other_params: "sensor=false"
                });
            });

        function initMap() {
            //Google maps is available and all components are ready to use.
            if ($scope.model.value === "") {
                $scope.model.value = "56.1073797,10.151832600000034";
            }

            var valueArray = $scope.model.value.split(",");
            latLng = new google.maps.LatLng(valueArray[0], valueArray[1]);
            var mapDiv = document.getElementById($scope.model.alias + '_map');
            var mapOptions = {
                zoom: 15,
                center: latLng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }

            map = new google.maps.Map(mapDiv, mapOptions);

            marker = new google.maps.Marker({
                map: map,
                position: latLng,
                draggable: true
            });

            google.maps.event.addListener(marker, "dragend", function (e) {
                var newLat = marker.getPosition().lat();
                var newLng = marker.getPosition().lng();

                $scope.model.value = newLat + "," + newLng;
                map.panTo(marker.position);

                $('a[data-toggle="tab"]').on('shown', function (e) {
                    google.maps.event.trigger(map, 'resize');
                });
            });

            $(window).resize(function() {
                google.maps.event.trigger(map, "resize");
                map.setCenter(latLng);
            });
        }

        $scope.resetMap = function() {
            google.maps.event.trigger(map, "resize");
            map.setCenter(latLng);
        }

        $scope.lookupAddress = function () {
            mapBuilderResource.lookupAddress($scope.searchField).then(function (response) {
                if (response.data.length !== 0) {
                    var latLng = new google.maps.LatLng(response.data[0], response.data[1]);
                    marker.setPosition(latLng);
                    map.panTo(marker.position);

                    var newLat = marker.getPosition().lat();
                    var newLng = marker.getPosition().lng();

                    $scope.model.value = newLat + "," + newLng;
                }
            });
        }
    });
