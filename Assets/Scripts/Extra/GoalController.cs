using UnityEngine;
using UnityEngine.UI;


public class GoalController : MonoBehaviour
{
    [SerializeField]
    private GameObject store;
    [SerializeField]
    private GameObject height;
    [SerializeField]
    private GameObject furnaceScreen;
    [SerializeField]
    private GameObject[] goalTexts;
    [SerializeField]
    private ImprovementData[] ImprovementDatas;
    [SerializeField]
    private ImprovementController[] Controllers;

    [SerializeField]
    private Image ImageOfPrice;
    [SerializeField]
    private Text TextOfPrice;
    [SerializeField]
    private Color standardColorOfPrice;
    [SerializeField]
    private Color completedColorOfPrice;
    private ImprovementData data;
    private bool notAllMax = true;

    [SerializeField]
    private Noob noob;

    private void Start()
    {
        UpdateGoal();
        CheckGoal();
    }

    public void UpdateGoal()
    {
        for (int i = 0; i < 4; i++)
        {
            goalTexts[i].SetActive(false);
        }
        data = ImprovementDatas[PlayerPrefs.GetInt("currentGoodGoal")];
        goalTexts[PlayerPrefs.GetInt("currentGoodGoal")].SetActive(true);

        switch (data.priceType[PlayerPrefs.GetInt(data.ImprovementName)])
        {
            case "stone":
                ImageOfPrice.sprite = data.priceSprite[0];
                break;
            case "ironTreasure":
                ImageOfPrice.sprite = data.priceSprite[1];
                break;
            case "goldTreasure":
                ImageOfPrice.sprite = data.priceSprite[2];
                break;
            case "diamond":
                ImageOfPrice.sprite = data.priceSprite[3];
                break;
            default:
                ImageOfPrice.sprite = data.priceSprite[4];
                break;
        }
        TextOfPrice.color = standardColorOfPrice;
        TextOfPrice.text = data.priceNumber[PlayerPrefs.GetInt(data.ImprovementName)].ToString();
        CheckGoal();
    }

    public void ChangeGoal()
    {
        notAllMax = false;
        for (int i = 0; i < 4; i++)
        {
            if (ImprovementDatas[i].priceType[PlayerPrefs.GetInt(ImprovementDatas[i].ImprovementName)] != "max")
            {
                PlayerPrefs.SetInt("currentGoodGoal", i);
                UpdateGoal();
                notAllMax = true;
                PlayerPrefs.SetInt("GoalRequest", 1);
                break;
            }
        }

        if (notAllMax == false) 
        {
            height.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }

    public void CheckGoal()
    {
        switch (data.priceType[PlayerPrefs.GetInt(data.ImprovementName)])
        {
            case "stone":
                ImageOfPrice.sprite = data.priceSprite[0];
                if (data.priceNumber[PlayerPrefs.GetInt(data.ImprovementName)] <= PlayerPrefs.GetInt("stone"))
                {
                    GoalCompleted();
                }
                break;
            case "ironTreasure":
                ImageOfPrice.sprite = data.priceSprite[1];
                if (data.priceNumber[PlayerPrefs.GetInt(data.ImprovementName)] <= PlayerPrefs.GetInt("ironTreasure"))
                {
                    GoalCompleted();
                }
                break;
            case "goldTreasure":
                ImageOfPrice.sprite = data.priceSprite[2];
                if (data.priceNumber[PlayerPrefs.GetInt(data.ImprovementName)] <= PlayerPrefs.GetInt("goldTreasure"))
                {
                    GoalCompleted();
                }
                break;
            case "diamond":
                ImageOfPrice.sprite = data.priceSprite[3];
                if (data.priceNumber[PlayerPrefs.GetInt(data.ImprovementName)] <= PlayerPrefs.GetInt("diamond"))
                {
                    GoalCompleted();
                }
                break;
            default:
                ImageOfPrice.sprite = data.priceSprite[4];
                if (data.priceNumber[PlayerPrefs.GetInt(data.ImprovementName)] <= PlayerPrefs.GetInt("emerald"))
                {
                    GoalCompleted();
                }
                break;
        }
    }

    private void GoalCompleted()
    {
        TextOfPrice.color = completedColorOfPrice;
        if (PlayerPrefs.GetInt("GoalRequest") == 1)
        {
            furnaceScreen.SetActive(false);
            PlayerPrefs.SetInt("GoalRequest", 0);
            Controllers[PlayerPrefs.GetInt("currentGoodGoal")].ClickToGood();
            noob.StoreOpened();
            store.SetActive(true);
        }
    }
}
