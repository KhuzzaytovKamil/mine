using UnityEngine;

public class RewardGetScore : MonoBehaviour
{
    public void UpdateAnimator()
    {
        gameObject.GetComponent<Animator>().SetBool("gotScore", false);
    }
}
