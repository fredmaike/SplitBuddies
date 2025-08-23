using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

using GroupModel = SplitBuddies.Models.Group; // Alias para evitar conflicto con Regex.Group

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para invitar usuarios a un grupo específico.
    /// Permite ingresar un email, valida el formato, verifica duplicados y agrega la invitación.
    /// </summary>
    public partial class InvitationForm : Form
    {
        private readonly GroupModel group;      // Grupo al que se enviará la invitación
        private readonly User currentUser;       // Usuario que envía la invitación

        /// <summary>
        /// Constructor principal.
        /// </summary>
        /// <param name="group">Grupo al que se enviarán las invitaciones.</param>
        /// <param name="user">Usuario que realiza la invitación.</param>
        public InvitationForm(GroupModel group, User user)
        {
            InitializeComponent();

            this.group = group ?? throw new ArgumentNullException(nameof(group));
            this.currentUser = user ?? throw new ArgumentNullException(nameof(user));

            this.Text = $"Invitar usuarios a {group.GroupName}";
        }

        /// <summary>
        /// Evento al hacer clic en el botón "Enviar".
        /// Valida el email, revisa duplicados y agrega la invitación.
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string inviteeEmail = txtEmail.Text.Trim();

            try
            {
                ValidateEmail(inviteeEmail);      // Validar formato
                CheckDuplicateMember(inviteeEmail); // Revisar que no sea miembro ya
                AddInvitation(inviteeEmail);      // Guardar invitación

                MessageBox.Show("Invitación enviada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Clear();                 // Limpiar campo
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Valida el formato del email usando una expresión regular simple.
        /// </summary>
        /// <param name="email">Email a validar.</param>
        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                throw new ArgumentException("Ingrese un email válido.");
        }

        /// <summary>
        /// Verifica que el email no esté ya registrado como miembro del grupo.
        /// </summary>
        /// <param name="email">Email a comprobar.</param>
        private void CheckDuplicateMember(string email)
        {
            group.Members ??= new System.Collections.Generic.List<string>();

            if (group.Members.Any(m => string.Equals(m, email, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Ese email ya es miembro del grupo.");
        }

        /// <summary>
        /// Crea y guarda la invitación en el DataManager.
        /// </summary>
        /// <param name="email">Email del invitado.</param>
        private void AddInvitation(string email)
        {
            var dataManager = DataManager.Instance;

            dataManager.Invitations.Add(new Invitation
            {
                InvitationId = dataManager.GetNextInvitationId(), // Genera un ID único
                GroupId = group.GroupId,
                InviteeEmail = email,
                InviterEmail = currentUser.Email,
                Status = InvitationStatus.Pending // Estado inicial pendiente
            });

            dataManager.SaveInvitations(); // Persiste los cambios
        }
    }
}
