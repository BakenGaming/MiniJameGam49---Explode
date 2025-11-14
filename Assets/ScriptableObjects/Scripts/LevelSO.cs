using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelSO : ScriptableObject
{
    public string levelName;
    public float levelLengthInSeconds;
    public float timeBetweenSpawnsStart;
    public float minTimeBetweenSpawns;
    public EnemySO[] levelEnemies;
}
