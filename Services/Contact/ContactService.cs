namespace GateWay;

public class ContactService : IContactContract
{
    private readonly ILogger<ContactService> logger;
    private readonly HttpClient client;
    public ContactService(ILogger<ContactService> logger, IHttpClientFactory factory)
    {
        this.logger = logger;
        this.client = factory.CreateClient("Client");
    }

    /// <summary>
    /// A call to external API to retrieve contact list
    /// </summary>
    /// <returns>
    // Collection of contacts
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Contact>> GetContactsAsync()
    {
        try
        {
            this.logger.LogInformation("Making a call to get client collection.");

            var response = await client.GetAsync("/api/Contact/contacts");

            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Success call to get contact collection");

                var contactList = await response.Content.ReadFromJsonAsync<List<Contact>>();

                return contactList!;
            }

            if (response != null)
            {
                this.logger.LogError("Something went wrong on getting data");
                throw new Exception("Unexpected error happened upon returning on getting data..");
            }
            return null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on getting data");
            throw new Exception("Unexpected error happened upon returning on getting contact collection..");
        }

    }


    /// <summary>
    /// A call to external API to create a record of contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns>
    // Boolean indicating success or failure
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<Boolean> CreateContact(Contact contact)
    {
        this.logger.LogInformation("Making a call create contact : {name}.", contact.Name);
        try
        {
            var response = await client.PostAsJsonAsync("/api/Contact/create", contact);

            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Successfully created contact {contactName}", contact.Name);

                return response.IsSuccessStatusCode;
            }
            else
            {
                this.logger.LogError("Something went wrong on creating contact {contactName}", contact.Name);
                throw new Exception($"Unexpected error happened upon creating record {contact.Name}..");
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on getting data");
            throw new Exception("Something went wrong upon creating contact");
        }

    }


    /// <summary>
    /// A call to external API to link clients to contact
    /// </summary>
    /// <param name="updateContact"></param>
    /// <returns>
    // Boolean indicating success or failure
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<Boolean> LinkClients(UpdateContact updateContact)
    {
        this.logger.LogInformation("Making a call link client : {name} to contacts ", updateContact.Email);
        try
        {
            var response = await client.PutAsJsonAsync("/api/Contact/link", updateContact);
            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Successfully linked client(s) to contacts  {contact}.", updateContact.Email);

                return response.IsSuccessStatusCode;
            }
            else
            {
                this.logger.LogError("Something went wrong on linking client(s)");
                throw new Exception($"Unexpected error happened upon linking client(s) to {updateContact.Email}..");
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on linking client");
            throw new Exception("Unexpected error happened upon linking client ..");
        }
    }

    /// <summary>
    /// A call to unlink liked client(s) to particular contact
    /// </summary>
    /// <param name="updateContact"></param>
    /// <returns>
    // Boolean indicating success or failure
    // </returns>
    /// <exception cref="Exception"></exception>
    public async Task<Boolean> DelinkClients(UpdateContact updateContact)
    {
        this.logger.LogInformation("Making a call to delink client : {name} to contacts ", updateContact);
        try
        {
            var response = await client.PutAsJsonAsync("/api/Contact/delink", updateContact);
            if (response.IsSuccessStatusCode)
            {
                this.logger.LogInformation("Successfully delinked client {clientName} to contacts.", updateContact);

                return response.IsSuccessStatusCode;
            }
            else
            {
                this.logger.LogError("Something went wrong on delinking client(s)");
                throw new Exception($"Unexpected error happened upon delinking clients ..");
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on delinking client");
            throw new Exception("Unexpected error happened upon delinking client ..");
        }
    }

}
