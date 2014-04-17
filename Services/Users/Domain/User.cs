namespace Burgerama.Services.Users.Domain
{
    public sealed class User
    {
        public string Id { get; private set; }

        public string Email { get; private set; }

        public User(string id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
