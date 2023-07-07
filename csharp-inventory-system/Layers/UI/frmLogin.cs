using log4net;
using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.BLL;
using csharp_inventory_system.Layers.Entities;
using csharp_inventory_system.Util;
using System.Drawing;

namespace csharp_inventory_system.Layers.UI
{
    public partial class frmLogin : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        private int contador = 0;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            //Close();
            Application.Exit();
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
                if (oUser == null)
                {
                    ++contador;
                    MessageBox.Show("Error en el acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (contador == 3)
                    {
                        MessageBox.Show("Se equivocó en 3 ocasiones, el Sistema se Cerrará por seguridad", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                        Application.Exit();
                    }
                }
                else
                {
                    bool respuesta = await EfectoConexion();
                    _MyLogControlEventos.InfoFormat("Entaplicación :{0}" /*Settings.Default.Nombre*/ );
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("Bienvenida Sra. Kattia Herrera");
                    frmPrincipal frm = new frmPrincipal();
                    frm.Show();
                    this.Hide();
                }
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> EfectoConexion()
        {
            toolStripPbBarra.Visible = true;
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                //Thread.Sleep(100);
                this.toolStripPbBarra.Value += 10;
                this.sttBarraInferior.Refresh();
            }
            return true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //if (txtLogin.Text == "")
            //    //{
            //    //    txtLogin.Text = "Digite el User";
            //    //    return;
            //    //}
            //    txtLogin.ForeColor = Color.Black;
            //    panel5.Visible = false;
            //}
            //catch 
            //{

               
            //}
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtPassword.Text == "")
                //{
                //    txtLogin.Text = "123456789";
                //    return;
                //}
                txtLogin.ForeColor = Color.Black;
                panel5.Visible= false;
            }
            catch
            {


            }
        }

        private void txtLogin_Click(object sender, EventArgs e)
        {
            txtLogin.SelectAll();
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }

        private void btnAceptar_MouseEnter(object sender, EventArgs e)
        {
            btnAceptar.ForeColor= Color.Black;
        }

        private void btnAceptar_MouseLeave(object sender, EventArgs e)
        {
            btnAceptar.ForeColor = Color.Black;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
