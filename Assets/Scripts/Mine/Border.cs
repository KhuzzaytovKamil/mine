using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField]
    private bool isVerticalBorder;

    public int restOfObjects;
    private GameObject createdObject;
    [SerializeField]
    private SpriteRenderer blockDestructionAnim;
    [SerializeField]
    private float blockStrength;
    private float restBlockStrength;

    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform lowerPoint;

    [SerializeField]
    private GameObject verticalBorderBlock;
    [SerializeField]
    private GameObject horizontalBorderBlock;
    [SerializeField]
    private GameSettings gameSettings;

    private void Start()
    {
        restBlockStrength = blockStrength;

        if (isVerticalBorder)
        {
            if ((restOfObjects > 1))
            {
                createdObject = Instantiate(verticalBorderBlock, lowerPoint.position, Quaternion.identity);
                createdObject.GetComponent<Border>().restOfObjects = restOfObjects - 1;
            }
        }
        else
        {
            if ((restOfObjects > 1))
            {
                createdObject = Instantiate(horizontalBorderBlock, leftPoint.position, Quaternion.identity);
                createdObject.GetComponent<Border>().restOfObjects = restOfObjects - 1;
            }
        }
    }

    public int mineing(float damage)
    {
        if (PlayerPrefs.GetInt("numberOfPickaxe") == 5)
        {
            restBlockStrength -= damage;

            if (restBlockStrength > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (((blockStrength / 7) * i < restBlockStrength) && (restBlockStrength <= (blockStrength / 7) * (i + 1)))
                    {
                        blockDestructionAnim.sprite = gameSettings.animationOfBlockDestructionFrame[7 - i];
                    }
                }

                return 0;
            }
            else
            {
                Destroy(gameObject);
                return 1;
            }
        }
        return 0;
    }
}
