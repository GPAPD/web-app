using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using web_app.Models;

public class ChatController : Controller
{
    private static readonly HttpClient client = new HttpClient();

    [HttpPost]
    public async Task<ActionResult> SendMessage([FromBody] MessageRequest request)
    {
        if (request == null)
            return BadRequest("Request is null");

        string userInput = request.user_input ?? "";

        // Send chat_history as-is; FastAPI expects a list of objects
        var payload = new
        {
            user_input = userInput,
            chat_history = request.chat_history
        };

        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://localhost:8000/live-chat", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(responseString);
        return Json(new { answer = data.answer });
    }
}
