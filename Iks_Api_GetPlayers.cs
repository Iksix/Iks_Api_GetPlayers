using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;

namespace Iks_Api_GetPlayers;

public class Iks_Api_GetPlayers : BasePlugin
{
    public override string ModuleName => "Iks_Api_GetPlayers";

    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Iks";

    [CommandHelper(whoCanExecute: CommandUsage.SERVER_ONLY)]
    [ConsoleCommand("xapi_getplayers")]
    public void OnGetPlayersCommand(CCSPlayerController? controller, CommandInfo info)
    {
        info.ReplyToCommand( "{");

        var players = XHelper.GetOnlinePlayers();

        for (int i = 0; i < players.Count; i++)
        {
            string steamid2 = players[i].AuthorizedSteamID!.SteamId2.ToString();
            string steamid3 = players[i].AuthorizedSteamID!.SteamId3.ToString();
            string steamid32 = players[i].AuthorizedSteamID!.SteamId32.ToString();
            string steamid64 = players[i].AuthorizedSteamID!.SteamId64.ToString();
            string name = $@"{players[i].PlayerName}";
            string team = players[i].TeamNum.ToString();
            string uid = players[i].UserId == null ? "undefined" : players[i].UserId.ToString();
            string immunity = AdminManager.GetPlayerImmunity(players[i]).ToString();

            string Deaths = players[i].ActionTrackingServices!.MatchStats.Deaths.ToString();
            string Damage = players[i].ActionTrackingServices!.MatchStats.Damage.ToString();
            string Kills = players[i].ActionTrackingServices!.MatchStats.Kills.ToString();

            string item = "\n\"" + steamid64 + "\": { \n";

            item += "\"name\": " + "\"" + XHelper.EscapeString(name) + "\", \n";
            item += "\"team\": " + "\"" + XHelper.EscapeString(team) + "\", \n";
            item += "\"uid\": " + "\"" + XHelper.EscapeString(uid ?? "undefined") + "\", \n";
            item += "\"immunity\": " + "\"" + XHelper.EscapeString(immunity) + "\", \n";
            item += "\"deaths\": " + "\"" + XHelper.EscapeString(Deaths) + "\", \n";
            item += "\"damage\": " + "\"" + XHelper.EscapeString(Damage) + "\", \n";
            item += "\"kills\": " + "\"" + XHelper.EscapeString(Kills) + "\", \n";
            item += "\"steamid2\": " + "\"" + XHelper.EscapeString(steamid2) + "\", \n";
            item += "\"steamid3\": " + "\"" + XHelper.EscapeString(steamid3) + "\", \n";
            item += "\"steamid32\": " + "\"" + XHelper.EscapeString(steamid32) + "\", \n";
            item += "\"steamid64\": " + "\"" + XHelper.EscapeString(steamid64) + "\" \n";

            if (i != players.Count - 1)
            {
                item += "}, \n";
                info.ReplyToCommand(item);
                return;
            }
            item += "} \n";
            info.ReplyToCommand(item);
        }
        info.ReplyToCommand( "}");
    }
}
