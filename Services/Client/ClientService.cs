namespace GateWay;

public class ClientService : IClientContract
{
    private readonly ILogger<ClientService> logger;
    private readonly HttpClient client;
    public ClientService(ILogger<ClientService> logger, IHttpClientFactory factory)
    {
        this.logger = logger;
        this.client = factory.CreateClient("Client");
    }


    /// <summary>
    /// A call to externa API to get client collection
    /// </summary>
    /// <returns>
    // Collection of clients
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Client>> GetClientsAsync()
    {
        try
        {
            this.logger.LogInformation("Making a call to get client collection .");

            var response = await client.GetAsync("/api/Client/clients");

            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Success call to get client collection");

                var clients = await response.Content.ReadFromJsonAsync<List<Client>>();

                return clients!;
            }

            if (response != null)
            {
                this.logger.LogError("Something went wrong on getting client collection");
                throw new Exception("Unexpected error happened upon returning on getting client collection..");
            }
            return null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on getting data");
            throw new Exception("Unexpected error happened upon returning on getting data..");
        }

    }


    /// <summary>
    /// A call to external API to create client record
    /// </summary>
    /// <param name="clientData"></param>
    /// <returns>
    // Boolean indicating success or failure
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<Boolean> CraeteClient(Client clientData)
    {
        this.logger.LogInformation("Making a call to create client : {name}.", clientData.Name);
        try
        {
            var response = await client.PostAsJsonAsync("/api/Client/create", clientData);

            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Successfully created client {clientName}", clientData.Name);

                return response.IsSuccessStatusCode;
            }
            else
            {
                this.logger.LogError("Something went wrong on creating client {data}", clientData.Name);
                throw new Exception($"Unexpected error happened upon creating  client record {clientData.Name}..");
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on getting creating client");
            throw new Exception("Unexpected error happened upon returning on creating client.");
        }

    }


    /// <summary>
    /// A call to external API to link contact(s) to a client
    /// </summary>
    /// <param name="updateClient"></param>
    /// <returns>
    // Boolean indicating success or failure
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<Boolean> LinkContacts(UpdateClient updateClient)
    {
        this.logger.LogInformation("Making a call link contact(s) to client : {name}  ", updateClient.clientCode);
        try
        {
            var response = await client.PutAsJsonAsync("/api/Client/link", updateClient);

            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Successfully linked contact(s) to client : {clientName} .", updateClient.clientCode);

                return response.IsSuccessStatusCode;
            }
            else
            {
                this.logger.LogError("Something went wrong on linking contact(s) to client {data}", updateClient.clientCode);
                throw new Exception($"Unexpected error happened upon linking contact(s) to client : {updateClient.clientCode}..");
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on linking contact(s)");
            throw new Exception("Unexpected error happened upon linking contact(s) ..");
        }
    }


    /// <summary>
    /// A call to external API to delink contact(s) from a particular client
    /// </summary>
    /// <param name="updateClient"></param>
    /// <returns>
    // Boolean indicating success of failure
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<Boolean> DelinkContacts(UpdateClient updateClient)
    {
        this.logger.LogInformation("Making a call to delink contact(s) from client : {name} ", updateClient.clientCode);
        try
        {
            var response = await client.PutAsJsonAsync("/api/Client/delink", updateClient);

            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Successfully delinked contact(s) from client  {clientName} .", updateClient.clientCode);

                return response.IsSuccessStatusCode;
            }
            else
            {
                this.logger.LogError("Something went wrong on delinking contact(s) from client {data}", updateClient.clientCode);
                throw new Exception($"Unexpected error happened upon delinking contact(s) from  record {updateClient.clientCode}..");
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on delinking contact(s)");
            throw new Exception("Unexpected error happened upon delinking contact(s) ..");
        }
    }
}
