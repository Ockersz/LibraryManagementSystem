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
    public partial class ViewBook : Form
    {
        public ViewBook()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewBook_Load(object sender, EventArgs e)
        {

            refreshTable();
        }

        public void refreshTable()
        {
            SqlConnection con = DBConnection.GetSqlConnection();

            SqlCommand cmd = new SqlCommand("select * from tblBook", con);
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset);
            dgvBooks.DataSource = dataset.Tables[0];
        }
        int rowId;
        int bookId;

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowId = e.RowIndex;
            DataGridViewRow selectedRow = dgvBooks.Rows[rowId];
            bookId = int.Parse(selectedRow.Cells[0].Value.ToString());
            txtName.Text = selectedRow.Cells[1].Value.ToString();
            txtAuthorName.Text = selectedRow.Cells[2].Value.ToString();
            txtPublication.Text = selectedRow.Cells[3].Value.ToString();
            txtPurchaseDate.Text = selectedRow.Cells[4].Value.ToString();
            txtPrice.Text = selectedRow.Cells[5].Value.ToString();
            txtQuantity.Text = selectedRow.Cells[6].Value.ToString();


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
     
                using (SqlConnection con = DBConnection.GetSqlConnection())
                {

                    SqlCommand cmd = new SqlCommand("select *from tblBook where bookName like '" + txtSearch.Text + "%'", con);
                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    dataAdapter.Fill(dataset);
                    dgvBooks.DataSource = dataset.Tables[0];
                }
            }
            else
            {
                String CS = "data source=.; database = LMSDB; integrated security=SSPI";
                using (SqlConnection con = new SqlConnection(CS))
                {

                    SqlCommand cmd = new SqlCommand("select *from tblBookInfos", con);
                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    dataAdapter.Fill(dataset);
                    dgvBooks.DataSource = dataset.Tables[0];
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {

                using (SqlConnection con = DBConnection.GetSqlConnection())
                {

                    SqlCommand cmd = new SqlCommand("select *from tblBook where bookName like '" + txtSearch.Text + "%'", con);
                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    dataAdapter.Fill(dataset);
                    dgvBooks.DataSource = dataset.Tables[0];
                }
            }
            else { MessageBox.Show("Please enter something to search", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
            using (SqlConnection con = DBConnection.GetSqlConnection())
            {
                if (MessageBox.Show("Data will be Updated. Do you Want to Confirm", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("update tblBook set bookName ='" + txtName.Text + "', bookAuthor ='" + txtAuthorName.Text + "'" +
                                       ", bookPublication ='" + txtPublication.Text + "', bookDate = '" + txtPurchaseDate.Text + "', bookPrice =" + Int64.Parse(txtPrice.Text) + " " +
                                       ", bookQuantity =" + Int64.Parse(txtQuantity.Text) + " where bookId =" + bookId + " ", con);
                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    dataAdapter.Fill(dataset);
                    MessageBox.Show("Book which has ID =  " + bookId + "  is Updated.", " Success" + MessageBoxButtons.OK + MessageBoxIcon.Information);
                    txtName.Clear();
                    txtAuthorName.Clear();
                    txtPublication.Clear();
                    txtPurchaseDate.Clear();
                    txtPrice.Clear();
                    txtQuantity.Clear();
                    refreshTable();
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = DBConnection.GetSqlConnection())
            {
                if (MessageBox.Show("Data will be deleted. Do you Want to Confirm", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("Delete from tblBookInfos where bkId =" + rowId + " ", con);
                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    dataAdapter.Fill(dataset);
                    MessageBox.Show("Book which has ID =  " + bookId + "is Deleted.", " Success" + MessageBoxButtons.OK + MessageBoxIcon.Information);
                    txtName.Clear();
                    txtAuthorName.Clear();
                    txtPublication.Clear();
                    txtPurchaseDate.Clear();
                    txtPrice.Clear();
                    txtQuantity.Clear();
                    refreshTable();
                }

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtAuthorName.Clear();
            txtPublication.Clear();
            txtPurchaseDate.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            Dashboard ds = new Dashboard();
            this.Hide();
            ds.ShowDialog();
            
        }
    }
}
