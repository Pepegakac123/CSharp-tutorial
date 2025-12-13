using System;

User admin = new User("Admin", true);
IDocument adminDoc = new AccessProxy(admin);

Console.WriteLine($"Próba dostępu użytkownika: {admin.Name}");
adminDoc.ReadContent();

User guest = new User("Gość", false);
IDocument guestDoc = new AccessProxy(guest);

Console.WriteLine($"\nPróba dostępu użytkownika: {guest.Name}");
guestDoc.ReadContent();

public class User
{
    public string Name { get; private set; }
    public bool HasAccess { get; private set; }

    public User(string name, bool hasAccess)
    {
        Name = name;
        HasAccess = hasAccess;
    }
}

public interface IDocument
{
    void ReadContent();
}
public class SecureDocument : IDocument
{
    public void ReadContent()
    {
        Console.WriteLine("Reading secure content... [TAJNE DANE]");
    }
}

public class AccessProxy : IDocument
{
    private SecureDocument _realDocument;
    private User _user;

    public AccessProxy(User user)
    {
        _user = user;
        _realDocument = new SecureDocument();
    }

    public void ReadContent()
    {
        if (_user.HasAccess)
        {
            _realDocument.ReadContent();
        }
        else
        {
            Console.WriteLine("Access denied!");
        }
    }
}
