using System;
using System.Collections;
using System.Collections.Generic;
using _3_Scripts.Missions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItemUI : MonoBehaviour
{
    public Action OnCollectAllMissionRewards;
    [SerializeField] private Image rewardImage;
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    [SerializeField] private TextMeshProUGUI missionDescriptionText;
    [SerializeField] private TextMeshProUGUI missionProgressText;
    [SerializeField] private Button collectButton;
    [SerializeField] private GameObject tickImage;
    
    private Mission _mission;
    
    public void UpdateUI(Mission mission)
    {
        _mission = mission;
        rewardImage.sprite = mission.reward.rewardSprite;
        rewardAmountText.text = mission.reward.amount.ToString();
        missionDescriptionText.text = mission.missionDescription;
        missionProgressText.text = mission.canHavePartialProgress
            ? $"{Mathf.Clamp(mission.missionCurrentValue, 0, mission.missionTargetValue)}/{mission.missionTargetValue}"
            : $"{Mathf.Clamp(mission.missionCurrentValue, 0, 1)}/1";
        collectButton.interactable = mission.missionCurrentValue >= mission.missionTargetValue && !mission.isRewardCollected;
        
        tickImage.SetActive(mission.isRewardCollected);
    }

    public void OnClickCollect()
    {
        MissionManager.GetInstance().CollectMissionReward(_mission, delegate { OnCollectAllMissionRewards?.Invoke(); });
        collectButton.interactable = false;
        tickImage.SetActive(true);
    }
}
