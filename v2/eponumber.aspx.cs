using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class v2_eponumber : AdminBasePage
{
	DataSet dst = new DataSet();    //for creating Temp talbes templated on an existing sql table
	bool bEdit = false;
	string orderId = "";
	string ok = "";
	void Page_Load(Object Src, EventArgs E)
	{
		TS_PageLoad(); //do common things, LogVisit etc...
					   //if(!SecurityCheck("technician"))
					   //	return;
		if (Request.QueryString[0] != null)
			orderId = Request.QueryString[0];

		if (orderId == "")
			return;

		if (Request.Form["cmd"] == "Save")
		{
			if (!doPONumberUpdate())
				return;
		}

		printForm();

	}

	bool doPONumberUpdate()
	{

		string sc = " UPDATE orders SET po_number ='" + Request.Form["po_number"] + "' WHERE id =" + orderId;
		try
		{
			SqlDataAdapter myCommand = new SqlDataAdapter(sc, myConnection);
			myCommand.Fill(dst, "po_number");

		}
		catch (Exception e)
		{
			ShowExp(sc, e);
			return false;
		}
		sc = " SELECT invoice_number FROM orders WHERE id =" + orderId;
		try
		{
			SqlDataAdapter myCommand = new SqlDataAdapter(sc, myConnection);
			myCommand.Fill(dst, "chkInv");
		}
		catch (Exception e)
		{
			ShowExp(sc, e);
			return false;
		}

		if (dst.Tables["chkInv"].Rows[0]["invoice_number"].ToString() != "" && dst.Tables["chkInv"].Rows[0]["invoice_number"].ToString() != null)
		{
			sc = " UPDATE invoice SET cust_ponumber ='" + Request.Form["po_number"] + "' WHERE invoice_number = '" + dst.Tables["chkInv"].Rows[0]["invoice_number"].ToString() + "'";
			try
			{
				myCommand = new SqlCommand(sc);
				myCommand.Connection = myConnection;
				myCommand.Connection.Open();
				myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			catch (Exception e)
			{
				ShowExp(sc, e);
				return false;
			}

		}
		ok = "Done";

		return true;
	}
	string getPONUMBER(string id)
	{
		int rows = 0;
		if (dst.Tables["existpo"] != null)
			dst.Tables["existpo"].Clear();
		string sc = " SELECT po_number FROM orders WHERE id=" + id;
		try
		{
			SqlDataAdapter myCommand = new SqlDataAdapter(sc, myConnection);
			rows = myCommand.Fill(dst, "existpo");
		}
		catch (Exception e)
		{
			ShowExp(sc, e);
			return "";
		}
		if (rows < 0)
			return "";
		return dst.Tables["existpo"].Rows[0]["po_number"].ToString();
	}
	void printForm()
	{
		Response.Write("<form action=eponumber.aspx?" + orderId + " method=post>");
		Response.Write("<table align=center width=100% cellspacing=0 cellpadding=1 border=1 bordercolor=#83CCF6 bgcolor=white");
		Response.Write(" style=\"font-family:Verdana;font-size:8pt;border-width:1px;border-style:Solid;border-collapse:collapse;fixed\">");
		Response.Write(" <tr><td><input type=text name=po_number size=20 value=" + getPONUMBER(orderId) + "><input type=submit name=cmd value=Save>&nbsp;<b style=\"color:red\">" + ok + "</b> </td></tr>");
		Response.Write(" </table>");
	}
}