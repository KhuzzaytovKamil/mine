using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using YG.Example;

public class GameManager : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    public void Save()
    {
        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        YandexGame.savesData.stone = PlayerPrefs.GetInt("stone");
        YandexGame.savesData.coal = PlayerPrefs.GetInt("coal");
        YandexGame.savesData.iron = PlayerPrefs.GetInt("iron");
        YandexGame.savesData.ironTreasure = PlayerPrefs.GetInt("ironTreasure");
        YandexGame.savesData.gold = PlayerPrefs.GetInt("gold");
        YandexGame.savesData.goldTreasure = PlayerPrefs.GetInt("goldTreasure");
        YandexGame.savesData.diamond = PlayerPrefs.GetInt("diamond");
        YandexGame.savesData.emerald = PlayerPrefs.GetInt("emerald");

        YandexGame.savesData.FoodImprovement = PlayerPrefs.GetInt("FoodImprovement");
        YandexGame.savesData.LuckImprovement = PlayerPrefs.GetInt("LuckImprovement");
        YandexGame.savesData.PickaxeImprovement = PlayerPrefs.GetInt("PickaxeImprovement");

        YandexGame.savesData.Blocks = PlayerPrefs.GetInt("Blocks");
    }
    public void OpenWindow(GameObject Window) => Window.SetActive(true);

    public void CloseWindow(GameObject Window) => Window.SetActive(false);

    public void SwichSceneTo(string SceneName)
    {
        Save();
        SceneManager.LoadScene(SceneName);
    }
}