using UnityEngine;
using System.Collections;

public class destroyer : MonoBehaviour
{
    private int damage = 1;

    private void Awake()
    {
        StartCoroutine(ActiveOff());
    }

    private IEnumerator ActiveOff()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Noob>() != null)
        {
            other.gameObject.GetComponent<Noob>().LoseOneUnitOfHealth(damage);
        }
        else if (other.gameObject.GetComponent<Block>() != null)
        {
            other.gameObject.GetComponent<Block>().destroyedByTNT();
        }
    }
}
