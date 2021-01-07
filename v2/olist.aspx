<%@ Page Language="C#" AutoEventWireup="true" CodeFile="olist.aspx.cs" Inherits="v2_olist"  MasterPageFile="~/Master_Page/Admin_Layout/AdminLayout.master"%>


<%--注意加入MasterPageFile--%>

<%@ Register Src="~/User_Control/Breadcrumbs.ascx" TagPrefix="uc1" TagName="Breadcrumbsl" %>
<%--<%@Import Namespace="System.Data.SqlClient" %>
<%@Import Namespace ="System.Data" %>--%>
<asp:Content ContentPlaceHolderId="AdditionalCSS" runat="server">


</asp:Content>

<asp:Content ContentPlaceHolderId="Header" runat="server">

   <uc1:Breadcrumbsl runat="server" id="Breadcrumbs"  GTitle="Sales List" />

</asp:Content>
<asp:Content ContentPlaceHolderId="Content" runat="server">

  <!-- Main content -->
  
    <div class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-12">
            <div class="card">
        
              <div class="card-body">
              <%Print_Page(); %>
              </div>
            </div>
            <!-- /.card -->

 
          </div>
          <!-- /.col-md-6 -->
  
        </div>
        <!-- /.row -->
      </div>
      <!-- /.container-fluid -->
    </div>
 <!-- /.content -->
</asp:Content>
<asp:Content ContentPlaceHolderId="AdditionalJS" runat="server">

    <script>
        document.f.kw.focus();
        $(function () {
            $('[data-toggle="tooltip"]').tooltip();

        });
    </script>
     
</asp:Content>