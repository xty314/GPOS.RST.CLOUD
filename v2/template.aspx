<%@ Page Language="C#" AutoEventWireup="true" CodeFile="template.aspx.cs" Inherits="v2_template" MasterPageFile="~/Master_Page/Admin_Layout/AdminLayout.master" %>

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
       <%Print_Page(); %>
 <!-- /.content -->
</asp:Content>
<asp:Content ContentPlaceHolderId="AdditionalJS" runat="server">


</asp:Content>