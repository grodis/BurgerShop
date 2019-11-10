(function () {
    var app = angular.module('myApp')
    app.controller("CheeseController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/CheeseApi")
            .then(function (response) {
                $scope.cheeses = response.data;
            });
    });

}());