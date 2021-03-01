using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Renta_de_vehiculos
{
    public partial class FormVehiculos : Form
    {
        VehiculoBL _vehiculo;
        public FormVehiculos()
        {
            InitializeComponent();

            _vehiculo = new VehiculoBL();
            vehiculoBindingSource.DataSource = _vehiculo.ObtenerVehiculos();
        }

        private void activoCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void activoLabel_Click(object sender, EventArgs e)
        {

        }

        private void vehiculoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            vehiculoBindingSource.EndEdit();
            var vehiculo = (Vehiculo)vehiculoBindingSource.Current;

            var resultado = _vehiculo.GuardarVehiculo(vehiculo);
            if (resultado.Exitoso == true)
            {
                vehiculoBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotenes(true);

                MessageBox.Show("Producto Guardado");

            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
            
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _vehiculo.AgregarVehiculo();
            vehiculoBindingSource.MoveLast();

            DeshabilitarHabilitarBotenes(false);
        }

        private void DeshabilitarHabilitarBotenes(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            Cancelar.Visible = !valor;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
            if (iDTextBox.Text != "")
            {
                var resultado = MessageBox.Show("¿Desea Eliminar este Registro?", "Eliminar ", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(iDTextBox.Text);
                    Eliminar(id);
                }
                
            }
        }

        private void Eliminar(int id)
        {
            var resultado = _vehiculo.EliminarVehiculo(id);

            if (resultado == true)
            {
                vehiculoBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el producto");
            }

        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotenes(true);
            Eliminar(0);
        }
    }
}
