namespace GateWay;

public class ContactManagerService : IContactManagerContract
{
    private readonly ILogger<ContactManagerService> logger;
    private readonly IContactContract contactContract;

    public ContactManagerService(ILogger<ContactManagerService> logger, IContactContract contactContract)
    {
        this.logger = logger;
        this.contactContract = contactContract;
    }


    /// <summary>
    /// Method retrieves a collection of contacts
    /// </summary>
    /// <returns>
    // Collection of contacts
    // </returns>
    public async Task<List<Contact>> GetContactsAsync()
    {
        try
        {
            logger.LogInformation("Getting contact list...");

            var contactList = await contactContract.GetContactsAsync();

            return contactList;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something went wrong on retrieving contact lis");
            throw;

        }

    }


    /// <summary>
    /// Method creates a record of a contact.
    /// </summary>
    /// <param name="contact"></param>
    /// <returns>
    // Boolean indicating a success or failure
    // </returns>
    public async Task<Boolean> CreateContactAsync(Contact contact)
    {
        this.logger.LogInformation("Request to create a contact record..");
        try
        {
            var response = await contactContract.CreateContact(contact);

            if (response) this.logger.LogInformation("Successfully created a contact record..");

            else this.logger.LogError("Failed to create a contact record..");

            return response;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Failed to create contact...");
            throw;
        }
    }


    /// <summary>
    /// Method links client(s) to a partular contact
    /// </summary>
    /// <param name="updateContact"></param>
    /// <returns></returns>
    public async Task<Boolean> LinkClientAsync(UpdateContact updateContact)
    {
        this.logger.LogInformation("Linking client(s) to contact {email}", updateContact.Email);
        try
        {
            var response = await contactContract.LinkClients(updateContact);

            if (response) this.logger.LogInformation("Successfully linking client(s) to contact {email}", updateContact.Email);

            else this.logger.LogError("Failed to linking client(s) to contact {email}", updateContact.Email);

            return response;

        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Failed to link client(s) to contact {email}...", updateContact.Email);
            throw;
        }

    }


    /// <summary>
    /// Method unlink a client(s) from a contact.
    /// </summary>
    /// <param name="updateContact"></param>
    /// <returns>
    // Boolean indicating a success or failure.
    // </returns>
    public async Task<Boolean> DelinkClientAsync(UpdateContact updateContact)
    {
        this.logger.LogInformation("Unlinking client(s) to contact {email}", updateContact.Email);
        try
        {
            var response = await contactContract.DelinkClients(updateContact);

            if (response) this.logger.LogInformation("Successfully unlinking client(s) to contact {email}", updateContact.Email);

            else this.logger.LogError("Failed to unlinking client(s) to contact {email}", updateContact.Email);

            return response;

        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Failed to unlink client(s) to contact {email}...", updateContact.Email);
            throw;
        }

    }
}
