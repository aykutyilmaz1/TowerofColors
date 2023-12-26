using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3_Scripts.Missions
{
    [CreateAssetMenu(fileName = "MissionsData", menuName = "ScriptableObjects/MissionData", order = 0)]
    public class MissionsData : ScriptableObject
    {
        public List<Mission> missions;

        private void OnValidate()
        {
            foreach (Mission mission in missions)
            {
                mission.editorText = mission.missionType + " - " + mission.missionTargetValue + " - " + mission.missionDifficulty;
            }
        }
    }

    [System.Serializable]
    public class Mission
    {
        [HideInInspector] public string editorText;
        [HideInInspector] public int missionCurrentValue; // What the player has achieved so far
        [HideInInspector] public bool isRewardCollected;

        public string missionDescription;
        public int missionTargetValue; // What the player needs to achieve
        public MissionType missionType;
        public MissionDifficulty missionDifficulty;
        public bool canHavePartialProgress = true;
        public Reward reward;
    }
}