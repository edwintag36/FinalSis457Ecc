using CadECC;
using ClnECC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpECC
{
    public partial class Form1 : Form
    {
        FrmAutenticacion frmAutenticacion;
        bool esNuevo;
        public object ProductoCln { get; private set; }
        public Form1( FrmAutenticacion frmAutenticacion)
        {
            InitializeComponent();
            this.frmAutenticacion = frmAutenticacion;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(755, 310);
        }
        private void listar()
        {
            var lista = SerieCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["titulo"].HeaderText = "Titulo";
            dgvLista.Columns["sinopsis"].HeaderText = "Sinopsis";
            dgvLista.Columns["director"].HeaderText = "Director";
            dgvLista.Columns["duracion"].HeaderText = "Duracion";
            dgvLista.Columns["fechaEstreno"].HeaderText = "Fecha Estreno";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";

            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
            if (lista.Count > 0) dgvLista.Rows[0].Cells["titulo"].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }
        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(755, 468);
            txtTitulo.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(755, 468);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var serie = SerieCln.get(id);

            txtTitulo.Text = serie.titulo;
            txtSinopsis.Text = serie.sinopsis;
            txtDirector.Text = serie.director;
            nudDuracion.Value = serie.duracion;
            txtTitulo.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string titulo = dgvLista.Rows[index].Cells["titulo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"Está seguro que sea dar de baja el Producto con código {titulo}", "::: Mensaje - Minerva ::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                SerieCln.eliminar(id, Util.usuario.usuario);
                listar();
                MessageBox.Show($"Producto dado de baja correctamente", "::: Mensaje - Minerva ::", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(755, 310);
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                guardar();
            }
           
        }

        private bool validar()
        {
            bool esValido = true;
            erpTitulo.SetError(txtTitulo, "");
            erpSinopsis.SetError(txtSinopsis, "");
            erpDirector.SetError(txtDirector, "");

            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                erpTitulo.SetError(txtTitulo, "El campo titulo es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtSinopsis.Text))
            {
                erpSinopsis.SetError(txtSinopsis, "El campo sinopsis es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDirector.Text))
            {
                erpDirector.SetError(txtDirector, "El campo director es obligatorio");
                esValido = false;
            }
            return esValido;
        }

        private void guardar()
        {

            var serie = new Serie();
            

            serie.titulo = txtTitulo.Text.Trim();
            serie.sinopsis = txtSinopsis.Text.Trim();
            serie.director = txtDirector.Text.Trim();
            serie.duracion = (int)nudDuracion.Value;
            
            serie.usuarioRegistro = Util.usuario.usuario;

            serie.fechaEstreno = DateTime.Now;

            if (esNuevo)
            {
                serie.registroActivo = true;
                SerieCln.insertar(serie);

            }
            else
            {
                int index = dgvLista.CurrentCell.RowIndex;
                serie.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                SerieCln.actualizar(serie);
            }

            btnCancelar.PerformClick();
            listar();
            dgvLista.Rows[dgvLista.Rows.Count - 1].Selected = true; // Separar con búsqueda
            MessageBox.Show($"Serie insertado correctamente","::: Mensaje - LabECC :::",MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void limpiar()
        {
            txtTitulo.Text = String.Empty;
            txtSinopsis.Text = String.Empty;
            txtDirector.Text = String.Empty;
            nudDuracion.Text = String.Empty;
            
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgvLista_AutoSizeColumnModeChanged(object sender, DataGridViewAutoSizeColumnModeEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAutenticacion.Visible = true;
        }

        
    }
}
