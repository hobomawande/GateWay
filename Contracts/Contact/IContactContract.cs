namespace GateWay;

public interface IContactContract
{
    Task<List<Contact>> GetContactsAsync();
    Task<Boolean> CreateContact(Contact contact);
    Task<Boolean> LinkClients(UpdateContact updateContact);
    Task<Boolean> DelinkClients(UpdateContact updateContact);
}
