using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("PlayerPrefsKey")]
    public const string StagePassedKey = "StagePassed";
    public const string RecordStageKey = "RecordStage";

    public const string WalletAmountKey  = "WalletAmount";
    public const string Collected—oinsAmountKey = "Collected—oinsAmount";

    public const string FireRateKey = "FireRate";

    [Header("Stage Settings")]
    public int StagePassed = 1;
    public int RecordStage = 0;

    [Header("Player Statistic")]
    public float _speed = 6f;
    public int Collected—oinsAmount = 0;
    public int WalletAmount = 0;
    public float FireRate = 1;
}
