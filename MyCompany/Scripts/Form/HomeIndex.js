var myApp = angular.module("MyApp", [])
myApp.controller("IndexDataCtrl", function ($scope, $http, $compile)
{
    var directPage = "/Home";

    $http.post(directPage + "/GetIndex").
         success(function (data, status, headers, config) {
             $scope.IndexData = data;
             //hideLoading();
         }).
         error(function (data, status, headers, config) {
             // log error
             //hideLoading();
         });

    $scope.goToCampaign = function()
    {
        var id = this.m.val;
        $.post(directPage + "/GoToCampaign/" + id, function (obj)
        {
            window.location.href = "/Supplier";
        });
    }
});