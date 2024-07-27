using UnityEngine;
using UnityEngine.UI;

public class Pickaxe : MonoBehaviour
{
    [SerializeField]
    private int numberOfPickaxe;
    [SerializeField]
    private PickaxeData pickaxeData;
    [SerializeField]
    private Text text;
    [SerializeField]
    private ShowMoneyController showMoneyController;

    [SerializeField]
    private Task task;

    private void Start()
    {
        if (PlayerPrefs.GetInt("numberOfPickaxe" + numberOfPickaxe.ToString()) == 0)
        {
            text.text = "цена:" + pickaxeData.priceOfPickaxe[numberOfPickaxe].ToString();
        }
        else
        {
            text.text = "куплено";
        }
    }
    
    public void buyPickaxe()
    {
        if (PlayerPrefs.GetInt("numberOfPickaxe" + numberOfPickaxe.ToString()) == 0)
        {
            if (pickaxeData.priceOfPickaxe[numberOfPickaxe] <= PlayerPrefs.GetInt("score"))
            {
                text.text = "куплено";
                PlayerPrefs.SetInt("numberOfPickaxe" + numberOfPickaxe.ToString(), 1);
                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - pickaxeData.priceOfPickaxe[numberOfPickaxe]);
                task.UpdateStatus();
                showMoneyController.UpdateScore();

                if (pickaxeData.powerOfPickaxe[numberOfPickaxe] > pickaxeData.powerOfPickaxe[PlayerPrefs.GetInt("numberOfPickaxe")])
                {
                    PlayerPrefs.SetInt("numberOfPickaxe", numberOfPickaxe);
                }
            }
        }        
    }
}