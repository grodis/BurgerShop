(function () {
    var app = angular.module('myApp')
    app.controller("OrderController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/OrderApi")
            .then(function (response) {
                $scope.orders = response.data;
                console.log($scope.orders);
            });
    });

}());