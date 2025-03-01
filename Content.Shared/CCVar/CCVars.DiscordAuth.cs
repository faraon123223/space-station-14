using Robust.Shared.Configuration;

namespace Content.Shared.CCVar;

public sealed partial class CCVars
{
        public static readonly CVarDef<bool> DiscordAuthEnabled =
            CVarDef.Create("discord_auth.enabled", true, CVar.SERVERONLY);

        public static readonly CVarDef<string> DiscordAuthUrl =
            CVarDef.Create("discord_auth.url", "", CVar.SERVERONLY);

        public static readonly CVarDef<string> DiscordAuthRoleId =
            CVarDef.Create("discord_auth.role_id", "", CVar.SERVERONLY);

        public static readonly CVarDef<string> DiscordRoleOocColor =
            CVarDef.Create("discord_auth.role_ooc_color", "", CVar.SERVERONLY);
}
