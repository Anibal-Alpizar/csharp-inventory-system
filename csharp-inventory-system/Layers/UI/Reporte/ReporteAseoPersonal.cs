using csharp_inventory_system.Layers.UI.Acerca_de;
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

namespace csharp_inventory_system.Layers.UI.Reporte
{
    public partial class ReporteAseoPersonal : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public ReporteAseoPersonal()
        {
            InitializeComponent();
        }

        Panel p = new Panel();
        private void ReporteAseoPersonal_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSetProductos.BodegaProducto6' table. You can move, or remove it, as needed.
            
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            FrmAcercaDe ofrmAcercaDe;
            try
            {
                ofrmAcercaDe = new FrmAcercaDe();
                ofrmAcercaDe.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            frmPrincipal ofrmPrincipal;
            try
            {
                ofrmPrincipal = new frmPrincipal();
                ofrmPrincipal.Show();
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

        private void button10_Click(object sender, EventArgs e)
        {
            ReporteAlimentos oReporteAlimentos;
            try
            {
                oReporteAlimentos = new ReporteAlimentos();
                oReporteAlimentos.Show();
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

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteLimpieza oReporteLimpieza;
            try
            {
                oReporteLimpieza = new ReporteLimpieza();
                oReporteLimpieza.Show();
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

        private void btnReporteGeneral_Click(object sender, EventArgs e)
        {
            ReporteGeneral oReporteGeneral;
            try
            {
                oReporteGeneral = new ReporteGeneral();
                oReporteGeneral.Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (!plnReportesMnu.Visible)
            {
                plnReportesMnu.Visible = true;
            }
            else
            {
                plnReportesMnu.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmMantenimientoAlimentos ofrmMantenimientoAlimentos;
            try
            {
                ofrmMantenimientoAlimentos = new frmMantenimientoAlimentos();
                ofrmMantenimientoAlimentos.Show();
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

        private void button7_Click(object sender, EventArgs e)
        {
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSetProductos.BodegaProducto' Puede moverla o quitarla según sea necesario.
            this.bodegaProducto3TableAdapter1.Fill(this.dataSetProductos.BodegaProducto3);
            this.reportViewer1.RefreshReport();
        }

        private void btnBuscar_MouseEnter(object sender, EventArgs e)
        {
            btnBuscar.ForeColor = Color.Black;
        }

        private void btnBuscar_MouseLeave(object sender, EventArgs e)
        {
            btnBuscar.ForeColor = Color.Black;
        }

        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            this.bodegaProducto6TableAdapter.Fill(this.dataSetProductos.BodegaProducto6);

            this.reportViewer2.RefreshReport();
        }

        private void btnBuscar2_MouseEnter(object sender, EventArgs e)
        {
            btnBuscar2.ForeColor = Color.Black;
        }

        private void btnBuscar2_MouseLeave(object sender, EventArgs e)
        {
            btnBuscar2.ForeColor= Color.Black;
        }
    }
}
