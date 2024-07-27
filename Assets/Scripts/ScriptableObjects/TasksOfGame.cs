using UnityEngine;

[CreateAssetMenu(menuName = "TasksOfGame")]
public class TasksOfGame : ScriptableObject
{
    public string[] goalText;
    public string[] goalType;
    public int[] goalNumber;
    public int[] rewardNumber;
}
