using _3._Scripts.Characters;
using UnityEngine;
using VInspector;

namespace _3._Scripts.UI.Scriptable.Shop
{
    [CreateAssetMenu(fileName = "UpgradeItem", menuName = "Shop Items/Upgrade Item", order = 0)]
    public class UpgradeItem : ShopItem
    {
        [SerializeField] private float booster;
        [SerializeField] private Color color;
        
        public float Booster => booster;

        public Color Color => color;
        public override string Title()
        {
            return $"{(1 - booster + 1) * 100}%";
        }
    }
}