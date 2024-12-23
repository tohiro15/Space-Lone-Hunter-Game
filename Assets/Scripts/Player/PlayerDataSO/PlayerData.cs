using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("PlayerPrefsKey")]
    public const string WALLET_AMOUNT_KEY  = "WalletAmount";
    public const string COLLECTED_COINS_AMOUNT_KEY = "Collected—oinsAmount";

    public const string BULLET_COUNT_KEY = "BulletCountKey";

    public const string FIRE_RATE_KEY = "FireRate";
    public const string FIRE_DAMAGE_KEY= "FireDamage";

    [Header("Player Statistic")]
    public int CollectedCoinsAmount = 0;
    public int WalletAmount = 0;
    public float Speed = 6f;

    public int BulletCount = 1;

    public int FireDamage = 1;
    public float FireRate = 1;
}
