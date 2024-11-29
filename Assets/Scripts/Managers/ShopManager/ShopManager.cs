using System.Collections;
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
    }

    [Header("Item Settings")]
    [SerializeField] private List<ShopItem> items;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform contentPanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _walletUGUI;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerData _playerData;

    private PurchaseText _purchaseTextScript;

    private void Start()
    {
        _purchaseTextScript = GetComponent<PurchaseText>();

        _walletUGUI.text = $"WALLET: {_playerData.WalletAmount}";

        PopulateShop();
    }
    public void PopulateShop()
    {
        foreach (ShopItem item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, contentPanel);

            newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
            newItem.transform.Find("ItemImage").GetComponent<Image>().sprite = item.ItemImage;
            newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = $"PRICE: {item.Price.ToString()} COINS";
            newItem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
        }
    }
    public void BuyItem(ShopItem item)
    {
        Debug.Log("Предмет куплен.");
    }

}
