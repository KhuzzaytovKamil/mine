using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Victory : MonoBehaviour
{
    public void CloseVictoryScreen()
    {
        YandexGame.ReviewShow(true);
        PlayerPrefs.SetInt("IsGameCompleted", 1);
        SceneManager.LoadScene("Mine");
    }
}
