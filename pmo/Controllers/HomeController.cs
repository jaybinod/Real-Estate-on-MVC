using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using pmo.Models;

namespace pmo.Controllers
{
    public class HomeController : Controller
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        private pmo.Models.PropertyImage PI = new PropertyImage();
        
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            CallList();
            GetPropertyData propertydata = new GetPropertyData();

            MainPageModel data = new MainPageModel();
            data.AllProperty = propertydata.GetAllProperty();
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(MainPageModel sm)
        {
            MainPageModel data = new MainPageModel();
            GetPropertyData propertydata = new GetPropertyData();

            try
            {
                if (ModelState.IsValid)
                {              
                    data.AllProperty = propertydata.GetAllProperty(sm.SearchModel);

                    //ContactModel con = new ContactModel();
                    //status = con.InsertVisitor(ct);

                    ModelState.Clear();
                }
                else
                {
                    data.AllProperty = propertydata.GetAllProperty();
                }
            }
            catch
            {

            }
            CallList();
     
            return View(data);
        }

        public ActionResult PropertyDetails(string id)
        {
            SqlCommand cmd = new SqlCommand("Select * from PropertyListMaster PLM, ProjectMaster PM, LocationMaster LM, PropertyTypeMaster PTM Where PLM.ProjectID=PM.ProjectID and PLM.PTID=PTM.PTID and PM.LocationID=LM.LocationID and PLM.Estate_ID='" + id + "'", conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            //DataTable pdt = new DataTable();
            //cmd.Fill(pdt);
            int PropertyID = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                PropertyID = (int)dr["PropertyID"];
                ViewBag.PropertyID = (int)dr["PropertyID"];
                ViewBag.BedRooms = dr["BedRooms"];
                ViewBag.BathRooms = dr["BathRooms"];
                ViewBag.Balconies = dr["Balconies"];
                ViewBag.StoreRooms = dr["StoreRooms"];
                ViewBag.SuperArea = dr["SuperArea"];
                ViewBag.CarpetArea = dr["CarpetArea"];
                ViewBag.FaceSide = dr["FaceSide"];
                ViewBag.Status = dr["Possession"];
                ViewBag.PropertyType = dr["PropertyType"];
                ViewBag.floor = dr["floor"];
                ViewBag.CarParking = dr["CarParking"];
                ViewBag.ProjectName = dr["ProjectName"];
                ViewBag.Location = dr["Location"];
                ViewBag.Builder = dr["Builder"];
                ViewBag.TotalBuilding = dr["TotalBuilding"];
                ViewBag.LiftinEachBuilding = dr["LiftinEachBuilding"];
                ViewBag.TotalFlatinProject = dr["TotalFlatinProject"];
            }
            dr.Close();
            conn.Close();
            
            ViewData.Model = PI.GetImagesList(PropertyID).AsEnumerable();

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            
           
            CallList();
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel ct )
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ContactModel con = new ContactModel();
                    status = con.InsertVisitor(ct);
                    ModelState.Clear();
                }
            }
            catch
            {
                                
            }

            if (status == true)
            {
                CallList();
                //Contact();
                
                ViewBag.Message = "Thank you for contact us. we will call you soon";
                return View();
            }
            else
            {
                CallList();
                ViewBag.Message = "Ah... Please try after sometime";
                return View();
            }
        }

        [NonAction]
        public void CallList()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from PropertytypeMaster Order by IndexOrder", conn);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ViewBag.PropertyTypeList = ToSelectList(dt, "PropertyType", "PropertyType");

            //SqlDataAdapter adptAge = new SqlDataAdapter("Select * from PropertyAgeMaster order by IndexOrder", conn);
            //DataTable dtAge = new DataTable();
            //adptAge.Fill(dtAge);
            //ViewBag.PropertyAgeList = ToSelectList(dtAge, "Age", "Age");

            SqlDataAdapter adptLoc = new SqlDataAdapter("Select * from LocationMaster order by location", conn);
            DataTable dtLoc = new DataTable();
            adptLoc.Fill(dtLoc);
            ViewBag.LocationList = ToSelectList(dtLoc, "Location", "Location");

            SqlDataAdapter adptBudget = new SqlDataAdapter("Select * from BudgetMaster order by IndexOrder", conn);
            DataTable dtBudget = new DataTable();
            adptBudget.Fill(dtBudget);
            ViewBag.BudgetList = ToSelectList(dtBudget, "Budget", "Budget");

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
