(function () {
    var app = angular.module('myApp')
    app.controller("VeggieController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/VeggieApi")
            .then(function (response) {
                $scope.veggies = response.data;
            });
    });

}());