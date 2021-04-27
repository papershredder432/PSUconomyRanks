using System;
using System.Collections.Generic;
using System.Linq;
using fr34kyn01535.Uconomy;
using Rocket.API;
using Rocket.Core;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMUconomyRanks.Commands
{
    public class BuyRank : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var validRanks = PSRMUconomyRanks.Instance.ValidGroups;
            UnturnedPlayer unturnedPlayer = (UnturnedPlayer) caller;
            var buyRank = validRanks.FirstOrDefault(x => x.RankName.ToLower() == command[0].ToLower());

            if (buyRank == null)
            {
                ChatManager.serverSendMessage($"There is no rank you can buy by that name!", Color.red, null, unturnedPlayer.SteamPlayer(), EChatMode.SAY, null, true);
                return;
            }

            if (PSRMUconomyRanks.Instance.Configuration.Instance.UseXP)
            {
                unturnedPlayer.Experience -= Convert.ToUInt16(buyRank.Cost);
                ChatManager.serverSendMessage($"You bought {buyRank.RankName} for {buyRank.Cost} experience.", Color.green, null, unturnedPlayer.SteamPlayer(), EChatMode.SAY, null, true);

            }
            else if (!PSRMUconomyRanks.Instance.Configuration.Instance.UseXP)
            {
                Uconomy.Instance.Database.IncreaseBalance(unturnedPlayer.CSteamID.ToString(), -buyRank.Cost);
                ChatManager.serverSendMessage($"You bought {buyRank.RankName} for {buyRank.Cost} {Uconomy.Instance.Configuration.Instance.MoneyName}", Color.green, null, unturnedPlayer.SteamPlayer(), EChatMode.SAY, null, true);
            }
            R.Permissions.AddPlayerToGroup(buyRank.RankName, caller);
            ChatManager.serverSendMessage($"You can relog to get your new suffix, prefix, and chat color.", Color.green, null, unturnedPlayer.SteamPlayer(), EChatMode.SAY, null, true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "buyrank";
        public string Help => "Lets you buy a rank using your balance.";
        public string Syntax => "/buyrank <rankname>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "ps.uconomyranks.buy" };
    }
}