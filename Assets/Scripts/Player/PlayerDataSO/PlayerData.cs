using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public const string WalletAmountKey  = "WalletAmount";
    public const string FireRateKey = "FireRate";

    public float _speed = 6f;

    public int WalletAmount = 0;
    public float FireRate = 1;

    public int CurrentScore = 0;
    
    private void OnEnable()
    {
        WalletAmount = PlayerPrefs.GetInt(WalletAmountKey, 0);
        FireRate = PlayerPrefs.GetInt(FireRateKey, 1);
    }
}
