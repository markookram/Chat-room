using ChatRoom.Domain.Events;
using ChatRoom.Persistence.DataStore.SqlLite.Persistence.Context;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.IntegrationTests
{
    public class TestingWebAppFactoryFixture<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<SqlLiteChatRoomLogDataContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<SqlLiteChatRoomLogDataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryChatEventTest");
                });

                descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<SqlLiteChatRoomDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<SqlLiteChatRoomDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryChatRoomTest");
                });

                var sp = services.BuildServiceProvider();
                EventsContext = sp.GetService<SqlLiteChatRoomLogDataContext>()!;
            });
        }

#pragma warning disable CS8618
        public TestingWebAppFactoryFixture()
#pragma warning restore CS8618
        {
            Mvc.Extensions.Composition.WithSqlDb = true;
        }

        public SqlLiteChatRoomLogDataContext EventsContext
        {
            get;
            private set;
        }

        public void RemoveEvent(ChatEvent @event)
        {
            EventsContext.ChatEvents.Remove(@event);

            var res = EventsContext.SaveChanges();

            res.Should().Be(1);
        }
    }
}