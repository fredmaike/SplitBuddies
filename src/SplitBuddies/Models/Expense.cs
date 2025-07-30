using System;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    // Clase que representa un gasto realizado dentro de un grupo..
    public class Expense
    {
        // Identificador único del gasto
        public int Id { get; set; }

        // Nombre del gasto
        public string Name { get; set; }

        // Descripción adicional del gasto
        public string Description { get; set; }

        // Correo electrónico de la persona que pagó el gasto
        public string PaidByEmail { get; set; }

        // Lista de correos de los usuarios que participan en el gasto
        public List<string> InvolvedUsersEmails { get; set; } = new List<string>();

        // Monto total del gasto
        public decimal Amount { get; set; }

        // Fecha en que se hizo el gasto
        public DateTime Date { get; set; }

        // ID del grupo al que pertenece este gasto
        public int GroupId { get; set; }

        public Expense() { }

        // Constructor con todos los campos necesarios para crear una instancia de gasto
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
