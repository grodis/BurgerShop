
(function () {
    var app = angular.module('myApp', ['ngRoute'])
        .controller('HomeController', function ($scope) {
            $scope.Message = "Home Page"
        })
        .controller('AboutController', function ($scope) {
            $scope.Message = "About Page"
        })
    app.config(function ($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "/Angular/Burger",
                controller: "BurgerController"
            })
            .when("/Burger", {
                templateUrl: "/Angular/Burger",
                controller: "BurgerController"
            })
            .when("/Drink", {
                templateUrl: "/Angular/Drink",
                controller: "DrinkController"
            })
            .when("/Cheese", {
                templateUrl: "/Angular/Cheese",
                controller: "CheeseController"
            })
            .when("/Meat", {
                templateUrl: "/Angular/Meat",
                controller: "MeatController"
            })
            .when("/Bread", {
                templateUrl: "/Angular/Bread",
                controller: "BreadController"
            })
            .when("/Sauce", {
                templateUrl: "/Angular/Sauce",
                controller: "SauceController"
            })
            .when("/Veggie", {
                templateUrl: "/Angular/Veggie",
                controller: "VeggieController"
            })
            .when("/Order", {
                templateUrl: "/Angular/Order",
                controller: "OrderController"
            });

    });


}());