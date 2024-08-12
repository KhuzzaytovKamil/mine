using UnityEngine;

public class Item : MonoBehaviour
{
    public int numberOfItem;

    private void Awake()
    {
        gameObject.name = "Item";
    }
}
