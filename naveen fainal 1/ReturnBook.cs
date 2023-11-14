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
            refreshTable();
        }

        public void refreshTable()
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
        string promisedDate;
        Int64 rowId;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bkId = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                bkIssueDate = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                promisedDate = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
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
        int penaltyDays;
        decimal penaltyPrice;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(promisedDate, out DateTime issueDate))
            {
                DateTime selectedDate = dateTimePicker1.Value;
                penaltyDays = (int)(selectedDate - issueDate).TotalDays;
                penaltyDays = Math.Max(penaltyDays, 0);

                decimal penaltyRatePerDay = 200;
                penaltyPrice = penaltyDays * penaltyRatePerDay;

                lblPenalty.Text ="Rs. " + penaltyPrice.ToString();
            }
            else
            {
                MessageBox.Show("Invalid issue date format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBConnection.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "Update tblIssueBooks set returnDate='" + dateTimePicker1.Value.ToString("d") + "', overdueDays = '"+penaltyDays+"', penaltyPrice = '"+ penaltyPrice + "' where studentId='" + txtSearch.Text + "' and BookId=" + bkId + " ";
                cmd.ExecuteNonQuery();
                MessageBox.Show(txtBookName.Text + " Book is returned Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }
    }
}
