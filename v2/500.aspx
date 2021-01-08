<%@ Page Language="C#" AutoEventWireup="true" CodeFile="500.aspx.cs" Inherits="v2_500"  MasterPageFile="~/Master_Page/Admin_Layout/AdminLayout.master" %>


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
    <div class="row">
             <div class="error-page">
        <h2 class="headline text-danger">500</h2>

        <div class="error-content">
          <h3><i class="fas fa-exclamation-triangle text-danger"></i> Oops! Something went wrong.</h3>

          <p>
            We will work on fixing that right away.
            Meanwhile, you may <a href="../../index.html">return to dashboard</a> or try using the search form.
          </p>
           <button id="ShowErrorMessage">show</button>
        </div>
      </div>
    </div>
 
      <!-- /.error-page -->
        <div class="row">
          <div class="col-lg-12">
            <div class="card" id="error-info" style="display:none;">

              <div class="card-body" >
                <%=Application["error"]%>
               <xmp>


</xmp>
                 

                
              </div>
            </div>
            <!-- /.card -->

        
          </div>
          <!-- /.col-md-6 -->
        </div>
    </section>
 <!-- /.content -->
</asp:Content>
<asp:Content ContentPlaceHolderId="AdditionalJS" runat="server">
    <script>
        //$("#error-info ").hide();
        var $style = $("#error-info style").text()
        $style = $style.replace(`body {font-family:"Verdana";font-weight:normal;font-size: .7em;color:black;} 
         p {font-family:"Verdana";font-weight:normal;color:black;margin-top: -5px}`, "");
        $("#error-info style").html($style)
        
        $(document).on("click", "#ShowErrorMessage", function () {
            $("#error-info ").toggle();
        })
    </script>

</asp:Content>
