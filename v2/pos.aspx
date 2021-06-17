<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pos.aspx.cs" Inherits="v2_pos" MasterPageFile="~/master/Admin_Layout/AdminLayout.master" %>



<%@ Register Src="~/User_Control/Breadcrumbs.ascx" TagPrefix="uc1" TagName="Breadcrumbsl" %>
<%--<%@Import Namespace="System.Data.SqlClient" %>
<%@Import Namespace ="System.Data" %>--%>
<asp:Content ContentPlaceHolderId="AdditionalCSS" runat="server">
    <link href="/src/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css" rel="stylesheet" />

</asp:Content>

<asp:Content ContentPlaceHolderId="Header" runat="server">
   <uc1:Breadcrumbsl runat="server" id="Breadcrumbs"  />
</asp:Content>
<asp:Content ContentPlaceHolderId="Content" runat="server">

  <!-- Main content -->
       <%Print_Page(); %>

    <form runat=server>
		<asp:Label id=LInv runat=server/>

		<asp:DataGrid id=MyDataGridBill 
			runat=server 
			AutoGenerateColumns=true
			BackColor=White 
			BorderWidth=1px 
			BorderStyle=Solid 
			BorderColor=#EEEEEE
			CellPadding=1
			CellSpacing=0
			Font-Name=Verdana 
			Font-Size=8pt 
			width=100% 
			style=fixed
			HorizontalAlign=center
			>
			<HeaderStyle BackColor=#7b90ac ForeColor=White Font-Bold=true/>
			<ItemStyle ForeColor=DarkSlateBlue/>
			<AlternatingItemstyle BackColor=#EEEEEE/>
    
			<Columns>
				<asp:HyperLinkColumn
					 HeaderText=Select
					 DataNavigateUrlField=id
					 DataNavigateUrlFormatString="pos.aspx?bi={0}"
					 DataTextField=name/>
				<asp:HyperLinkColumn
					 HeaderText=Edit
					 DataNavigateUrlField=id
					 DataNavigateUrlFormatString="ecard.aspx?id={0}"
					 Text=Edit/>
			</Columns>
		</asp:DataGrid>

		<asp:DataGrid id=MyDataGridQ
			runat=server 
			AutoGenerateColumns=true
			BackColor=White 
			BorderWidth=1px 
			BorderStyle=Solid 
			BorderColor=#EEEEEE
			CellPadding=1
			CellSpacing=0
			Font-Name=Verdana 
			Font-Size=8pt 
			width=100% 
			style=fixed
			HorizontalAlign=center
			>

			<HeaderStyle BackColor=#7b90ac ForeColor=White Font-Bold=true/>
			<ItemStyle ForeColor=DarkSlateBlue/>
			<AlternatingItemstyle BackColor=#EEEEEE/>
    
			<Columns>
				<asp:HyperLinkColumn
					 HeaderText=Select
					 DataNavigateUrlField=id
					 DataNavigateUrlFormatString="q.aspx?ci={0}"
					 DataTextField=name/>
				<asp:HyperLinkColumn
					 HeaderText=Edit
					 DataNavigateUrlField=id
					 DataNavigateUrlFormatString="eacc.aspx?id={0}"
					 Text=Edit/>
			</Columns>
		</asp:DataGrid>

		<asp:DataGrid id=MyDataGrid
			runat=server 
			AutoGenerateColumns=false
			BackColor=White 
			BorderWidth=1px 
			BorderStyle=Solid 
			BorderColor=#EEEEEE
			CellPadding=1
			CellSpacing=0
			Font-Name=Verdana 
			Font-Size=8pt 
			width=100% 
			style=fixed
			HorizontalAlign=center
			AllowPaging=True
			PageSize=50
			PagerStyle-PageButtonCount=20
			PagerStyle-Mode=NumericPages
			PagerStyle-HorizontalAlign=Left
			OnPageIndexChanged=MyDataGrid_PageA
			>

			<HeaderStyle BackColor=#7b90ac ForeColor=White Font-Bold=true/>
			<ItemStyle ForeColor=DarkSlateBlue/>
			<AlternatingItemstyle BackColor=#EEEEEE/>
    
			<Columns>
				<asp:BoundColumn HeaderText=ID DataField=id/>
				<asp:HyperLinkColumn
					 HeaderText=Select
					 DataNavigateUrlField=uri
					 DataNavigateUrlFormatString="{0}"
					 DataTextField=trading_name/>
				<asp:HyperLinkColumn
					 HeaderText=Select
					 DataNavigateUrlField=uri
					 DataNavigateUrlFormatString="{0}"
					 DataTextField=company/>
				<asp:HyperLinkColumn
					 HeaderText=Select
					 DataNavigateUrlField=uri
					 DataNavigateUrlFormatString="{0}"
					 DataTextField=name/>
				<asp:HyperLinkColumn
					 HeaderText=Edit
					 DataNavigateUrlField=id
					 DataNavigateUrlFormatString="ecard.aspx?id={0}"
					 Text=Edit/>
			</Columns>
		</asp:DataGrid>

	</form>


 <!-- /.content -->
</asp:Content>
<asp:Content ContentPlaceHolderId="AdditionalJS" runat="server">
<script src="/src/plugins/select2/js/select2.min.js"></script>
    <script>
		$("select").select2({
            ajax: {
                url: "https://api.github.com/search/repositories",
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        q: params.term, // search term
                        page: params.page
                    };
                },
                processResults: function (data, params) {
                    // parse the results into the format expected by Select2
                    // since we are using custom formatting functions we do not need to
                    // alter the remote JSON data, except to indicate that infinite
                    // scrolling can be used
                    params.page = params.page || 1;

                    return {
                        results: data.items,
                        pagination: {
                            more: (params.page * 30) < data.total_count
                        }
                    };
                },
                cache: true
            },
            placeholder: 'Search for a repository',
            minimumInputLength: 1,
            templateResult: formatRepo,
            templateSelection: formatRepoSelection
		});
        function formatRepo(repo) {
            if (repo.loading) {
                return repo.text;
            }

            var $container = $(
                "<div class='row'>" +
                "<div class='col-1'><img style='width:100%' src='" + repo.owner.avatar_url + "' /></div>" +
                "<div class='col-3'>" +
                "<div class='select2-result-repository__title'></div>" +
                "<div class='select2-result-repository__description'></div>" +
                "<div class='select2-result-repository__statistics'>" +
                "<div class='select2-result-repository__forks'><i class='fa fa-flash'></i> </div>" +
                "<div class='select2-result-repository__stargazers'><i class='fa fa-star'></i> </div>" +
                "<div class='select2-result-repository__watchers'><i class='fa fa-eye'></i> </div>" +
                "</div>" +
                "</div>" +
                "</div>"
            );

            $container.find(".select2-result-repository__title").text(repo.full_name);
            $container.find(".select2-result-repository__description").text(repo.description);
            $container.find(".select2-result-repository__forks").append(repo.forks_count + " Forks");
            $container.find(".select2-result-repository__stargazers").append(repo.stargazers_count + " Stars");
            $container.find(".select2-result-repository__watchers").append(repo.watchers_count + " Watchers");

            return $container;
        }

        function formatRepoSelection(repo) {
            return repo.full_name || repo.text;
        }
    </script>
    <script type="text/javascript">
        
		function OnShippingMethodChange() {
            var m = Number(document.form1.shipping_method.value);
            if (m == 1) { document.all('tShipTo').style.visibility = 'hidden'; document.all('tPT').style.visibility = 'visible'; }
            else { document.all('tShipTo').style.visibility = 'visible'; document.all('tPT').style.visibility = 'hidden'; }
        }
        function OnSpecialShiptoChange() {
            var v = document.all('ssta').style.visibility;
            if (v != 'hidden') {
                document.all('ssta').style.visibility = 'hidden';
                document.all('tshiptoaddr').style.visibility = 'visible';
                document.all('tshiptoaddr').style.top = 10;
            } else {
                document.all('ssta').style.visibility = 'visible';
                document.all('tshiptoaddr').style.visibility = 'hidden';
            }
        }																
 /*
function iCalPrice(price, qty, i, discount)
{
	var dtotal, dDiscount;
	if(parseFloat(discount))
		dDiscount = discount;
	else
		dDiscount = 0;
	dDiscount = (dDiscount / 100);

	//price = price.replace('$', '');
	price = convertPrice(price);
	qty = qty.replace('$', '');
	if(IsNumberic(price) && IsNumberic(qty))
	{
                dtotal = (price * (1 - dDiscount)) * qty;
		dtotal = dtotal.toFixed(2);
		eval("document.form1.dtotal" + i + ".value = dtotal")
	}
	var bfalse;
	bfalse = false;
	if(!IsNumberic(price))
	{
                bfalse = true;
	}
	if(!IsNumberic(qty))
	{
                bfalse = true;
	}
	if(bfalse)
		return false;
	else
		return true;
//	window.alert(dtotal);

}
function convertPrice(sPrice)
{
	var sSwap, bFoundDot;
	sSwap = '';
	for (i = 0; i < sPrice.length; i++)
	{
		if(parseFloat(sPrice.charAt(i)))
		{
                sSwap = sSwap + sPrice.charAt(i);
		}
		if(sPrice.charAt(i) == '.' && !bFoundDot)
		{
                sSwap = sSwap + sPrice.charAt(i);
			bFoundDot = true;
		}
		if(sPrice.charAt(i) == '0')
			sSwap = sSwap + sPrice.charAt(i);
	}
	return sSwap;

}
function IsNumberic(sText)
	{
	   var ValidChars = '0123456789.';
	   var IsNumber=true;
	   var Char;
 	   for (i = 0; i < sText.length && IsNumber == true; i++)
	   {
                Char = sText.charAt(i);
		  if (ValidChars.indexOf(Char) == -1)
		  {
                IsNumber = false;
		  }
	   }
		   return IsNumber;
	}
*/
    </script>
</asp:Content>
