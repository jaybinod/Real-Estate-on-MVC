using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Helpers;
using pmo.Models;
using pmo.Filters;

//using System.Data.Linq;


namespace pmo.Controllers
{
    [UserAuthenticationFilter]
    public class pmoAdminController : Controller
    {
        //
        // GET: /pmoAdmin/
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);



        public ActionResult Home()
        {
            
                return View();
            
        }
        /// <summary>
        /// Project Action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProjectCreate()
        {
            CallList();
            return View();
        }
               
        [HttpPost]
        public ActionResult ProjectCreate(ProjectMasterModel PMM)
        {
            //MainPageModel data = new MainPageModel();
            //GetPropertyData propertydata = new GetPropertyData();
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectMasterModel pm = new ProjectMasterModel();

                    status = pm.InsertProject(PMM);


                    ModelState.Clear();
                }
                else
                {
             
                }
            }
            catch
            {

            }

            if (status == true)
            {
                CallList();
                //Contact();

                ViewBag.Message = "Data Saved Sucessfuly";
                return View();
            }
            else
            {
                CallList();
                ViewBag.Message = "Ah... somthing wrong, Please try after sometime";
                return View();
            }
        }

        public ActionResult ProjectList()
        {
            //GetPropertyData propertydata = new GetPropertyData();

            ProjectMasterModel data = new ProjectMasterModel();
            data.AllProjects = data.GetAllProjectList();
            return View(data);
        }

        [HttpGet]
        public ActionResult ProjectEdit(int id)
        {
            ProjectMasterModel data = new ProjectMasterModel();
            data.AllProjects = data.GetAllProjectList(id);
              
            CallList();
            return View(data);
        }

        [HttpPost]
        public ActionResult ProjectEdit(ProjectMasterModel PMM)
        {
            //MainPageModel data = new MainPageModel();
            //GetPropertyData propertydata = new GetPropertyData();
            int id = PMM.AllProjects[0].ProjectID;
            bool status = false;
            try
            {
                //if (ModelState.IsValid)
                //{
                    ProjectMasterModel pm = new ProjectMasterModel();

                    status = pm.UpdateProject(PMM);


                    ModelState.Clear();
                //}
                //else
                //{

                //}
            }
            catch
            {

            }

            if (status == true)
            {
                ProjectMasterModel data = new ProjectMasterModel();
                data.AllProjects = data.GetAllProjectList(id);
                CallList();
                //Contact();

                ViewBag.Message = "Data Saved Sucessfuly";
                return View(data);
            }
            else
            {
                ProjectMasterModel data = new ProjectMasterModel();
                data.AllProjects = data.GetAllProjectList(id);
                CallList();
                ViewBag.Message = "Ah... somthing wrong, Please try after sometime";
                return View(data);
            }
        }

        /// <summary>
        /// Property Action
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult PropertyList()
        {
            //GetPropertyData propertydata = new GetPropertyData();

            List<PropertyDetailsAll> data = new List<PropertyDetailsAll>();
            PropertyDetailsAction gpd = new PropertyDetailsAction();
            data = gpd.GetAllPropertyList();
            return View(data);
        }

        [HttpGet]
        public ActionResult PropertyEdit(int ID)
        {
            //GetPropertyData propertydata = new GetPropertyData();
            PropertyDetailsEdit datae = new PropertyDetailsEdit();
            PropertyDetailsAction gpd = new PropertyDetailsAction();
            datae.PropertyEdit = gpd.GetAllPropertyEdit(ID);
            CallListForPeoperty();
            return View(datae.PropertyEdit[0]);
        }

        [HttpPost]
        public ActionResult PropertyEdit(PropertyDetailsEdit PD)
        {
            PropertyDetailsEdit datae = new PropertyDetailsEdit();
            PropertyDetailsAction gpd = new PropertyDetailsAction();
            int id = PD.PropertyID;
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    PropertyDetailsAction pm = new PropertyDetailsAction();
                    status = pm.UpdateProperty(PD);
                    ModelState.Clear();
                }
                else{}
            }
            catch{}

            if (status == true)
            {
                datae.PropertyEdit = gpd.GetAllPropertyEdit(id);
                CallListForPeoperty();
                ViewBag.Message = "Data Saved Sucessfuly";
                return View(datae.PropertyEdit[0]);
            }
            else
            {
                datae.PropertyEdit = gpd.GetAllPropertyEdit(id);
                CallListForPeoperty();
                ViewBag.Message = "Ah... somthing wrong, Please try after sometime";
                return View(datae.PropertyEdit[0]);
            }
        }

        [HttpGet]
        public ActionResult PropertyCreate()
        {
            //GetPropertyData propertydata = new GetPropertyData();
            //PropertyDetailsEdit datae = new PropertyDetailsEdit();
            //PropertyDetailsAction gpd = new PropertyDetailsAction();
            //datae.PropertyEdit = gpd.GetAllPropertyEdit(ID);
            CallListForPeoperty();
            return View();
        }

        [HttpPost]
        public ActionResult PropertyCreate(PropertyDetailsEdit PD)
        {
            //int id = PD.PropertyID;
            bool status = false;
            try
            {
                //if (ModelState.IsValid)
                //{
                    PropertyDetailsAction pm = new PropertyDetailsAction();
                    status = pm.insertProperty(PD);
                    ModelState.Clear();
                //}
                //else { }
            }
            catch { }

            if (status == true)
            {
                
                CallListForPeoperty();
                ViewBag.Message = "Data Saved Sucessfuly";
                return View();
            }
            else
            {
                
                CallListForPeoperty();
                ViewBag.Message = "Ah... somthing wrong, Please try after sometime";
                return View();
            }
        }

        public ActionResult UploadImages(int id)
        {
            SqlCommand cmd = new SqlCommand("Select PTM.PropertyType, PM.ProjectName, LM.location, PM.Builder from PropertyListMaster PLM, PropertyTypeMaster PTM, ProjectMaster PM, LocationMaster LM where PLM.PTID=PTM.PTID and PLM.ProjectID=PM.ProjectID and PM.LocationID=LM.LocationID and PLM.PropertyID=" + id, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drm = cmd.ExecuteReader();
            string prop = "";
            if(drm.Read())
            {
                prop = drm["PropertyType"].ToString() + " | " + drm["ProjectName"].ToString() + " | " + drm["Location"].ToString() + " By " + drm["Builder"].ToString();
            }
            PropertyImageUpload imgfile = new PropertyImageUpload();
            imgfile.PropertyID = id;
            imgfile.property = prop;

            return View(imgfile);
        }
        [HttpPost]
        public ActionResult UploadImages(PropertyImageUpload imgp)
        {
            //create path to store in database
            string filename = imgp.PropertyID + "-" + imgp.user_image_data.FileName;

            //img.Save("path");

            //store image in folder
            imgp.user_image_data.SaveAs(Server.MapPath("~/images/PropertyImages") + "/" + filename);

            WebImage img = new WebImage(Server.MapPath("~/images/PropertyImages") + "/" + filename);
            if (img.Height > 724)
                img.Resize(1000, 724, false);

            img.Save(Server.MapPath("~/images/PropertyImages") + "/" + filename);

            //insert in database
            SqlCommand cmdfupd = new SqlCommand("Select * from PropertyImage where ImageName='" + filename + "'", conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlDataReader dru = cmdfupd.ExecuteReader();
            bool existing = false;
            if (dru.Read())
            {
                existing = true;
            }
            dru.Close();
            if (existing == false)
            {
                SqlCommand cmdf = new SqlCommand("Select top 1 * from PropertyImage order by ImageId desc", conn);
                SqlDataReader drf = cmdf.ExecuteReader();
                int ImageID = 0;
                if (drf.Read())
                {
                    ImageID = int.Parse(drf["ImageID"].ToString());
                }
                ImageID++;
                drf.Close();

                SqlCommand cmd = new SqlCommand("Insert into PropertyImage (ImageID, PropertyID, ImageName, ActiveView) Values(@ImageID, @PropertyID, @ImageName,0)", conn);
                cmd.Parameters.Add(new SqlParameter("@ImageID", SqlDbType.Int)).Value = ImageID;
                cmd.Parameters.Add(new SqlParameter("@PropertyID", SqlDbType.Int)).Value = imgp.PropertyID;
                cmd.Parameters.Add(new SqlParameter("@ImageName", SqlDbType.NVarChar, filename.Length)).Value = filename;
                cmd.ExecuteNonQuery();
            }
            PropertyImageUpload imgfile = new PropertyImageUpload();
            imgfile.PropertyID = imgp.PropertyID;

            return View(imgfile);

        }

        [HttpGet]
        public ActionResult DeleteImage(int ID)
        {
            //GetPropertyData propertydata = new GetPropertyData();
            string pid=Request.QueryString["PropertyID"];
            SqlCommand cmd = new SqlCommand("Delete from PropertyImage where ImageId=" + ID,conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            PropertyDetailsEdit datae = new PropertyDetailsEdit();
            PropertyDetailsAction gpd = new PropertyDetailsAction();
            datae.PropertyEdit = gpd.GetAllPropertyEdit(ID);
            CallListForPeoperty();
            return RedirectToAction("UploadImages", "pmoadmin", new { id = pid });
            //return View("UploadImage/"+pid);
        }

        [NonAction]
        public void CallList()
        {

            SqlDataAdapter adptLoc = new SqlDataAdapter("Select * from LocationMaster where location<>'(Any)' order by location", conn);
            DataTable dtLoc = new DataTable();
            adptLoc.Fill(dtLoc);
            ViewBag.LocationList = ToSelectList(dtLoc,"LocationID","Location");

        }

        public void CallListForPeoperty()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from PropertytypeMaster Order by IndexOrder", conn);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ViewBag.PropertyTypeList = ToSelectList(dt, "PTID", "PropertyType");

            SqlDataAdapter adptLoc = new SqlDataAdapter("Select * from ProjectMaster order by projectName", conn);
            DataTable dtLoc = new DataTable();
            adptLoc.Fill(dtLoc);
            ViewBag.ProjectName = ToSelectList(dtLoc, "ProjectID", "ProjectName");

            SqlDataAdapter adptBudget = new SqlDataAdapter("Select * from BudgetMaster order by IndexOrder", conn);
            DataTable dtBudget = new DataTable();
            adptBudget.Fill(dtBudget);
            ViewBag.BudgetList = ToSelectList(dtBudget, "BudgetID", "Budget");

            SqlDataAdapter adptAge = new SqlDataAdapter("Select * from PropertyAgeMaster order by IndexOrder", conn);
            DataTable dtage = new DataTable();
            adptAge.Fill(dtage);
            ViewBag.AgeList = ToSelectList(dtage, "AgeID", "Age");

        }
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

    }
}
