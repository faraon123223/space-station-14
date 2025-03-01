using Content.Server.DiscordAuth.Database;
using Content.Shared.CCVar;
using Robust.Server.GameObjects; // Для PlayerAttachedEvent
using Robust.Server.Player;      // Для IPlayerSession
using Robust.Shared.Configuration;
using Robust.Server;

namespace Content.Server.DiscordAuth.Systems
{
    public sealed class DiscordAuthSystem : EntitySystem
    {
        [Dependency] private readonly IConfigurationManager _cfg = default!;
        [Dependency] private readonly DiscordAuthDbContext _db = default!;
        [Dependency] private readonly IPlayerManager _playerManager = default!;

        public override void Initialize()
        {
            SubscribeLocalEvent<PlayerAttachedEvent>(OnPlayerAttached);
        }

        private void OnPlayerAttached(PlayerAttachedEvent args)
        {
            if (!_cfg.GetCVar(CCVars.DiscordAuthEnabled)) return;

            var player = args.Player;
            if (!_db.IsUserAuthorized(player.UserId))
                ShowAuthWindow(player);
        }

        private void ShowAuthWindow(IPlayerSession session)
        {
            var token = Guid.NewGuid().ToString("N");
            _db.StoreAuthToken(session.UserId, token);
            // Дополнительная логика отправки сообщения игроку
        }
    }
}
