namespace GateWay;

public interface IClientContract
{
    Task<List<Client>> GetClientsAsync();
    Task<Boolean> CraeteClient(Client client);
    Task<Boolean> LinkContacts(UpdateClient updateClient);
    Task<Boolean> DelinkContacts(UpdateClient updateClient);
}
