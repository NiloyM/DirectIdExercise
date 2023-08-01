using DirectIdExercise.Configuration;
using DirectIdExercise.Handlers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Linq.Expressions;

namespace DirectIdExercise.Tests
{
    public class GetStatementHandlerTests
    {
        private readonly Mock<ILogger<GetStatementHandler>> _logger;
        public GetStatementHandlerTests() {
            _logger = new Mock<ILogger<GetStatementHandler>>();
           
        }
        [Fact]
        public async void When_Right_File_Is_Provided_The_Handler_Generate_List_Of_Report_Object()
        {
            var directIdConfiguration = new DirectIdConfiguration
            {
                FileToRead = "apollo-carter.json"
            };
            var options = new Mock<IOptions<DirectIdConfiguration>>();
            options.Setup(ap => ap.Value).Returns(directIdConfiguration);

            var getStatementHandler = new GetStatementHandler(_logger.Object, options.Object);
            var reports = await getStatementHandler.Handle(new Quaries.GetStatementQuery(), new CancellationToken());
            Assert.NotNull(reports);
            Assert.Equal(1108, reports.Count);

        }

        [Fact]
        public async void When_Wrong_File_Is_Provided_The_Handler_Throws_Exception()
        {
            var directIdConfiguration = new DirectIdConfiguration
            {
                FileToRead = "apollo-carter-x.json"
            };
            var options = new Mock<IOptions<DirectIdConfiguration>>();
            options.Setup(ap => ap.Value).Returns(directIdConfiguration);

            var getStatementHandler = new GetStatementHandler(_logger.Object, options.Object);

            await getStatementHandler.Handle(new Quaries.GetStatementQuery(), new CancellationToken());

            _logger.Verify(x => x.Log(
                        It.IsAny<LogLevel>(),
                        It.IsAny<EventId>(),
                        It.IsAny<It.IsAnyType>(),
                        It.IsAny<Exception>(),
                        (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
           

        }
    }
}