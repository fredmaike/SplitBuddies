using System;
using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Controllers
{
    /// <summary>
    /// Controlador responsable de la gestión de gastos dentro de un grupo.
    /// Permite crear un gasto, validar participantes y registrar la transacción.
    /// </summary>
    public class ExpenseController
    {
        /// <summary>
        /// Crea y registra un nuevo gasto en el sistema.
        /// </summary>
        /// <param name="name">Nombre del gasto.</param>
        /// <param name="description">Descripción opcional del gasto.</param>
        /// <param name="paidByEmail">Correo del usuario que pagó.</param>
        /// <param name="participants">Lista de correos de los participantes del gasto.</param>
        /// <param name="amount">Monto del gasto.</param>
        /// <param name="date">Fecha en que ocurrió el gasto.</param>
        /// <param name="groupId">Identificador del grupo al que pertenece el gasto.</param>
        /// <returns>El objeto Expense creado y registrado.</returns>
        /// <exception cref="ArgumentException">Si algún parámetro es inválido.</exception>
        /// <exception cref="InvalidOperationException">Si el grupo o los participantes no son válidos.</exception>
        public Expense CreateExpense(
            string name,
            string description,
            string paidByEmail,
            List<string> participants,
            decimal amount,
            DateTime date,
            int groupId)
        {
            // Validar que los parámetros de entrada son correctos
            ValidateExpenseInput(name, paidByEmail, participants, amount);

            // Obtener el grupo correspondiente al ID
            var group = GetGroupById(groupId);

            // Validar que el pagador pertenece al grupo
            if (!group.Members.Contains(paidByEmail))
                throw new InvalidOperationException($"El usuario {paidByEmail} no pertenece al grupo {group.GroupName}.");

            // Validar que todos los participantes pertenecen al grupo
            EnsureParticipantsBelongToGroup(group, participants);

            // Crear el objeto Expense con los datos proporcionados
            var expense = new Expense
            {
                Id = DataManager.Instance.GetNextExpenseId(), // Generar ID único
                Name = name,
                Description = description,
                PaidByEmail = paidByEmail,
                InvolvedUsersEmails = participants,
                Amount = amount,
                Date = date,
                GroupId = groupId
            };

            // Registrar el gasto en la lista global de gastos
            DataManager.Instance.Expenses.Add(expense);

            // Registrar el gasto dentro del grupo
            group.Expenses.Add(expense.Id);

            return expense;
        }

        #region Private Helpers

        /// <summary>
        /// Valida que los parámetros del gasto sean correctos.
        /// Lanza ArgumentException si algún valor es inválido.
        /// </summary>
        private static void ValidateExpenseInput(string name, string paidByEmail, List<string> participants, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del gasto no puede estar vacío.", nameof(name));

            if (string.IsNullOrWhiteSpace(paidByEmail))
                throw new ArgumentException("El correo del pagador es obligatorio.", nameof(paidByEmail));

            if (participants == null || participants.Count == 0)
                throw new ArgumentException("Debe haber al menos un participante.", nameof(participants));

            if (amount <= 0)
                throw new ArgumentException("El monto del gasto debe ser mayor que cero.", nameof(amount));
        }

        /// <summary>
        /// Obtiene un grupo por su ID.
        /// Lanza InvalidOperationException si no existe.
        /// </summary>
        private static Group GetGroupById(int groupId)
        {
            var group = DataManager.Instance.Groups.FirstOrDefault(g => g.GroupId == groupId);
            return group ?? throw new InvalidOperationException($"No se encontró el grupo con ID {groupId}.");
        }

        /// <summary>
        /// Valida que todos los participantes pertenezcan al grupo.
        /// Lanza InvalidOperationException si algún participante es externo.
        /// </summary>
        private static void EnsureParticipantsBelongToGroup(Group group, List<string> participants)
        {
            var invalidUsers = participants.Where(p => !group.Members.Contains(p)).ToList();
            if (invalidUsers.Any())
                throw new InvalidOperationException(
                    $"Los siguientes usuarios no pertenecen al grupo {group.GroupName}: {string.Join(", ", invalidUsers)}.");
        }

        #endregion
    }
}
