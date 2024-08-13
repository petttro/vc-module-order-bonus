angular.module('OrderBonusModule')
    .factory('OrderBonusModule.webApi', ['$resource', function ($resource) {
        return $resource('api/order-bonus-module');
    }]);
