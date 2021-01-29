<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cat.aspx.cs" Inherits="v2_cat" MasterPageFile="~/Master_Page/Admin_Layout/AdminLayout.master"  %>



<%--import MasterPageFile--%>

<%@ Register Src="~/User_Control/Breadcrumbs.ascx" TagPrefix="uc1" TagName="Breadcrumbsl" %>
<%--<%@Import Namespace="System.Data.SqlClient" %>
<%@Import Namespace ="System.Data" %>--%>
<asp:Content ContentPlaceHolderId="AdditionalCSS" runat="server">
<link href="/src/plugins/jstree/jstree.min.css" rel="stylesheet" />
	
     <style>
	 #Pager{
		 height:30px;
	 }
	 #container{
		margin:auto;
		 margin-top:30px;
		 width:80%;
	 }
	 .table-success{
		 background:#dddddd!important;
		 border-color:black!important;
	 }
	 th{
		 <%-- height:10px!important; --%>
		 min-height:10px!important;
		  padding:3px!important;
	 }
	 td{

		 padding:3px!important;
	 }
	 </style>
</asp:Content>

<asp:Content ContentPlaceHolderId="Header" runat="server">

      <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark" id="Title1">Category</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <div class="float-right">
                         <form id='updateForm' method="post" action="cat.aspx" >
                                <input class="btn bg-blue"  type="submit" name="cmd" id='updateBtn' value="Update Category">
                        </form>
                       
                       <%-- <button type="button" class="btn btn-success"><i class="fa fa-download"></i>Export </button>--%>
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderId="Content" runat="server">

  <!-- Main content -->
        
         <section class="content">
             <div class="container-fluid">
                 <div class="row" style='display: none'>
                     <div class="col-md-12">
                         <div class="card card-primary">
                             <div class="card-body">
                                 <div class=" form-group col-12">
                                     <button type="button" class="btn btn-info btn-lg float-right" id="UpdateCategoryBtn">Update Category</button>
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
                 <div class="row">
                     <div class="col-md-4  offset-md-1">
                         <div class="card card-primary">
                             <div class="card-body">
                                 <div class=" form-group col-12">
                                     <button type="button " class="btn btn-primary" id="AddCategoryBtn">Add New Category</button>
                                 </div>
                                 <div id="catalog_tree"></div>
                             </div>
                         </div>
                     </div>
                     <div class="col-md-6" id='editContainer' style='z-index: 0'>
                         <form action="cat.aspx?cmd=updateCat" class='sticky-top' method="post" id='NewCategoryForm'>
                             <div class="card card-info">
                                 <div class="card-body">
                                     <div class="form-group row" id='catRow'>
                                         <label for="inputEmail3" class="col-sm-2 col-form-label">Category</label>
                                         <div class="col-sm-10" id='catDiv'>
                                             <input type="text" class="form-control" name='cat'>
                                         </div>
                                     </div>
                                     <div class="form-group row" id='scatRow'>
                                         <label for="inputPassword3" class="col-sm-2 col-form-label">S_Category</label>
                                         <div class="col-sm-10" id='scatDiv'>
                                             <input type="text" class="form-control" name='scat'>
                                         </div>
                                     </div>
                                     <div class="form-group row" id='sscatRow'>
                                         <label for="inputPassword3" class="col-sm-2 col-form-label">SS_Category</label>
                                         <div class="col-sm-10" id='sscatDiv'>
                                             <input type="text" class="form-control" name='sscat'>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="card-footer">
                                     <button type="submit" class="btn btn-primary" name='oper' id='saveBtn' value='edit'>Save</button>
                                     <button type="submit" class="btn btn-warning float-right" name='oper' id='deleteBtn' value='del' value="">Delete</button>
                                 </div>
                             </div>
                             <input type="hidden" name='oldCat'>
                             <input type="hidden" name='oldSCat'>
                             <input type="hidden" name='oldSSCat'>
                         </form>
                     </div>
                 </div>
         </section>
         <form action="cat.aspx" method="post" id='NewCategoryForm'>
             <div class="modal fade" id="categoryModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                 <div class="modal-dialog" role="document">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h5 class="modal-title" id="exampleModalLabel">New Category</h5>
                             <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                 <span aria-hidden="true">&times;</span>
                             </button>
                         </div>
                         <div class="modal-body">
                             <div class="form-group row">
                                 <label for="recipient-name" class="col-form-label col-sm-2">Cat:</label>
                                 <input type="text" class="form-control  col-sm-10" name='new_cat' id="new_cat">
                             </div>
                             <input type="hidden" class="form-control  col-sm-10" name='new_scat' id="new_sscat">
                             <input type="hidden" class="form-control  col-sm-10" name='new_sscat' id="new_sscat">
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                             <button type="submit" name='cmd' value='addCategory' class="btn btn-primary">Save</button>
                         </div>
                     </div>
                 </div>
             </div>
         </form>
         <form action="cat.aspx" method="post" id='NewSCategoryForm'>
             <div class="modal fade" id="scategoryModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                 <div class="modal-dialog" role="document">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h5 class="modal-title" id="exampleModalLabel">New Sub Category</h5>
                             <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                 <span aria-hidden="true">&times;</span>
                             </button>
                         </div>
                         <div class="modal-body">
                             <div class="form-group row">
                                 <label for="recipient-name" class="col-form-label col-sm-2">Cat:</label>
                                 <input type="text" class="form-control  col-sm-10" name='new_cat' id="s_new_cat" readonly>
                             </div>
                             <div class="form-group row">
                                 <label for="recipient-name" class="col-form-label col-sm-2">S_Cat:</label>
                                 <input type="text" class="form-control  col-sm-10" name='new_scat' id="s_new_scat">
                             </div>
                             <input type="hidden" class="form-control  col-sm-10" name='new_sscat' id="s_new_sscat">
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                             <button type="submit" name='cmd' value='addCategory' class="btn btn-primary">Save</button>
                         </div>
                     </div>
                 </div>
             </div>
         </form>
         <form action="cat.aspx" method="post" id='NewSSCategoryForm'>
             <div class="modal fade" id="sscategoryModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                 <div class="modal-dialog" role="document">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h5 class="modal-title" id="exampleModalLabel">New SS_Category</h5>
                             <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                 <span aria-hidden="true">&times;</span>
                             </button>
                         </div>
                         <div class="modal-body">
                             <div class="form-group row">
                                 <label for="recipient-name" class="col-form-label col-sm-2">Cat:</label>
                                 <input type="text" class="form-control  col-sm-10" name='new_cat' id="ss_new_cat" readonly>
                             </div>
                             <div class="form-group row">
                                 <label for="recipient-name" class="col-form-label col-sm-2">S_Cat:</label>
                                 <input type="text" class="form-control  col-sm-10" name='new_scat' id="ss_new_scat" readonly>
                             </div>
                             <div class="form-group row">
                                 <label for="recipient-name" class="col-form-label col-sm-2">SS_Cat:</label>
                                 <input type="text" class="form-control  col-sm-10" name='new_sscat' id="ss_new_sscat">
                             </div>
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                             <button type="submit" name='cmd' value='addCategory' class="btn btn-primary">Save</button>
                         </div>
                     </div>
                 </div>
             </div>
         </form>
 <!-- /.content -->
</asp:Content>
<asp:Content ContentPlaceHolderId="AdditionalJS" runat="server">
<script src="/src/plugins/lodash/lodash.js"></script>
<script src="/src/plugins/jstree/jstree.min.js"></script>
 <script src="/src/js/page/cat.js"></script>
</asp:Content>
