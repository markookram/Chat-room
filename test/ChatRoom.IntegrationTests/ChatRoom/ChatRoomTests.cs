using System.Linq;
using System.Web;
using ChatRoom.Application.Abstractions.Events.Enum;
using ChatRoom.Domain.Events.Enum;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Xunit;
using Xunit.Sdk;

namespace ChatRoom.IntegrationTests.ChatRoom;

[TestCaseOrderer(
    "ChatRoom.IntegrationTests.PriorityOrderer",
    "RunTestsInOrder.XUnit")]
public class ChatRoomTests : IClassFixture<TestingWebAppFactoryFixture<Program>>,
    IClassFixture<ChatRoomDataStoreFixture>
{
    private readonly HttpClient _client;
    private readonly ChatRoomDataStoreFixture _dataFixture;
    private readonly TestingWebAppFactoryFixture<Program> _factoryFixture;

    public ChatRoomTests(TestingWebAppFactoryFixture<Program> factoryFixture,
        ChatRoomDataStoreFixture dataFixture)
    {
        _client = factoryFixture.CreateClient(new WebApplicationFactoryClientOptions()
        {
            BaseAddress = new Uri("https://localhost:44336/")
        });

        _factoryFixture = factoryFixture;
        _dataFixture = dataFixture;
    }

    [Theory, TestPriority(1)]
    [InlineData(GranularityType.All, "<textarea class=\"form-control\" rows=\"20\">08:10 PM:\tMike on Sql leaves\r\n06:08 PM:\tMike on Sql comments: Oooo sorry guys I have to go on another meeting, sorryyyyy....\r\n04:06 PM:\tKate on Sql high-fives: Bob on Sql\r\n02:04 PM:\tBob on Sql comments: Before we start, Kate last time forgot to tell you how I like your high-five gesture :)\r\n12:02 PM:\tAlice on Sql leaves\r\n10:00 AM:\tAlice on Sql comments: Yes, sorry my headphones were muted.\r\n07:58 AM:\tKate on Sql comments: Alice do you hear us?\r\n05:56 AM:\tBob on Sql comments: Same to you\r\n03:54 AM:\tMike on Sql comments: Hi...\r\n01:52 AM:\tAlice on Sql enters the room\r\n11:50 PM:\tKate on Sql enters the room\r\n09:48 PM:\tBob on Sql enters the room\r\n07:46 PM:\tMike on Sql enters the room</textarea>")]
    [InlineData(GranularityType.Hourly, "<textarea class=\"form-control\" rows=\"20\">08:00 PM: \r\n\r\n        Mike on Sql leaves\r\n\r\n06:00 PM: \r\n\r\n        Mike on Sql comments: Oooo sorry guys I have to go on another meeting, sorryyyyy....\r\n\r\n04:00 PM: \r\n\r\n        Kate on Sql high-fives: Bob on Sql\r\n\r\n02:00 PM: \r\n\r\n        Bob on Sql comments: Before we start, Kate last time forgot to tell you how I like your high-five gesture :)\r\n\r\n12:00 PM: \r\n\r\n        Alice on Sql leaves\r\n\r\n10:00 AM: \r\n\r\n        Alice on Sql comments: Yes, sorry my headphones were muted.\r\n\r\n07:00 AM: \r\n\r\n        Kate on Sql comments: Alice do you hear us?\r\n\r\n05:00 AM: \r\n\r\n        Bob on Sql comments: Same to you\r\n\r\n03:00 AM: \r\n\r\n        Mike on Sql comments: Hi...\r\n\r\n01:00 AM: \r\n\r\n        Alice on Sql enters the room\r\n\r\n11:00 PM: \r\n\r\n        Kate on Sql enters the room\r\n\r\n09:00 PM: \r\n\r\n        Bob on Sql enters the room\r\n\r\n07:00 PM: \r\n\r\n        Mike on Sql enters the room\r\n\r\n</textarea>")]
    [InlineData(GranularityType.Minute, "<textarea class=\"form-control\" rows=\"20\">08:10 PM: \r\n\r\n        Mike on Sql leaves\r\n\r\n06:08 PM: \r\n\r\n        Mike on Sql comments: Oooo sorry guys I have to go on another meeting, sorryyyyy....\r\n\r\n04:06 PM: \r\n\r\n        Kate on Sql high-fives: Bob on Sql\r\n\r\n02:04 PM: \r\n\r\n        Bob on Sql comments: Before we start, Kate last time forgot to tell you how I like your high-five gesture :)\r\n\r\n12:02 PM: \r\n\r\n        Alice on Sql leaves\r\n\r\n10:00 AM: \r\n\r\n        Alice on Sql comments: Yes, sorry my headphones were muted.\r\n\r\n07:58 AM: \r\n\r\n        Kate on Sql comments: Alice do you hear us?\r\n\r\n05:56 AM: \r\n\r\n        Bob on Sql comments: Same to you\r\n\r\n03:54 AM: \r\n\r\n        Mike on Sql comments: Hi...\r\n\r\n01:52 AM: \r\n\r\n        Alice on Sql enters the room\r\n\r\n11:50 PM: \r\n\r\n        Kate on Sql enters the room\r\n\r\n09:48 PM: \r\n\r\n        Bob on Sql enters the room\r\n\r\n07:46 PM: \r\n\r\n        Mike on Sql enters the room\r\n\r\n</textarea>")]
    [InlineData(GranularityType.AggregatedByHour, "<textarea class=\"form-control\" rows=\"20\">08:00 PM: \r\n\t\t1 left\r\n\r\n06:00 PM: \r\n\t\t1 comments\r\n\r\n04:00 PM: \r\n\t\t1 person high-fived 1 other people\r\n\r\n02:00 PM: \r\n\t\t1 comments\r\n\r\n12:00 PM: \r\n\t\t1 left\r\n\r\n10:00 AM: \r\n\t\t1 comments\r\n\r\n07:00 AM: \r\n\t\t1 comments\r\n\r\n05:00 AM: \r\n\t\t1 comments\r\n\r\n03:00 AM: \r\n\t\t1 comments\r\n\r\n01:00 AM: \r\n\t\t1 person entered\r\n\r\n11:00 PM: \r\n\t\t1 person entered\r\n\r\n09:00 PM: \r\n\t\t1 person entered\r\n\r\n07:00 PM: \r\n\t\t1 person entered\r\n\r\n</textarea>")]
    [InlineData(GranularityType.AggregatedByMinute, "<textarea class=\"form-control\" rows=\"20\">08:10 PM: \r\n\t\t1 left\r\n\r\n06:08 PM: \r\n\t\t1 comments\r\n\r\n04:06 PM: \r\n\t\t1 person high-fived 1 other people\r\n\r\n02:04 PM: \r\n\t\t1 comments\r\n\r\n12:02 PM: \r\n\t\t1 left\r\n\r\n10:00 AM: \r\n\t\t1 comments\r\n\r\n07:58 AM: \r\n\t\t1 comments\r\n\r\n05:56 AM: \r\n\t\t1 comments\r\n\r\n03:54 AM: \r\n\t\t1 comments\r\n\r\n01:52 AM: \r\n\t\t1 person entered\r\n\r\n11:50 PM: \r\n\t\t1 person entered\r\n\r\n09:48 PM: \r\n\t\t1 person entered\r\n\r\n07:46 PM: \r\n\t\t1 person entered\r\n\r\n</textarea>")]
    public async Task ChatRoomLog_CheckLogs_Test(GranularityType type, string expectedResult)
    {
        var testRoom = _dataFixture.Rooms[0];

        var postRequest = new HttpRequestMessage(HttpMethod.Get, "/ChatRoomLog/CheckLogs");
        var formModel = new Dictionary<string, string>
        {
            { "granularityId", $"{(int)type}" },
            { "chatRoomId", $"{testRoom.Id}" }
        };
        postRequest.Content = new FormUrlEncodedContent(formModel);

        var response = await _client.SendAsync(postRequest);

        response.EnsureSuccessStatusCode();

        var responseString = HttpUtility.HtmlDecode(await response.Content.ReadAsStringAsync());

        responseString.Should().Contain(expectedResult);
    }

    [Fact, TestPriority(2)]
    public async Task ChatRoom_WelcomeToTheLoby_Test()
    {
        var response = await _client.GetAsync("ChatRoom/Index");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        responseString.Should().Contain("Welcome to our Chat-room loby");
    }

    [Fact, TestPriority(3)]
    public async Task ChatRoom_GetIntoTheRoom_Test()
    {
        var testRoom = _dataFixture.Rooms[0]; //Sql server
        var testParticipant = _dataFixture.Participants[3]; //Alice on Sql

        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/ChatRoom/GetIntoTheRoom");
        var formModel = new Dictionary<string, string>
        {
            { "ChatRoomId", $"{testRoom.Id}" },
            { "ParticipantId", $"{testParticipant.Id}" }
        };
        postRequest.Content = new FormUrlEncodedContent(formModel);

        var chatEventsNum = _factoryFixture.EventsContext.ChatEvents.Count();

        var response = await _client.SendAsync(postRequest);

        response.EnsureSuccessStatusCode();

        var responseString = HttpUtility.HtmlDecode(await response.Content.ReadAsStringAsync());

        //Assert
        _factoryFixture.EventsContext.ChatEvents.Count().Should().Be(chatEventsNum + 1);

        responseString.Should().Contain($"Welcome '{testParticipant.Name}' in the '{testRoom.Name}' chat-room.");

        var @event = _factoryFixture.EventsContext.ChatEvents.OrderByDescending(e => e.CreatedOn).First();
        @event.Should().NotBeNull();
        @event.Type.Should().Be(EventType.ParticipantEntered);

        //Clean
        _factoryFixture.RemoveEvent(@event);
    }

    [Theory, TestPriority(4)]
    [InlineData(EventType.ParticipantCommented,  "{0} sent.")]
    [InlineData(EventType.PariticipantHighFived, "High-five successfully sent to {0}.")]
    public async Task ChatRoom_SendMessage_Test(EventType type,  string expectedResult)
    {
        var testRoom = _dataFixture.Rooms[0];
        var testParticipant = _dataFixture.Participants[0];
        var testToParticipant = _dataFixture.Participants[1];
        var comment = "Hi from test";

        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/ChatRoom/SendMessage");
        var formModel = new Dictionary<string, string>
        {
            { "roomId", $"{testRoom.Id}" },
            { "participantId", $"{testParticipant.Id}" },
            { "message", comment}
        };
        if (type == EventType.PariticipantHighFived)
        {
            formModel.Add("toParticipantId", $"{testToParticipant.Id}");
        }

        postRequest.Content = new FormUrlEncodedContent(formModel);

        var chatEventsNum = _factoryFixture.EventsContext.ChatEvents.Count();

        var response = await _client.SendAsync(postRequest);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        //Assert
        _factoryFixture.EventsContext.ChatEvents.Count().Should().Be(chatEventsNum + 1);

        responseString.Should().Contain(string.Format(expectedResult, type == EventType.ParticipantCommented ? comment : $"{testToParticipant.Name}"));

        var @event = _factoryFixture.EventsContext.ChatEvents.OrderByDescending(e => e.CreatedOn).First();
        @event.Should().NotBeNull();
        @event.Type.Should().Be(type);

        //Clean
        _factoryFixture.RemoveEvent(@event);
    }
}