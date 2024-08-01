using UnityEngine;

public class ImprovementController : MonoBehaviour
{
    [SerializeField]
    private ImprovementsManager Manager;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject frame;
    [SerializeField]
    private GameObject upgradeButton;
    [SerializeField]
    private GameObject notCompleteCondition;
    [SerializeField]
    private ImprovementData ImprovementData;

    public void ClickToGood()
    {
        Manager.OffAll();
        text.SetActive(true);
        frame.SetActive(true);
    }
}
