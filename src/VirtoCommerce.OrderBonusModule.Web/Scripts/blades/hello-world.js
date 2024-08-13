angular.module('OrderBonusModule')
    .controller('OrderBonusModule.helloWorldController', ['$scope', 'OrderBonusModule.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'OrderBonusModule';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'OrderBonusModule.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
