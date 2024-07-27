using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnergyImprovement : MonoBehaviour
{
    [SerializeField]
    private int[] priceOfImprovement;
    [SerializeField]
    private int[] Energy;
    [SerializeField]
    private Text descriptionText;
    [SerializeField]
    private Text priceText;
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

    private void Start()
    {
        if (maxLvl != PlayerPrefs.GetInt("EnergyImprovement"))
        {
            notMaxLvlObject.SetActive(true);
            priceText.text = priceOfImprovement[PlayerPrefs.GetInt("EnergyImprovement")].ToString();
        }
        else
        {
            maxLvlObject.SetActive(true);
        }
        descriptionText.text = "Энергия: " + PlayerPrefs.GetInt("Energy").ToString() + " \n(Уровень " + PlayerPrefs.GetInt("EnergyImprovement").ToString() + ")";

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
                    priceText.text = priceOfImprovement[PlayerPrefs.GetInt("EnergyImprovement") + 1].ToString();
                }
                else
                {
                    notMaxLvlObject.SetActive(false);
                    maxLvlObject.SetActive(true);
                }
                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - priceOfImprovement[PlayerPrefs.GetInt("EnergyImprovement")]);
                PlayerPrefs.SetInt("EnergyImprovement", PlayerPrefs.GetInt("EnergyImprovement") + 1);
                descriptionText.text = "Энергия: " + PlayerPrefs.GetInt("Energy").ToString() + " \n(Уровень " + PlayerPrefs.GetInt("EnergyImprovement").ToString() + ")";
                showMoneyController.UpdateScore();
                task.UpdateStatus();
            }
        }
    }
}
