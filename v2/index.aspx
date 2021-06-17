<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="v2_index"   MasterPageFile="~/master/Admin_Layout/AdminLayout.master"%>

<%@ Register Src="~/User_Control/Breadcrumbs.ascx" TagPrefix="uc1" TagName="Breadcrumbs" %>

<%--<%@Import Namespace="System.Data.SqlClient" %>
<%@Import Namespace ="System.Data" %>--%>
<asp:Content ContentPlaceHolderId="AdditionalCSS" runat="server">


</asp:Content>

<asp:Content ContentPlaceHolderId="Header" runat="server">
    <uc1:Breadcrumbs runat="server" id="Breadcrumbs"  GTitle="Gpos" />
</asp:Content>
<asp:Content ContentPlaceHolderId="Content" runat="server">

  <!-- Main content -->
     
 <!-- /.content -->
</asp:Content>
<asp:Content ContentPlaceHolderId="AdditionalJS" runat="server">


</asp:Content>