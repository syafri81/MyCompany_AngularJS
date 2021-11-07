var id = 0;

$(function () {
    id = $("#IDExpend").val();
    showTable();
    
    $("#IDSupplier").change(function ()
    {
        showTable();
        if (id > 0)
            removeDetail(id);
        addDetail();
            
    });
});

showTable = function()
{
    var idSupplier = $("#IDSupplier").val();
    if (idSupplier == 0) {
        $("#myTable").hide();
        //$("#myTable").find('tbody').empty();
    }
    else {
        $("#myTable").show();
    }
}

beginDelete = function ()
{
    $("#div_message").empty();
    $("#div_message").append("<h5 class='alert alert-danger box_button' style='font-weight:600;margin-bottom:0px;padding:8px'>" + "Sure to delete this data?" + "</h5>");
}

cannotDelete = function(message)
{
    $("#div_message").empty();
    $("#div_message").append("<h5 class='alert alert-info box_button' style='font-weight:600;margin-bottom:0px;padding:8px'>" + message + "</h5>");
}

function typeAheadProduct()
{
    $("#product_" + id).typeahead({
        hint: true,
        highlight: true,
        minLength: 1,
        source: function (request, response) {
            var idSupplier = $("#IDSupplier").val();
            $.ajax({
                url: '/PublicUse/AutoCompleteProduct/',
                data: "{ 'prefix': '" + request + "'," + "'idSupplier': " + idSupplier + "}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(data);
                    items = [];
                    map = {};
                    $.each(data, function (i, item) {
                        var id = item.val;
                        var name = item.label;
                        var price = moneyFormat(item.price);
                        map[name] = { id: id, name: name, price: price };
                        items.push(name);
                    });
                    response(items);
                    $(".dropdown-menu").css("height", "auto");
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        updater: function (item) {
            $('#idproduct_' + id).val(map[item].id);
            $('#price_' + id).val(map[item].price);
            return item;
        }
    });
}

function addDetail() {    
    id = parseInt($("#maxID").val());

    var price = $("#price_" + id).val();
    if (price == "")
    {
        alert("Detail expenditure is required.");
        return;
    }

    id = id + 1;

    console.log("id begin: " + id);

    var removeHTML = "<button type='button' onclick='removeDetail(" + id + ")' class='btn btn-danger box_button btn-crud' title='remove'><i class='fa fa-minus-circle fa-2x'></i></button>";

    var html = "<tr id='detail_" + id + "'>";
    html += "<td id='tdno_" + id + "'>" + id;
    html += "<input type='hidden' id='idproduct_" + id + "' name='idproduct_" + id + "' />";
    html += "</td>";
    html += "<td><input style='width:100%;max-width:100%' type='text' id='product_" + id + "' name='product_" + id + "' onkeypress='typeAheadProduct()' /></td>";
    html += "<td><input type='number' id='volume_" + id + "' name='volume_" + id + "' style='width:100%;max-width:100%;text-align:right' min='0' max='1000' onblur='totalEachProduct(" + id +")' /></td>";
    html += "<td><input style='width:100%;max-width:100%;text-align:right' type='text' id='price_" + id + "' name='price_" + id + "' readonly='readonly' /></td>";
    html += "<td><input style='width:100%;max-width:100%;text-align:right' type='text' id='amount_" + id + "' name='amount_" + id + "' readonly='readonly' /></td>";
    html += "<td>";
    html += "<button type='button' onclick='addDetail()' class='btn btn-cyan box_button btn-crud' title='save'><i class='fa fa-cart-arrow-down fa-2x'></i></button>";
    html += removeHTML;
    html += "</td>";
    html += "</tr>";

    $("#myTable").find('tbody').append(html);

    $("#maxID").val(id);
}

function removeDetail(idRemove)
{
    $("#detail_" + idRemove).remove();
    var maxID = $("#maxID").val();
    console.log("maxID:" + maxID);
    console.log("idRemove:" + idRemove);
    var numb = 1;
    for (var i = 1; i <= maxID; i++)
    {
        if (i != idRemove)
        {
            document.getElementById("tdno_" + i).innerHTML = numb;
            numb += 1;
        }
    }
    $("#maxID").val(numb);
    id = numb - 1;
}

function totalEachProduct(thisID)
{
    var vol = $("#volume_" + thisID).val();
    var price = $("#price_" + thisID).val();
    price = accounting.unformat(price);
    var total = vol * price;
    total = moneyFormat(total);
    console.log("amount:" + total);
    $("#amount_" + thisID).val(total);
    totalAll();
}

function totalAll()
{
    var maxID = $("#maxID").val();
    var total = 0;
    for (var i = 1; i <= maxID; i++)
    {
        total += accounting.unformat($("#amount_" + i).val());
    }
    total = moneyFormat(total);
    $("#div_total").empty();
    $("#div_total").append("<span class='badge badge-danger pull-right' style='font-size:30px;border-radius:15px'>" + total + ",-</span>");
}

function moneyCurrency(obj)
{
    var result = accounting.formatNumber(obj);
    result = "Rp. " + result + ",-"
    return result;
}

function moneyFormat(obj) {
    var result = accounting.formatNumber(obj);
    return result;
}