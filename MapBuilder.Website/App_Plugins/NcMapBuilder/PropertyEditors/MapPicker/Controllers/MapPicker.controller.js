angular.module("umbraco")
    .controller("Novicell.MapPickerController", function ($scope, mapBuilderResource) {
        mapBuilderResource.getAllMaps().then(function (response) {
            $scope.maps = response.data;
        });
    });