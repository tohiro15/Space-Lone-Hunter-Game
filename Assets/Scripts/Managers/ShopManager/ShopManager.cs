using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string ItemName;
        public Sprite ButtonImage;

        public int DefaultValue;
        public int CurrentValue;

        public string Description;

        public int Price;
        public int IncreasedPrice;

        public int CurrentPurchase;
        public int TotalPurchase;
    }

    [Header("Item Settings")]
    [SerializeField] private List<ShopItem> items;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform contentPanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _walletUGUI;

    [Header("Sound effects")]
    [SerializeField] private AudioSource _baseAudioSource;
    [SerializeField] private AudioClip _buySoundClip;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerData _playerData;
    private PlayerDataManager _playerDM;

    private const string CurrentValue = "CurrentValue";
    private const string CurrentPurchace = "CurrentPurchase";
    private const string Price = "Price";

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
            item.CurrentValue = PlayerPrefs.GetInt(GetUniqueKey(CurrentValue, item), item.DefaultValue);
            item.CurrentPurchase = PlayerPrefs.GetInt(GetUniqueKey(CurrentPurchace, item), 0);
            item.Price = PlayerPrefs.GetInt(GetUniqueKey(Price, item), item.Price);

            GameObject newItem = Instantiate(itemPrefab, contentPanel);

            newItem.transform.Find("ButtonImage").GetComponent<Image>().sprite = item.ButtonImage;
            newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.ItemName;
            if (item.CurrentPurchase >= item.TotalPurchase)
            {
                newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = $"MAX";
                newItem.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = $"{item.Description}\n(SPEED {item.CurrentValue})";
            }
            else
            {
                newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = $"{item.Price.ToString()} COINS";
                newItem.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = $"{item.Description}\n({item.CurrentValue} SPEED +10)";
            }
            newItem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => BuyItem(item, newItem));
            newItem.transform.Find("NumberPurchase").GetComponent<TextMeshProUGUI>().text = $"{item.CurrentPurchase.ToString()}/{item.TotalPurchase.ToString()}";
        }
    }
    public void BuyItem(ShopItem item, GameObject newItem)
    {
        if (_playerData.WalletAmount >= item.Price && item.CurrentPurchase < item.TotalPurchase)
        {
            _playerData.WalletAmount -= item.Price;
            _walletUGUI.text = $"WALLET: {_playerData.WalletAmount}";

            _playerData.FireRate -= 0.1f;
            item.CurrentValue += 10;
            item.CurrentPurchase += 1;
            item.Price += item.IncreasedPrice;

            newItem.transform.Find("NumberPurchase").GetComponent<TextMeshProUGUI>().text = $"{item.CurrentPurchase.ToString()}/{item.TotalPurchase.ToString()}";
            if (item.CurrentPurchase >= item.TotalPurchase)
            {
                newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = $"MAX";
                newItem.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = $"{item.Description}\n(SPEED {item.CurrentValue})";
            }
            else
            {
                newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = $"{item.Price.ToString()} COINS";
                newItem.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = $"{item.Description}\n({item.CurrentValue} SPEED +10)";
            }

            _baseAudioSource.PlayOneShot(_buySoundClip);

            _playerDM.SavePlayerData();

            PlayerPrefs.SetInt(GetUniqueKey(CurrentValue, item), item.CurrentValue);
            PlayerPrefs.SetInt(GetUniqueKey(CurrentPurchace, item), item.CurrentPurchase);
            PlayerPrefs.SetInt(GetUniqueKey(Price, item), item.Price);
            PlayerPrefs.Save();
        }
    }

    private string GetUniqueKey(string baseKey, ShopItem item)
    {
        return $"{baseKey}_{item.ItemName}";
    }
}
