using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace pmo.Models
{
    public class PropertyImage
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public string Active { get; set; }
        

        public static List<PropertyImage> GetImages(int PropertyID)
        {
            List<PropertyImage> PImages = new List<PropertyImage>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            SqlCommand cmd = new SqlCommand("Select * from PropertyImage where PropertyID=" + PropertyID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int act = 0;
            while(dr.Read())
            {
                int imgid =int.Parse(dr["ImageID"].ToString());
                
                if(act==0)
                    PImages.Add(new PropertyImage { Active = "active", ImageName=dr["ImageName"].ToString(), ImageID=imgid });
                else
                    PImages.Add(new PropertyImage { Active = "", ImageName = dr["ImageName"].ToString(), ImageID = imgid });
                
                act++;
            }
            dr.Close();
            conn.Close();
            return PImages;
        }

        public DataTable GetImagesList(int PropertyID)
        {
            //List<PropertyImage> PImages = new List<PropertyImage>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from PropertyImage where PropertyID=" + PropertyID, conn);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            //SqlDataReader dr = cmd.ExecuteReader();
            //int act = 0;
            //while (dr.Read())
            //{
            //    if (act == 0)
            //        PImages.Add(new PropertyImage { Active = "active", ImageName = dr["ImageName"].ToString() });
            //    else
            //        PImages.Add(new PropertyImage { Active = "", ImageName = dr["ImageName"].ToString() });

            //    act++;
            //}
            //dr.Close();
            //conn.Close();
            return dt;
        }
    }
}