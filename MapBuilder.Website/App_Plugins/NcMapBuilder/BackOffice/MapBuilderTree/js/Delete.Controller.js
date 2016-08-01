angular.module('umbraco')
    .controller('Novicell.DeleteController', function ($scope, navigationService, notificationsService, mapBuilderResource) {
        $scope.name = $scope.dialogOptions.currentNode.name;
        var parentId = $scope.dialogOptions.currentNode.parentId;

        $scope.removeItem = function () {
            if (parentId === 'Maps') {
                mapBuilderResource.removeMap($scope.dialogOptions.currentNode.id.substr(4));
                document.location = '#/MapBuilder/MapBuilderTree/edit/Maps';
            } else if (parentId === 'Data') {
                mapBuilderResource.removeDataSource('{\'Id\':' + $scope.dialogOptions.currentNode.id.substr(5) + '}');
                document.location = '#/MapBuilder/MapBuilderTree/edit/Data';
            }
            notificationsService.success('Success', 'Deleted \'' + $scope.name + '\'');

            navigationService.syncTree({ tree: 'MapBuilderTree', path: [-1, parentId], forceReload: true }).then(function (syncArgs) {
                navigationService.reloadNode(syncArgs.node);
            });

            navigationService.hideDialog();
        }
    });