using UnityEngine;

[CreateAssetMenu(menuName = "FrequencyOfHeight")]
public class FrequencyOfHeight : ScriptableObject
{
    public int minHeight;
    public int maxHeight;
    
    public int numberOfType;

    public int[] frequencyOfBropoutOfType;
    public Sprite[] spritesOfBlock;
    public float[] blockStrength;

    public int TNTchance;
}
