'use strict';

/*
 * Generator for NcMapBuilder
 * Author(s):    Mark Arndt Lønquist - MLO
 *               mlo@novicell.dk
 */

var ncmapbuilder = ncmapbuilder || {}
var google = google || {}

ncmapbuilder.generator = ncmapbuilder.generator || function () {
    var mapAlias = '';
    var mapsModel = {};
    var isLoaded = false;
    var minCoordinates = [100, 100];
    var maxCoordinates = [0, 0];
    var maxDistance = 6.0;

    function init(alias, nodes, titleProperty, coordsProperty) {
        mapAlias = alias;
        var nodeIds = JSON.parse(nodes);

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                var response = JSON.parse(xhr.response);
                if (response.Success) {
                    mapsModel = response.Data;
                    initGoogleMapsScript();
                } else {
                    console.log(response.ErrorMessage);
                }
            }
        };
        xhr.open('POST', '/umbraco/api/MapBuilderApi/getmapmodel', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.send(JSON.stringify(
            {
                Alias: mapAlias,
                NodeIds: nodeIds,
                TitleProperty: titleProperty,
                CoordsProperty: coordsProperty
            }));
    }

    function initGoogleMapsScript() {
        var element = document.getElementById('map-container');

        var mapsUrl = 'https://maps.googleapis.com/maps/api/js?callback=ncmapbuilder.generator.loadMap';

        if (mapsModel.ApiKey != null && mapsModel.ApiKey.length) {
            mapsUrl = 'https://maps.googleapis.com/maps/api/js?key=' + mapsModel.ApiKey + '&callback=ncmapbuilder.generator.loadMap';
        }

        // If the map is in view on load, load the map,
        // else we setup an onscroll to load it, once it gets into view.
        if (!isLoaded && element && isScrolledIntoView(element)) {
            isLoaded = true;
            // Async load the GMaps API and run "loadMap"
            var script = document.createElement('script');
            script.type = 'text/javascript';
            script.src = mapsUrl;
            document.body.appendChild(script);
        } else {
            document.onscroll = function () {
                if (!isLoaded && element && isScrolledIntoView(element)) {
                    isLoaded = true;
                    // Async load the GMaps API and run "loadMap"
                    var script = document.createElement('script');
                    script.type = 'text/javascript';
                    script.src = mapsUrl;
                    document.body.appendChild(script);
                }
            }
        }
    }

    function loadMap() {
        var element = document.getElementById('map-container');

        var centerCoordinates = new google.maps.LatLng(mapsModel.Center[0], mapsModel.Center[1]);

        var mapOptions = {
            center: centerCoordinates,
            zoom: mapsModel.InitialZoom,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: mapsModel.Style,
            draggable: mapsModel.Draggable,
            panControl: mapsModel.Draggable,
            scrollwheel: mapsModel.ScrollWheelEnabled,
            mapTypeControl: mapsModel.UseMapTypeControl,
            streetViewControl: mapsModel.UseStreetViewControl,
            zoomControl: mapsModel.UseZoomControl
        }

        if (mapsModel.UseHybridMap) {
            mapOptions.mapTypeId = google.maps.MapTypeId.HYBRID;
        }

        var map = new google.maps.Map(element, mapOptions);

        var infowindow = new google.maps.InfoWindow({
            maxWidth: mapsModel.InfoWindowWidth
        });
        var marker;
        var markers = [];

        var icon = {
            url: mapsModel.DefaultIconStyleModel.Url,
            scaledSize: new google.maps.Size(mapsModel.DefaultIconStyleModel.Width, mapsModel.DefaultIconStyleModel.Height)
        }

        mapsModel.Nodes.forEach(function (node) {

            if (node.Coordinates[0] < minCoordinates[0]) {
                minCoordinates = node.Coordinates;
            }

            if (node.Coordinates[0] > maxCoordinates[0]) {
                maxCoordinates = node.Coordinates;
            }

            var markerOptions = {
                position: new google.maps.LatLng(node.Coordinates[0], node.Coordinates[1]),
                title: node.Title,
                icon: icon,
                map: map
            }

            marker = new google.maps.Marker(markerOptions);

            markers.push(addMarker(marker, node.InfoWindowContent, infowindow));
        });

        if (mapsModel.UseClustering) {
            var clusterStyleOptions = mapsModel.ClusterStyle;

            var clusterOptions = {
                styles: [
                    {
                        anchor: [clusterStyleOptions[0].Anchor[0], clusterStyleOptions[0].Anchor[1]],
                        textColor: clusterStyleOptions[0].TextColor,
                        url: clusterStyleOptions[0].Url,
                        width: parseInt(clusterStyleOptions[0].Width),
                        height: parseInt(clusterStyleOptions[0].Height)
                    },
                    {
                        anchor: [parseInt(clusterStyleOptions[1].Anchor[0]), parseInt(clusterStyleOptions[1].Anchor[1])],
                        textColor: clusterStyleOptions[1].TextColor,
                        url: clusterStyleOptions[1].Url,
                        width: parseInt(clusterStyleOptions[1].Width),
                        height: parseInt(clusterStyleOptions[1].Height)
                    },
                    {
                        anchor: [parseInt(clusterStyleOptions[2].Anchor[0]), parseInt(clusterStyleOptions[2].Anchor[1])],
                        textColor: clusterStyleOptions[2].TextColor,
                        url: clusterStyleOptions[2].Url,
                        width: parseInt(clusterStyleOptions[2].Width),
                        height: parseInt(clusterStyleOptions[2].Height)
                    },
                    {
                        anchor: [parseInt(clusterStyleOptions[3].Anchor[0]), parseInt(clusterStyleOptions[3].Anchor[1])],
                        textColor: clusterStyleOptions[3].TextColor,
                        url: clusterStyleOptions[3].Url,
                        width: parseInt(clusterStyleOptions[3].Width),
                        height: parseInt(clusterStyleOptions[3].Height)
                    },
                    {
                        anchor: [parseInt(clusterStyleOptions[4].Anchor[0]), parseInt(clusterStyleOptions[4].Anchor[1])],
                        textColor: clusterStyleOptions[4].TextColor,
                        url: clusterStyleOptions[4].Url,
                        width: parseInt(clusterStyleOptions[4].Width),
                        height: parseInt(clusterStyleOptions[4].Height)
                    }
                ]
            };

            var markerCluster = new MarkerClusterer(map, markers, clusterOptions);
        }

        if (mapsModel.UseGeoLocation) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

                    map.setZoom(12);
                    map.setCenter(pos);
                });
            } else {
                console.log('GeoLocation not available');
            }
        }

        var minZoom = mapsModel.MinZoom;
        var maxZoom = mapsModel.MaxZoom;

        google.maps.event.addListener(map, "zoom_changed", function () {
            if (map.getZoom() < parseInt(minZoom)) map.setZoom(parseInt(minZoom));
            if (map.getZoom() > parseInt(maxZoom)) map.setZoom(parseInt(maxZoom));
        });

        google.maps.event.addDomListener(window, "resize", function () {
            var center = map.getCenter();
            google.maps.event.trigger(map, "resize");
            map.setCenter(center);
        });

        // Calculate center from positions  
        var diff = [
            (maxCoordinates[0] - minCoordinates[0]) / 2,
            (maxCoordinates[1] - minCoordinates[1]) / 2
        ];

        if (diff[0] < 0) {
            diff[0] *= -1;
        }

        if (diff[1] < 0) {
            diff[1] *= -1;
        }

        if (diff[0] < maxDistance && diff[1] < maxDistance) {

            var lat, lng;

            if (minCoordinates[0] > maxCoordinates[0]) {
                lat = maxCoordinates[0] + diff[0]
            } else {
                lat = minCoordinates[0] + diff[0]
            }

            if (minCoordinates[1] > maxCoordinates[1]) {
                lng = maxCoordinates[1] + diff[1]
            } else {
                lng = minCoordinates[1] + diff[1]
            }

            var center = {
                lat: lat,
                lng: lng
            };

            map.setCenter(center);
        }

    }

    // Helper functions
    function addMarker(marker, content, infowindow) {
        if (mapsModel.UseInfoWindows) {
            marker.addListener('click', function () {
                infowindow.setContent(content);
                infowindow.open(marker.get('map'), marker);
            });
        }

        return marker;
    }

    function isScrolledIntoView(el) {
        var elemTop = el.getBoundingClientRect().top;
        var elemBottom = el.getBoundingClientRect().bottom;

        return (elemBottom >= 0) && (elemTop <= window.innerHeight);
    }

    return {
        init: init,
        loadMap: loadMap
    }
}();