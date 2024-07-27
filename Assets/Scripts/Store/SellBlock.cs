using UnityEngine;
using UnityEngine.UI;

public class SellBlocks : MonoBehaviour
{
    [SerializeField]
    private GameObject thereAreBlocks;
    [SerializeField]
    private GameObject thereAreNotBlocks;
    [SerializeField]
    private BlockGood blockGood;
    [SerializeField]
    private int numberOfBlock;
    [SerializeField]
    private Image blockImage;
    [SerializeField]
    private Text rewardText;
    [SerializeField]
    private ShowMoneyController showMoneyController;
    [SerializeField]
    private string namesOfBlocks;
    [SerializeField]
    private Text numbersOfBlocksText;
    [SerializeField]
    private Transform rewardTransform;
    [SerializeField]
    private GameObject getScoreAnim;
    private GameObject createdGetScoreAnim;
    [SerializeField]
    private GameObject Canvas;


    private void Start()
    {
        numbersOfBlocksText.text = PlayerPrefs.GetInt(namesOfBlocks).ToString();
        blockImage.sprite = blockGood.spritesOfBlock[numberOfBlock];
        if (PlayerPrefs.GetInt(blockGood.nameOfBlock[numberOfBlock]) > 0)
        {
            thereAreBlocks.SetActive(true);
            rewardText.text = "Продать +" + (blockGood.blockPrice[numberOfBlock] * PlayerPrefs.GetInt(blockGood.nameOfBlock[numberOfBlock])).ToString();
        }
        else
        {
            thereAreNotBlocks.SetActive(true);
        }
    }

    public void sellBlocks()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + blockGood.blockPrice[numberOfBlock] * PlayerPrefs.GetInt(blockGood.nameOfBlock[numberOfBlock]));
        PlayerPrefs.SetInt(blockGood.nameOfBlock[numberOfBlock], 0);
        createdGetScoreAnim = Instantiate(getScoreAnim, rewardTransform.position, rewardTransform.rotation);
        createdGetScoreAnim.transform.SetParent(Canvas.transform);
        createdGetScoreAnim.transform.localScale = new Vector3(1, 1, 1);
        numbersOfBlocksText.text = PlayerPrefs.GetInt(namesOfBlocks).ToString();
        showMoneyController.UpdateScore();
        thereAreNotBlocks.SetActive(true);
        thereAreBlocks.SetActive(false);
    }
}
