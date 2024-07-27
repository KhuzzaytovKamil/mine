using UnityEngine;

[CreateAssetMenu(menuName = "BlockGood")]
public class BlockGood : ScriptableObject
{
    public string[] nameOfBlock;
    public int[] blockPrice;
    public Sprite[] spritesOfBlock;
}
