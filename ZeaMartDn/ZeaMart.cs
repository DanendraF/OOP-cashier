using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeaMartDn
{
    public partial class ZeaMart : Form

    {

        FormBarang form;

        public ZeaMart()
        {
            InitializeComponent();
            form = new FormBarang(this);
        }

        public void Display()
        {
            DbBarang.DisplayAndSearch("SELECT ID, kode, namabarang, harga, stok, image FROM zeamart", dataGridView1);
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            //FormBarang form = new FormBarang(this);
            form.Clear();
            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ZeaMart_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            DbBarang.DisplayAndSearch("SELECT * FROM zeamart WHERE CONCAT(id, kode, namabarang, stok) LIKE'%"+txtsearch.Text+"%'", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                form.Clear();
                form.id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.kode = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.nama_barang = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.harga = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.stok = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.UpdateInfo();
                form.ShowDialog();
                //edit
                return;
            }
            if(e.ColumnIndex == 1)
            {
                //Delete
                //MessageBox.Show("Anda ingin menghapus data ini?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if(MessageBox.Show("Anda ingin menghapus data ini?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DbBarang.DeleteBarang(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display();
                }
                return;
            }
        }
    }
}
