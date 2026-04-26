using GateWay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientManagerContract clientManager;
        private readonly ILogger<ClientController> logger;

        public ClientController(ILogger<ClientController> logger, IClientManagerContract clientManager)
        {
            this.clientManager = clientManager;
            this.logger = logger;

        }

        /// <summary>
        /// Get method that retrieves a collection of clients
        /// </summary>
        /// <returns>
        // A collection of clients
        // </returns>
        [HttpGet("clients")]
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            this.logger.LogInformation("Initiating a call to get clients.....");
            try
            {
                var clients = await clientManager.GetClientsAsync();
                if (clients == null) return NotFound("User not found");

                return Ok(clients);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Create a new record of a client
        /// </summary>
        /// <param name="client"></param>
        /// <returns>
        //Boolean indicating a success or failure, therafter retrieves a list of clients with newly created record. 
        // </returns>
        [HttpPost("create")]
        public async Task<ActionResult<List<Client>>> CreateClient(Client client)
        {
            this.logger.LogInformation("Request to create client {name}", client.Name);
            try
            {
                var isCreated = await clientManager.CreateClientAsync(client);

                if (isCreated == false) return BadRequest("Failed to create record...");

                this.logger.LogInformation("Client {code} is created succesful..", client.Name);


                return Ok(await clientManager.GetClientsAsync());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Something went wrong");
                throw;
            }

        }

        /// <summary>
        /// A call to link contacts to a particular client
        /// </summary>
        /// <param name="updateClient"></param>
        /// <returns>
        // Boolean indicates success or failure
        // </returns>
        [HttpPut("link")]
        public async Task<IActionResult> LinkContacts(UpdateClient updateClient)
        {
            try
            {
                var request = await clientManager.LinkContactAsync(updateClient);

                return Ok(request);

            }
            catch (Exception ex)
            {

                this.logger.LogError(ex, "Something went wrong");
                throw;
            }

        }

        /// <summary>
        /// A call to thet database to unlink the selectes contacts that were linked to a client
        /// </summary>
        /// <param name="updateContact"></param>
        /// <returns>
        // Bool indicating success or failure.
        // </returns>
        [HttpPut("delink")]
        public async Task<IActionResult> DelinkContacts(UpdateClient updateContact)
        {
            try
            {
                var request = await clientManager.DelinkContactAsync(updateContact);

                return Ok(request);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Something went wrong");
                throw;
            }

        }

    }
}
