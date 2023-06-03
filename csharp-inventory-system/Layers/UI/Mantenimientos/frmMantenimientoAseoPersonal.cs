﻿using csharp_inventory_system.Interfaces.Bodega;
using csharp_inventory_system.Layers.BLL.Bodega;
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

    public partial class frmMantenimientoAseoPersonal : Form
    {
        private static readonly ILog _MyLogControlEventos = LogManager.GetLogger("MyControlEventos");
        public frmMantenimientoAseoPersonal()
        {
            InitializeComponent();
        }

        Panel p = new Panel();

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

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
                    this.txtProducto.Enabled = false;
                    this.txtPrecioUnitario.Enabled = true;
                    this.txtEntrante.Enabled = true;
                    this.txtSaliente.Enabled = true;
                    this.cmbUnidadMedida.Enabled = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtPrecioUnitario.Focus();
                    break;

                case EstadoMantenimiento.Borrar:
                    break;
                case EstadoMantenimiento.Ninguno: break;
            }
        }

        private void CargarDatos()
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=inventariodb;User ID=sa;Password=123456");
            string query = "SELECT Nombre FROM BodegaProducto WHERE TipoBodega = 'Aseo_Personal'";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Nombre";
            cmbProductos.DataSource = dt;
            dgvDatos.AutoGenerateColumns = false;
            //dgvDatos.RowTemplate.Height =50 ;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            IBLLBodegaAseoPersonal _IBLLBodegaProducto = new BLLBodegaAseoPersonal();
            dgvDatos.DataSource = _IBLLBodegaProducto.GetAllProductosAseoPersonal();
            CambiarEstado(EstadoMantenimiento.Ninguno);
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            IBLLBodegaAseoPersonal _IBLLBodegaAseoPersonal = new BLLBodegaAseoPersonal();
            try
            {
                BodegaProducto oBodegaProducto = new BodegaProducto();
                if (string.IsNullOrEmpty(txtProducto.Text))
                {
                    MessageBox.Show("El nombre del producto es un dato requerido!", "Atención");
                    this.txtProducto.Focus();
                    return;
                }
                oBodegaProducto.TipoBodega = txtAlimentos.Text;
                oBodegaProducto.Nombre = this.txtProducto.Text;
                oBodegaProducto.UnidadMedida = cmbUnidadMedida.SelectedItem.ToString();
                oBodegaProducto.Precio = double.Parse(this.txtPrecioUnitario.Text);
                oBodegaProducto.Fecha = DateTime.Now;
                oBodegaProducto.InventarioInicial = int.Parse(this.txtPrecioUnitario.Text) * int.Parse(this.txtEntrante.Text);
                oBodegaProducto.CantidadEntradas = int.Parse(this.txtEntrante.Text);
                oBodegaProducto.CantidadSalidas = int.Parse(this.txtSaliente.Text);
                oBodegaProducto.InventarioFinal = 0;

                oBodegaProducto = await _IBLLBodegaAseoPersonal.SaveBodegaAseoPersonal(oBodegaProducto);

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

        private void toolStripBtnNuevo_Click(object sender, EventArgs e)
        {
            this.CambiarEstado(EstadoMantenimiento.Nuevo);
        }

        private void toolStripBtnEditar_Click(object sender, EventArgs e)
        {

        }

        private void toolStripBtnBorrar_Click(object sender, EventArgs e)
        {

        }

        private void toolStripBtnSalir_Click(object sender, EventArgs e)
        {

        }

        private void frmMantenimientoAseoPersonal_Load(object sender, EventArgs e)
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
    }
}
