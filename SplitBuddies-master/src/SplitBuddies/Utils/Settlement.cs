using System;

namespace SplitBuddies.Models
{
    /// <summary>
    /// Representa una transferencia propuesta de un usuario a otro para saldar deudas.
    /// </summary>
    public class Settlement
    {
        /// <summary>
        /// Correo electrónico del usuario que debe pagar.
        /// </summary>
        public string FromEmail { get; init; }

        /// <summary>
        /// Correo electrónico del usuario que recibirá el pago.
        /// </summary>
        public string ToEmail { get; init; }

        /// <summary>
        /// Monto a transferir. Siempre debe ser positivo.
        /// </summary>
        public decimal Amount { get; init; }

        /// <summary>
        /// Constructor opcional para crear la transferencia.
        /// </summary>
        /// <param name="fromEmail">Usuario que paga.</param>
        /// <param name="toEmail">Usuario que recibe.</param>
        /// <param name="amount">Monto a transferir (≥ 0).</param>
        public Settlement(string fromEmail, string toEmail, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(fromEmail))
                throw new ArgumentException("El correo del emisor no puede estar vacío.", nameof(fromEmail));
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("El correo del receptor no puede estar vacío.", nameof(toEmail));
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "El monto debe ser mayor o igual a cero.");

            FromEmail = fromEmail;
            ToEmail = toEmail;
            Amount = amount;
        }

        // Constructor por defecto para deserialización JSON
        public Settlement() { }
    }
}
