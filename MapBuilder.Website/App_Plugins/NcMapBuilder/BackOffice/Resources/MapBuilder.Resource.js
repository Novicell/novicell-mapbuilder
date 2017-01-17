angular.module('umbraco.resources')
    .factory('mapBuilderResource', function($http) {
    ﻿   var apiPath = 'backoffice/api/MapBuilderBackOfficeApi/';
        return {
            // Maps
            getAllMaps: function() {
                return $http.get(apiPath + 'GetAllMaps');
            },
            createNewMap: function(name) {
                return $http.post(apiPath + 'CreateNewMap?name=' + name);
            },
            getMap: function(id) {
                return $http.get(apiPath + 'GetMap?id=' + id);
            },
            saveMap: function(data) {
                return $http.post(apiPath + 'SaveMap', data);
            },
            removeMap: function(id) {
                return $http.post(apiPath + 'RemoveMap?id=' + id);
            },

            // Data sources
            createNewDataSource: function (name) {
                return $http.post(apiPath + 'CreateNewDataSource?name=' + name);
            },
            getAllDataSources: function() {
                return $http.get(apiPath + 'GetAllDataSources');
            },
            getDataSource: function (id) {
                return $http.get(apiPath + 'GetDataSource?id=' + id);
            },
            saveDataSource: function (data) {
                return $http.post(apiPath + 'SaveDataSource', data);
            },
            removeDataSource: function (data) {
                return $http.post(apiPath + 'RemoveDataSource', data);
            },
            getDocumentTypes: function() {
                return $http.get(apiPath + 'GetDocumentTypes');
            },
            getDocumentTypeProperties: function(alias) {
                return $http.post(apiPath + 'GetDocumentTypeProperties?docTypeAlias=' + alias);
            },

            // Lookup address
            lookupAddress: function(address) {
                return $http.post(apiPath + 'LookUpCoords?address=' + address);
            }
        }
    });
