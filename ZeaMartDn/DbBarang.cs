using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeaMartDn
{
    class DbBarang
    {
        public static MySqlConnection GetConnection() 
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=zeamart";
            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySql Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }

        public static void AddBarang(Barang std)
        {
            string sql = "INSERT INTO zeamart VALUES (NULL, @kode, @namabarang, @harga, @stok, @gambar)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@kode", MySqlDbType.VarChar).Value = std.Kode;
            cmd.Parameters.Add("@namabarang", MySqlDbType.VarChar).Value = std.Nama_barang;
            cmd.Parameters.Add("@harga", MySqlDbType.VarChar).Value = std.Harga;
            cmd.Parameters.Add("@stok", MySqlDbType.VarChar).Value = std.Stok;
            cmd.Parameters.Add("@gambar", MySqlDbType.VarChar).Value = std.Gambar;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Menginput data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Gagal Menginput data. \n" + ex.Message , "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void UpdateBarang(Barang std, string id)
        {
            string sql = "UPDATE zeamart SET kode = @kode, namabarang = @namabarang, harga = @harga, stok = @stok, image = @gambar WHERE ID = @id";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@kode", MySqlDbType.VarChar).Value = std.Kode;
            cmd.Parameters.Add("@namabarang", MySqlDbType.VarChar).Value = std.Nama_barang;
            cmd.Parameters.Add("@harga", MySqlDbType.VarChar).Value = std.Harga;
            cmd.Parameters.Add("@stok", MySqlDbType.VarChar).Value = std.Stok;
            cmd.Parameters.Add("@gambar", MySqlDbType.Blob).Value = std.Gambar;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Mengupdate data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Gagal Mengupdate data. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void DeleteBarang(string id)
        {
            string sql = "DELETE FROM zeamart WHERE ID = @id";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Menghapus data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Gagal Menghapus data. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void DisplayAndSearch(string query, DataGridView dgv)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand( sql, con);
            MySqlDataAdapter adp  =new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            con.Close();
        }

    }
}
