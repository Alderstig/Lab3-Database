using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsAppLab3;

namespace WinformAppLab3
{
    public partial class FormMain : Form
    {
        private List<string> titlesInStore = new List<string>();
        public List<StockBalance> SelectedStockbalance { get; set; }
        public Lab2Context db { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new Lab2Context();

            if (db.Database.CanConnect())
            {
                var stores = db.Stores.ToList();

                foreach (var store in stores)
                {
                    treeViewStores.Nodes.Add(store.StoreName);
                }
            }
        }

        private void treeViewStores_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            buttonAddBook.Enabled = true;
            buttonDeleteBook.Enabled = false;
            dataGridViewStock.Rows.Clear();
            titlesInStore.Clear();

            SelectedStockbalance = db.StockBalances.Include(book => book.Isbn13Navigation).Where(store => store.Store.StoreName.Contains(treeViewStores.SelectedNode.Text)).ToList();
            var books = db.Books.ToList();

            for (int i = 0; i < books.Count; i++)
            {
                for (int j = 0; j < SelectedStockbalance.Count; j++)
                {
                    if (string.Equals(books[i].Id, SelectedStockbalance[j].Isbn13)) titlesInStore.Add(books[i].Title);
                }
            }

            for (int i = 0; i < titlesInStore.Count; i++)
            {
                dataGridViewStock.Rows.Add(titlesInStore[i], SelectedStockbalance[i].Amount);
            }
        }

        private void buttonAddBook_Click(object sender, EventArgs e)
        {
            using (FormAddBook formAddBook = new FormAddBook(titlesInStore))
            {
                formAddBook.ShowDialog();

                if (formAddBook.DialogResult == DialogResult.OK)
                {
                    foreach (var book in formAddBook.BookToAdd)
                    {
                        db.Add(new StockBalance() { Isbn13 = book.Id, Amount = 0, StoreId = SelectedStockbalance.ElementAt(0).StoreId });
                    }

                    db.SaveChanges();
                }
            }
        }

        private void dataGridViewStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonDeleteBook.Enabled = true;
            buttonAddBook.Enabled = false;
        }

        private void buttonDeleteBook_Click(object sender, EventArgs e)
        {
            db.Remove(SelectedStockbalance.ElementAt(dataGridViewStock.CurrentCell.RowIndex));
            db.SaveChanges();
        }

        private void dataGridViewStock_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            SelectedStockbalance.ElementAt(dataGridViewStock.CurrentCell.RowIndex).Amount = Convert.ToInt32(dataGridViewStock.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            db.SaveChanges();
        }
    }
}
