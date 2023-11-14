using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace naveen_fainal_1
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Book add_Book = new Add_Book();
            add_Book.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewBook viewBook = new ViewBook();
            viewBook.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Issue_Book issueBook = new Issue_Book();
            issueBook.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnBook returnBook = new ReturnBook();
            returnBook.Show();
            this.Hide();
        }

        private void btnDetailBook_Click(object sender, EventArgs e)
        {
            BookDetails db = new BookDetails();
            this.Hide();
            db.Show();
        }
    }
}
