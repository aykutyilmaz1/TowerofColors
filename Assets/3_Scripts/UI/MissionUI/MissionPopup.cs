using System;
using System.Collections;
using System.Collections.Generic;
using _3_Scripts.Missions;
using UnityEngine;

namespace _3_Scripts.UI.MissionUI
{
    public class MissionPopup : MonoBehaviour
    {
        [SerializeField] private List<MissionItemUI> missionItems;
        
        private void OnEnable()
        {
            UpdateMissions(MissionManager.GetInstance().GetMissionPopupData());
        }

        private void UpdateMissions(List<Mission> missions)
        {
            for (int i = 0; i < missions.Count; i++)
            {
                missionItems[i].UpdateUI(missions[i]);
                missionItems[i].OnCollectAllMissionRewards = delegate
                {
                    StartCoroutine(CloseAndReopenPopup());
                };
            }
        }
        
        private IEnumerator CloseAndReopenPopup()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

    }
}