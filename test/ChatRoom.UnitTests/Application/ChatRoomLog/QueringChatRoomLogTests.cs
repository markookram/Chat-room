using ChatRoom.Application.Abstractions.Events.Enum;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Application.Abstractions.Queries.Params;
using ChatRoom.Application.Services.ChatRoomLog.Queries;
using ChatRoom.Domain.Events;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ChatRoom.UnitTests.Application.ChatRoomLog;

public class QueringChatRoomLogTests  : IClassFixture<ChatRoomEventLogDataStoreFixture>, IClassFixture<ChatRoomDataStoreFixture>
{

    private readonly Mock<IChatRoomLogRepository<ChatEvent>> _mockChatRoomLogRepository;
    private readonly ChatRoomEventLogDataStoreFixture _eventsFixture;
    private readonly ChatRoomDataStoreFixture _chatRoomDataStoreFixture;

    private readonly QueryAll _queryAll;
    private readonly QueryByHour _queryByHour;
    private readonly QueryByMinute _queryByMinute;
    private readonly QueryAggregateByHour _queryAggregateByHour;
    private readonly QueryAggregateByMinute _queryAggregateByMinute;


    public QueringChatRoomLogTests(ChatRoomEventLogDataStoreFixture eventsFixture, ChatRoomDataStoreFixture chatRoomDataStoreFixture)
    {
        _eventsFixture = eventsFixture;
        _chatRoomDataStoreFixture = chatRoomDataStoreFixture;

        _mockChatRoomLogRepository = new Mock<IChatRoomLogRepository<ChatEvent>>();

        _mockChatRoomLogRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_eventsFixture.ChatEvents);

        _queryAll = new QueryAll(It.IsAny<ILogger<QueryAll>>(), _mockChatRoomLogRepository.Object);
        _queryByHour = new QueryByHour(It.IsAny<ILogger<QueryByHour>>(), _mockChatRoomLogRepository.Object);
        _queryByMinute = new QueryByMinute(It.IsAny<ILogger<QueryByMinute>>(), _mockChatRoomLogRepository.Object);
        _queryAggregateByHour = new QueryAggregateByHour(It.IsAny<ILogger<QueryAggregateByHour>>(), _mockChatRoomLogRepository.Object);
        _queryAggregateByMinute = new QueryAggregateByMinute(It.IsAny<ILogger<QueryAggregateByMinute>>(), _mockChatRoomLogRepository.Object);
    }

    [Theory]
    [InlineData(GranularityType.All)]
    [InlineData(GranularityType.Hourly)]
    [InlineData(GranularityType.Minute)]
    [InlineData(GranularityType.AggregatedByHour)]
    [InlineData(GranularityType.AggregatedByMinute)]
    public async Task QueryAllTest(GranularityType type)
    {
        var startDateTime = new DateTime(2023, 3, 18, 8, 10, 0);

        for (int i = 0; i < _eventsFixture.ChatEvents.Count; i++)
        {
            var @event = _eventsFixture.ChatEvents[i];
            @event.TweakDateOfBirth(startDateTime.AddHours(i).AddMinutes(i));
        }

        StringQueryResult result;
        switch (type)
        {
            case GranularityType.All:
                result = await _queryAll.ExecuteAsync(new QueryParams(type).AddRoomId(ChatRoomEventLogDataStoreFixture.RoomId));
                result.Should().NotBeNull();
                result.Result.Length.Should().Be(118);
                result.Result.Should()
                    .Be("11:13 AM:\tKate high-fives: Bob.\r\n10:12 AM:\tAlice leaves.\r\n09:11 AM:\tBob comments: Hi.\r\n08:10 AM:\tMike enters the room.");
                break;
            case GranularityType.Hourly:
                result = await _queryByHour.ExecuteAsync(new QueryParams(type).AddRoomId(ChatRoomEventLogDataStoreFixture.RoomId));
                result.Should().NotBeNull();
                result.Result.Length.Should().Be(176);
                result.Result.Should()
                    .Be("11:00 AM: \r\n\r\n        Kate high-fives: Bob.\r\n\r\n10:00 AM: \r\n\r\n        Alice leaves.\r\n\r\n09:00 AM: \r\n\r\n        Bob comments: Hi.\r\n\r\n08:00 AM: \r\n\r\n        Mike enters the room.\r\n\r\n");
                break;
            case GranularityType.Minute:
                result = await _queryByMinute.ExecuteAsync(new QueryParams(type).AddRoomId(ChatRoomEventLogDataStoreFixture.RoomId));
                result.Should().NotBeNull();
                result.Result.Length.Should().Be(176);
                result.Result.Should()
                    .Be("11:13 AM: \r\n\r\n        Kate high-fives: Bob.\r\n\r\n10:12 AM: \r\n\r\n        Alice leaves.\r\n\r\n09:11 AM: \r\n\r\n        Bob comments: Hi.\r\n\r\n08:10 AM: \r\n\r\n        Mike enters the room.\r\n\r\n");
                break;
            case GranularityType.AggregatedByHour:
                result = await _queryAggregateByHour.ExecuteAsync(new QueryParams(type).AddRoomId(ChatRoomEventLogDataStoreFixture.RoomId));
                result.Should().NotBeNull();
                result.Result.Length.Should().Be(133);
                result.Result.Should()
                    .Be("11:00 AM: \r\n\t\t1 high-fived other people.\r\n\r\n10:00 AM: \r\n\t\t1 left.\r\n\r\n09:00 AM: \r\n\t\t1 comments.\r\n\r\n08:00 AM: \r\n\t\t1 person entered.\r\n\r\n");
                break;
            case GranularityType.AggregatedByMinute:
                result = await _queryAggregateByMinute.ExecuteAsync(new QueryParams(type).AddRoomId(ChatRoomEventLogDataStoreFixture.RoomId));
                result.Should().NotBeNull();
                result.Result.Length.Should().Be(133);
                result.Result.Should()
                    .Be("11:13 AM: \r\n\t\t1 high-fived other people.\r\n\r\n10:12 AM: \r\n\t\t1 left.\r\n\r\n09:11 AM: \r\n\t\t1 comments.\r\n\r\n08:10 AM: \r\n\t\t1 person entered.\r\n\r\n");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        ;
    }
}