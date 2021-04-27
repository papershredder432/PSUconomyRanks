using System.Collections.Generic;
using PSRMUconomyRanks.Models;
using Rocket.API;

namespace PSRMUconomyRanks
{
    public class PSRMUconomyRanksConfiguration : IRocketPluginConfiguration
    {
        public bool UseXP { get; set; }
        public List<PurchasableRank> PurchasableRanks { get; set; }
        
        public void LoadDefaults()
        {
            UseXP = false;
            
            PurchasableRanks = new List<PurchasableRank>
            {
                new PurchasableRank
                {
                    RankName = "VIP",
                    Cost = 1500
                }
            };
        }
    }
}