angular.module('umbraco')
.controller('Novicell.EditMapController', function ($routeParams, assetsService, notificationsService, mapBuilderResource, localizationService) {
    var ctrl = this;

    // Sets up an object for binding
    ctrl.content = {};

    // Some other variables
    ctrl.oneIsCollapsed = false;
    ctrl.twoIsCollapsed = false;
    ctrl.threeIsCollapsed = false;
    ctrl.fourIsCollapsed = false;
    ctrl.fiveIsCollapsed = false;

    ctrl.zoomLevels = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21];
    ctrl.textColors = ['white', 'black'];

    // Tabs
    ctrl.content.tabs = [
        { id: 1, label: 'General' },
        { id: 2, label: 'Map' },
        { id: 3, label: 'Markers' },
        { id: 4, label: 'Clustering' },
        { id: 5, label: 'Info Window' },
        { id: 6, label: 'Controls' }
    ];

    // Gets correct language for tabs
    localizationService.localize("ncmb_maps_tabs_general").then(function (value) {
        ctrl.content.tabs[0].label = value;
    });
    localizationService.localize("ncmb_maps_tabs_map").then(function (value) {
        ctrl.content.tabs[1].label = value;
    });
    localizationService.localize("ncmb_maps_tabs_markers").then(function (value) {
        ctrl.content.tabs[2].label = value;
    });
    localizationService.localize("ncmb_maps_tabs_clustering").then(function (value) {
        ctrl.content.tabs[3].label = value;
    });
    localizationService.localize("ncmb_maps_tabs_infowindows").then(function (value) {
        ctrl.content.tabs[4].label = value;
    });
    localizationService.localize("ncmb_maps_tabs_controls").then(function (value) {
        ctrl.content.tabs[5].label = value;
    });

    // Gets selected map
    var getMapData = function () {
        mapBuilderResource.getMap($routeParams.id.substr(4)).then(function (response) {
            if (response.data.Success) {
                ctrl.content.map = response.data.Data;
                if (ctrl.content.map.DataId === -1) {
                    ctrl.content.map.DataId = null;
                }
                ctrl.content.clusterStyle = JSON.parse(response.data.Data.ClusterStyle);
                ctrl.content.defaultIconStyle = JSON.parse(response.data.Data.DefaultIconStyle);
                if (response.data.Data.Style.length) {
                    ctrl.content.mapStyle = JSON.stringify(JSON.parse(response.data.Data.Style), undefined, 2);
                } else {
                    ctrl.content.mapStyle = response.data.Data.Style;
                }
            } else {
                console.log(response.data.ErrorMessage);
            }
        });
    }
    getMapData();

    // Gets possible data sources (id, name)
    mapBuilderResource.getAllDataSources().then(function (response) {
        ctrl.content.dataSources = response.data;
    });

    // Saves
    ctrl.save = function (form) {
        form.$submitted = true;

        var map = ctrl.content.map;
        map.ClusterStyle = JSON.stringify(ctrl.content.clusterStyle);
        map.DefaultIconStyle = JSON.stringify(ctrl.content.defaultIconStyle);
        map.Style = JSON.parse(JSON.stringify(ctrl.content.mapStyle));

        if (form.$valid) {
            mapBuilderResource.saveMap(map).then(function (response) {
                if (response.data.Success) {
                    form.$dirty = false;
                    notificationsService.success('Success', 'Map settings updated successfully.');
                } else {
                    notificationsService.error('Error', 'There was an error updating the map settings: ' + response.data.ErrorMessage + '.');
                }
            });
        } else {
            notificationsService.info('Info', 'You have not filled out all required fields.');
        }

    }
});
