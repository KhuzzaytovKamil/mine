using UnityEngine;
using UnityEngine.UI;

public class ShowDataController : MonoBehaviour
{
    [SerializeField]
    private Text money_text;
    [SerializeField]
    private string dataType;

    private void Start() => money_text.text = PlayerPrefs.GetInt(dataType).ToString();

    public void UpdateScore() => money_text.text = PlayerPrefs.GetInt(dataType).ToString();
    
}
