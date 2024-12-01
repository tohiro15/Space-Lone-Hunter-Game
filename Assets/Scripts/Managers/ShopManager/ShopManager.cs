using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public Sprite ItemImage;
        public int Price;
        public int IncreasedPrice;
        public int currentPurchase;
        public int totalPurchase;
    }

    [Header("Item Settings")]
    [SerializeField] private List<ShopItem> items;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform contentPanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _walletUGUI;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerData _playerData;
    private PlayerDataManager _playerDM;

    private const string CurrenttPurchace = "CurrentPurchase";

    private void Start()
    {
        _walletUGUI.text = $"WALLET: {_playerData.WalletAmount}";

        _playerDM = GetComponent<PlayerDataManager>();

        PopulateShop();
    }
    public void PopulateShop()
    {
        foreach (ShopItem item in items)
        {
            item.currentPurchase = PlayerPrefs.GetInt(CurrenttPurchace, 0);

            GameObject newItem = Instantiate(itemPrefab, contentPanel);

            newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
            newItem.transform.Find("ItemImage").GetComponent<Image>().sprite = item.ItemImage;
            newItem.transform.Find("ItemImage").GetComponentInChildren<TextMeshProUGUI>().text = $"{item.currentPurchase.ToString()}/{item.totalPurchase.ToString()}";
            newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = $"PRICE: {item.Price.ToString()} COINS";
            newItem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => BuyItem(item, newItem));
        }
    }
    public void BuyItem(ShopItem item, GameObject newItem)
    {
        if(_playerData.WalletAmount < item.Price)
        {
            Debug.Log("Недостаточно монет!");
        }
        else if (_playerData.WalletAmount >= item.Price)
        {
            if (item.currentPurchase < item.totalPurchase)
            {
                _playerData.WalletAmount -= item.Price;
                _walletUGUI.text = $"WALLET: {_playerData.WalletAmount}";

                _playerData.FireRate -= 0.1f;
                item.currentPurchase += 1;
                item.Price += item.IncreasedPrice;
                newItem.transform.Find("ItemImage").GetComponentInChildren<TextMeshProUGUI>().text = $"{item.currentPurchase.ToString()}/{item.totalPurchase.ToString()}";
                newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = $"PRICE: {item.Price.ToString()} COINS";

                _playerDM.SavePlayerData();
                PlayerPrefs.SetInt(CurrenttPurchace, item.currentPurchase);
            }
        }
    }

}
