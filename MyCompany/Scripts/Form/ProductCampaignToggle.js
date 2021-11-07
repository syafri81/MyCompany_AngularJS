$(function ()
{
    var idProduct = $("#IDProduct").val();
    if (idProduct > 0)
    {
        comboSupplier(idProduct);
    }
});

function comboSupplier(idProduct) {
    var campaignID = $("#IDCampaign").val();
    $.post("/PublicUse/ComboSupplier?campaignID=" + campaignID, function (combo) {
        $("#IDSupplier").find('option').remove();
        for (var i = 0; i < combo.length; i++) {
            $('#IDSupplier').append($('<option>').text(combo[i].label).attr('value', combo[i].val));
        }
    });
}