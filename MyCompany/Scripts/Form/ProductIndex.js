var myApp = angular.module("MyApp", [])
myApp.controller("IndexDataCtrl", function ($scope, $http, $compile)
{
    var directPage = "/Product";

    $http.post(directPage + "/GetIndex").
         success(function (data, status, headers, config) {
             $scope.IndexData = data.products;
             //hideLoading();
             var combo = data.campaigns;
             $("#IDCampaign").find('option').remove();
             for (var i = 0; i < combo.length; i++) {
                 $('#IDCampaign').append($('<option>').text(combo[i].label).attr('value', combo[i].val));
             }
         }).
         error(function (data, status, headers, config) {
             // log error
             //hideLoading();
         });

    $scope.edit = function () {
        var id = this.m.IDProduct;
        $.ajax({
            type: 'POST',
            url: directPage + "/Edit",
            data: { id: id },
            success: createCallBack
        })
    }

    createCallBack = function () {
        window.location.href = directPage + "/Create";
    }

    $scope.delete = function () {
        var id = this.m.IDProduct;
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
        var id = this.m.IDProduct;
        var active = 0;
        var chkValue = this.m.IsActive;
        if (chkValue == false) {
            active = 1;
        }
        else {
            active = 0;
        }
        $.post(directPage + "/MakeActive?id=" + id + "&&isActive=" + active, function (dmr) {
            //window.location.href = "/Supplier";
            window.location.reload(false);
        });
    }
});