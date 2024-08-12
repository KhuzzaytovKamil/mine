using UnityEngine;
using UnityEngine.UI;

public class ImprovementsManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfGoods;
    [SerializeField]
    private GameObject[] texts;
    [SerializeField]
    private GameObject[] frames;

    [SerializeField]
    private Image ImageOfPrice;
    [SerializeField]
    private Text TextOfPrice;
    [SerializeField]
    private GameObject upgradeButton;
    [SerializeField]
    private GameObject notCompleteCondition;
    [SerializeField]
    private GameObject maxLvlScreen;
    public GameObject Price;

    [SerializeField]
    private Text stoneText;
    [SerializeField]
    private Text ironTreasureText;
    [SerializeField]
    private Text goldTreasureText;
    [SerializeField]
    private Text diamondText;
    [SerializeField]
    private Text emeraldText;

    [SerializeField]
    private GoalController GoalController;
    public GameObject goal;
    public Image switchOfGoal;
    public Sprite switchOn;
    public Sprite switchOff;

    public ImprovementController Controller;
    public ImprovementData ImprovementData;
    public Image ImageOfGood;

    [SerializeField]
    private Text[] upgradeDigitInfo—urrentNumber;
    [SerializeField]
    private Text[] upgradeDigitInfoBonusFromUpdate;
    private float upgradeBonus;
    [SerializeField]
    private Noob noob;
    [SerializeField]
    private AudioSource upgrade;

    public void OffAll()
    {
        for (int i = 0; i < numberOfGoods; i++)
        {
            texts[i].SetActive(false);
            frames[i].SetActive(false);
        }
    }

    private void Update()
    {
        UpdateData();
    }

    public void UpdateButton()
    {
        switch (ImprovementData.priceType[PlayerPrefs.GetInt(ImprovementData.ImprovementName)])
        {
            case "stone":
                PlayerPrefs.SetInt("stone", PlayerPrefs.GetInt("stone") - ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)]);
                stoneText.text = PlayerPrefs.GetInt("stone").ToString();
                break;
            case "ironTreasure":
                PlayerPrefs.SetInt("ironTreasure", PlayerPrefs.GetInt("ironTreasure") - ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)]);
                ironTreasureText.text = PlayerPrefs.GetInt("ironTreasure").ToString();
                break;
            case "goldTreasure":
                PlayerPrefs.SetInt("goldTreasure", PlayerPrefs.GetInt("goldTreasure") - ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)]);
                goldTreasureText.text = PlayerPrefs.GetInt("goldTreasure").ToString();
                break;
            case "diamond":
                PlayerPrefs.SetInt("diamond", PlayerPrefs.GetInt("diamond") - ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)]);
                diamondText.text = PlayerPrefs.GetInt("diamond").ToString();
                break;
            default:
                PlayerPrefs.SetInt("emerald", PlayerPrefs.GetInt("emerald") - ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)]);
                emeraldText.text = PlayerPrefs.GetInt("emerald").ToString();
                break;
        }
        PlayerPrefs.SetInt(ImprovementData.ImprovementName, PlayerPrefs.GetInt(ImprovementData.ImprovementName) + 1);

            PlayerPrefs.SetInt("GoalRequest", 1);
        UpdateData();
        noob.UpgradeHappened(Controller.goodNumber);
        upgrade.Play();
    }

    public void UpdateData()
    {
        if (ImprovementData != null)
        {
            notCompleteCondition.SetActive(false);
            upgradeButton.SetActive(false);
            maxLvlScreen.SetActive(false);
            upgradeDigitInfo—urrentNumber[Controller.goodNumber].text = ImprovementData.power[PlayerPrefs.GetInt(ImprovementData.ImprovementName)].ToString();
            if (ImprovementData.priceType[PlayerPrefs.GetInt(ImprovementData.ImprovementName)] != "max")
            {
                upgradeBonus = ImprovementData.power[PlayerPrefs.GetInt(ImprovementData.ImprovementName) + 1] - ImprovementData.power[PlayerPrefs.GetInt(ImprovementData.ImprovementName)];
                if (Controller.goodNumber != 2)
                {
                    upgradeDigitInfoBonusFromUpdate[Controller.goodNumber].text = "+" + upgradeBonus.ToString();
                }
                else
                {
                    upgradeDigitInfoBonusFromUpdate[Controller.goodNumber].text = "+" + upgradeBonus.ToString() + "%";
                }
            }
            else
            {
                upgradeDigitInfoBonusFromUpdate[Controller.goodNumber].gameObject.SetActive(false);
            }

            switch (ImprovementData.priceType[PlayerPrefs.GetInt(ImprovementData.ImprovementName)])
            {
                case "stone":
                    ImageOfPrice.sprite = ImprovementData.priceSprite[0];
                    if (ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)] <= PlayerPrefs.GetInt("stone"))
                    {
                        upgradeButton.SetActive(true);
                    }
                    else
                    {
                        notCompleteCondition.SetActive(true);
                    }
                    break;
                case "ironTreasure":
                    ImageOfPrice.sprite = ImprovementData.priceSprite[1];
                    if (ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)] <= PlayerPrefs.GetInt("ironTreasure"))
                    {
                        upgradeButton.SetActive(true);
                    }
                    else
                    {
                        notCompleteCondition.SetActive(true);
                    }
                    break;
                case "goldTreasure":
                    ImageOfPrice.sprite = ImprovementData.priceSprite[2];
                    if (ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)] <= PlayerPrefs.GetInt("goldTreasure"))
                    {
                        upgradeButton.SetActive(true);
                    }
                    else
                    {
                        notCompleteCondition.SetActive(true);
                    }
                    break;
                case "diamond":
                    ImageOfPrice.sprite = ImprovementData.priceSprite[3];
                    if (ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)] <= PlayerPrefs.GetInt("diamond"))
                    {
                        upgradeButton.SetActive(true);
                    }
                    else
                    {
                        notCompleteCondition.SetActive(true);
                    }
                    break;
                case "emerald":
                    ImageOfPrice.sprite = ImprovementData.priceSprite[4];
                    if (ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)] <= PlayerPrefs.GetInt("emerald"))
                    {
                        upgradeButton.SetActive(true);
                    }
                    else
                    {
                        notCompleteCondition.SetActive(true);
                    }
                    break;
                case "max":
                    goal.SetActive(false);
                    maxLvlScreen.SetActive(true);
                    Price.SetActive(false);
                    if (PlayerPrefs.GetInt("currentGoodGoal") == Controller.goodNumber)
                    {
                        GoalController.ChangeGoal();
                    }
                    break;
                default:
                    break;
            }
            TextOfPrice.text = ImprovementData.priceNumber[PlayerPrefs.GetInt(ImprovementData.ImprovementName)].ToString();

            ImageOfGood.sprite = ImprovementData.goodSprite[PlayerPrefs.GetInt(ImprovementData.ImprovementName) + 1];
            if (PlayerPrefs.GetInt("currentGoodGoal") == Controller.goodNumber)
            {
                GoalController.UpdateGoal();
            }
        }
    }

    public void Switch()
    {
        PlayerPrefs.SetInt("currentGoodGoal", Controller.goodNumber);
        switchOfGoal.sprite = switchOn;
        PlayerPrefs.SetInt("GoalRequest", 1);
        GoalController.UpdateGoal();
    }
}