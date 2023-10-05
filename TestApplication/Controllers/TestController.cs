using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Core;
using TestApplication.Usecase;

namespace TestApplication.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private const string BiometricsImportedTopicName = "topic_0";
        private readonly IProducer<String, String> _producer;
        private readonly ITestUsecase _testUsecase;

        public TestController(IProducer<String, String> producer, ITestUsecase testUsecase)
        {
            _producer = producer;
            _testUsecase = testUsecase;
        }
        
        [HttpGet, Route("")]
        public async Task<ActionResult<Test>> GetTests()
        {
            string msg = "Hello World!";
            var result = await _producer.ProduceAsync(BiometricsImportedTopicName,
                new Message<string, string>() { Value = msg });

            _producer.Flush();

            if (_testUsecase.SendMail(msg))
            {
                Console.WriteLine("Message sent successfully.");
            }
            return Ok("Test");
        }

        [HttpGet, Route("status")]
        public ActionResult<Test> GetTestStatus()
        {
            return Ok("Success");
        }
    }
}
