
using System;
using System.Windows.Forms;

public partial class GroupDetailsForm : Form
{
    private Grupo grupo;
    private GastoController gastoController = new();

    public GroupDetailsForm(Grupo grupoSeleccionado)
    {
        InitializeComponent();
        grupo = grupoSeleccionado;
        MostrarBalances();
        MostrarGastos();
    }

    private void MostrarBalances()
    {
        lstBalances.Items.Clear();
        foreach (var usuario in grupo.Miembros)
        {
            decimal balance = gastoController.ObtenerBalanceUsuario(grupo, usuario);
            lstBalances.Items.Add($"{usuario.Nombre}: {balance:+0;-0}");
        }
    }

    private void MostrarGastos()
    {
        lstGastos.Items.Clear();
        foreach (var gasto in grupo.Gastos)
        {
            string linea = $"{gasto.Nombre} - ₡{gasto.Monto}";
            lstGastos.Items.Add(linea);
        }
    }

    private void lstGastos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = lstGastos.SelectedIndex;
        if (index >= 0)
        {
            var gasto = grupo.Gastos[index];
            var detalleForm = new DetalleGastoForm(gasto);
            detalleForm.ShowDialog();
        }
    }
}
