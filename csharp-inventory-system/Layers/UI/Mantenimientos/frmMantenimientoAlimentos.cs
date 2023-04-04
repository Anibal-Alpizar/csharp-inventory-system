using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.BLL;
using csharp_inventory_system.Layers.Entities;
using csharp_inventory_system.Util;
using log4net;
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
    public partial class frmMantenimientoAlimentos : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public frmMantenimientoAlimentos()
        {
            InitializeComponent();
        }
        Panel p = new Panel();
        private void btnMouseEnter(Object sender, EventArgs e)
        {
            //Button btn = sender as Button;
            //pMenu.Controls.Add(p);
            //p.BackColor = Color.Aqua;
            //p.Size = new Size(140, 5);
            //p.Location = new Point(btn.Location.X, btn.Location.Y + 40);
        }

        private void btnMouseLeave(Object sender, EventArgs e)
        {
            pMenu.Controls.Remove(p);
        }

        private void button3_Click_1(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
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

            this.txtProducto.Enabled = false;
            this.txtPrecioUnitario.Enabled = false;
            this.txtEntrante.Enabled = false;
            this.txtSaliente.Enabled = false;

            btnAceptar.Enabled = false;
            btnCancelar.Enabled = false;
            this.cmbUnidadMedida.Enabled = false;

            if (cmbUnidadMedida.Items.Count > 0) this.cmbUnidadMedida.SelectedIndex = 0;

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    this.txtProducto.Enabled = true;
                    this.txtPrecioUnitario.Enabled = true;
                    this.txtEntrante.Enabled = true;
                    this.txtSaliente.Enabled = true;
                    this.cmbUnidadMedida.Enabled = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtProducto.Focus();
                    break;

                case EstadoMantenimiento.Editar:
                    // habilitar
                    break;

                case EstadoMantenimiento.Borrar:
                    // habilitar
                    break;

                case EstadoMantenimiento.Ninguno: break;
            }
        }

        private void toolStripBtnNuevo_Click(object sender, EventArgs e)
        {
            this.CambiarEstado(EstadoMantenimiento.Nuevo);
        }

        private void frmMantenimientoAlimentos_Load(object sender, EventArgs e)
        {
            try
            {
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
            string query = "SELECT Nombre FROM BodegaProducto WHERE TipoBodega = 'Alimentos'";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Nombre";
            cmbProductos.DataSource = dt;
            dgvDatos.AutoGenerateColumns = false;
            dgvDatos.RowTemplate.Height = 50;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            IBLLBodegaProducto _IBLLBodegaProducto = new BLLBodegaProducto();
            dgvDatos.DataSource = _IBLLBodegaProducto.GetAllProductos();
            CambiarEstado(EstadoMantenimiento.Ninguno);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            BodegaProducto oBodegaProducto = null;
            IBLLBodegaProducto _IBLLBodegaProducto = new BLLBodegaProducto();
            try
            {
                if (txtProducto.Text == "")
                {
                    MessageBox.Show("El nombre del producto es un dato requerido!", "Atención");
                    return;
                }
                if (txtPrecioUnitario == null)
                {
                    MessageBox.Show("El precio unitario es un dato requerido!", "Atención");
                    return;
                }
                if (txtEntrante == null)
                {
                    MessageBox.Show("La cantidad entrante debe ser mayor a cero!", "Atención");
                    return;
                }
                if (txtSaliente == null)
                {
                    MessageBox.Show("La cantidad saliente debe ser cero o mayor!", "Atención");
                    return;
                }

                oBodegaProducto = new BodegaProducto();

                //oBodegaProducto.IdBodegaProducto
                oBodegaProducto.TipoBodega = txtAlimentos.Text;
                oBodegaProducto.Nombre = this.txtProducto.Text;
                oBodegaProducto.UnidadMedida = cmbUnidadMedida.SelectedItem.ToString();
                oBodegaProducto.Precio = double.Parse(this.txtPrecioUnitario.Text);
                oBodegaProducto.Fecha = DateTime.Now;
                oBodegaProducto.InventarioInicial = int.Parse(this.txtPrecioUnitario.Text) * int.Parse(this.txtEntrante.Text);
                oBodegaProducto.CantidadEntradas = int.Parse(this.txtEntrante.Text);
                oBodegaProducto.CantidadSalidas = int.Parse(this.txtSaliente.Text);
                oBodegaProducto.InventarioFinal = 0;

                oBodegaProducto = _IBLLBodegaProducto.SaveBodegaProducto(oBodegaProducto);

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
            string query = $"SELECT SUM(CantidadFinal) FROM BodegaProducto WHERE TipoBodega='Alimentos' AND Nombre='{nombreProducto}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
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
    }
}
