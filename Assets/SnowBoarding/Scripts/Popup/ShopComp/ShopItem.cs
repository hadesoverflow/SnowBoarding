using System;
using DenkKits.GameServices.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnowBoarding.Scripts.Popup.ShopComp
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Button btnBuy;
        [SerializeField] private Button btnEquip;
        [SerializeField] private GameObject labelOwned;
        [SerializeField] private GameObject labelPrice;
        [SerializeField] private TextMeshProUGUI textPrice;
        [SerializeField] private TextMeshProUGUI textEquiped;
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private Image itemImage;


        /// <summary>
        /// Khởi tạo item trong shop
        /// </summary>
        private void OnEnable()
        {
            SoArchitectureManager.Instance.OnSkinEquippedChangedEvent.AddListener(skinEquipped =>
            {
                if (skinEquipped == _data.itemID)
                {
                    textEquiped.text = "Equipped";
                    btnEquip.interactable = false;
                }
                else
                {
                    textEquiped.text = "Equip";
                    btnEquip.interactable = true;
                }

                btnEquip.SetActive(false);
            });

            SoArchitectureManager.Instance.OnSkinOwnedChangedEvent.AddListener(skinBuy =>
            {
                if (skinBuy == _data.itemID)
                {
                    labelPrice.SetActive(false);
                    btnBuy.gameObject.SetActive(false);
                    labelOwned.SetActive(true);
                    btnEquip.gameObject.SetActive(true);
                }

                btnEquip.SetActive(false);
            });
        }

        private ShopItemData _data;

        public void InitItem(ShopItemData data, bool isOwned, bool isEquipped, System.Action onBuyClick,
            System.Action onEquipClick)
        {
            _data = data;
            itemImage.sprite = data.icon;
            textName.text = data.itemName;
            if (isOwned)
            {
                labelPrice.SetActive(false);
                btnBuy.gameObject.SetActive(false);
                labelOwned.SetActive(true);
                btnEquip.gameObject.SetActive(true);

                if (isEquipped)
                {
                    textEquiped.text = "Equipped";
                    btnEquip.interactable = false;
                }
                else
                {
                    textEquiped.text = "Equip";
                    btnEquip.interactable = true;
                    btnEquip.onClick.RemoveAllListeners();
                    btnEquip.onClick.AddListener(() => onEquipClick?.Invoke());
                }
            }
            else
            {
                labelPrice.SetActive(true);
                textPrice.text = data.price.ToString();
                btnBuy.gameObject.SetActive(true);
                btnEquip.gameObject.SetActive(false);
                labelOwned.SetActive(false);

                btnBuy.onClick.RemoveAllListeners();
                btnBuy.onClick.AddListener(() => onBuyClick?.Invoke());
            }
            btnEquip.SetActive(false);

        }
    }
}