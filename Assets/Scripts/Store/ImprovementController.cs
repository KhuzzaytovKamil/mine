using UnityEngine;
using UnityEngine.UI;

public class ImprovementController : MonoBehaviour
{
    public int goodNumber;
    [SerializeField]
    private ImprovementsManager Manager;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject frame;
    [SerializeField]
    private ImprovementData ImprovementData;

    [SerializeField]
    private Image ImageOfGood;


    private void Start()
    {
        ImageOfGood.sprite = ImprovementData.goodSprite[PlayerPrefs.GetInt(ImprovementData.ImprovementName) + 1];
    }

    public void ClickToGood()
    {
        Manager.Price.SetActive(true);
        if (ImprovementData.priceType[PlayerPrefs.GetInt(ImprovementData.ImprovementName)] != "max")
        {
            Manager.goal.SetActive(true);
        }
        else
        {
            Manager.goal.SetActive(false);
        }
        
        if (PlayerPrefs.GetInt("currentGoodGoal") == goodNumber)
        {
            Manager.switchOfGoal.sprite = Manager.switchOn;
        }
        else
        {
            Manager.switchOfGoal.sprite = Manager.switchOff;
        }
        Manager.OffAll();
        text.SetActive(true);
        frame.SetActive(true);

        Manager.Controller = this;
        Manager.ImageOfGood = ImageOfGood;
        Manager.ImprovementData = ImprovementData;

        Manager.UpdateData();
    }
}
