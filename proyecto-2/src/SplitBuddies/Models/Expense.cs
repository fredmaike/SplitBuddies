using System;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    /// <summary>
    /// Representa un gasto realizado dentro de un grupo.
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Identificador �nico del gasto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre o t�tulo del gasto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripci�n adicional que explique el gasto.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Correo electr�nico de la persona que pag� el gasto.
        /// </summary>
        public string PaidByEmail { get; set; }

        /// <summary>
        /// Lista de correos electr�nicos de los usuarios involucrados en el gasto.
        /// </summary>
        public List<string> InvolvedUsersEmails { get; set; } = new List<string>();

        /// <summary>
        /// Monto total del gasto.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Fecha en que se realiz� el gasto.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Identificador del grupo al que pertenece este gasto.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Expense() { }

        /// <summary>
        /// Constructor que inicializa todas las propiedades necesarias para crear un gasto.
        /// </summary>
        /// <param name="name">Nombre o t�tulo del gasto.</param>
        /// <param name="description">Descripci�n del gasto.</param>
        /// <param name="paidByEmail">Correo de quien pag� el gasto.</param>
        /// <param name="involvedUsersEmails">Lista de correos de los usuarios involucrados.</param>
        /// <param name="amount">Monto total del gasto.</param>
        /// <param name="date">Fecha del gasto.</param>
        /// <param name="groupId">ID del grupo al que pertenece el gasto.</param>
        public Expense(string name, string description, string paidByEmail, List<string> involvedUsersEmails, decimal amount, DateTime date, int groupId)
        {
            Name = name;
            Description = description;
            PaidByEmail = paidByEmail;
            InvolvedUsersEmails = involvedUsersEmails;
            Amount = amount;
            Date = date;
            GroupId = groupId;
        }
    }
}
