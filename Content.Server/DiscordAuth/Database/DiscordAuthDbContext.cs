using System.IO;
using Microsoft.EntityFrameworkCore;
using Robust.Shared.Network;

namespace Content.Server.DiscordAuth.Database
{
    public class DiscordAuthDbContext : DbContext
    {
        public DbSet<DiscordAuthToken> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..",
                "DiscordAuthBot",
                "data",
                "DiscordAuth.db"
            );

            options.UseSqlite($"Data Source={dbPath}");
        }

        public bool IsUserAuthorized(NetUserId userId)
            => Tokens.Any(t => t.UserId == userId && t.IsVerified);

        public void StoreAuthToken(NetUserId userId, string token)
        {
            Tokens.Add(new DiscordAuthToken
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow
            });
            SaveChanges();
        }
    }

    public class DiscordAuthToken
    {
        public NetUserId UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string DiscordRoles { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsVerified { get; set; }
    }
}
