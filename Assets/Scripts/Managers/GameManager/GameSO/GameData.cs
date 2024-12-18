using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 3)]
public class GameData : ScriptableObject
{
    [Header("PlayerPrefsKey")]
    public const string LEVEL_CURRENT_KEY = "CurrentLevel";

    public const string STAGE_CURRENT_KEY = "CurrentStage";
    public const string STAGE_TOTAL_KEY = "TotalStage";
    public const string RECORD_STAGE_KEY = "RecordStage";

    [Header("Level Settings")]
    public string[] LevelsName;
    public Color[] LevelsColor;

    [Header("Statistics")]
    public int CurrentLevel = 0;
    public int CurrentStage = 1;
    public int TotalStage = 5;

    public int RecordStage = 0;
}
