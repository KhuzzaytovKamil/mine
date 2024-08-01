using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LuckImprovement : MonoBehaviour
{
    [SerializeField]
    private int[] priceOfImprovement;
    [SerializeField]
    private float[] Luck;
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
        if (maxLvl != PlayerPrefs.GetInt("LuckImprovement"))
        {
            notMaxLvlObject.SetActive(true);
            priceText.text = priceOfImprovement[PlayerPrefs.GetInt("LuckImprovement")].ToString();
        }
        else
        {
            maxLvlObject.SetActive(true);
        }
        descriptionText.text = "Шанс выпадения x2: " + PlayerPrefs.GetFloat("Luck").ToString() + " \n(Уровень " + PlayerPrefs.GetInt("LuckImprovement").ToString() + ")";
    }

    public void buyImprovement()
    {
        if (maxLvl != PlayerPrefs.GetInt("LuckImprovement"))
        {
            if (priceOfImprovement[PlayerPrefs.GetInt("LuckImprovement")] <= PlayerPrefs.GetInt("score"))
            {
                PlayerPrefs.SetFloat("Luck", Luck[PlayerPrefs.GetInt("LuckImprovement")]);
                if (maxLvl != PlayerPrefs.GetInt("LuckImprovement") + 1)
                {
                    priceText.text = priceOfImprovement[PlayerPrefs.GetInt("LuckImprovement") + 1].ToString();
                }
                else
                {
                    notMaxLvlObject.SetActive(false);
                    maxLvlObject.SetActive(true);
                }
                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - priceOfImprovement[PlayerPrefs.GetInt("LuckImprovement")]);
                PlayerPrefs.SetInt("LuckImprovement", PlayerPrefs.GetInt("LuckImprovement") + 1);
                descriptionText.text = "Шанс выпадения x2: " + PlayerPrefs.GetFloat("Luck").ToString() + " \n(Уровень " + PlayerPrefs.GetInt("LuckImprovement").ToString() + ")";
                showMoneyController.UpdateScore();
                task.UpdateStatus();
            }
        }
    }
}
