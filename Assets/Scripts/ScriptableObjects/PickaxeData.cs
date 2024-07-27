using UnityEngine;

[CreateAssetMenu(menuName = "Pickaxe")]
public class PickaxeData : ScriptableObject
{
    public int[] priceOfPickaxe;
    public Sprite[] pickaxeSprite;
    public float[] powerOfPickaxe;
}
