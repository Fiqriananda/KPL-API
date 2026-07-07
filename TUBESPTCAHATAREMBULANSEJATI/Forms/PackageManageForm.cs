using System;
using System.Drawing;
using System.Windows.Forms;
using TUBESPTCAHATAREMBULANSEJATI.Services;
using TUBESPTCAHATAREMBULANSEJATI.Models;

namespace TUBESPTCAHATAREMBULANSEJATI.Forms
{
    public class PackageManageForm : Form
    {
        private Paket? _paket;

        private TextBox txtNomerResi;
        private TextBox txtPengirim;
        private TextBox txtPenerima;
        private TextBox txtAlamatAsal;
        private TextBox txtAlamatTujuan;
        private TextBox txtBeratKg;
        private ComboBox cmbStatusPengiriman;
        private TextBox txtKota;
        private TextBox txtBiaya;

        private Button btnSave;
        private Button btnCancel;

        public PackageManageForm(Paket? paket = null)
        {
            _paket = paket;
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            bool isEdit = _paket != null;
            this.Text = isEdit ? "Edit Paket" : "Tambah Paket";
            this.Size = new Size(400, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int yOffset = 20;

            this.Controls.Add(new Label { Text = "Nomer Resi:", Location = new Point(20, yOffset) });
            txtNomerResi = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtNomerResi);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Pengirim:", Location = new Point(20, yOffset) });
            txtPengirim = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtPengirim);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Penerima:", Location = new Point(20, yOffset) });
            txtPenerima = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtPenerima);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Alamat Asal:", Location = new Point(20, yOffset) });
            txtAlamatAsal = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtAlamatAsal);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Alamat Tujuan:", Location = new Point(20, yOffset) });
            txtAlamatTujuan = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtAlamatTujuan);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Berat (Kg):", Location = new Point(20, yOffset) });
            txtBeratKg = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtBeratKg);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Status:", Location = new Point(20, yOffset) });
            cmbStatusPengiriman = new ComboBox { Location = new Point(150, yOffset), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatusPengiriman.Items.AddRange(new string[] { "Pending", "Diproses", "Dikirim", "Selesai", "Batal" });
            cmbStatusPengiriman.SelectedIndex = 0;
            this.Controls.Add(cmbStatusPengiriman);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Kota:", Location = new Point(20, yOffset) });
            txtKota = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtKota);
            yOffset += 40;

            this.Controls.Add(new Label { Text = "Biaya:", Location = new Point(20, yOffset) });
            txtBiaya = new TextBox { Location = new Point(150, yOffset), Width = 200 };
            this.Controls.Add(txtBiaya);
            yOffset += 50;

            btnSave = new Button
            {
                Text = "Simpan",
                Location = new Point(150, yOffset),
                Width = 90,
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Batal",
                Location = new Point(260, yOffset),
                Width = 90,
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            // RBAC Logic: If Kurir, disable everything except Status
            if (ApiClient.Role == "Kurir")
            {
                txtNomerResi.Enabled = false;
                txtPengirim.Enabled = false;
                txtPenerima.Enabled = false;
                txtAlamatAsal.Enabled = false;
                txtAlamatTujuan.Enabled = false;
                txtBeratKg.Enabled = false;
                txtKota.Enabled = false;
                txtBiaya.Enabled = false;
            }
        }

        private void LoadData()
        {
            if (_paket != null)
            {
                txtNomerResi.Text = _paket.NomerResi;
                txtPengirim.Text = _paket.Pengirim;
                txtPenerima.Text = _paket.Penerima;
                txtAlamatAsal.Text = _paket.AlamatAsal;
                txtAlamatTujuan.Text = _paket.AlamatTujuan;
                txtBeratKg.Text = _paket.BeratKg.ToString();
                cmbStatusPengiriman.SelectedItem = _paket.StatusPengiriman;
                txtKota.Text = _paket.Kota;
                txtBiaya.Text = _paket.Biaya.ToString();
            }
            else
            {
                txtNomerResi.Text = "RS-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!double.TryParse(txtBeratKg.Text, out double berat) || !decimal.TryParse(txtBiaya.Text, out decimal biaya))
            {
                MessageBox.Show("Berat dan Biaya harus berupa angka.");
                return;
            }

            var paketToSave = new Paket
            {
                Id = _paket?.Id ?? 0,
                NomerResi = txtNomerResi.Text,
                Pengirim = txtPengirim.Text,
                Penerima = txtPenerima.Text,
                AlamatAsal = txtAlamatAsal.Text,
                AlamatTujuan = txtAlamatTujuan.Text,
                BeratKg = berat,
                StatusPengiriman = cmbStatusPengiriman.Text,
                Kota = txtKota.Text,
                Biaya = biaya,
                TanggalDikirm = _paket?.TanggalDikirm ?? DateTime.Now,
                TanggalDiterima = cmbStatusPengiriman.Text == "Selesai" ? DateTime.Now : _paket?.TanggalDiterima
            };

            btnSave.Enabled = false;

            if (_paket == null)
            {
                var result = await ApiClient.CreatePaketAsync(paketToSave);
                if (result.Success)
                {
                    MessageBox.Show("Paket berhasil ditambahkan.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Gagal");
                }
            }
            else
            {
                var result = await ApiClient.UpdatePaketAsync(paketToSave.Id, paketToSave);
                if (result.Success)
                {
                    MessageBox.Show("Paket berhasil diperbarui.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Gagal");
                }
            }

            btnSave.Enabled = true;
        }
    }
}
