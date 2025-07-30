using UnityEngine;

[CreateAssetMenu(menuName = "LevelDataSO/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public int level;
    public int optimumquestioncount;
    public int minnumber;
    public int maxnumber;
    public int mindelaycount;
    public int maxdelaycount;
    public int spawncount;
    public float spawninterval;
    public float speed;
    public float combospeedratio;
    public float gameDuration = 90f;
    public int bubblecount;
}