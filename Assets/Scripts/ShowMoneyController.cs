using UnityEngine;
using UnityEngine.UI;

public class ShowMoneyController : MonoBehaviour
{
    [SerializeField]
    private Text money_text;
    private void Start() => money_text.text = PlayerPrefs.GetInt("score").ToString();
    public void UpdateScore() => money_text.text = PlayerPrefs.GetInt("score").ToString();
    
}
