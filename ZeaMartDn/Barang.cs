using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeaMartDn
{
    internal class Barang
    {

        public string Kode { get; set; }

        public string Nama_barang { get; set; }

        public string Harga { get; set; }

        public string Stok { get; set; }

        public string Gambar { get; set; }
        public Barang(string kode, string nama_barang, string harga, string stok, string gambar)
        {
            Kode = kode;
            Nama_barang = nama_barang;
            Harga = harga;
            Stok = stok;
            Gambar = gambar;
        }
    }
}
