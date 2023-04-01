﻿using csharp_inventory_system.Util;
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

        private void CambiarEstado(EstadoMantenimiento estado)
        {
            // limpiar todo

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    // habilitar 
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
    }
}