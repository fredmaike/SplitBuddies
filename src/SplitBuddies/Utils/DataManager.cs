using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using SplitBuddies.Models;  

public class DataManager
{
    private static DataManager instance;
    public static DataManager Instance => instance ??= new DataManager();

    public string BasePath { get; set; } = "";

    public List<User> Users { get; private set; } = new List<User>();
    public List<Group> Groups { get; private set; } = new List<Group>();
    public List<Expense> Expenses { get; private set; } = new List<Expense>();

    public void LoadUsers()
    {
        string path = Path.Combine(BasePath, "usuarios.json"); 
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            Users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }
    }

    public void LoadGroups()
    {
        string path = Path.Combine(BasePath, "grupos.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            Groups = JsonConvert.DeserializeObject<List<Group>>(json) ?? new List<Group>();
        }
    }

    public void LoadExpenses()
    {
        string path = Path.Combine(BasePath, "gastos.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            Expenses = JsonConvert.DeserializeObject<List<Expense>>(json) ?? new List<Expense>();
        }
    }

    public void SaveUsers(string fileName)
    {
        string path = Path.Combine(BasePath, fileName);
        var json = JsonConvert.SerializeObject(Users, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void SaveGroups(string fileName)
    {
        string path = Path.Combine(BasePath, fileName);
        var json = JsonConvert.SerializeObject(Groups, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void SaveExpenses(string fileName)
    {
        string path = Path.Combine(BasePath, fileName);
        var json = JsonConvert.SerializeObject(Expenses, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}
