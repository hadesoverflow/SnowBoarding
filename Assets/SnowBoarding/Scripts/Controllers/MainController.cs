using System;
using DenkKits.GameServices.Audio.Scripts;
using DenkKits.GameServices.Manager;
using DenkKits.GameServices.SaveData;
using DenkKits.UIManager.Scripts.Base;
using DenkKits.UIManager.Scripts.UIView;
using UnityEngine;

namespace _GAME.Scripts.Controllers
{
    public class MainController : MonoBehaviour
    {
        private void Awake()
        {
            UIManager.Instance.ViewManager.ShowView(UIViewName.MainView);
            UIManager.Instance.HideTransition(() => { });
        }

        private string curSkin = "default";

        private void Start()
        {
            curSkin = SaveDataHandler.Instance.saveData.equippedSkinID;
        }

        public void LeftBtn()
        {
            var skins = SaveDataHandler.Instance.saveData.ownedSkins;
            int currentIndex = skins.IndexOf(curSkin);
            int newIndex = (currentIndex - 1 + skins.Count) % skins.Count;
            SetSkin(skins[newIndex]);
        }

        public void RightBtn()
        {
            var skins = SaveDataHandler.Instance.saveData.ownedSkins;
            int currentIndex = skins.IndexOf(curSkin);
            int newIndex = (currentIndex + 1) % skins.Count;
            SetSkin(skins[newIndex]);
        }

        private void SetSkin(string skinID)
        {
            curSkin = skinID;
            SaveDataHandler.Instance.EquipSkin(skinID);
            SoArchitectureManager.Instance.OnSkinEquippedChangedEvent.Raise(skinID);
        }
    }
}