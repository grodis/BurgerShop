

(function () {
    var app = angular.module('myApp')
    app.controller("BurgerController", function ($scope, $http) {
        $http.get("https://localhost:44390/api/BurgerApi")
            .then(function (response) {
                $scope.burgers = response.data;
            });
    });

}());