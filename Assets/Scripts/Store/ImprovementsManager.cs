using UnityEngine;

public class ImprovementsManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfGoods;
    [SerializeField]
    private GameObject[] texts;
    [SerializeField]
    private GameObject[] frames;

    public void OffAll()
    {
        for (int i = 0; i < numberOfGoods; i++)
        {
            texts[i].SetActive(false);
            frames[i].SetActive(false);
        }
    }
}
