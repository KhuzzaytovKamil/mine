using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TasksOfGame")]
public class TasksOfGame : ScriptableObject
{
    public string[] goalText;
    public Dictionary<string, string>[] openWith;
    public string[] goalType;
    public int[] goalNumber;
    public int[] rewardNumber;
}
