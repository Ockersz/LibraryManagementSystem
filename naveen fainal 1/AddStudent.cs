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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBConnection.GetSqlConnection())
            {
                if (txtsName.Text != "" && txtsId.Text != "" && txtstContact.Text != "" && txtstEmail.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("insert into tblStudent (studentId,studentName,studentContact,studentEmail) values('" + txtsId.Text + "', '" + txtsName.Text + "'," +
                   "'" + int.Parse(txtstContact.Text) + "', '" + txtstEmail.Text + "')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Infos saved.", " Success" + MessageBoxButtons.OK + MessageBoxIcon.Information);
                    txtsName.Clear();
                    txtsId.Clear();
                    txtstContact.Clear();
                    txtstEmail.Clear();
                }
                else
                {
                    MessageBox.Show("No Info entered.", "Error" + MessageBoxButtons.OK + MessageBoxIcon.Warning);
                }

            }
        }
    }
}
