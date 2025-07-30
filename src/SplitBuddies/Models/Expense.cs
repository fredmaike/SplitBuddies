using System;
using System.Collections.Generic;

public class Expense
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PaidByEmail { get; set; }
    public List<string> InvolvedUsersEmails { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int GroupId { get; set; }

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
