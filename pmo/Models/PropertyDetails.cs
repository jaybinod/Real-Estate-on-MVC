using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace pmo.Models
{
    public class PropertyDetails
    {
        public string BedRooms { get; set; }
        public string BathRooms { get; set; }
        public string Balconies { get; set; }
        public string StoreRooms { get; set; }
        public string SuperArea { get; set; }
        public string CarpetArea { get; set; }
        public string FaceSide { get; set; }
        public string Status { get; set; }
        public string PropertyType { get; set; }
        public string floor { get; set; }
        public string CarParking { get; set; }
        //public string ProjectName { get; set; }
        //public string Location { get; set; }
        //public string Builder { get; set; }
        //public string TotalBuilding { get; set; }
        //public string LiftinEachBuilding { get; set; }
        //public string TotalFlatinProject { get; set; }
        
    }

    public class PropertyDetailsAll
    {
        public bool ViewStatus { get; set; }
        [Display(Name="ID")]
        public int PropertyID { get; set; }
        public string BedRooms { get; set; }
        public string BathRooms { get; set; }
        public string Balconies { get; set; }
        public string StoreRooms { get; set; }
        [Display(Name = "Area")]
        public string SuperArea { get; set; }
        public string CarpetArea { get; set; }
        public string FaceSide { get; set; }
        [Display(Name = "Possession")]
        public string Possession { get; set; }
        public int PTID { get; set; }
        [Display(Name = "Type")]
        public string PropertyType { get; set; }
        public string Floor { get; set; }
        [Display(Name = "Car Parking")]
        public string CarParking { get; set; }
        public int ProjectID { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public string Location { get; set; }
        public string Builder { get; set; }
        
        //public string Age { get; set; }
        [Display(Name="Total Building")]
        public int TotalBuilding { get; set; }
        [Display(Name = "Lift in Each Building")]
        public int LiftinEachBuilding { get; set; }
        [Display(Name = "Total Flat in Project")]
        public int TotalFlatinProject { get; set; }
        [Display(Name = "Start Price")]
        public string StartPrice { get; set; }
        public string Budget { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime CreatedDate { get; set; }
        public List<PropertyDetailsAll> Property { get; set; }
    }

    public class PropertyDetailsEdit
    {
        [Display(Name="View Status")]
        public bool ViewStatus { get; set; }
        public int PropertyID { get; set; }
        public string BedRooms { get; set; }
        public string BathRooms { get; set; }
        public string Balconies { get; set; }
        public string StoreRooms { get; set; }
        [Required]
        public string SuperArea { get; set; }
        [Required]
        public string CarpetArea { get; set; }
        [Required]
        public string FaceSide { get; set; }
        [Required]
        [Display(Name = "Possession")]
        public string Possession { get; set; }
        [Required]
        public int PTID { get; set; }
        [Display(Name="Property")]
        public string PropertyType { get; set; }
        [Required]
        public string Floor { get; set; }
        [Required]
        public string CarParking { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public string StartPrice { get; set; }
        [Required]
        public int BudgetID { get; set; }
        [Display (Name = "Budget Range")]
        public string Budget { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<PropertyDetailsEdit> PropertyEdit { get; set; }
    }

    public class PropertyImageUpload
    {
        public int PropertyID { get; set; }
        [Display(Name = "Project Name")]
        public string property { get; set; }
        public HttpPostedFileBase user_image_data { get; set; }
        //public string ImageName { get; set; }
    }

    public class PropertyDetailsAction
    {
        public List<PropertyDetailsAll> GetAllPropertyList()
        {
            List<PropertyDetailsAll> plist = new List<PropertyDetailsAll>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * from PropertyListMaster PLM, ProjectMaster PM, LocationMaster LM, PropertyTypeMaster PTM, BudgetMaster BM Where PLM.ProjectID=PM.ProjectID and PLM.PTID=PTM.PTID and PLM.BudgetID=BM.BudgetID and PM.LocationID=LM.LocationID order by ProjectName", conn);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            foreach (DataRow drow in dt.Rows)
            {
                bool mViewStatus = bool.Parse(drow["ViewStatus"].ToString());
                int mPropertyID= int.Parse(drow["PropertyID"].ToString());
                int mLocationID = (int)drow["LocationID"];
                string mLocation = (string)drow["Location"];
                string mProjectName = (string)drow["ProjectName"];
                string mBuilder = (string)drow["Builder"];
                int mTotalBuilding = int.Parse(drow["TotalBuilding"].ToString());
                int mLiftInEachBuilding = int.Parse(drow["LiftInEachBuilding"].ToString());
                int mTotalFlatInProject = int.Parse(drow["TotalFlatInProject"].ToString());
                string mbedrooms = drow["Bedrooms"].ToString();
                string mbathrooms = drow["bathrooms"].ToString();
                string mBalconies = drow["Balconies"].ToString();
                string mStoreRooms = drow["StoreRooms"].ToString();
                string mSuperArea = drow["SuperArea"].ToString();
                string mCarpetArea = drow["CarpetArea"].ToString();
                string mFaceSide = drow["FaceSide"].ToString();
                string mstatus = drow["Possession"].ToString();
                string mPropertyType = drow["PropertyType"].ToString();
                string mFloor = drow["Floor"].ToString();
                string mcarParking = drow["carParking"].ToString();
                DateTime mdate = DateTime.Parse(drow["Entry_Date"].ToString());
                
                string mbudget = drow["Budget"].ToString();
                //double mstartprice=0;
                //try{
                    string mstartprice = drow["StartPrice"].ToString();
                //}
                //catch{}
                plist.Add((new PropertyDetailsAll { PropertyID = mPropertyID, ViewStatus=mViewStatus,  CreatedDate=mdate, StartPrice = mstartprice, Budget = mbudget, Builder = mBuilder, LiftinEachBuilding = mLiftInEachBuilding, TotalBuilding = mTotalBuilding, TotalFlatinProject = mTotalFlatInProject, Location = mLocation, ProjectName = mProjectName, Balconies = mBalconies, BathRooms = mbathrooms, BedRooms = mbedrooms, CarParking = mcarParking, CarpetArea = mCarpetArea, Floor = mFloor, FaceSide = mFaceSide, PropertyType = mPropertyType, Possession = mstatus, StoreRooms = mStoreRooms, SuperArea = mSuperArea }));
            }
            return plist;
        }
    
        public List<PropertyDetailsEdit> GetAllPropertyEdit(int ID)
        {
            List<PropertyDetailsEdit> plist = new List<PropertyDetailsEdit>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * from PropertyListMaster PLM, ProjectMaster PM, LocationMaster LM, PropertyTypeMaster PTM, BudgetMaster BM Where PLM.ProjectID=PM.ProjectID and PLM.PTID=PTM.PTID and PLM.BudgetID=BM.BudgetID and PM.LocationID=LM.LocationID and PLM.PropertyID="+ID, conn);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            if(dt.Rows.Count>0)
            {
                DataRow drow = dt.Rows[0];
                bool mViewStatus = bool.Parse(drow["ViewStatus"].ToString());
                int mPropertyID = int.Parse(drow["PropertyID"].ToString());
                int mLocationID = (int)drow["LocationID"];
                string mLocation = (string)drow["Location"];
               int mProjectID = int.Parse(drow["ProjectID"].ToString());
                string mProjectName = (string)drow["ProjectName"];
                string mBuilder = (string)drow["Builder"];
                int mTotalBuilding = int.Parse(drow["TotalBuilding"].ToString());
                int mLiftInEachBuilding = int.Parse(drow["LiftInEachBuilding"].ToString());
                int mTotalFlatInProject = int.Parse(drow["TotalFlatInProject"].ToString());
                string mbedrooms = drow["Bedrooms"].ToString();
                string mbathrooms = drow["bathrooms"].ToString();
                string mBalconies = drow["Balconies"].ToString();
                string mStoreRooms = drow["StoreRooms"].ToString();
                string mSuperArea = drow["SuperArea"].ToString();
                string mCarpetArea = drow["CarpetArea"].ToString();
                string mFaceSide = drow["FaceSide"].ToString();
                string mstatus = drow["Possession"].ToString();
                int mPTID = int.Parse(drow["PTID"].ToString());
                string mPropertyType = drow["PropertyType"].ToString();
                string mFloor = drow["Floor"].ToString();
                string mcarParking = drow["carParking"].ToString();
                int mbudgetID = int.Parse(drow["budgetId"].ToString());
                string mbudget = drow["Budget"].ToString();
                //double mstartprice = 0;
                //try
                //{
                   string mstartprice = drow["StartPrice"].ToString();
                //}
                //catch { }
                plist.Add((new PropertyDetailsEdit { PropertyID = mPropertyID, ViewStatus=mViewStatus, PTID = mPTID, ProjectID = mProjectID, StartPrice = mstartprice, BudgetID = mbudgetID, Budget = mbudget, ProjectName = mProjectName, Balconies = mBalconies, BathRooms = mbathrooms, BedRooms = mbedrooms, CarParking = mcarParking, CarpetArea = mCarpetArea, Floor = mFloor, FaceSide = mFaceSide, PropertyType = mPropertyType, Possession = mstatus, StoreRooms = mStoreRooms, SuperArea = mSuperArea }));
            }
            return plist;
        }

        public bool UpdateProperty(PropertyDetailsEdit Prop)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            SqlCommand cmdid = new SqlCommand("Select * from ProjectMaster PM, LocationMaster LM where PM.LocationID=LM.LocationID and ProjectID="+Prop.ProjectID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader dr;
            dr = cmdid.ExecuteReader();
            int projectID = 0;
            string mLocation = "";
            string mproject = "";
            if (dr.Read())
            {
                projectID = int.Parse(dr["ProjectID"].ToString());
                mLocation = dr["Location"].ToString().Trim();
                mproject = dr["ProjectName"].ToString().Trim();
            }
            dr.Close();
            //projectID++;
            SqlCommand cmdPT = new SqlCommand("Select * from PropertytypeMaster where PTID=" + Prop.PTID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drL;
            drL = cmdPT.ExecuteReader();
            string PropertyT = "";
            if (drL.Read())
            {
                PropertyT = drL["PropertyType"].ToString();
            }
            drL.Close();

            string propertyIDLink = mproject + "-" + mLocation +" "+PropertyT+ " " + Prop.PropertyID;
            propertyIDLink = propertyIDLink.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            propertyIDLink = propertyIDLink.Replace(" ", "-");

            SqlCommand cmd = new SqlCommand("Update PropertyListMaster set ViewStatus=@ViewStatus, Estate_ID=@Estate_ID, ProjectID=@ProjectID, PTID=@PTID, Possession=@Possession, StartPrice=@StartPrice, BedRooms=@BedRooms, BathRooms=@BathRooms, Balconies=@Balconies, StoreRooms=@StoreRooms, SuperArea=@SuperArea, CarpetArea=@CarpetArea, FaceSide=@FaceSide, Floor=@Floor, CarParking=@CarParking where PropertyID=" + Prop.PropertyID, conn);

            cmd.Parameters.Add(new SqlParameter("@Estate_ID", SqlDbType.NVarChar, propertyIDLink.Trim().Length)).Value = propertyIDLink.Trim();
            cmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.Int)).Value = Prop.ProjectID;
            cmd.Parameters.Add(new SqlParameter("@PTID", SqlDbType.Int)).Value = Prop.PTID;
            cmd.Parameters.Add(new SqlParameter("@StartPrice", SqlDbType.NVarChar, Prop.StartPrice.ToString().Trim().Length)).Value = Prop.StartPrice.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@BedRooms", SqlDbType.NVarChar, Prop.BedRooms.ToString().Trim().Length)).Value = Prop.BedRooms.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@BathRooms", SqlDbType.NVarChar, Prop.BathRooms.ToString().Trim().Length)).Value = Prop.BathRooms.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@Balconies", SqlDbType.NVarChar, Prop.Balconies.ToString().Trim().Length)).Value = Prop.Balconies.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@StoreRooms", SqlDbType.NVarChar, Prop.StoreRooms.ToString().Trim().Length)).Value = Prop.StoreRooms.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@SuperArea", SqlDbType.NVarChar, Prop.SuperArea.ToString().Trim().Length)).Value = Prop.SuperArea.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@CarpetArea", SqlDbType.NVarChar, Prop.CarpetArea.ToString().Trim().Length)).Value = Prop.CarpetArea.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@FaceSide", SqlDbType.NVarChar, Prop.FaceSide.ToString().Trim().Length)).Value = Prop.FaceSide.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@Floor", SqlDbType.NVarChar, Prop.Floor.ToString().Trim().Length)).Value = Prop.Floor.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@CarParking", SqlDbType.NVarChar, Prop.CarParking.ToString().Trim().Length)).Value = Prop.CarParking.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@Possession", SqlDbType.NVarChar, Prop.Possession.ToString().Trim().Length)).Value = Prop.Possession.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@ViewStatus", SqlDbType.Bit)).Value = Prop.ViewStatus;

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool insertProperty(PropertyDetailsEdit Prop)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            
            SqlCommand cmdid = new SqlCommand("Select top 1 * from PropertyListMaster order by PropertyID desc", conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader dr;
            dr = cmdid.ExecuteReader();
            int propertyID = 0;
            if (dr.Read())
            {
                propertyID = int.Parse(dr["PropertyID"].ToString());
            }
            dr.Close();
            propertyID++;

            SqlCommand cmdidp = new SqlCommand("Select * from ProjectMaster PM, LocationMaster LM where PM.LocationID=LM.LocationID and ProjectID=" + Prop.ProjectID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drp;
            drp = cmdidp.ExecuteReader();
            //int projectID = 0;
            string mLocation = "";
            string mproject = "";
            if (drp.Read())
            {
               // projectID = int.Parse(dr["ProjectID"].ToString());
                mLocation = drp["Location"].ToString().Trim();
                mproject = drp["ProjectName"].ToString().Trim();
            }
            drp.Close();
            //projectID++;
            SqlCommand cmdPT = new SqlCommand("Select * from PropertytypeMaster where PTID=" + Prop.PTID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drL;
            drL = cmdPT.ExecuteReader();
            string PropertyT = "";
            if (drL.Read())
            {
                PropertyT = drL["PropertyType"].ToString();
            }
            drL.Close();

            string propertyIDLink = mproject + "-" + mLocation + " " + PropertyT + " " + propertyID;
            propertyIDLink = propertyIDLink.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            propertyIDLink = propertyIDLink.Replace(" ", "-");

            SqlCommand cmd = new SqlCommand("Insert into PropertyListMaster (Estate_ID, Member_ID,ProjectID, PTID, BudgetID, Possession, StartPrice, BedRooms, BathRooms, Balconies, StoreRooms, SuperArea, CarpetArea, FaceSide, Floor, CarParking,PropertyID, Entry_date,Sales_Status) Values(@Estate_ID, 0, @ProjectID, @PTID, @BudgetID, @Possession, @StartPrice, @BedRooms, @BathRooms, @Balconies, @StoreRooms, @SuperArea, @CarpetArea, @FaceSide, @Floor, @CarParking, @PropertyID,@Created_Date,'N')", conn);

            cmd.Parameters.Add(new SqlParameter("@PropertyID", SqlDbType.Int)).Value = propertyID;
            cmd.Parameters.Add(new SqlParameter("@Estate_ID", SqlDbType.NVarChar, propertyIDLink.Trim().Length)).Value = propertyIDLink.Trim();
            cmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.Int)).Value = Prop.ProjectID;
            cmd.Parameters.Add(new SqlParameter("@PTID", SqlDbType.Int)).Value = Prop.PTID;
            cmd.Parameters.Add(new SqlParameter("@BudgetID", SqlDbType.Int)).Value = Prop.BudgetID;
            cmd.Parameters.Add(new SqlParameter("@StartPrice", SqlDbType.NVarChar, Prop.StartPrice.ToString().Trim().Length)).Value = Prop.StartPrice.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@BedRooms", SqlDbType.NVarChar, Prop.BedRooms.ToString().Trim().Length)).Value = Prop.BedRooms.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@BathRooms", SqlDbType.NVarChar, Prop.BathRooms.ToString().Trim().Length)).Value = Prop.BathRooms.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@Balconies", SqlDbType.NVarChar, Prop.Balconies.ToString().Trim().Length)).Value = Prop.Balconies.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@StoreRooms", SqlDbType.NVarChar, Prop.StoreRooms.ToString().Trim().Length)).Value = Prop.StoreRooms.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@SuperArea", SqlDbType.NVarChar, Prop.SuperArea.ToString().Trim().Length)).Value = Prop.SuperArea.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@CarpetArea", SqlDbType.NVarChar, Prop.CarpetArea.ToString().Trim().Length)).Value = Prop.CarpetArea.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@FaceSide", SqlDbType.NVarChar, Prop.FaceSide.ToString().Trim().Length)).Value = Prop.FaceSide.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@Floor", SqlDbType.NVarChar, Prop.Floor.ToString().Trim().Length)).Value = Prop.Floor.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@CarParking", SqlDbType.NVarChar, Prop.CarParking.ToString().Trim().Length)).Value = Prop.CarParking.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@Possession", SqlDbType.NVarChar, Prop.Possession.ToString().Trim().Length)).Value = Prop.Possession.ToString().Trim();
            cmd.Parameters.Add(new SqlParameter("@Created_Date", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy/MM/dd");
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
    }
}