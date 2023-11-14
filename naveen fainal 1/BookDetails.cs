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
    public partial class BookDetails : Form
    {
        public BookDetails()
        {
            InitializeComponent();
        }

        private void BookDetails_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = DBConnection.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = " select studentId, bookId, issueDate, promisedDate from tblIssueBooks where returnDate is null";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                da.Fill(dataSet);
                dgvIssue.DataSource = dataSet.Tables[0];
                /* 2nd gridview reurn book part*/
                cmd.CommandText = " select *from tblIssueBooks where returnDate is not null";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataSet dataSet1 = new DataSet();
                da1.Fill(dataSet1);
                dgvReturn.DataSource = dataSet1.Tables[0];

            }
        }
    }
}
