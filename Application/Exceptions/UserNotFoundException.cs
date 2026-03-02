namespace Application.Exceptions
{
    public class UserNotFoundException: Exception
    {
        public int StatusCode { get; } = 404;
        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(): base("Usuario no encontrado.")
        {
        }
    }
}
