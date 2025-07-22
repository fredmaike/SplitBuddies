namespace SplitBuddies.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        // Comparación lógica basada en el correo
        public override bool Equals(object obj)
        {
            if (obj is not User other)
                return false;

            return this.Email == other.Email;
        }

        public override int GetHashCode()
        {
            return Email?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
