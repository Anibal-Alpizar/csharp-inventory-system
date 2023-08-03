using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Interfaces.Bodega;
using csharp_inventory_system.Layers.BLL;
using csharp_inventory_system.Layers.BLL.Bodega;
using csharp_inventory_system.Layers.Entities;
using csharp_inventory_system.Layers.UI.Acerca_de;
using csharp_inventory_system.Layers.UI.Reporte;
using csharp_inventory_system.Util;
using log4net;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_inventory_system.Layers.UI.Mantenimientos
{
    public partial class frmMantenimientoLimpieza : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public frmMantenimientoLimpieza()
        {
            InitializeComponent();
        }

        Panel p = new Panel();

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            pMenu.Controls.Add(p);
            p.BackColor = Color.Aqua;
            p.Size = new Size(140, 5);
            p.Location = new Point(btn.Location.X, btn.Location.Y + 40);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            pMenu.Controls.Remove(p);
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

        /// <summary>
        /// Cambia el estado del proceso
        /// </summary>
        /// <param name="estado">Enum del proceso</param>
        private void CambiarEstado(EstadoMantenimiento estado)
        {
            this.txtProducto.Clear();
            this.txtPrecioUnitario.Clear();
            this.txtEntrante.Clear();
            this.txtSaliente.Clear();
            this.txtInventarioInicial.Clear();
            this.txtNuevasSalidas.Text = "0";
            this.txtNuevasEntradas.Text = "0";
           


            this.txtProducto.Enabled = false;
            this.txtPrecioUnitario.Enabled = false;
            this.txtEntrante.Visible = false;
            this.txtSaliente.Visible = false;
            this.lblCantidadEntradas.Visible = false;
            this.lblCantidadSalidas.Visible = false;
            this.txtNuevasSalidas.Enabled = false;
            this.txtNuevasEntradas.Enabled = false;
            this.dtpFechaIngreso.Enabled = false;
            this.txtInventarioInicial.Enabled = false;

            btnAceptar.Enabled = false;
            btnCancelar.Enabled = false;
            this.cmbUnidadMedida.Enabled = false;
            this.cboTipoEntrada.Enabled = false;

            if (cmbUnidadMedida.Items.Count > 0) this.cmbUnidadMedida.SelectedIndex = 0;

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    this.txtProducto.Enabled = true;
                    this.txtPrecioUnitario.Enabled = true;
                    this.txtEntrante.Text = "0";
                    this.txtSaliente.Text = "0";
                    this.lblCantidadEntradas.Visible = false;
                    this.lblCantidadSalidas.Visible = false;
                    this.txtNuevasSalidas.Enabled = true;
                    this.txtNuevasEntradas.Enabled = true;
                    this.txtInventarioInicial.Enabled = true;
                    this.dtpFechaIngreso.Enabled=true;
                    this.cmbUnidadMedida.Enabled = true;
                    this.cboTipoEntrada.Enabled=true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtProducto.Focus();
                    break;

                case EstadoMantenimiento.Editar:
                    this.txtProducto.Enabled = false;
                    this.txtPrecioUnitario.Enabled = true;
                    this.txtEntrante.Enabled = false;
                    this.txtSaliente.Enabled = false;
                    this.lblCantidadEntradas.Visible = false;
                    this.lblCantidadSalidas.Visible = false;
                    this.txtNuevasSalidas.Enabled = true;
                    this.txtNuevasEntradas.Enabled = true;
                    this.txtInventarioInicial.Enabled = true;
                    this.dtpFechaIngreso.Enabled = true;
                    this.cmbUnidadMedida.Enabled = true;
                    this.cboTipoEntrada.Enabled = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtPrecioUnitario.Focus();
                    break;

                case EstadoMantenimiento.Borrar:
                    break;
                case EstadoMantenimiento.Ninguno: break;
            }
        }

        private void toolStripBtnNuevo_Click(object sender, EventArgs e)
        {
            this.CambiarEstado(EstadoMantenimiento.Nuevo);
        }

        private void frmMantenimientoLimpieza_Load(object sender, EventArgs e)
        {

            try
            {
                //AjustarDatagrid();
                CargarDatos();
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=inventariodb;User ID=sa;Password=123456");
            string query = "SELECT Nombre FROM BodegaProducto WHERE TipoBodega = 'Limpieza'";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Nombre";
            cmbProductos.DataSource = dt;
            dgvDatos.AutoGenerateColumns = false;
            //dgvDatos.RowTemplate.Height =50 ;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            IBLLBodegaLimpieza _IBLLBodegaProducto = new BLLBodegaLimpieza();
            dgvDatos.DataSource = _IBLLBodegaProducto.GetAllProductosLimpieza();
            CambiarEstado(EstadoMantenimiento.Ninguno);
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            IBLLBodegaLimpieza _IBLLBodegaProducto = new BLLBodegaLimpieza();
            try
            {
                BodegaProducto oBodegaProducto = new BodegaProducto();
                if(string.IsNullOrEmpty(txtProducto.Text))
                {
                    MessageBox.Show("El nombre del producto es un dato requerido!", "Atención");
                    this.txtProducto.Focus();
                    return;
                }
                oBodegaProducto.TipoBodega = txtAlimentos.Text;
                oBodegaProducto.Nombre = this.txtProducto.Text;
                oBodegaProducto.UnidadMedida = cmbUnidadMedida.SelectedItem.ToString();
                oBodegaProducto.TipoEntrada = cboTipoEntrada.SelectedItem.ToString();
                oBodegaProducto.Precio = double.Parse(this.txtPrecioUnitario.Text);
                oBodegaProducto.Fecha = DateTime.Parse(this.dtpFechaIngreso.Text);
                oBodegaProducto.InventarioInicial = int.Parse(this.txtInventarioInicial.Text);
                oBodegaProducto.CantidadEntradas =int.Parse(this.txtNuevasEntradas.Text) + int.Parse(this.txtEntrante.Text);
                oBodegaProducto.CantidadSalidas = int.Parse(this.txtNuevasSalidas.Text)  + int.Parse(this.txtSaliente.Text);
                oBodegaProducto.InventarioFinal = 0;
              

                oBodegaProducto = await _IBLLBodegaProducto.SaveBodegaLimpieza(oBodegaProducto);

                if (oBodegaProducto != null)
                    this.CargarDatos();
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }       
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.CambiarEstado(EstadoMantenimiento.Ninguno);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataRowView selectedRow = (DataRowView)cmbProductos.SelectedItem;
            string nombreProducto = selectedRow["Nombre"].ToString();
            string connectionString = "Data Source=localhost;Initial Catalog=inventariodb;User ID=sa;Password=123456";
            string query = $"SELECT ( (InventarioInicial + CantidadEntradas) - CantidadSalidas) AS CantidadDisponible FROM BodegaProducto WHERE TipoBodega='Limpieza' AND Nombre=@nombreProducto";

            //(InventarioInicial + CantidadEntradas - CantidadSalidas)
            //InventarioInicial es el valor inicial del inventario para el producto en la bodega.
            //CantidadEntradas es la cantidad total de entradas del producto en la bodega.
            //CantidadSalidas es la cantidad total de salidas del producto de la bodega.

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombreProducto", nombreProducto);
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    this.txtResultado.Text = result.ToString();
                }
                else
                {
                    this.txtResultado.Text = "0";
                }
            }


        }

        private void btnAceptar_MouseEnter(object sender, EventArgs e)
        {
            btnAceptar.ForeColor = Color.Black;
        }

        private void btnAceptar_MouseLeave(object sender, EventArgs e)
        {
            btnAceptar.ForeColor = Color.Black;
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            btnCancelar.ForeColor = Color.Black;
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.ForeColor = Color.Black;
        }

        private void btnBuscar_MouseEnter(object sender, EventArgs e)
        {
            btnBuscar.ForeColor = Color.Black;
        }

        private void btnBuscar_MouseLeave(object sender, EventArgs e)
        {
            btnBuscar.ForeColor = Color.Black;
        }

        private void toolStripBtnEditar_Click(object sender, EventArgs e)
        {
            BodegaProducto oBodegaProducto = null;
            try
            {
                if (this.dgvDatos.SelectedCells.Count > 0)
                {
                    // Obtener el índice de la celda seleccionada
                    int rowIndex = this.dgvDatos.SelectedCells[0].RowIndex;
                    // Obtener el objeto BodegaProducto de la fila correspondiente
                    oBodegaProducto = this.dgvDatos.Rows[rowIndex].DataBoundItem as BodegaProducto;

                    // Cambiar de estado
                    this.CambiarEstado(EstadoMantenimiento.Editar);
                    this.txtAlimentos.Text = oBodegaProducto.TipoBodega;
                    this.txtProducto.Text = oBodegaProducto.Nombre;
                    cmbProductos.SelectedIndex = cmbProductos.FindString(oBodegaProducto.IdBodegaProducto.ToString());
                    cboTipoEntrada.SelectedIndex = cboTipoEntrada.FindString(oBodegaProducto.IdBodegaProducto.ToString());
                    this.txtPrecioUnitario.Text = oBodegaProducto.Precio.ToString();
                    this.txtEntrante.Text = oBodegaProducto.CantidadEntradas.ToString();
                    this.txtInventarioInicial.Text= oBodegaProducto.InventarioInicial.ToString();
                    this.txtSaliente.Text= oBodegaProducto.CantidadSalidas.ToString();
                    this.dtpFechaIngreso.Value = oBodegaProducto.Fecha;
                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void toolStripBtnBorrar_Click(object sender, EventArgs e)
        {
            IBLLBodegaLimpieza _BLLBodegaLimpieza = new BLLBodegaLimpieza();
            try
            {
                if (this.dgvDatos.SelectedCells.Count > 0)
                {
                    int rowIndex = this.dgvDatos.SelectedCells[0].RowIndex;
                    BodegaProducto oBodegaProducto = this.dgvDatos.Rows[rowIndex].DataBoundItem as BodegaProducto;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro {oBodegaProducto.Nombre.Trim()} {oBodegaProducto.Nombre.Trim()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _BLLBodegaLimpieza.DeleteBodegaProducto(oBodegaProducto.Nombre);
                        this.CargarDatos();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void toolStripBtnSalir_Click(object sender, EventArgs e)
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

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button11_Click(object sender, EventArgs e)
        {
            ReporteAlimentos ofrmAcercaDe;
            try
            {
                ofrmAcercaDe = new ReporteAlimentos();
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

        private void button10_Click(object sender, EventArgs e)
        {
            ReporteAseoPersonal oReporteAseoPersonal;
            try
            {
                oReporteAseoPersonal = new ReporteAseoPersonal();
                oReporteAseoPersonal.Show();
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
            ReporteGeneral ofrmAcercaDe;
            try
            {
                ofrmAcercaDe = new ReporteGeneral();
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
    }
}
