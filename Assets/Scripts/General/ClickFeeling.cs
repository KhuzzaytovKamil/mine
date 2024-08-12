using UnityEngine;

public class ClickFeeling : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ImprovementsManager ImprovementsManager;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void click()
    {
        animator.SetBool("click", true);
        animator.SetBool("endOfClick", false);
    }

    public void animationEndTrigger()
    {
        animator.SetBool("endOfClick", true);
        animator.SetBool("click", false);
        ImprovementsManager.UpdateButton();
    }
}
