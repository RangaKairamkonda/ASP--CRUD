using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                display();
        }
        string s = ConfigurationManager.ConnectionStrings["sa"].ToString();
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(s);
            con.Open();
            SqlCommand com = new SqlCommand("sa_insert", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", TextBox1.Text);
            com.Parameters.AddWithValue("@name", TextBox2.Text);
            com.Parameters.AddWithValue("@address", TextBox3.Text);
            com.ExecuteNonQuery();
            con.Close();
            display();
        }
        public void display()
        {
            SqlConnection co = new SqlConnection(s);
            co.Open();
            SqlCommand com = new SqlCommand("sa_display", co);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = com.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
            co.Close();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            display();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(s);
            con.Open();
            GridViewRow row = GridView1.Rows[e.RowIndex];
            Control c = row.FindControl("Label1");
            Label l = (Label)c;
            SqlCommand com = new SqlCommand("sa_delete", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", l.Text);
            com.ExecuteNonQuery();
            con.Close();
            
            display();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            display();
        }

        

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(s);
            con.Open();
            GridViewRow row = GridView1.Rows[e.RowIndex];
            Control c = row.FindControl("TextBox1");
            TextBox t = (TextBox)c;
            Control c1 = row.FindControl("TextBox2");
            TextBox t1 = (TextBox)c1;
            Control c2 = row.FindControl("TextBox3");
            TextBox t2 = (TextBox)c2;

            SqlCommand com = new SqlCommand("sa_update", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", t.Text);
            com.Parameters.AddWithValue("@name", t1.Text);
            com.Parameters.AddWithValue("@address", t2.Text);
            com.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            display();
        }
    }
}