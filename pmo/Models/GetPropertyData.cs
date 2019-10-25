using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pmo.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace pmo.Models
{
    public class GetPropertyData
    {
        public List<PropertyIndex> GetAllProperty()
        {
            List<PropertyIndex> plist = new List<PropertyIndex>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT PLM.Estate_ID, PLM.PropertyID, PM.Builder, PM.ProjectName, PLM.SuperArea, PLM.StartPrice, PTM.PropertyType, LM.location FROM [PropertyListMaster] PLM, ProjectMaster PM, PropertytypeMaster PTM, LocationMaster LM Where PLM.ProjectID=PM.ProjectID and PLM.PTID=PTM.PTID and PM.LocationID=LM.LocationID and Sales_Status='N' and PLM.ViewStatus=1 Order By ProjectName, Startprice", conn);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            foreach (DataRow drow in dt.Rows)
            {
                string mEstate_Id = drow["Estate_Id"].ToString();
                int mPropertyID = (int)drow["PropertyID"];
                string mBuilder = (string)drow["Builder"];
                string mFlatBHK = (string)drow["Propertytype"];
                string mLocation = (string)drow["Location"];
                string mProjectName = (string)drow["ProjectName"];
                string mSizeArea = (string)drow["SuperArea"];
                string mStartPrice = drow["StartPrice"].ToString();
                //string mPType = (string)drow["Type"];
                //string mValueIn = (string)drow["ValueIN"];

                plist.Add((new PropertyIndex { Estate_Id = mEstate_Id, PropertyID = mPropertyID, Builder = mBuilder, FlatBHK = mFlatBHK, Location = mLocation, ProjectName = mProjectName, SizeArea = mSizeArea, StartPrice = mStartPrice }));
            }
            return plist;
        }

        public List<PropertyIndex> GetAllProperty(SearchModel scm)
        {
            List<PropertyIndex> plist = new List<PropertyIndex>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            string query = "";
            string property = "", location = "", projectage="", budget="";
            if(scm.Property!=null)
            {
                property = scm.Property;
            }
            else
            {
                property = "";
            }
            if (scm.Location!=null)
            {
                location = scm.Location;
            }
            else
            {
                location = "";
            }

            //if (scm.ProjectAge.Trim().Length > 0)
            //{
            //    projectage = scm.ProjectAge;
            //}
            //else
            //{
            //    projectage = "";
            //}
            
            if (scm.Budget!=null)
            {
                budget = scm.Budget;
            }
            else
            {
                budget = "";
            }
            SqlCommand cmd = new SqlCommand("SELECT PLM.Estate_ID, PLM.PropertyID, PM.Builder, PM.ProjectName, PLM.SuperArea, PLM.CarpetArea, PLM.StartPrice, PTM.PropertyType, LM.location FROM [PropertyListMaster] PLM, ProjectMaster PM, PropertytypeMaster PTM, LocationMaster LM, BudgetMaster BM Where PLM.ProjectID=PM.ProjectID and PLM.PTID=PTM.PTID and PLM.LocationID=LM.LocationID and Sales_Status='N' and PLM.ViewStatus=1 and PLM.BudgetID=BM.budgetID and (ltrim(rtrim(PTM.PropertyType))=@PropertyType and ltrim(rtrim(LM.Location))=@Location and ltrim(rtrim(BM.budget))=@Budget) Order By ProjectName, Startprice", conn);
            cmd.Parameters.Add(new SqlParameter("@PropertyType", SqlDbType.NVarChar)).Value = property.Trim();
            cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.NVarChar)).Value = location.Trim();
            cmd.Parameters.Add(new SqlParameter("@Budget", SqlDbType.NVarChar)).Value = budget.Trim();
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            foreach (DataRow drow in dt.Rows)
            {
                string mEstate_Id = drow["Estate_Id"].ToString();
                int mPropertyID = (int)drow["PropertyID"];
                string mBuilder = (string)drow["Builder"];
                string mFlatBHK = (string)drow["Propertytype"];
                string mLocation = (string)drow["Location"];
                string mProjectName = (string)drow["ProjectName"];
                string mSizeArea = (string)drow["SuperArea"];
                string mStartPrice = drow["StartPrice"].ToString();
                //string mPType = (string)drow["Type"];
                //string mValueIn = (string)drow["ValueIN"];

                plist.Add((new PropertyIndex { Estate_Id = mEstate_Id, PropertyID = mPropertyID, Builder = mBuilder, FlatBHK = mFlatBHK, Location = mLocation, ProjectName = mProjectName, SizeArea = mSizeArea, StartPrice = mStartPrice }));
            }
            return plist;
        }
    }
}