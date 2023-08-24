using Amazon.SQS.Model;
using AWS_SQS.Interfaces;
using AWS_SQS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS_SQS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SQSController : ControllerBase
    {
        private readonly ISqsService _sqsService;

        public SQSController(ISqsService sqsService)
        {
            _sqsService = sqsService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PublishToDoItem([FromBody] ToDoItemModel model)
        {
            try
            {
                await _sqsService.PublishToDoItemAsync(model);
                return Ok("Message sent to SQS");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Retrieve")]
        public async Task<IActionResult> RetrieveToDoItems()
        {
            try
            {
                var items = await _sqsService.GetToDoItemsAsync();
                if (items.Count()!=0)
                    return Ok(items);
                else
                    return Ok("No queue found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
