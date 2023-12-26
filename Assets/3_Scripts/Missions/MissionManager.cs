using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3_Scripts.Missions
{
    public enum MissionType { ExplosiveHit, FinishOverBallCount, PassComboCount, DestroyBarrels}
    public enum MissionDifficulty { Easy, Medium, Hard }
    
    public class MissionManager : MonoBehaviour
    {
        [SerializeField] private MissionsData missionsData;

        private static MissionManager _instance;
        private bool _canShowMissions;
        private List<Mission> _activeMissions;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); // Don't destroy the object when loading a new scene
                
                _canShowMissions = RemoteConfig.MISSIONS_ENABLED;
                if(_canShowMissions) InitializeMissions();
            }
            else
            {
                Destroy(gameObject); // Destroy any additional instances
            }
        }

        public bool CanShowMissions()
        {
            return _canShowMissions;
        }
        
        public List<Mission> GetMissionPopupData()
        {
            return _activeMissions;
        }
        
        public void UpdateMissionProgress(MissionType missionType, int amount)
        {
            foreach (var mission in _activeMissions)
            {
                if (mission.missionType != missionType) continue;

                if (mission.canHavePartialProgress)
                {
                    mission.missionCurrentValue += amount;

                    Debug.Log($"Mission {mission.missionType} progress: {mission.missionCurrentValue}/{mission.missionTargetValue}");
                }
                else if(mission.missionTargetValue < amount)
                {
                     mission.missionCurrentValue = amount;
                     Debug.Log($"Mission {mission.missionType} progress: {mission.missionCurrentValue}/{mission.missionTargetValue}");
                }
            }
        }
        
        public void CollectMissionReward(Mission mission, Action onAllRewardsCollected = null)
        {
            if (mission.isRewardCollected) return;
            
            mission.isRewardCollected = true;
            Debug.Log($"Mission {mission.missionType} reward collected - {mission.reward.amount} {mission.reward.rewardType}");
            
            if (_activeMissions.All(m => m.isRewardCollected))
            {
                Debug.Log("All rewards collected - Resetting missions");
                InitializeMissions();
                onAllRewardsCollected?.Invoke();
            }
        }

        public static MissionManager GetInstance()
        {
            return _instance;
        }
        
        private void InitializeMissions()
        {
            _activeMissions = new List<Mission>();

            foreach (var mission in missionsData.missions)
            {
                mission.missionCurrentValue = 0;
                mission.isRewardCollected = false;
            }
            
            var shuffledMissions = missionsData.missions.OrderBy(x => Guid.NewGuid()).ToList(); // Shuffle missions
            var easyMission = shuffledMissions.Find(mission => mission.missionDifficulty == MissionDifficulty.Easy);
            var mediumMission = shuffledMissions.Find(mission => mission.missionDifficulty == MissionDifficulty.Medium);
            var hardMission = shuffledMissions.Find(mission => mission.missionDifficulty == MissionDifficulty.Hard);

            _activeMissions.Add(easyMission);
            _activeMissions.Add(mediumMission);
            _activeMissions.Add(hardMission);
        }
    }
}