using System.Formats.Asn1;

namespace GateWay;

public class ClientManagerService : IClientManagerContract
{
    private readonly ILogger<ClientManagerService> logger;
    private readonly IClientContract clientContract;

    public ClientManagerService(ILogger<ClientManagerService> logger, IClientContract clientContract)
    {
        this.logger = logger;
        this.clientContract = clientContract;
    }

    /// <summary>
    /// Retrieves client list.
    /// </summary>
    /// <returns>
    // Collection of clients
    // </returns>
    public async Task<List<Client>> GetClientsAsync()
    {
        logger.LogInformation("Getting client collection ...");
        try
        {
            var clientList = await clientContract.GetClientsAsync();

            logger.LogInformation("Client collection retrieved...");

            return clientList;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong.");
            throw;
        }

    }


    /// <summary>
    /// Creates a record of a client
    /// </summary>
    /// <param name="client"></param>
    /// <returns>
    // Boolean indicating success or failure.
    // </returns>
    public async Task<Boolean> CreateClientAsync(Client client)
    {
        logger.LogInformation("Creating a new client  ...");
        try
        {
            var isCreated = await clientContract.CraeteClient(client);

            if (isCreated) logger.LogInformation("Client {clientcode} was created ...", client.Name);

            else logger.LogError("Failed to create client {clientcode}...", client.Name);

            return isCreated;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Failed to create client");
            throw;
        }

    }


    /// <summary>
    /// Method links a contact to a client.
    /// </summary>
    /// <param name="updateClient"></param>
    /// <returns>
    // Boolean indicating success or failure
    // </returns>
    public async Task<Boolean> LinkContactAsync(UpdateClient updateClient)
    {
        logger.LogInformation("Call to link contacts to client : {clientCode} ...", updateClient.clientCode);
        try
        {
            var isLinked = await clientContract.LinkContacts(updateClient);

            if (isLinked) logger.LogInformation("Contacts linked successful for client {clientcode} ...", updateClient.clientCode);

            else logger.LogError("Failed to link contacts for client {clientcode}...", updateClient.clientCode);

            return isLinked;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Failed to link a contact");
            throw;
        }

    }


    /// <summary>
    /// Method unlinks a linked contact
    /// </summary>
    /// <param name="updateClient"></param>
    /// <returns>
    // Boolean indicating success or failure
    // </returns>
    public async Task<Boolean> DelinkContactAsync(UpdateClient updateClient)
    {
        logger.LogInformation("Call to delink contacts for client {clientcode} ...", updateClient.clientCode);
        try
        {
            var isDelinked = await clientContract.DelinkContacts(updateClient);

            if (isDelinked) logger.LogInformation("Contacts unlinked successful for client {clientcode} ...", updateClient.clientCode);

            else logger.LogError("Failed to delink contacts for client {clientcode}...", updateClient.clientCode);

            return isDelinked;

        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Failed to unlik a contact");
            throw;
        }

    }
}
