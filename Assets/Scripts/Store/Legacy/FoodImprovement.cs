using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FoodImprovement : MonoBehaviour
{
    [SerializeField]
    private int[] priceOfImprovement;
    [SerializeField]
    private int[] Energy;
    [SerializeField]
    private GameObject FoodText;
    [SerializeField]
    private ShowMoneyController showMoneyController;
    [SerializeField]
    private int maxLvl;
    [SerializeField]
    private GameObject notMaxLvlObject;
    [SerializeField]
    private GameObject maxLvlObject;

    [SerializeField]
    private Task task;

    public void ClickToGood()
    {
        
    }

    public void buyImprovement()
    {
        if (maxLvl != PlayerPrefs.GetInt("EnergyImprovement"))
        {
            if (priceOfImprovement[PlayerPrefs.GetInt("EnergyImprovement")] <= PlayerPrefs.GetInt("score"))
            {
                PlayerPrefs.SetInt("Energy", Energy[PlayerPrefs.GetInt("EnergyImprovement")]);
                if (maxLvl != PlayerPrefs.GetInt("EnergyImprovement") + 1)
                {
                    
                }
                else
                {
                    notMaxLvlObject.SetActive(false);
                    maxLvlObject.SetActive(true);
                }
                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - priceOfImprovement[PlayerPrefs.GetInt("EnergyImprovement")]);
                PlayerPrefs.SetInt("EnergyImprovement", PlayerPrefs.GetInt("EnergyImprovement") + 1);
                showMoneyController.UpdateScore();
                task.UpdateStatus();
            }
        }
    }
}
