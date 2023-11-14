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
    public partial class Issue_Book : Form
    {
        public Issue_Book()
        {
            InitializeComponent();
        }
        int counting;
        string studentId;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                using (SqlConnection con = DBConnection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = " select *from tblStudent where studentId='" + txtSearch.Text + "'";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    cmd.CommandText = " select count(studentId) from tblIssueBooks where studentId='" + txtSearch.Text + "' and returnDate is null";
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    counting = int.Parse(dataSet1.Tables[0].Rows[0][0].ToString());          
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        studentId = dataSet.Tables[0].Rows[0][0].ToString();
                        txtSName.Text = dataSet.Tables[0].Rows[0][1].ToString();
                        txtSId.Text = dataSet.Tables[0].Rows[0][0].ToString();
                        txtSEmail.Text = dataSet.Tables[0].Rows[0][3].ToString();
                        txtSContact.Text = dataSet.Tables[0].Rows[0][2].ToString();
                        
                    }
                    else
                    {
                        MessageBox.Show("Student Number is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        List<int> bookIds = new List<int>();

        private void Issue_Book_Load(object sender, EventArgs e)
        {
            
            using (SqlConnection con = DBConnection.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT bookId, bookName FROM tblBook", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                 
                 List<string> bookNames = new List<string>();

                while (reader.Read())
                 {
                    int bookId = reader.GetInt32(0);
                    string bookName = reader.GetString(1);

                    bookIds.Add(bookId);
                    bookNames.Add(bookName);
                }

                reader.Close();
                for (int i = 0; i < bookNames.Count; i++)
                {
                    cmbBook.Items.Add($"{bookIds[i]} - {bookNames[i]}");
                 }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtSId.Clear();
            txtSearch.Clear();
            txtSContact.Clear();
            txtSEmail.Clear();
            txtSName.Clear();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (txtSId.Text != "" && txtSName.Text != "" && txtSContact.Text != "" && txtSEmail.Text != "" )
            {
                if (cmbBook.SelectedIndex != -1 && counting <= 2)
                {
                    
                    using (SqlConnection con = DBConnection.GetSqlConnection())
                    {   
                        SqlCommand cmd = new SqlCommand("insert into tblIssueBooks(studentId,bookId,issueDate,promisedDate) values('" + int.Parse(txtSId.Text) + "','" + bookIds[cmbBook.SelectedIndex]+"', '"+DateTime.Now.ToString("d")+"', '"+dtpReturnDate.Value.ToString("d") +"') ", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Book Issued successfully. ", " Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSId.Clear();
                        txtSName.Clear();
                        txtSContact.Clear();
                        txtSEmail.Clear();
                        txtSearch.Clear();
                        cmbBook.SelectedIndex = -1;
                        dtpReturnDate.Value = DateTime.Now;

                    }
                }
                else
                {
                    MessageBox.Show("No Selected book or" + " Student who has StudentNumber= " + txtSId.Text + " has reached the maximum book number should be taken. ", "  Error or Max book number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("There is no Student Number which is entered. Please enter student number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
