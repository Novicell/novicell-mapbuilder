angular.module('umbraco')
    .controller('Novicell.CreateController', function ($scope, navigationService, notificationsService, mapBuilderResource) {
        var parent = $scope.dialogOptions.currentNode;
        var parentId = parent.id;

        $scope.createNew = function () {
            if (parentId === 'Maps') {
                mapBuilderResource.createNewMap($scope.name).then(function(response) {
                    if (response.data.Success) {
                        document.location = '#/MapBuilder/MapBuilderTree/edit/map-' + response.data.Data;
                    } else {
                        console.log(response.data.ErrorMessage);
                    }
                });
            } else if (parentId === 'Data') {
                mapBuilderResource.createNewDataSource($scope.name).then(function(response) {
                    if (response.data.Success) {
                        document.location = '#/MapBuilder/MapBuilderTree/edit/data-' + response.data.Data;
                    } else {
                        console.log(response.data.ErrorMessage);
                    }
                });
            }

            notificationsService.success('Success', 'Created new \'' + parentId + '\' with name: \'' + $scope.name + '\'');

            navigationService.syncTree({ tree: 'MapBuilderTree', path: [-1, parentId], forceReload: true }).then(function (syncArgs) {
                navigationService.reloadNode(syncArgs.node);
            });

            navigationService.hideDialog();
        }
    });