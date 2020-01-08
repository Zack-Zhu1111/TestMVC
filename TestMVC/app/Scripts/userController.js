(function (app) {
    var userController = function ($scope,$http) {
        $scope.message = "Hello World!";
        //$http.get("/api/BaseAPI/GetAllUser")
        //    .sunccess(function (data) {
        //        $scope.user = data;
        //    })
    };
    //userController.$inject = ["$scope", "$http"];
    app.controller("userController", userController);
    //app.controller("userController", function ($scope, $http) {
    //    $http({
    //        method: 'GET',
    //        url: 'api/BaseAPI/GetAllUser'
    //    }).then(function (data) {
    //        $scope.user = data;
    //    });
    //});
}(angular.module("userlist")));