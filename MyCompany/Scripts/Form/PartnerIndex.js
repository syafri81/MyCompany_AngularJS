var myApp = angular.module("MyApp", [])
myApp.controller("IndexDataCtrl", function ($scope, $http, $compile)
{
    var directPage = "/Partner";

    $http.post(directPage + "/GetIndex").
         success(function (data, status, headers, config) {
             $scope.IndexData = data;
             //hideLoading();
         }).
         error(function (data, status, headers, config) {
             // log error
             //hideLoading();
         });

    $scope.edit = function()
    {
        var IDNumber = this.m.IDNumber;
        var Name = this.m.Name;
        $.ajax({
            type: 'POST',
            url: directPage + "/Edit",
            data: { id: IDNumber },
            success: createCallBack
        })
    }

    createCallBack = function ()
    {
        window.location.href = directPage + "/Create";
    }

    $scope.delete = function () {
        var id = this.m.IDPartner;
        beginDelete();
        $("#modaldelete").modal();
        $("#btndelete").click(function () {
            $.ajax({
                type: 'POST',
                url: directPage + "/Delete",
                data: { id: id },
                success: deleteCallBack
            })
        });
    }

    deleteCallBack = function (dmr) {
        if (dmr.isSuccess == true) {
            window.location.href = directPage;
        }
        else {
            cannotDelete(dmr.messages[0]);
        }
    }

    $scope.toggleActive = function () {
        var id = this.m.IDPartner;
        var active = 0;
        var chkValue = this.m.IsActive;
        if (chkValue == false) {
            active = 1;
        }
        else {
            active = 0;
        }
        $.post(directPage +  "/MakeActive?id=" + id + "&&isActive=" + active, function (dmr) {
            //window.location.href = "/Supplier";
            window.location.reload(false);
        });
    }
});