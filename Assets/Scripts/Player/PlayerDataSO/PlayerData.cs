using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("PlayerPrefsKey")]
    public const string StagePassedKey = "StagePassed";
    public const string RecordStageKey = "RecordStage";

    public const string WalletAmountKey  = "WalletAmount";
    public const string FireRateKey = "FireRate";

    [Header("Stage Settings")]
    public int StagePassed = 1;
    public int RecordStage = 0;

    [Header("Player Statistic")]
    public float _speed = 6f;
    public int Collected—oinsAmount = 0;
    public int WalletAmount = 0;
    public float FireRate = 1;
    
    private void Awake()
    {
        StagePassed = PlayerPrefs.GetInt(StagePassedKey, 1);
        RecordStage = PlayerPrefs.GetInt(RecordStageKey, 0);

        WalletAmount = PlayerPrefs.GetInt(WalletAmountKey, 0);

        FireRate = PlayerPrefs.GetFloat(FireRateKey, 1);
    }
}
