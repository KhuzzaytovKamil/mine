using UnityEngine;
using UnityEngine.UI;

public class TradeController : MonoBehaviour
{

    [SerializeField]
    private GoalController GoalController;

    [SerializeField]
    private int numberOfPrice_1;
    [SerializeField]
    private string typeOfPrice_1;
    [SerializeField]
    private int numberOfPrice_2;
    [SerializeField]
    private string typeOfPrice_2;

    [SerializeField]
    private int numberOfGood;
    [SerializeField]
    private string typeOfGood;

    [SerializeField]
    private int numberOfAssets;
    [SerializeField]
    private ShowDataController[] assets;

    public void UpdateAssets()
    {
        for (int i = 0; i < numberOfAssets; i++)
        {
            assets[i].UpdateScore();
        }
    }

    public void Trade()
    {
        if ((PlayerPrefs.GetInt(typeOfPrice_1) >= numberOfPrice_1) && (PlayerPrefs.GetInt(typeOfPrice_2) >= numberOfPrice_2))
        {
            PlayerPrefs.SetInt(typeOfPrice_1, PlayerPrefs.GetInt(typeOfPrice_1) - numberOfPrice_1);
            PlayerPrefs.SetInt(typeOfPrice_2, PlayerPrefs.GetInt(typeOfPrice_2) - numberOfPrice_2);
            PlayerPrefs.SetInt(typeOfGood, PlayerPrefs.GetInt(typeOfGood) + numberOfGood);
            UpdateAssets();
            GoalController.CheckGoal();
        }
    }
}
