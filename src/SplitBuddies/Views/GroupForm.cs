using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public class GroupForm : Form
    {
        private TextBox txtGroupName;
        private Button btnCreate;
        private CheckedListBox clbUsers;

        public Group NuevoGrupo { get; private set; }

        private List<User> usuarios;

        public GroupForm(List<User> users)
        {
            usuarios = users;

            this.Text = "Crear Grupo";
            this.Size = new Size(350, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label();
            lblName.Text = "Nombre del grupo:";
            lblName.Location = new Point(10, 20);
            this.Controls.Add(lblName);

            txtGroupName = new TextBox();
            txtGroupName.Location = new Point(10, 45);
            txtGroupName.Width = 300;
            this.Controls.Add(txtGroupName);

            Label lblUsers = new Label();
            lblUsers.Text = "Seleccione miembros:";
            lblUsers.Location = new Point(10, 80);
            this.Controls.Add(lblUsers);

            clbUsers = new CheckedListBox();
            clbUsers.Location = new Point(10, 105);
            clbUsers.Size = new Size(300, 180);
            foreach (var user in users)
                clbUsers.Items.Add(user);

            clbUsers.DisplayMember = "Name"; // <-- Importante para mostrar nombres reales
            this.Controls.Add(clbUsers);

            btnCreate = new Button();
            btnCreate.Text = "Crear Grupo";
            btnCreate.Location = new Point(10, 300);
            btnCreate.Click += BtnCreate_Click;
            this.Controls.Add(btnCreate);
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGroupName.Text) || clbUsers.CheckedItems.Count == 0)
            {
                MessageBox.Show("Ingrese un nombre y seleccione al menos un miembro.");
                return;
            }

            List<User> seleccionados = new List<User>();
            foreach (var item in clbUsers.CheckedItems)
                seleccionados.Add((User)item);

            NuevoGrupo = new Group
            {
                Name = txtGroupName.Text.Trim(),
                Members = seleccionados,
                Expenses = new List<Expense>()
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
