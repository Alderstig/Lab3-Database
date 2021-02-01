using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinformsAppLab3
{
    public partial class FormAddBook : Form
    {
        public List<Book> BookToAdd { get; set; }
        private Lab2Context db = new Lab2Context();
        private List<string> titles = new List<string>();

        public FormAddBook(List<string> activeTitles)
        {
            InitializeComponent();

            foreach (var title in activeTitles)
            {
                titles.Add(title);
            }
        }

        private void FormAddBook_Load(object sender, EventArgs e)
        {
            if (db.Database.CanConnect())
            {
                var books = db.Books.ToList();

                foreach (var book in books)
                {
                    if (!titles.Contains(book.Title)) dataGridViewBooks.Rows.Add(book.Title);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridViewBooks.CurrentCell.RowIndex;
            int columnIndex = dataGridViewBooks.CurrentCell.ColumnIndex;

            BookToAdd = db.Books.Where(book => book.Title.Contains(dataGridViewBooks.Rows[rowIndex].Cells[columnIndex].Value.ToString())).ToList();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonAdd.Enabled = true;
        }
    }
}
