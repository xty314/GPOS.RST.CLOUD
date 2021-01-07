<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompanyInfoModal.ascx.cs" Inherits="User_Control_CompanyInfoModal" %>
<div class="modal fade" id="CompanyInfoModal" aria-hidden="true" style="display: none;">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Company Infomation</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                <form  method="post" class="form-horizontal" id="CompanyInfoForm">
                <div class="form-group row">
                    <label for="inputEmail3" class="col-sm-2 col-form-label text-md">Email</label>
                    <div class="col-sm-10">
                      <input type="email" class="form-control" id="inputEmail3" placeholder="Email">
                    </div>
                  </div>
                </form>
            </div>
            <div class="modal-footer justify-content-between">
                <button  type="button"  class="btn btn-default" data-dismiss="modal">Close</button>
                <button type="submit" class="btn btn-primary" form="CompanyInfoForm" name="cmd" value="save">Save changes</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
