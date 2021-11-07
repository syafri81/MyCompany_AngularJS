var myApp = angular.module("MyApp", [])
myApp.controller("IndexDataCtrl", function ($scope, $http, $compile)
{
    var directPage = "/Expenditure";
    var idExpend = $("#IDExpend").val();

    $http.post(directPage + "/GetDetail?idExpend=" + idExpend).
         success(function (data, status, headers, config) {
             $scope.IndexData = data;
             $scope.sumThis();
             $("#maxID").val(data.length);
             //hideLoading();
         }).
         error(function (data, status, headers, config) {
             // log error
             //hideLoading();
         });

    $scope.totalThis = function(idx)
    {
        totalEachProduct(idx);
    }

    $scope.sumThis = function ()
    {
        var total = 0;
        for (var i = 0; i < $scope.IndexData.length; i++)
        {
            var amount = $scope.IndexData[i].Amount;
            total += amount;
        }

        //var result = accounting.formatNumber(total);
        var result = moneyCurrency(total);
        console.log("result:" + result);

        $("#div_total").empty();
        $("#div_total").append("<span class='badge badge-danger pull-right' style='font-size:30px;border-radius:15px'>" + result + "</span>");
    }

    $scope.formatThis = function(obj)
    {        
        return moneyFormat(obj);
    }

    $scope.removeDetail = function(obj)
    {
        removeDetail(obj);
        var idDetail = this.m.IDDetail;
        var idExpend = this.m.IDExpend;

        $.post(directPage + "/RemoveDetail?idDetail=" + idDetail + "&&idExpend=" + idExpend, function (data)
        {
            location.reload(false);
        });
    }
});