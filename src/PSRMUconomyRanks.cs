using System.Collections.Generic;
using System.Linq;
using PSRMUconomyRanks.Models;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Plugins;
using Logger = Rocket.Core.Logging.Logger;

namespace PSRMUconomyRanks
{
    public class PSRMUconomyRanks : RocketPlugin<PSRMUconomyRanksConfiguration>
    {
        public static PSRMUconomyRanks Instance { get; set; }
        public List<PurchasableRank> ValidGroups { get; set; }
        
        protected override void Load()
        {
            Instance = this;

            ValidGroups = Configuration.Instance.PurchasableRanks;
            
            Logger.LogWarning($"{Name} {Assembly.GetName().Version} loaded! Made by papershredder432, join the support Discord here: https://discord.gg/ydjYVJ2");

            foreach (var purchasableRank in ValidGroups.ToList())
            {
                RocketPermissionsGroup g = R.Permissions.GetGroup(purchasableRank.RankName);

                if (g != null) continue;
                Logger.LogWarning($"\"{purchasableRank.RankName}\" does not exist, and any player trying to purchase this rank will not be able to buy it.");
                ValidGroups.Remove(purchasableRank);
            }
        }
        
        protected override void Unload()
        {
            Instance = null;
            
            Logger.LogWarning($"{Name} {Assembly.GetName().Version} unloaded.");
        }
    }
}