(function () {
    var app = angular.module('myApp')
    app.controller("SauceController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/SauceApi")
            .then(function (response) {
                $scope.sauces = response.data;
            });
    });

}());