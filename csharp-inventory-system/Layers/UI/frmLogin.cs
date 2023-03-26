using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.BLL;
using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_inventory_system.Layers.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            IBLLUser _BLLUser = new BLLUser();
            epError.Clear();
            User oUser = null;
            try
            {
                if (string.IsNullOrEmpty(this.txtLogin.Text))
                {
                    epError.SetError(txtLogin, "Debe ingresar un nombre de usuario");
                    this.txtLogin.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtPassword.Text))
                {
                    epError.SetError(txtPassword, "Debe ingresar una contraseña");
                    this.txtPassword.Focus();
                    return;
                }
                oUser = _BLLUser.Login(this.txtLogin.Text, this.txtPassword.Text);
            }
            
        }
    }


}
