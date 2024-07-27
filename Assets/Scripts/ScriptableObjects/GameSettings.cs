using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public GameObject lowerBlock;
    public GameObject upperBlock;

    public Sprite[] animationOfBlockDestructionFrame;
}
