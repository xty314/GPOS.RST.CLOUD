using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class v2_cat : AdminBasePage
{
	DataSet dsc = new DataSet();    //DataSet cache for code_relations and product_drop
	DataSet dst = new DataSet();    //for creating Temp talbes templated on an existing sql table
	DataSet dsv = new DataSet();
	//DataRow[] drs;	//for sorting
	string m_l;
	string m_action;
	string m_show;
	string m_q;
	string m_c;
	string m_s;
	string m_ss;
	string m_co = "-1";
	string m_so = "-1";
	string m_sso = "-1";

	string m_new_dept;
	string m_new_cat;
	string m_new_scat;
	string m_new_sscat;
	protected void Page_Load(object sender, EventArgs e)
    {
		TS_PageLoad(); //do common things, LogVisit etc...

		if (!SecurityCheck("manager"))
			return;

		GetQueryStrings();

		if (Request.QueryString["cmd"] == "updateCat")
		{
			string oper = Request.Form["oper"];

			if (oper == "edit")
			{
				string c_old = Request.Form["oldCat"];
				string c_new = Request.Form["cat"];
				string c_sold = Request.Form["oldSCat"];
				string c_snew = Request.Form["scat"];
				string c_ssold = Request.Form["oldSSCat"];
				string c_ssnew = Request.Form["sscat"];
				if (c_snew == "" && c_ssnew != "" && c_new != "")
				{
					if (UpdateCatalogForAll(c_old, c_sold, c_ssold, c_new, c_ssnew, ""))
					{
						Response.Write("<meta http-equiv=\"refresh\" content=\"1; URL=cat.aspx?\">");
					}
				}
				else
				{
					if (UpdateCatalogForAll(c_old, c_sold, c_ssold, c_new, c_snew, c_ssnew))
					{
						Response.Write("<meta http-equiv=\"refresh\" content=\"1; URL=cat.aspx?\">");

					}
				}
			}
			else if (oper == "del")
			{
				DoDelete();
				Response.Write("<meta http-equiv=\"refresh\" content=\"1; URL=cat.aspx?\">");
			}
			else
			{

			}





		}


		if (Request.Form["cmd"] == "Update Category")
		{
			if (UpdateCatalogFromCodeRelations())
			{
				Response.Write("<br><br><br><center><h2>" + Lang("Update Catalog successfully, please wait a moment") + "</h2><br>");

				Response.Write("<meta http-equiv=\"refresh\" content=\"2; URL=cat.aspx?\">");
				Response.End();
			}
		}
		if (Request.Form["cmd"] == "addCategory")
		{
			if (m_new_cat == "" || m_new_cat == null)
			{
				Response.Write("<script language=javascript>window.alert('First Category cannot be empty!!!');</script");
				Response.Write(">");
			}
			else if (CheckCategory(m_new_cat, m_new_scat, m_new_sscat))
			{
				Response.Write("<script language=javascript>window.alert('Category Exists!!!');</script");
				Response.Write(">");
			}
			else
			{
				InsertNewCategory(m_new_cat, m_new_scat, m_new_sscat);
				Response.Write("<script language=javascript>window.alert('Category " + m_new_cat + "/ " + m_new_scat + "/ " + m_new_sscat + " Add!!!');</script");
				Response.Write(">");
			}
			// Response.Write("<meta http-equiv=\"refresh\" content=\"0; URL=catalog.aspx\">");
		}
	}
  
	Boolean UpdateCatalogForAll(string catValueOld, string scatValueOld, string sscatValueOld, string catValue, string scatValue, string sscatValue)
	{
		string sc = "";
		string level = "ss";
		if (catValueOld != "" && scatValueOld == "" && sscatValueOld == "")
		{
			level = "c";
		}
		else if (catValueOld != "" && scatValueOld != "" && sscatValueOld == "")
		{
			level = "s";
		}
		else
		{
			level = "ss";
		}


		if (level == "c")
		{
			sc += " Update catalog SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " WHERE 1 = 1";
			sc += " AND cat = N'" + catValueOld + "'";
			sc += " UPDATE code_relations SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " WHERE 1=1 ";
			sc += " AND cat = N'" + catValueOld + "'";
			sc += " UPDATE product SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " WHERE 1=1 ";
			sc += " AND cat = N'" + catValueOld + "'";
		}
		else if (level == "s")
		{
			sc += " Update catalog SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " ,s_cat = N'" + scatValue + "'";
			sc += " WHERE 1 = 1";
			sc += " AND s_cat = N'" + scatValueOld + "'";
			sc += " AND cat = N'" + catValueOld + "'";

			sc += " UPDATE code_relations SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " ,s_cat = N'" + scatValue + "'";
			sc += " WHERE 1=1 ";
			sc += " AND cat = N'" + catValueOld + "'";
			sc += " AND s_cat = N'" + scatValueOld + "'";
			sc += " UPDATE product SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " ,s_cat = N'" + scatValue + "'";
			sc += " WHERE 1=1 ";
			sc += " AND cat = N'" + catValueOld + "'";
			sc += " AND s_cat = N'" + scatValueOld + "'";
		}
		else if (level == "ss")
		{
			sc += " Update catalog SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " ,s_cat = N'" + scatValue + "'";
			sc += " ,ss_cat = N'" + sscatValue + "'";
			sc += " WHERE 1 = 1";
			sc += " AND cat = N'" + catValueOld + "'";
			sc += " AND s_cat = N'" + scatValueOld + "'";
			sc += " AND ss_cat = N'" + sscatValueOld + "'";
			sc += " UPDATE code_relations SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " ,s_cat = N'" + scatValue + "'";
			sc += " ,ss_cat = N'" + sscatValue + "'";
			sc += " WHERE 1=1 ";
			sc += " AND cat = N'" + catValueOld + "'";
			sc += " AND s_cat = N'" + scatValueOld + "'";
			sc += " AND ss_cat = N'" + sscatValueOld + "'";
			sc += " UPDATE product SET ";
			sc += " cat = N'" + catValue + "'";
			sc += " ,s_cat = N'" + scatValue + "'";
			sc += " ,ss_cat = N'" + sscatValue + "'";
			sc += " WHERE 1=1 ";
			sc += " AND cat = N'" + catValueOld + "'";
			sc += " AND s_cat = N'" + scatValueOld + "'";
			sc += " AND ss_cat = N'" + sscatValueOld + "'";
		}
		// return false;
		try
		{
			myCommand = new SqlCommand(sc);
			myCommand.Connection = myConnection;
			myConnection.Open();
			myCommand.ExecuteNonQuery();
			myCommand.Connection.Close();
		}
		catch (Exception e)
		{
			ShowExp(sc, e);
			return false;
		}
		return true;
	}
	bool UpdateCatalogFromCodeRelations()
	{
		string sc = "Begin Transaction ";
		// sc +=	" Delete from catalog where 1=1 ";
		// sc +=	" Delete from catalog where cat = '' or cat is null ";
		// sc += " Insert into catalog(seq, cat, s_cat,ss_cat) select distinct 99, cat, s_cat, ss_cat from code_relations where 1=1 and cat != '' and cat is not null"; 
		// sc+=@"  INSERT INTO catalog (seq, cat, s_cat,ss_cat)
		// select '99', cat,scat,sscat from
		// (select distinct cat,'' as 'scat',''as 'sscat'   from code_relations where cat !=''
		// union
		// select distinct cat,s_cat as 'scat',''as 'sscat'   from code_relations where s_cat !=''
		// union
		// select distinct cat,s_cat as 'scat',ss_cat as 'sscat'   from code_relations where s_cat !=''and ss_cat!='') as t";
		sc += " UPDATE catalog set s_cat='' where s_cat='zzzOthers' and cat!='Brands' ";
		sc += " UPDATE catalog set ss_cat='' where ss_cat='zzzOthers' and cat!='Brands' ";
		sc += @"MERGE INTO catalog as c
			USING 
					(select distinct cat,'' as 'scat',''as 'sscat'   from code_relations where cat !=''
					union
					select distinct cat,s_cat as 'scat',''as 'sscat'   from code_relations where s_cat !=''
					union
					select distinct cat,s_cat as 'scat',ss_cat as 'sscat'   from code_relations where s_cat !=''and ss_cat!='') as u
			ON (c.cat=u.cat and c.s_cat=u.scat and c.ss_cat=u.sscat)
			WHEN NOT matched THEN 
			insert values('99',u.cat,u.scat,u.sscat);";
		sc += " Insert into catalog(seq, cat, s_cat,ss_cat) select distinct 1, 'Brands' , brand,'zzzOthers' from code_relations where 1=1 and brand != '' and brand is not null";
		sc += " Commit ";
		try
		{
			myCommand = new SqlCommand(sc);
			myCommand.Connection = myConnection;
			myConnection.Open();
			myCommand.ExecuteNonQuery();
			myCommand.Connection.Close();
		}
		catch (Exception e)
		{
			ShowExp(sc, e);
			return false;
		}
		return true;
	}
	void GetQueryStrings()
	{
		m_action = Request.QueryString["a"];
		m_show = Request.QueryString["sh"];

		m_q = Request.QueryString["q"];
		m_c = Request.QueryString["c"];
		m_s = Request.QueryString["s"];
		m_ss = Request.QueryString["ss"];

		if (Request.QueryString["co"] != null)
			m_co = Request.QueryString["co"];
		if (Request.QueryString["so"] != null)
			m_so = Request.QueryString["so"];
		if (Request.QueryString["sso"] != null)
			m_sso = Request.QueryString["sso"];

		m_new_dept = Request.Form["new_depart"];
		m_new_cat = Request.Form["new_cat"];
		m_new_scat = Request.Form["new_scat"];
		m_new_sscat = Request.Form["new_sscat"];
	}
	Boolean DoDelete()
	{
		string sc = "Begin Transaction ";

		string cat = Request.Form["cat"];

		string scat = Request.Form["scat"];

		string sscat = Request.Form["sscat"];
		sc += " DELETE FROM catalog WHERE 1 =1 ";
		sc += " AND cat = N'" + cat + "'";
		sc += " AND s_cat = N'" + scat + "'";
		sc += " AND ss_cat = N'" + sscat + "'";

		//update code_relations
		sc += " UPDATE code_relations SET ";
		sc += " cat ='', s_cat = '', ss_cat = ''";
		sc += " WHERE 1 =1 ";
		sc += " AND cat = N'" + cat + "'";
		sc += " AND s_cat = N'" + scat + "'";
		sc += " AND ss_cat = N'" + sscat + "'";

		//update product
		sc += " UPDATE product SET ";
		sc += " cat ='', s_cat = '', ss_cat = ''";
		sc += " WHERE 1 =1 ";
		sc += " AND cat = N'" + cat + "'";
		sc += " AND s_cat = N'" + scat + "'";
		sc += " AND ss_cat = N'" + sscat + "'";

		sc += " Commit ";
		try
		{
			myCommand = new SqlCommand(sc);
			myCommand.Connection = myConnection;
			myConnection.Open();
			myCommand.ExecuteNonQuery();
			myCommand.Connection.Close();
		}
		catch (Exception e)
		{
			ShowExp(sc, e);
			return false;
		}

		return true;

	}
	bool CheckCategory(string cat, string scat, string sscat)
	{
		string sc = "";
		int rows = 0;


		if (dst.Tables["CheckCategory"] != null)
			dst.Tables["CheckCategory"].Clear();
		sc = " SELECT * FROM catalog WHERE 1=1 ";
		sc += " AND seq = 99 ";
		sc += " AND cat = N'" + cat + "'";
		sc += " AND s_cat = N'" + scat + "'";
		sc += " AND ss_cat = N'" + sscat + "'";
		try
		{
			myAdapter = new SqlDataAdapter(sc, myConnection);
			rows = myAdapter.Fill(dst, "CheckCategory");
		}
		catch (Exception ex)
		{
			ShowExp(sc, ex);
			return true;
		}
		if (rows > 0)
			return true;
		else
			return false;
	}
	void InsertNewCategory(string cat, string scat, string sscat)
	{
		// if(scat == ""|| scat== null)
		//     scat = "zzzOthers";
		// if(sscat == "")
		//     sscat = "zzzOthers";
		if (cat == "" || cat == null)
			return;
		string sc = "";
		sc = " INSERT INTO catalog (seq, cat, s_cat, ss_cat)VALUES(";
		sc += "'99', N'" + cat + "', N'" + scat + "', N'" + sscat + "'";
		sc += ")";
		//DEBUG("sc",sc);
		//return ;	
		try
		{
			myCommand = new SqlCommand(sc);
			myCommand.Connection = myConnection;
			myConnection.Open();
			myCommand.ExecuteNonQuery();
			myCommand.Connection.Close();
		}
		catch (Exception e)
		{
			ShowExp(sc, e);
			return;
		}
	}
}