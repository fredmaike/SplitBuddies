using System;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    // Clase que representa un gasto realizado dentro de un grupo..
    public class Expense
    {
        // Identificador �nico del gasto
        public int Id { get; set; }

        // Nombre del gasto
        public string Name { get; set; }

        // Descripci�n adicional del gasto
        public string Description { get; set; }

        // Correo electr�nico de la persona que pag� el gasto
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
