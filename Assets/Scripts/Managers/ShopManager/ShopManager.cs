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
        public string NumberPurchase;

        public int Price;
        public int IncreasedPrice;

        public int CurrentPurchase;
        public int TotalPurchase;

        public bool isFireSpeed = false;
        public bool isFireDamage = false;
        public bool isBulletCount = false;
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

    ShopItemUI shopItemUI;

    private const string CURRENT_VALUE_KEY = "CurrentValue";
    private const string CURRENT_PURCHASE_KEY = "CurrentPurchase";
    private const string PRICE_KEY = "Price";

    private void Start()
    {
        _walletUGUI.text = $" Œÿ≈À≈ : {_playerData.WalletAmount} ÃŒÕ≈“";

        PopulateShop();
    }
    public void PopulateShop()
    {
        foreach (ShopItem item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, contentPanel);
            shopItemUI = newItem.GetComponent<ShopItemUI>();

            item.CurrentValue = PlayerPrefs.GetInt(GetUniqueKey(CURRENT_VALUE_KEY, item), item.DefaultValue);
            item.CurrentPurchase = PlayerPrefs.GetInt(GetUniqueKey(CURRENT_PURCHASE_KEY, item), 1);
            item.Price = PlayerPrefs.GetInt(GetUniqueKey(PRICE_KEY, item), item.Price);

            shopItemUI.ButtonImage.sprite = item.ButtonImage;
            shopItemUI.ItemName.text = item.ItemName;
            shopItemUI.PriceText.text = item.Price.ToString();
            shopItemUI.Description.text = item.Description;
            shopItemUI.NumberPurchase.text = item.NumberPurchase;

            UpdateShopItemUI(newItem, item);

            newItem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => BuyItem(item, newItem));
        }
    }
    public void BuyItem(ShopItem item, GameObject newItem)
    {
        if (_playerData.WalletAmount >= item.Price && item.CurrentPurchase < item.TotalPurchase)
        {
            if (item.isFireSpeed == true)
            {
                PlayerPrefs.SetFloat(PlayerData.FIRE_RATE_KEY, _playerData.FireRate -= 0.1f);
                ApplyPurchase(item, 10);
            }
            else if (item.isFireDamage == true)
            {
                PlayerPrefs.SetInt(PlayerData.FIRE_DAMAGE_KEY, _playerData.FireDamage += 1);
                ApplyPurchase(item, 1);
            }
            else if (item.isBulletCount == true)
            {
                PlayerPrefs.SetInt(PlayerData.BULLET_COUNT_KEY, _playerData.BulletCount += 1);
                ApplyPurchase(item, 1);
            }

            _walletUGUI.text = $" Œÿ≈À≈ : {_playerData.WalletAmount} ÃŒÕ≈“";
            UpdateShopItemUI(newItem, item);

            _baseAudioSource.PlayOneShot(_buySoundClip);

            DataManager.Instance.SaveDataAfterShop();

            SavePlayerPrefs(item);
        }
    }
    private void ApplyPurchase(ShopItem item, int value)
    {
        _playerData.WalletAmount -= item.Price;
        item.CurrentValue += value;
        item.CurrentPurchase += 1;
        item.Price += item.IncreasedPrice;
    }
    private void UpdateWalletUI()
    {
        _walletUGUI.text = $" Œÿ≈À≈ : {_playerData.WalletAmount} ÃŒÕ≈“";
    }
    private void UpdateShopItemUI(GameObject newItem, ShopItem item)
    {
        shopItemUI = newItem.GetComponent<ShopItemUI>();

        shopItemUI.PriceText.text = item.Price.ToString();
        shopItemUI.Description.text = item.Description;

        shopItemUI.NumberPurchase.text = $"{item.CurrentPurchase}/{item.TotalPurchase}";
        if (item.CurrentPurchase >= item.TotalPurchase)
        {
            
            shopItemUI.PriceText.text = "MAX";
            if (item.isFireSpeed) shopItemUI.Description.text = $"Ã¿ —»Ã¿À‹Õ¿ﬂ — Œ–Œ—“‹ ¬€—“–≈À¿\n({item.CurrentValue})";
            else if(item.isFireDamage) shopItemUI.Description.text = $"Ã¿ —»Ã¿À‹Õ€… ”–ŒÕ\n({item.CurrentValue})";
            else if(item.isBulletCount) shopItemUI.Description.text = $"Ã¿ —»Ã¿À‹ÕŒ≈  ŒÀ»◊≈—“¬Œ œ”À‹\n({item.CurrentValue})";
        }
        else
        {
            shopItemUI.PriceText.text = $"{item.Price}\nÃŒÕ≈“";
            if (item.isFireSpeed) shopItemUI.Description.text = $"{item.Description}";
            else if (item.isFireDamage) shopItemUI.Description.text = $"{item.Description}";
            else if (item.isBulletCount) shopItemUI.Description.text = item.Description;
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
