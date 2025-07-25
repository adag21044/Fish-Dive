using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Game/LevelDatabase", order = 2)]
public class LevelDatabase : ScriptableObject
{
    public LevelDataSO[] levels;
}
