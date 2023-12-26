using System;
using _3_Scripts.Missions;
using UnityEngine;

namespace _3_Scripts.UI.MissionUI
{
    public class MissionButton : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(MissionManager.GetInstance().CanShowMissions());
        }
    }
}