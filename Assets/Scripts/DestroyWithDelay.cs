using System.Collections;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    [SerializeField]
    private float delay;

    private void Start()
    {
        StartCoroutine(delayCoroutine());
    }

    private IEnumerator delayCoroutine()
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
