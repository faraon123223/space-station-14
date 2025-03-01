using Content.Server.DiscordAuth.Database;
using Content.Shared.CCVar;
using Content.Shared.Chat;        // Для OOCMessageEvent
using Robust.Server.Player;       // Для IPlayerSession
using Robust.Shared.Configuration;
using Robust.Shared.Player;
using Robust.Server;

namespace Content.Server.DiscordAuth.Systems
{
    public sealed class DiscordOocColorSystem : EntitySystem
    {
        [Dependency] private readonly IConfigurationManager _cfg = default!;
        [Dependency] private readonly DiscordAuthDbContext _db = default!;
        [Dependency] private readonly IPlayerManager _playerManager = default!;

        public override void Initialize()
        {
            SubscribeNetworkEvent<OOCMessageEvent>(OnOocMessage);
        }

        private void OnOocMessage(OOCMessageEvent ev, EntitySessionEventArgs args)
        {
            var player = _playerManager.GetSessionByUserId(args.SenderSession.UserId);
            ApplyDiscordColor(player, ref ev.Message);
        }

        private void ApplyDiscordColor(ICommonSession player, ref string message)
        {
            // Логика изменения цвета
        }
    }
}
