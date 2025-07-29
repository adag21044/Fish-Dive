using UnityEngine;

[CreateAssetMenu(menuName = "LevelDataSO/LevelDataBase")]
public class LevelDatabase : ScriptableObject
{
    public LevelDataSO[] levels;
}
