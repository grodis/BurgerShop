(function () {
    var app = angular.module('myApp')
    app.controller("BreadController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/BreadApi")
            .then(function (response) {
                $scope.breads = response.data;
            });
    });

}());