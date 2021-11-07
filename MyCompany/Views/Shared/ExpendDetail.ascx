<%--<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExpendDetail.ascx.cs" Inherits="MyCompany.Views.Shared.ExpendDetail" %>--%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MyCompany.Models.ExpendDetail>" %>
<div class="modal fade" id="modalexpenddetail" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:800px">
        <div class="modal-content box_button">
            <div id="div_header" class="modal-header header_dialog">
                <button type="button" class="close btn btn-outline-blue" style="margin-top:-5px;margin-right:-15px;padding-left:5px;padding-right:5px" data-dismiss="modal" aria-label="Close"><i class="fa fa-remove fa-3x"></i></button>
                <h4><i class="fa fa-money"></i> <label id="amountExpend">Detail</label></h4>
            </div>
            <div class="modal-body box_button body_save">
                <%--<div id="div_message" align="center">
                    <h5 class="alert alert-danger box_button" style="font-weight:600;margin-bottom:0px;padding:8px">Sure to delete this data?</h5>
                </div>--%>
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="control-label col-md-2">No Faktur</label>
                        <div class="col-md-10">
                            <input type="text" id="IDFaktur" class = "form-control", style = "max-width:30%;width:30%" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Supplier</label>
                        <div class="col-md-10">
                            <input type="text" id="SupplierName" class = "form-control", style = "max-width:80%;width:80%" readonly="readonly" />
                            
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Faktur Date</label>
                        <div class="col-md-10">
                            <input type="text" id="FakturDate" class = "form-control", style = "max-width:30%;width:30%" readonly="readonly" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer modal_footer_10" style="margin-top:0px">
                <table id="myDetail" class="table" style="margin-bottom:0px">
                    <thead style="background-color: #388e3c;color:white;font-weight:600">
                        <tr>
                            <th>#</th>
                            <th style="width:60%">Product Name</th>
                            <th style="width:10%;text-align:right">Vol</th>
                            <th style="width:15%;text-align:right">Price</th>
                            <th style="width:15%;text-align:right">Total</th>
                        </tr>
                    </thead>
                    <tbody style="overflow-y:auto">
                        <tr id="detail_{{$index + 1}}" ng-repeat="m in DetailData">
                            <td id="tdno_{{$index + 1}}">{{$index + 1}}</td>
                            <td><input style="width:100%;max-width:100%" type="text" id="product_{{$index + 1}}" name="product_{{$index + 1}}" readonly="readonly" value="{{m.ProductName}}" /></td>
                            <td><input type="number" id="volume_{{$index + 1}}" name="volume_{{$index + 1}}" style="width:100%;max-width:100%;text-align:right" min="0" max="1000" readonly="readonly" value="{{m.Volume}}" /></td>
                            <td><input style="width:100%;max-width:100%;text-align:right" type="text" id="price_{{$index + 1}}" name="price_{{$index + 1}}" readonly="readonly" value="{{formatThis(m.Price)}}" /></td>
                            <td><input style="width:100%;max-width:100%;text-align:right" type="text" id="amount_{{$index + 1}}" name="amount_{{$index + 1}}" readonly="readonly" value="{{formatThis(m.Amount)}}" /></td>                            
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>