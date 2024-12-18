using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 3)]
public class GameData : ScriptableObject
{
    [Header("PlayerPrefsKey")]
    public const string STAGE_PASSED_KEY = "StagePassed";
    public const string RECORD_STAGE_KEY = "RecordStage";

    [Header("Settings")]
    public string[] LevelsName;
    public Color[] LevelsColor;

    [Header("Statistics")]
    public int CurrentLevel = 0;
    public int CurrentStage = 1;
    public int TotalStage = 5;

    [Header("Stage Settings")]
    public string StageName;

    public int StagePassed = 1;
    public int RecordStage = 0;
}
