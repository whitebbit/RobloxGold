﻿using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Config;

namespace _3._Scripts.Upgrades
{
    public class UpgradeHandler
    {
        public void SetUpgrade(string id, IEnumerable<GlovePoint> points)
        {
            var upgrade = Configuration.Instance.AllUpgrades.FirstOrDefault(u => u.ID == id);
            foreach (var point in points)
            {
                if (upgrade is not null) point.Initialize(upgrade.Color);
            }
        }
    }
}