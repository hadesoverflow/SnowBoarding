using System.Collections.Generic;
using DenkKits.GameServices.Audio.Scripts;
using DenkKits.GameServices.Manager;
using DenkKits.GameServices.SaveData;
using DenkKits.UIManager.Scripts.Base;
using DenkKits.UIManager.Scripts.UIPopup;
using SnowBoarding.Scripts.Popup.ShopComp;
using SuperScrollView;
using UnityEngine;

namespace SnowBoarding.Scripts.Popup
{
    public class ShopPopup : UIPopup
    {
        [Header("Reference")] 
        [SerializeField] private LoopGridView _loopGridView;
        [SerializeField] private List<ShopItemData> loopListData;

        private SaveData saveData;

        protected override void OnInit()
        {
            base.OnInit();
            _loopGridView.InitGridView(loopListData.Count, OnGridItemUpdate);
        }

        protected override void OnShowing()
        {
            base.OnShowing();
            LoadShop();
        }

        private LoopGridViewItem OnGridItemUpdate(LoopGridView gridView, int itemIndex, int row, int col)
        {
            if (itemIndex < 0 || itemIndex >= loopListData.Count) return null;

            var data = loopListData[itemIndex];
            var itemObj = gridView.NewListViewItem("ShopItemGrid");
            var itemUI = itemObj.GetComponent<ShopItem>();

            bool isOwned = SaveDataHandler.Instance.saveData.ownedSkins.Contains(data.itemID);
            bool isEquipped = SaveDataHandler.Instance.saveData.equippedSkinID == data.itemID;

            itemUI.InitItem(
                data,
                isOwned,
                isEquipped,
                () => BuyItem(data),
                () => EquipItem(data)
            );

            return itemObj;
        }
        

        private void LoadShop()
        {
            saveData = SaveDataHandler.Instance.saveData;
            _loopGridView.SetListItemCount(loopListData.Count, false);
            _loopGridView.RefreshAllShownItem();
        }

        private void BuyItem(ShopItemData data)
        {
            if (SaveDataHandler.Instance.UserHighScore >= data.price)
            {
                SaveDataHandler.Instance.UserHighScore -= data.price;
                SaveDataHandler.Instance.AddOwnedSkin(data.itemID);
                // Gửi sự kiện
                AudioManager.Instance.PlaySfx(AudioName.Gameplay_PlayerScore);

                SoArchitectureManager.Instance.OnSkinOwnedChangedEvent.Raise(data.itemID);
                LoadShop();
            }
            else
            {
                UIManager.Instance.AlertManager.ShowAlertMessage("Not enough stars!");
                Debug.Log("Not enough money!");
            }
        }

        private void EquipItem(ShopItemData data)
        {
            SaveDataHandler.Instance.EquipSkin(data.itemID);

            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            SoArchitectureManager.Instance.OnSkinEquippedChangedEvent.Raise(data.itemID);
            LoadShop();
        }
    }

    [System.Serializable]
    public class ShopItemData
    {
        public string itemID;
        public string itemName;
        public int price;
        public Sprite icon;
    }
}
