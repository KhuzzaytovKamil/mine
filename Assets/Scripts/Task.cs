using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;

public class Task : MonoBehaviour
{
    [SerializeField]
    private TasksOfGame TasksOfGame;
    [SerializeField]
    private Text goalText;
    [SerializeField]
    private int percentageOfTaskCompletion;
    [SerializeField]
    private string[] namesOfResourcesNeededForMining;
    [SerializeField]
    private string[] namesOfImprovementNeededForUpdate;
    [SerializeField]
    private GameObject reward;
    [SerializeField]
    private ShowMoneyController showMoneyController;
    [SerializeField]
    private GameObject getScoreAnim;
    private GameObject createdGetScoreAnim;
    [SerializeField]
    private GameObject Canvas;
    [SerializeField]
    private GameObject descriptionOfTask;
    [SerializeField]
    private Text descriptionText;

    private void Start()
    {
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        if (PlayerPrefs.GetInt("IsGameCompleted") == 0)
        {
            descriptionOfTask.SetActive(false);
            reward.SetActive(false);
            for (int i = 0; i < 6; i++)
            {
                if (TasksOfGame.goalType[PlayerPrefs.GetInt("goalNumber")] == (namesOfResourcesNeededForMining[i]))
                {
                    percentageOfTaskCompletion = PlayerPrefs.GetInt(namesOfResourcesNeededForMining[i]) * 100 / TasksOfGame.goalNumber[PlayerPrefs.GetInt("goalNumber")];
                    break;
                }
            }

            for (int numberOfPickaxe = 0; numberOfPickaxe < 6; numberOfPickaxe++)
            {
                if (TasksOfGame.goalType[PlayerPrefs.GetInt("goalNumber")] == (numberOfPickaxe.ToString()))
                {
                    if (PlayerPrefs.GetInt("numberOfPickaxe" + numberOfPickaxe.ToString()) == 0)
                    {
                        percentageOfTaskCompletion = 0;
                    }
                    else
                    {
                        percentageOfTaskCompletion = 100;
                    }
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (TasksOfGame.goalType[PlayerPrefs.GetInt("goalNumber")] == (namesOfImprovementNeededForUpdate[i]))
                {
                    print(namesOfImprovementNeededForUpdate);
                    percentageOfTaskCompletion = PlayerPrefs.GetInt(namesOfImprovementNeededForUpdate[i]) * 100 / TasksOfGame.goalNumber[PlayerPrefs.GetInt("goalNumber")];
                    break;
                }
            }

            goalText.text = TasksOfGame.goalText[PlayerPrefs.GetInt("goalNumber")] + " (" + percentageOfTaskCompletion.ToString() + "%)";

            if (percentageOfTaskCompletion >= 100)
            {
                reward.SetActive(true);
                descriptionOfTask.SetActive(true);
                descriptionText.text = TasksOfGame.goalText[PlayerPrefs.GetInt("goalNumber")] + " (" + percentageOfTaskCompletion.ToString() + "%)";
                goalText.text = "Получи награду (иконка подарка)";
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void GetReward()
    {

        createdGetScoreAnim = Instantiate(getScoreAnim, reward.transform.position, reward.transform.rotation);
        createdGetScoreAnim.transform.SetParent(Canvas.transform);
        createdGetScoreAnim.transform.localScale = new Vector3(1, 1, 1);
        percentageOfTaskCompletion = 0;
        YandexMetrica.Send(TasksOfGame.goalText[PlayerPrefs.GetInt("goalNumber")]);
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + TasksOfGame.rewardNumber[PlayerPrefs.GetInt("goalNumber")]);
        showMoneyController.UpdateScore();
        PlayerPrefs.SetInt("goalNumber", PlayerPrefs.GetInt("goalNumber") + 1);
        UpdateStatus();
        if (PlayerPrefs.GetInt("goalNumber") % 5 == 0)
        {
            YandexGame.ReviewShow(true);
        }
    }
}
