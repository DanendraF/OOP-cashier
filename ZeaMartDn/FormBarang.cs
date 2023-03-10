using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeaMartDn
{
    public partial class FormBarang : Form
    {
        private readonly ZeaMart _parent;
        public string id, kode, nama_barang, harga, stok;

        public FormBarang(ZeaMart parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=zeamart";
            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("My Sql Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Choose Image | *.jpg;.png;.jpeg;.gif;";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(opf.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtkode_TextChanged(object sender, EventArgs e)
        {

        }

        public void UpdateInfo()
        {
            button2.Text = "Update";
            txtkode.Text = kode;
            txtnama.Text = nama_barang;
            txtharga.Text = harga;
            txtstok.Text = stok;
        }

        public void Clear()
        {
            button2.Text = "Simpan";
            txtkode.Text = txtnama.Text = txtharga.Text = txtstok.Text = string.Empty;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(txtkode.Text.Trim().Length < 1)
            {
                MessageBox.Show("kode Barang Tidak Ada ( > 1).");
                return;
            }
            if (txtnama.Text.Trim().Length < 1)
            {
                MessageBox.Show("nama Barang Tidak Ada ( > 1).");
                return;
            }
            if (txtharga.Text.Trim().Length < 1)
            {
                MessageBox.Show("harga Barang Tidak Ada ( > 1).");
                return;
            }
            if (txtstok.Text.Trim().Length < 1)
            {
                MessageBox.Show("stok Barang Tidak Ada ( > 1).");
                return;
            }
            if(button2.Text == "Simpan")
            {
                /*Barang std = new Barang(txtkode.Text.Trim(), txtnama.Text.Trim(), txtharga.Text.Trim(), txtstok.Text.Trim());
                DbBarang.AddBarang(std);
                Clear();*/
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                string sql = "INSERT INTO zeamart VALUES(NULL, @Kode, @Produk, @Harga, @Stok, @Gambar)";
                MySqlConnection conn = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@Kode", MySqlDbType.VarChar).Value = txtkode.Text;
                cmd.Parameters.Add("@Produk", MySqlDbType.VarChar).Value = txtnama.Text;
                cmd.Parameters.Add("@Harga", MySqlDbType.VarChar).Value = txtharga.Text;
                cmd.Parameters.Add("@Stok", MySqlDbType.VarChar).Value = txtstok.Text;
                cmd.Parameters.Add("@Gambar", MySqlDbType.Blob).Value = img;

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Gagal Menambahkan \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            if(button2.Text == "Update")
            {
                /*Barang std = new Barang(txtkode.Text.Trim(), txtnama.Text.Trim(), txtharga.Text.Trim(), txtstok.Text.Trim());
                DbBarang.UpdateBarang(std, id); */
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                string sql = "UPDATE zeamart SET kode=@Kode, namabarang=@Produk, harga=@Harga, stok=@Stok, image=@Gambar WHERE ID=@Id";
                MySqlConnection conn = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@Produk", MySqlDbType.VarChar).Value = txtnama.Text;
                cmd.Parameters.Add("@Kode", MySqlDbType.VarChar).Value = txtkode.Text;
                cmd.Parameters.Add("@Harga", MySqlDbType.VarChar).Value = txtharga.Text;
                cmd.Parameters.Add("@Stok", MySqlDbType.VarChar).Value = txtstok.Text;
                cmd.Parameters.Add("@Gambar", MySqlDbType.Blob).Value = img;

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Gagal Mengubah \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            _parent.Display();
        }
    }
}
