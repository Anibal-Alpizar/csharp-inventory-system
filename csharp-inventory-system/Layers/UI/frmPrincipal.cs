using csharp_inventory_system.Layers.UI.Mantenimientos;
using csharp_inventory_system.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_inventory_system.Layers.UI
{
    public partial class frmPrincipal : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public frmPrincipal()
        {
            InitializeComponent();
        }

        Panel p = new Panel();
        private void btnMouseEnter(Object sender, EventArgs e)
        {
            Button btn = sender as Button;
            pMenu.Controls.Add(p);
            p.BackColor = Color.Aqua;
            p.Size = new Size(140, 5);
            p.Location = new Point(btn.Location.X, btn.Location.Y+40);
        }

        private void btnMouseLeave(Object sender, EventArgs e)
        {
            pMenu.Controls.Remove(p);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!pInventarios.Visible)
            {
                pInventarios.Visible = true;
            }
            else
            {
                pInventarios.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //
            frmMantenimientoAlimentos ofrmMantenimientosAlimentos;
            try
            {
                ofrmMantenimientosAlimentos = new frmMantenimientoAlimentos();
                //ofrmMantenimientosAlimentos.MdiParent = this;
                ofrmMantenimientosAlimentos.Show();
                this.Hide();
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //
            frmMantenimientoAseoPersonal ofrmMantenimientoAseoPersonal;
            try
            {
                ofrmMantenimientoAseoPersonal = new frmMantenimientoAseoPersonal();
                ofrmMantenimientoAseoPersonal.Show();
                this.Hide();
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmMantenimientoLimpieza ofrmMantenimientoLimpieza;
            try
            {
                ofrmMantenimientoLimpieza = new frmMantenimientoLimpieza();
                ofrmMantenimientoLimpieza.Show();
                this.Hide();
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
