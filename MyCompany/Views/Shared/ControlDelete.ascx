<%--<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlDelete.ascx.cs" Inherits="MyCompany.Views.Shared.ControlDelete" %>--%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="modal fade" id="modaldelete" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:500px">
        <div class="modal-content box_button">
            <div id="div_header" class="modal-header header_dialog">
                <button type="button" class="close btn btn-outline-blue" style="margin-top:-5px;margin-right:-15px;padding-left:5px;padding-right:5px" data-dismiss="modal" aria-label="Close"><i class="fa fa-remove fa-3x"></i></button>
                <h4>Remove Data</h4>
            </div>
            <div class="modal-body box_button body_save">
                <div id="div_message" align="center">
                    <h5 class="alert alert-danger box_button" style="font-weight:600;margin-bottom:0px;padding:8px">Sure to delete this data?</h5>
                </div>
            </div>
            <div class="modal-footer modal_footer_10">
                <button type="button" id="btndelete" class="btn btn-danger box_button btn-med"><i class="fa fa-trash fa-3x"></i></button>
                <button type="button" id="btncancel" data-dismiss="modal" class="btn btn-default box_button btn-med"><i class="fa fa-power-off fa-3x"></i></button>
            </div>
        </div>
    </div>
</div>