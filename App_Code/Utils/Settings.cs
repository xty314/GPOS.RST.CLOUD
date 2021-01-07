using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Settings 的摘要说明
/// </summary>
public  class Settings
{

    /// <summary>
    /// reading configuration from /appSettings.json
    /// </summary>
    /// <param name="key"> the "key" in the appSettings.json</param>
    /// <returns></returns>
    public static  string GetSetting(string key)
    {
        string appSettings = System.Web.HttpContext.Current.Server.MapPath("~/") + "/appSettings.json";

        using (System.IO.StreamReader file = System.IO.File.OpenText(appSettings))
        {
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o = (JObject)JToken.ReadFrom(reader);
                var value = o[key].ToString();
                return value;
            }
        }
    }
    /// <summary>
    /// get settings from database settings table
    /// </summary>
    /// <param name="settingName"></param>
    /// <returns></returns>
    public static Setting GetCompanySetting(string settingName)
    {
        string sc = "SELECT top 1 * FROM settings where name='" + settingName+"'";
     
        DBhelper dbhelper = new DBhelper();
        DataTable dt = dbhelper.ExecuteDataTable(sc);
        Setting setting = new Setting();
        if (dt.Rows.Count > 0)
        {
         setting =new Setting {
                name = (string)dt.Rows[0]["name"],
                cat = (string)dt.Rows[0]["cat"],
                description=(string)dt.Rows[0]["description"],
                hidden=(bool)dt.Rows[0]["hidden"],
                boolValue=(bool)dt.Rows[0]["bool_value"],
                value= (string)dt.Rows[0]["value"],
     
         };
        }
        else{
            throw new Exception("Setting："+settingName +" is not found.");
        }
      
      
        return setting;

    }
    public static Setting GetCompanySetting(string settingName,string settingCat)
    {
        string sc = "SELECT top 1 * FROM settings where name='" + settingName + "' and cat='"+settingCat+"'";

        DBhelper dbhelper = new DBhelper();
        DataTable dt = dbhelper.ExecuteDataTable(sc);
        Setting setting = new Setting();
        if (dt.Rows.Count > 0)
        {
            setting = new Setting
            {
                name = (string)dt.Rows[0]["name"],
                cat = (string)dt.Rows[0]["cat"],
                description = (string)dt.Rows[0]["description"],
                hidden = (bool)dt.Rows[0]["hidden"],
                boolValue = (bool)dt.Rows[0]["bool_value"],
                value = (string)dt.Rows[0]["value"],

            };
        }
        else
        {
            throw new Exception("Setting：" + settingName + "cat:"+settingCat+ " is not found.");
        }


        return setting;

    }
}