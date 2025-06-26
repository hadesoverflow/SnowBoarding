using System;
using System.Collections.Generic;
using DenkKits.GameServices.Manager;
using DenkKits.GameServices.SaveData;
using UnityEngine;

namespace SnowBoarding.Scripts.GamePlay
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private List<GameObject> skinList;

        private void Start()
        {
            LoadSkin();
            SoArchitectureManager.Instance.OnSkinEquippedChangedEvent.AddListener(LoadSkin);
        }

        public void LoadSkin()
        {
            string equippedSkin = SaveDataHandler.Instance.saveData.equippedSkinID;

            foreach (var skin in skinList)
            {
                bool isMatch = skin.name == equippedSkin;
                skin.SetActive(isMatch);
            }
        }

        private void OnDestroy()
        {
            SoArchitectureManager.Instance.OnSkinEquippedChangedEvent.RemoveListener(LoadSkin);
        }
    }
}