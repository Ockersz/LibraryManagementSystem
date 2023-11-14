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
    public partial class Add_Book : Form
    {
        public Add_Book()
        {
            InitializeComponent();
        }

        private void Add_Book_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Book name cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            } else if (txtAuthorName.Text == "")
            {
                MessageBox.Show("Author name cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (txtPublication.Text == "")
            {
                MessageBox.Show("Publication cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (txtPrice.Text == "")
            {
                MessageBox.Show("Book price cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (txtQuantity.Text == "")
            {
                MessageBox.Show("Book quantity cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection con = DBConnection.GetSqlConnection();
                SqlCommand cmd = new SqlCommand("insert into tblBook (bookName, bookAuthor, bookPublication, bookDate, bookPrice, bookQuantity) values ( '" + txtName.Text+"', '"+txtAuthorName.Text+"', '"+txtPublication.Text+"', '"+ dtpPurchaseDate.Value.ToString("yyyy-MM-dd") + "','"+int.Parse(txtPrice.Text)+"','"+int.Parse(txtQuantity.Text)+"')",con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if(i == 1)
                {
                    MessageBox.Show("Book Added Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                else
                {
                    MessageBox.Show("Book Add Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Clear feilds
                    clearFields();
                }

            }
        }

        public void clearFields()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtPublication.Clear();
            txtAuthorName.Clear();
            txtQuantity.Clear();
            dtpPurchaseDate.Value = DateTime.Now;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();
            this.Hide();
            ds.ShowDialog();
            
        }
    }
}
