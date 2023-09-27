using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiInputDataMahasiswa
{
    public partial class Form1 : Form
    {
        private List<Mahasiswa> list = new List<Mahasiswa>();
        public Form1()
        {
            InitializeComponent();
            InisialisasiListview();
        }

        //atur kolom list view
        private void InisialisasiListview()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nim", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama", 200, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Kelas", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai Huruf", 50, HorizontalAlignment.Center);
        }

        private void ResetForm()
        {
            txtNim.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            txtNilai.Text = "0";
            txtNim.Focus();


        }

        private bool NumericOnly(KeyPressEventArgs e)
        {
            var strValid = "0123456789";
            if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //input selain angka
                if (strValid.IndexOf(e.KeyChar) < 0)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }
        private void TampilkanData()
        {
            //kosongna data list view
            lvwMahasiswa.Items.Clear();

            // lakukan perulangan untuk menampilkan data mahasiswa ke list view
            foreach(var mhs in list)
            {

                    
                    if (mhs.Nilai >= 0 && mhs.Nilai <= 20)
                    {
                        mhs.Huruf = "E";
                    }
                    else if (mhs.Nilai >= 21 && mhs.Nilai <= 40)
                    {
                        mhs.Huruf = "D";
                    }
                    else if (mhs.Nilai >= 41 && mhs.Nilai <= 60)
                    {
                        mhs.Huruf = "C";
                    }
                    else if (mhs.Nilai >= 61 && mhs.Nilai <= 80)
                    {
                        mhs.Huruf = "B";
                    }
                    else if (mhs.Nilai >= 81 && mhs.Nilai >= 81)
                    {
                        mhs.Huruf = "A";
                    }


                    var noUrut = lvwMahasiswa.Items.Count + 1;

                    var item = new ListViewItem(noUrut.ToString());
                    item.SubItems.Add(mhs.Nim);
                    item.SubItems.Add(mhs.Nama);
                    item.SubItems.Add(mhs.Kelas);
                    item.SubItems.Add(mhs.Nilai.ToString());
                    item.SubItems.Add(mhs.Huruf);


                    lvwMahasiswa.Items.Add(item);
                }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void txtNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumericOnly(e);
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            //membuat objek mahasiswa
            Mahasiswa mhs = new Mahasiswa();

            //set nilai masing masing poropertynya 
            //berdasarkan inputan yang ada di form
            mhs.Nim = txtNim.Text;
            mhs.Nama = txtNama.Text;
            mhs.Kelas = txtKelas.Text;
            mhs.Nilai = int.Parse(txtNilai.Text);

           

            // tambahkan objek mahasiswa kedalam collection
            list.Add(mhs);

            var msg = "Data mahasiswa berhasil disimpan.";

            //tampilan dialog informasi

            MessageBox.Show(msg, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetForm();
        }

        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            // cek apakah mahasiswa sudah dipilih
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                // tampilkan konfirmasi
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin di hapus ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    //ambil index yang dipilih
                    var index = lvwMahasiswa.SelectedIndices[0];
                    //hapus objrk mahasiswa yang di  list
                    list.RemoveAt(index);
                    //refresh tampilan listview
                    TampilkanData();


                }

            }
            else
            {
                MessageBox.Show("Data Mahasiswa belum dipilih !!!", "peringatan ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
    }
}
