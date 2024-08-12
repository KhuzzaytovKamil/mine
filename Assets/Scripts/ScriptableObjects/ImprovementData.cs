using UnityEngine;

[CreateAssetMenu(menuName = "ImprovementData")]
public class ImprovementData : ScriptableObject
{
    public string ImprovementName;
    public int[] priceNumber;
    public string[] priceType;
    public Sprite[] priceSprite;
    public Sprite[] goodSprite;
    public float[] power;
}
