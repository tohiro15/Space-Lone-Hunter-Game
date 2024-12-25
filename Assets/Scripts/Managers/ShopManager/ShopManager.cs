using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public enum UpgradeType
{
    FireSpeed,
    FireDamage,
    BulletCount
}

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        [Header("Base Settings")]
        public string ItemName;
        public string Description;
        public int Price;
        public int IncreasedPrice;
        public int CurrentPurchase;
        public int TotalPurchase;
        [HideInInspector] public string NumberPurchase;

        [Header("Lock Settings")]
        public string LockText;

        [Header("Image Settings")]
        public Sprite ButtonImage;

        [Header("Value Settings")]
        public int DefaultValue;
        public int CurrentValue;

        [Header("Item Settings")]
        public UpgradeType UpgradeType;
        public bool isOpen = false;

        public float FireSpeedIncrease = 0.1f;
        public int FireDamageIncrease = 1;
        public int BulletCountIncrease = 1;
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
    [SerializeField] private GameData _gameData;

    private const string CURRENT_VALUE_KEY = "CurrentValue";
    private const string CURRENT_PURCHASE_KEY = "CurrentPurchase";
    private const string PRICE_KEY = "Price";

    private void Start()
    {
        SetItemAvailability();
        UpdateWalletUI();
        PopulateShop();
    }

    private void SetItemAvailability()
    {
        int improvementsUnlocked = _gameData.CurrentLevel + 1;
        for (int i = 0; i < items.Count; i++)
        {
            items[i].isOpen = i < improvementsUnlocked;
        }
        if (improvementsUnlocked > _gameData.ImprovementsUnlocked) _gameData.ImprovementsUnlocked = improvementsUnlocked;
    }

    public void PopulateShop()
    {
        foreach (ShopItem item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, contentPanel);
            ShopItemUI shopItemUI = newItem.GetComponent<ShopItemUI>();

            if (item.isOpen)
            {
                InitializeShopItem(shopItemUI, item, newItem);
            }
            else
            {
                LockShopItem(shopItemUI, item);
            }
        }
    }

    private void InitializeShopItem(ShopItemUI shopItemUI, ShopItem item, GameObject newItem)
    {
        item.CurrentValue = PlayerPrefs.GetInt(GetUniqueKey(CURRENT_VALUE_KEY, item), item.DefaultValue);
        item.CurrentPurchase = PlayerPrefs.GetInt(GetUniqueKey(CURRENT_PURCHASE_KEY, item), 1);
        item.Price = PlayerPrefs.GetInt(GetUniqueKey(PRICE_KEY, item), item.Price);

        shopItemUI.ButtonImage.sprite = item.ButtonImage;
        shopItemUI.ItemName.text = item.ItemName;
        shopItemUI.PriceText.text = item.Price.ToString();
        shopItemUI.Description.text = item.Description;
        shopItemUI.NumberPurchase.text = item.NumberPurchase;

        shopItemUI.LockButtonItem.gameObject.SetActive(false);
        shopItemUI.LockText.gameObject.SetActive(false);

        newItem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => BuyItem(item, newItem));

        UpdateShopItemUI(shopItemUI, item);
    }

    private void LockShopItem(ShopItemUI shopItemUI, ShopItem item)
    {
        shopItemUI.ButtonImage.sprite = item.ButtonImage;
        shopItemUI.LockText.text = item.LockText;

        shopItemUI.LockButtonItem.gameObject.SetActive(true);
        shopItemUI.LockText.gameObject.SetActive(true);

        shopItemUI.ItemName.gameObject.SetActive(false);
        shopItemUI.PriceText.gameObject.SetActive(false);
        shopItemUI.Description.gameObject.SetActive(false);
        shopItemUI.NumberPurchase.gameObject.SetActive(false);
    }

    public void BuyItem(ShopItem item, GameObject newItem)
    {
        if (_playerData.WalletAmount >= item.Price && item.CurrentPurchase < item.TotalPurchase)
        {
            switch (item.UpgradeType)
            {
                case UpgradeType.FireSpeed:
                    _playerData.FireRate -= item.FireSpeedIncrease;
                    PlayerPrefs.SetFloat(PlayerData.FIRE_RATE_KEY, _playerData.FireDamage);
                    break;
                case UpgradeType.FireDamage:
                    _playerData.FireDamage += item.FireDamageIncrease;
                    PlayerPrefs.SetInt(PlayerData.FIRE_DAMAGE_KEY, _playerData.FireDamage);
                    break;
                case UpgradeType.BulletCount:
                    _playerData.BulletCount += item.BulletCountIncrease;
                    PlayerPrefs.SetInt(PlayerData.BULLET_COUNT_KEY, _playerData.BulletCount);
                    break;
            }

            ApplyPurchase(item);
            UpdateWalletUI();
            UpdateShopItemUI(newItem.GetComponent<ShopItemUI>(), item);

            _baseAudioSource.PlayOneShot(_buySoundClip);
            DataManager.Instance.SaveDataAfterShop();

            SavePlayerPrefs(item);
        }
    }

    private void ApplyPurchase(ShopItem item)
    {
        _playerData.WalletAmount -= item.Price;
        item.CurrentValue += 1;
        item.CurrentPurchase += 1;
        item.Price += item.IncreasedPrice;
    }

    private void UpdateWalletUI()
    {
        _walletUGUI.text = $"{_playerData.WalletAmount}";
    }

    private void UpdateShopItemUI(ShopItemUI shopItemUI, ShopItem item)
    {
        shopItemUI.PriceText.text = item.Price.ToString();
        shopItemUI.Description.text = item.Description;
        shopItemUI.NumberPurchase.text = $"{item.CurrentPurchase}/{item.TotalPurchase}";
        if (item.CurrentPurchase >= item.TotalPurchase)
        {
            shopItemUI.PriceText.text = "MAX";
            shopItemUI.Description.text = $"{item.Description}\n({item.CurrentValue})";
            shopItemUI.NumberPurchase.text = $"{item.CurrentPurchase}/{item.TotalPurchase}";
            switch (item.UpgradeType)
            {
                case UpgradeType.FireSpeed: shopItemUI.Description.text = $"Ã¿ —»Ã¿À‹Õ¿ﬂ — Œ–Œ—“‹ ¬€—“–≈À¿\n({item.CurrentValue})"; break;
                case UpgradeType.FireDamage: shopItemUI.Description.text = $"Ã¿ —»Ã¿À‹Õ€… ”–ŒÕ\n({item.CurrentValue})"; break;
                case UpgradeType.BulletCount: shopItemUI.Description.text = $"Ã¿ —»Ã¿À‹ÕŒ≈  ŒÀ»◊≈—“¬Œ œ”À‹\n({item.CurrentValue})"; break;
            }
        }
        else
        {
            shopItemUI.PriceText.text = $"{item.Price}\n";
            shopItemUI.Description.text = item.Description;
        }
    }

    private void SavePlayerPrefs(ShopItem item)
    {
        PlayerPrefs.SetInt(GetUniqueKey(CURRENT_VALUE_KEY, item), item.CurrentValue);
        PlayerPrefs.SetInt(GetUniqueKey(CURRENT_PURCHASE_KEY, item), item.CurrentPurchase);
        PlayerPrefs.SetInt(GetUniqueKey(PRICE_KEY, item), item.Price);
        PlayerPrefs.Save();
    }

    private string GetUniqueKey(string baseKey, ShopItem item)
    {
        return $"{baseKey}_{item.ItemName}";
    }
}