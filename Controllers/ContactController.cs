using GateWay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactManagerContract contactManager;
        private readonly ILogger<ContactController> logger;
        public ContactController(ILogger<ContactController> logger, IContactManagerContract contactManager)
        {
            this.contactManager = contactManager;
            this.logger = logger;
        }

        /// <summary>
        /// A method that retrieves a collection of contacts
        /// </summary>
        /// <returns>
        // A collection of contacts
        // </returns>
        [HttpGet("contacts")]
        public async Task<ActionResult<List<Contact>>> GetContacts()
        {
            this.logger.LogInformation("Initiating a call to get contacts .....");
            try
            {
                var contancList = await contactManager.GetContactsAsync();
                if (contancList == null) return NotFound("Contact not found");

                return Ok(contancList);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// A call to create a new record of a contact.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>
        // Boolean indicating success or failure, thereafter retrieve new collectin containg newly created contact 
        // </returns>
        [HttpPost("create")]
        public async Task<ActionResult<List<Contact>>> CreateContact(Contact contact)
        {
            this.logger.LogInformation("Request to create contact {name}", contact.Name);

            try
            {
                var isCreated = await contactManager.CreateContactAsync(contact);

                if (isCreated == false) return BadRequest("Failed to create record...");

                this.logger.LogInformation("Client {code} is created succesful..", contact.Name);


                return Ok(await contactManager.GetContactsAsync());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Something went wrong");
                throw;
            }

        }

        /// <summary>
        /// Method links clients to a particular contacts. 
        /// </summary>
        /// <param name="updateContact"></param>
        /// <returns>
        // Boolean indicating success or failure
        // </returns>
        [HttpPut("link")]
        public async Task<IActionResult> LinkContacts(UpdateContact updateContact)
        {
            var isLiked = await contactManager.LinkClientAsync(updateContact);

            return Ok(isLiked);
        }

        /// <summary>
        /// A call to thet database to unlink the selectes clients that were linked to a contact
        /// </summary>
        /// <param name="updateContact"></param>
        /// <returns>
        // Boolean indicating success or failure
        // /returns>
        [HttpPut("delink")]
        public async Task<IActionResult> DelinkContacts(UpdateContact updateContact)
        {
            var isDelinked = await contactManager.DelinkClientAsync(updateContact);

            return Ok(isDelinked);
        }
    }
}
