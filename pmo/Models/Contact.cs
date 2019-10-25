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
    public class ContactModel
    {
        public int ID { get; set; }
        [Display(Name = "Your Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string emailId { get; set; }
        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "required Field")]
        public string mobile { get; set; }
        [Required]
        public string Property { get; set; }
        [Required]
        public string Location { get; set; }
        //[Required]
        //public string ProjectAge { get; set; }
        [Required]
        public string Budget { get; set; }

        public bool InsertVisitor(ContactModel contact)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand cmd = new SqlCommand("insert Into VisitorContact (Name, Email,mobile,ProjectType,Location,Budget,ContactDate) values(@Name, @EmailID,@mobile,@ProjectType,@Location,@Budget,@ContactDate)", conn);
            cmd.Parameters.Add(new SqlParameter("@Name",SqlDbType.NVarChar,contact.Name.Trim().Length)).Value = contact.Name;
            cmd.Parameters.Add(new SqlParameter("@EmailID",SqlDbType.NVarChar,contact.emailId.Trim().Length)).Value = contact.emailId;
            cmd.Parameters.Add(new SqlParameter("@mobile",SqlDbType.NVarChar,contact.mobile.Trim().Length)).Value = contact.mobile;
            cmd.Parameters.Add(new SqlParameter("@ProjectType",SqlDbType.NVarChar,contact.Property.Trim().Length)).Value = contact.Property;
            //cmd.Parameters.Add(new SqlParameter("@ProjectAge",SqlDbType.NVarChar,contact.ProjectAge.Trim().Length)).Value = contact.ProjectAge;
            cmd.Parameters.Add(new SqlParameter("@Location",SqlDbType.NVarChar,contact.Location.Trim().Length)).Value = contact.Location;
            cmd.Parameters.Add(new SqlParameter("@Budget",SqlDbType.NVarChar,contact.Budget.Trim().Length)).Value = contact.Budget;
            cmd.Parameters.Add(new SqlParameter("@ContactDate",SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy/MM/dd");
            if(conn.State==ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            //contact.Name = "";
            //contact.emailId = "";
            //contact.mobile = "";
            //contact.Property = "";
            //contact.ProjectAge = "";
            //contact.Location = "";
            //contact.Budget = "";
            return true;
        }
    }
}

