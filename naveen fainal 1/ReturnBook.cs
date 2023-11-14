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
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
              
                using (SqlConnection con = DBConnection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = " select *from tblIssueBooks where studentId='" + txtSearch.Text + "' and returnDate is null";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet);

                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("Student Number is invalid or no book issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        string bkId;
        string bkIssueDate;
        Int64 rowId;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bkId = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                bkIssueDate = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }

            using (SqlConnection con = DBConnection.GetSqlConnection())
            {
                con.Open();

                string query = "SELECT bookName FROM tblBook WHERE bookId = @BookId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookId", int.Parse(bkId));

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        da.Fill(dataSet);

                        if (dataSet.Tables[0].Rows.Count != 0)
                        {
                            txtBookName.Text = dataSet.Tables[0].Rows[0]["bookName"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Student Number is invalid or no book issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            txtIssueDate.Text = bkIssueDate;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
