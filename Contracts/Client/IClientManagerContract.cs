namespace GateWay;

public interface IClientManagerContract
{
    Task<List<Client>> GetClientsAsync();
    Task<Boolean> CreateClientAsync(Client client);
    Task<Boolean> LinkContactAsync(UpdateClient updateClient);
    Task<Boolean> DelinkContactAsync(UpdateClient updateClient);
}
