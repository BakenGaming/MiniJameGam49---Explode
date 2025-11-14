using UnityEngine;

[CreateAssetMenu(menuName ="Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public EnemyStatsSO enemyStatsSO;
}
