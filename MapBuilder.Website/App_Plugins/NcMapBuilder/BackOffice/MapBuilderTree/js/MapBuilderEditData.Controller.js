angular.module('umbraco')
    .controller('Novicell.EditDataController', function ($scope, $routeParams, notificationsService, mapBuilderResource, navigationService) {
        var ctrl = this;
        ctrl.content = {};
        ctrl.data = {};
        ctrl.docTypes = [];
        ctrl.docTypeProps = [];
        ctrl.style = {};

        ctrl.content.tabs = [
            { id: 1, label: 'General' }
        ];

        mapBuilderResource.getDataSource($routeParams.id.substr(5)).then(function (response) {
            ctrl.data = response.data;

            // Gets document type properties for already selected doc type
            mapBuilderResource.getDocumentTypeProperties(ctrl.data.DocAlias).then(function (response) {
                ctrl.docTypeProps = response.data;
            });

            // Gets doc types
            mapBuilderResource.getDocumentTypes().then(function (response) {
                ctrl.docTypes = response.data;
            });
        });

        ctrl.updateProps = function () {
            if (ctrl.data.DocAlias !== undefined) {
                // Gets document type properties selected doc type
                mapBuilderResource.getDocumentTypeProperties(ctrl.data.DocAlias).then(function (response) {
                    ctrl.docTypeProps = response.data;
                });
            }
        }

        ctrl.save = function (form) {
            form.$submitted = true;

            if (form.$valid) {
                mapBuilderResource.saveDataSource(ctrl.data)
                    .then(function (response) {
                        if (response.data.Success) {
                            form.$dirty = false;
                            notificationsService.success('Success', 'Update data source successful.');
                        } else {
                            notificationsService.error('Error',
                                'There was an error updating the data source: ' + response.data.ErrorMessage + '.');
                        }
                    });
            } else {
                notificationsService.info('Info', 'You have not filled out all required fields.');
            }
        }

        console.log($routeParams);

        navigationService.syncTree({ tree: 'MapBuilderTree', path: ["-1", "Data", $routeParams.id] });
    });
