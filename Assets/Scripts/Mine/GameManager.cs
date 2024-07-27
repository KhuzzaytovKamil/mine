using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class GameManager : MonoBehaviour
    {
        public void OpenWindow(GameObject Window) => Window.SetActive(true);

        public void CloseWindow(GameObject Window) => Window.SetActive(false);

        public void SwichSceneTo(string SceneName) => SceneManager.LoadScene(SceneName);

        public void AddCoins(int number) => PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + number);
    }
}