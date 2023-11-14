using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace naveen_fainal_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Username Cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password Cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                SqlConnection con = DBConnection.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("Select * from tblUser where UserName='" + txtUsername.Text + "' and Password ='" + txtPassword.Text + "' ", con);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    Form2 das = new Form2();
                    this.Hide();
                    das.Show();
                }

                else
                {
                    MessageBox.Show("Wrong Username OR Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
