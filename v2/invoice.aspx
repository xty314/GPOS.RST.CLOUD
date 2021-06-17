<%@ Page Language="C#" AutoEventWireup="true" CodeFile="invoice.aspx.cs" Inherits="v2_invoice" MasterPageFile="~/master/Admin_Layout/AdminLayout.master" %>



<%--注意加入MasterPageFile--%>

<%@ Register Src="~/User_Control/Breadcrumbs.ascx" TagPrefix="uc1" TagName="Breadcrumbsl" %>
<%@ Register Src="~/User_Control/CompanyInfoModal.ascx" TagPrefix="uc1" TagName="CompanyInfoModal" %>

<%--<%@Import Namespace="System.Data.SqlClient" %>
<%@Import Namespace ="System.Data" %>--%>
<asp:Content ContentPlaceHolderID="AdditionalCSS" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark" id="Title1">Invoice</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <div class="float-right">
                        <button class="btn bg-blue" data-toggle="modal" data-target="#CompanyInfoModal"><i class="fa fa-pen"></i> Edit Company Info </button>
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
<asp:Content ContentPlaceHolderID="Content" runat="server">

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="callout callout-info">
                        <h5><i class="fas fa-info"></i>Note:</h5>
                        This page has been enhanced for printing. Click the print button at the bottom of the invoice to test.
                    </div>

                    <div class="invoice p-3 mb-3">
                        <div id="invoice">
                             <%Print_Page(); %>
                        </div>
                       
                        <div class="row no-print">
                            <div class="col-12">
                                <button rel="noopener" target="_blank" id="PrintBtn" class="btn btn-default"><i class="fas fa-print"></i>Print</button>
                                <button type="button" class="btn btn-success float-right">
                                    <i class="far fa-credit-card"></i>Submit
                    Payment
                                </button>
                                <button type="button" class="btn btn-primary float-right" style="margin-right: 5px;">
                                    <i class="fas fa-download"></i>Generate PDF
                                </button>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <%--<%Print_Page(); %>--%>
    <!-- /.content -->


    <uc1:CompanyInfoModal runat="server" ID="CompanyInfoModal" />
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalJS" runat="server">
    <script>
        $(document).on("click", "#PrintBtn", function () {
            window.open("./invoice.html")
        })
    </script>
</asp:Content>
