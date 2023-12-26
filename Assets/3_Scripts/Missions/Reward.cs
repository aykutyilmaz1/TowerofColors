using UnityEngine;

namespace _3_Scripts.Missions
{
    public enum RewardType { Coin, Diamond, Gem, Skin }
    
    [System.Serializable]
    public class Reward
    {
        public RewardType rewardType;
        public Sprite rewardSprite;
        public int amount;
    }
}