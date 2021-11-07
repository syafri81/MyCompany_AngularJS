var myApp = angular.module("MyApp", [])
myApp.controller("IndexDataCtrl", function ($scope, $http, $compile)
{
    var directPage = "/Expenditure";

    $http.post(directPage + "/GetIndex").
         success(function (data, status, headers, config) {
             $scope.IndexData = data.expends;
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
        var id = this.m.IDExpend;
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
        var id = this.m.IDExpend;
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

    $scope.detail = function()
    {
        var id = this.m.IDExpend;
        $.post(directPage + "/Detail/" + id, function (data)
        {
            //console.log(data);
            let lbl = document.getElementById('amountExpend');
            lbl.innerHTML = moneyCurrency(data.Amount);
            $("#IDFaktur").val(data.IDFaktur);
            $("#SupplierName").val(data.SupplierName);
            $("#FakturDate").val(data.FakturDate);

            var details = data.Details;
            var idx = 1;
            for (var i = 0; i < details.length; i++)
            {
                var html = "<tr id='detail_" + i + "'>";
                html += "<td id='tdno_" + i + "'>" + idx;
                html += "</td>";
                html += "<td><input style='width:100%;max-width:100%' type='text' id='product_" + i + "' name='product_" + i + "' value='" + details[i].ProductName + "' readonly='readonly' /></td>";
                html += "<td><input type='number' id='volume_" + i + "' name='volume_" + i + "' value='" + details[i].Volume + "' style='width:100%;max-width:100%;text-align:right' min='0' max='1000' readonly='readonly' /></td>";
                var price = moneyFormat(details[i].Price);
                html += "<td><input style='width:100%;max-width:100%;text-align:right' type='text' id='price_" + i + "' name='price_" + i + "' value='" + price + "' readonly='readonly' /></td>";
                var amount = moneyFormat(details[i].Amount);
                html += "<td><input style='width:100%;max-width:100%;text-align:right' type='text' id='amount_" + i + "' name='amount_" + i + "' value='" + amount + "' readonly='readonly' /></td>";
                html += "</tr>";

                idx += 1;
                $("#myDetail").find('tbody').append(html);
            }
            
        });
        $("#modalexpenddetail").modal();
    }
});