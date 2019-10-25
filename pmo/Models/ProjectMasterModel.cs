using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace pmo.Models
{
    public class ProjectMasterModel
    {
        public int ProjectID { get; set; }
        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project Name is required")]
        public string ProjectName { get; set; }
        
        [Display(Name = "Builder Name")]
        [Required(ErrorMessage = "The Builder Name is required")]
        public string BuilderName { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "required Field")]
        public int LocationID { get; set; }

        [Display(Name = "Location")]
        //[Required(ErrorMessage = "required Field")]
        public string Location { get; set; }

        [Display(Name = "Total Building in Project")]
        [Required(ErrorMessage = "required Field")]
        public int totalBuilding { get; set; }

        [Display(Name = "Lift in Each Building")]
        [Required(ErrorMessage = "required Field")]
        public int LiftInEachBuilding { get; set; }
        
        [Display(Name = "Total Flat in Project")]
        [Required(ErrorMessage = "required Field")]
        public int TotalFlatInProject { get; set; }

        public List<ProjectMasterModel> AllProjects { get; set; }

        public bool InsertProject(ProjectMasterModel Project)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            SqlCommand cmdid = new SqlCommand("Select top 1 * from ProjectMaster order by ProjectID desc", conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader dr ;
            dr = cmdid.ExecuteReader();
            int projectID = 0;
            if(dr.Read())
            {
                projectID = int.Parse(dr["ProjectID"].ToString());
            }
            dr.Close();
            projectID++;
            SqlCommand cmdLocation = new SqlCommand("Select * from LocationMaster where LocationID="+Project.LocationID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drL;
            drL = cmdLocation.ExecuteReader();
            string Location = "";
            if(drL.Read())
            {
                Location = drL["Location"].ToString();
            }
            drL.Close();

            string projectIDLink = Project.ProjectName + "-" + Location + " " + Project.BuilderName + " " + projectID;
            projectIDLink = projectIDLink.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            projectIDLink = projectIDLink.Replace(" ", "-");

            SqlCommand cmd = new SqlCommand("insert Into ProjectMaster (ProjectID,ProjectIDLink,ProjectName, Builder, LocationID,TotalBuilding,LiftinEachBuilding,TotalFlatInProject) values(@ProjectID, @ProjectIDLink, @ProjectName, @Builder, @LocationID, @TotalBuilding, @LiftinEachBuilding, @TotalFlatInProject)", conn);

            cmd.Parameters.Add(new SqlParameter("@ProjectIDLink", SqlDbType.NVarChar, projectIDLink.Trim().Length)).Value = projectIDLink.Trim();
            cmd.Parameters.Add(new SqlParameter("@ProjectName", SqlDbType.NVarChar, Project.ProjectName.Trim().Length)).Value = Project.ProjectName.Trim();
            cmd.Parameters.Add(new SqlParameter("@Builder", SqlDbType.NVarChar, Project.BuilderName.Trim().Length)).Value = Project.BuilderName.Trim();
            cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int)).Value = Project.LocationID;
            cmd.Parameters.Add(new SqlParameter("@TotalBuilding", SqlDbType.Int)).Value = Project.totalBuilding;
            cmd.Parameters.Add(new SqlParameter("@LiftInEachBuilding", SqlDbType.Int)).Value = Project.LiftInEachBuilding;
            cmd.Parameters.Add(new SqlParameter("@TotalFlatInProject", SqlDbType.Int)).Value = Project.TotalFlatInProject;
            cmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.Int)).Value = projectID;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool UpdateProject(ProjectMasterModel Project)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            //SqlCommand cmdid = new SqlCommand("Select top 1 * from ProjectMaster order by ProjectID desc", conn);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            //SqlDataReader dr;
            //dr = cmdid.ExecuteReader();
            //int projectID = 0;
            //if (dr.Read())
            //{
            //    projectID = int.Parse(dr["ProjectID"].ToString());
            //}
            //dr.Close();
            //projectID++;
            SqlCommand cmdLocation = new SqlCommand("Select * from LocationMaster where LocationID=" + Project.AllProjects[0].LocationID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drL;
            drL = cmdLocation.ExecuteReader();
            string Location = "";
            if (drL.Read())
            {
                Location = drL["Location"].ToString();
            }
            drL.Close();

            string projectIDLink = Project.AllProjects[0].ProjectName + "-" + Location + " " + Project.AllProjects[0].BuilderName + " " + Project.AllProjects[0].ProjectID;
            projectIDLink = projectIDLink.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            projectIDLink = projectIDLink.Replace(" ", "-");

            SqlCommand cmd = new SqlCommand("Update ProjectMaster set ProjectIDLink=@ProjectIDLink, ProjectName=@ProjectName, Builder=@Builder, LocationID=@LocationID, TotalBuilding=@TotalBuilding, LiftinEachBuilding=@LiftinEachBuilding, TotalFlatInProject=@TotalFlatInProject where ProjectID=" + Project.AllProjects[0].ProjectID, conn);

            cmd.Parameters.Add(new SqlParameter("@ProjectIDLink", SqlDbType.NVarChar, projectIDLink.Trim().Length)).Value = projectIDLink.Trim();
            cmd.Parameters.Add(new SqlParameter("@ProjectName", SqlDbType.NVarChar, Project.AllProjects[0].ProjectName.Trim().Length)).Value = Project.AllProjects[0].ProjectName.Trim();
            cmd.Parameters.Add(new SqlParameter("@Builder", SqlDbType.NVarChar, Project.AllProjects[0].BuilderName.Trim().Length)).Value = Project.AllProjects[0].BuilderName.Trim();
            cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int)).Value = Project.AllProjects[0].LocationID;
            cmd.Parameters.Add(new SqlParameter("@TotalBuilding", SqlDbType.Int)).Value = Project.AllProjects[0].totalBuilding;
            cmd.Parameters.Add(new SqlParameter("@LiftInEachBuilding", SqlDbType.Int)).Value = Project.AllProjects[0].LiftInEachBuilding;
            cmd.Parameters.Add(new SqlParameter("@TotalFlatInProject", SqlDbType.Int)).Value = Project.AllProjects[0].TotalFlatInProject;
            
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            return true;
        }

        public List<ProjectMasterModel> GetAllProjectList()
        {
            List<ProjectMasterModel> plist = new List<ProjectMasterModel>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * from ProjectMaster PM, LocationMaster LM Where PM.LocationID=LM.LocationID order by ProjectName", conn);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            foreach (DataRow drow in dt.Rows)
            {
                int projid = int.Parse(drow["ProjectID"].ToString());
                int mlocid = (int)drow["LocationID"];
                string mLocation = (string)drow["Location"];
                string mProjectName = (string)drow["ProjectName"];
                string mBuilder = (string)drow["Builder"];
               int mTotalBuilding = int.Parse(drow["TotalBuilding"].ToString());
                int  mLiftInEachBuilding = int.Parse(drow["LiftInEachBuilding"].ToString());
                int mTotalFlatInProject = int.Parse(drow["TotalFlatInProject"].ToString());
                

                plist.Add((new ProjectMasterModel { ProjectID=projid, BuilderName = mBuilder, LiftInEachBuilding=mLiftInEachBuilding, totalBuilding=mTotalBuilding, TotalFlatInProject=mTotalFlatInProject, Location = mLocation, ProjectName = mProjectName }));
            }
            return plist;
        }

        public List<ProjectMasterModel> GetAllProjectList(int ID)
        {
            List<ProjectMasterModel> plist = new List<ProjectMasterModel>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * from ProjectMaster PM, LocationMaster LM Where PM.LocationID=LM.LocationID and ProjectID="+ID, conn);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            foreach (DataRow drow in dt.Rows)
            {
                int projid = int.Parse(drow["ProjectID"].ToString());
                int mlocid = (int)drow["LocationID"];
                string mLocation = (string)drow["Location"];
                string mProjectName = (string)drow["ProjectName"];
                string mBuilder = (string)drow["Builder"];
                int mTotalBuilding = int.Parse(drow["TotalBuilding"].ToString());
                int mLiftInEachBuilding = int.Parse(drow["LiftInEachBuilding"].ToString());
                int mTotalFlatInProject = int.Parse(drow["TotalFlatInProject"].ToString());


                plist.Add((new ProjectMasterModel { ProjectID = projid, BuilderName = mBuilder, LiftInEachBuilding = mLiftInEachBuilding, totalBuilding = mTotalBuilding, TotalFlatInProject = mTotalFlatInProject, Location = mLocation, LocationID=mlocid,  ProjectName = mProjectName }));
            }
            return plist;
        }
    }
}