(function () {
    var app = angular.module('myApp')
    app.controller("DrinkController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/DrinkApi")
            .then(function (response) {
                $scope.drinks = response.data;
            });
    });

}());