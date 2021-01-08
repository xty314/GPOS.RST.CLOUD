<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="v2_error" MasterPageFile="~/Master_Page/Admin_Layout/AdminLayout.master"%>


<%--import MasterPageFile--%>

<%@ Register Src="~/User_Control/Breadcrumbs.ascx" TagPrefix="uc1" TagName="Breadcrumbsl" %>
<%--<%@Import Namespace="System.Data.SqlClient" %>
<%@Import Namespace ="System.Data" %>--%>
<asp:Content ContentPlaceHolderId="AdditionalCSS" runat="server">


</asp:Content>

<asp:Content ContentPlaceHolderId="Header" runat="server">

   <uc1:Breadcrumbsl runat="server" id="Breadcrumbs"  GTitle="Gpos" />

</asp:Content>
<asp:Content ContentPlaceHolderId="Content" runat="server">

  <!-- Main content -->
      <section class="content">
      <div class="error-page">
        <h2 class="headline text-warning"> 404</h2>

        <div class="error-content">
          <h3><i class="fas fa-exclamation-triangle text-warning"></i> Oops! <%=Request.QueryString["aspxerrorpath"] %> Page not found.</h3>

          <p>
            We could not find the page you were looking for.
            Meanwhile, you may <a href="./default.aspx">return to Home page</a> .
          </p>


        </div>
        <!-- /.error-content -->
      </div>
      <!-- /.error-page -->
    </section>
 <!-- /.content -->
</asp:Content>
<asp:Content ContentPlaceHolderId="AdditionalJS" runat="server">


</asp:Content>
