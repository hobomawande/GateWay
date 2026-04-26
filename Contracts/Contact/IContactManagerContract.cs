namespace GateWay;

public interface IContactManagerContract
{
    Task<List<Contact>> GetContactsAsync();
    Task<Boolean> CreateContactAsync(Contact contact);
    Task<Boolean> LinkClientAsync(UpdateContact updateContact);
    Task<Boolean> DelinkClientAsync(UpdateContact updateContact);
}
