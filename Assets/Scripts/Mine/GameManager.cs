using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Main
{
    public class GameManager : MonoBehaviour
    {
        public void OpenWindow(GameObject Window) => Window.SetActive(true);

        public void CloseWindow(GameObject Window) => Window.SetActive(false);

        public void SwichSceneTo(string SceneName)
        {
            YandexGame.savesData.coal = PlayerPrefs.GetInt("coal");
            YandexGame.savesData.copper = PlayerPrefs.GetInt("copper");
            YandexGame.savesData.iron = PlayerPrefs.GetInt("iron");
            YandexGame.savesData.gold = PlayerPrefs.GetInt("gold");
            YandexGame.savesData.diamond = PlayerPrefs.GetInt("diamond");
            YandexGame.savesData.emerald = PlayerPrefs.GetInt("emerald");
            YandexGame.savesData.score = PlayerPrefs.GetInt("score");
            YandexGame.savesData.goalNumber = PlayerPrefs.GetInt("goalNumber");
            YandexGame.savesData.EnergyImprovement = PlayerPrefs.GetInt("EnergyImprovement");
            YandexGame.savesData.LuckImprovement = PlayerPrefs.GetInt("LuckImprovement");
            YandexGame.savesData.Energy = PlayerPrefs.GetInt("Energy");
            YandexGame.savesData.Luck = PlayerPrefs.GetInt("Luck");
            YandexGame.savesData.Blocks = PlayerPrefs.GetInt("Blocks");
            YandexGame.savesData.numberOfPickaxe = PlayerPrefs.GetInt("numberOfPickaxe");
            YandexGame.savesData.numberOfPickaxe1 = PlayerPrefs.GetInt("numberOfPickaxe1");
            YandexGame.savesData.numberOfPickaxe2 = PlayerPrefs.GetInt("numberOfPickaxe2");
            YandexGame.savesData.numberOfPickaxe3 = PlayerPrefs.GetInt("numberOfPickaxe3");
            YandexGame.savesData.numberOfPickaxe4 = PlayerPrefs.GetInt("numberOfPickaxe4");
            YandexGame.savesData.numberOfPickaxe5 = PlayerPrefs.GetInt("numberOfPickaxe5");
            YandexGame.savesData.IsGameCompleted = PlayerPrefs.GetInt("IsGameCompleted");
            YandexGame.SaveProgress();

            SceneManager.LoadScene(SceneName);
        }

        public void AddCoins(int number) => PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + number);
    }
}