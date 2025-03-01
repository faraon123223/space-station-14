// DiscordOocColorSystem.cs
using Content.Server.DiscordAuth.Database;
using Content.Shared.CCVar;
using Content.Shared.Chat;
using Robust.Server.Player;
using Robust.Shared.Configuration;
using Robust.Shared.Player;

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
            // Логика изменения цвета OOC
            if (player.AttachedEntity is null) return;

            var userId = player.UserId;
            var token = _db.Tokens.FirstOrDefault(t => t.UserId == userId);

            if (token?.DiscordRoles != null &&
                token.DiscordRoles.Contains(_cfg.GetCVar(CCVars.DiscordAuthRoleId)))
            {
                message = $"[color={_cfg.GetCVar(CCVars.DiscordRoleOocColor)}]{message}[/color]";
            }
        }
    }
}
