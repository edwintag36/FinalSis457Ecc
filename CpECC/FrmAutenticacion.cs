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
    public partial class FrmAutenticacion : Form
    {
        public FrmAutenticacion()
        {
            InitializeComponent();


            //var usuario = new Usuario();
           // usuario.usuario = "edwin";
           // usuario.clave = Util.Encrypt("hola123.");
           // usuario.rol = "administrador";
           // usuario.registroActivo = true;
            //UsuarioCln.insertar(usuario);

            //UsuarioCln.cambiarClave(1, Util.Encrypt("123456"));
        }

        private bool validar()
        {
            bool esValido = true;
            erpUsuario.SetError(txtUsuario, "");
            erpClave.SetError(txtClave, "");

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                erpUsuario.SetError(txtUsuario, "El campo Usuario es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                erpClave.SetError(txtClave, "El campo Contraseña es obligatorio");
                esValido = false;
            }
            return esValido;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (validar()) 
            { 
                string usuario = txtUsuario.Text.Trim();
                string clave = Util.Encrypt(txtClave.Text.Trim());
                var usuarioExistente = UsuarioCln.validar(usuario, clave);
                if (usuarioExistente != null)
                {

                    Util.usuario = usuarioExistente;
                    txtClave.Text = string.Empty;
                    Visible = false;
                    new Form1(this).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrecto", "::: Mensaje - Minerva :::",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
