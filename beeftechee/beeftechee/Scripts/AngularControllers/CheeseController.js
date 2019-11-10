(function () {
    var app = angular.module('myApp')
    app.controller("MeatController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/MeatApi")
            .then(function (response) {
                $scope.meats = response.data;
            });
    });

}());