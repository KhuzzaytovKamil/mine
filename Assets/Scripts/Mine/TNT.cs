using UnityEngine;

public class TNT : MonoBehaviour
{
    private void Awake()
    {
        gameObject.name = "TNT";
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Noob>() != null || other.gameObject.GetComponent<destroyer>() != null)
        {
            gameObject.GetComponent<Animator>().SetBool("startExplosion", true);
        }
    }
}
