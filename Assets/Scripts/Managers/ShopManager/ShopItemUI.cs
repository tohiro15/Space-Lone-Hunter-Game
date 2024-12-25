using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("Image UI")]
    public Image ButtonImage;
    public Image LockButtonItem;

    [Header("UnlockText UI")]
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI NumberPurchase;

    [Header("LockText UI")]
    public TextMeshProUGUI LockText;

    [Header("Button UI")]
    public Button BuyButton;
}
