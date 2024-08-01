using UnityEngine;

public class Block : MonoBehaviour
{
    public int numberOfTypeOfThisBlock;
    private int selectionIntervalOfBlockType;
    private GameObject createdObject;
    private FrequencyOfHeight localFrequencyOfHeight;

    [SerializeField]
    private SpriteRenderer blockDestructionAnim;
    private float blockStrength;
    private float restBlockStrength;

    public int restOfObjectsOnLeft;
    public int height;

    [SerializeField]
    private bool isItUpperBlock;
    public bool startActions = false;

    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform lowerPoint;

    [SerializeField]
    private int numberOfFrequencyOfHeight;
    [SerializeField]
    private FrequencyOfHeight[] frequencyOfHeight;
    [SerializeField]
    private GameSettings gameSettings;

    private void Start()
    {
        gameObject.name = "Block";

        if (startActions)
        {
            Action();
        }
        
    }

    public void Action()
    {
        for (int i = 0; i < numberOfFrequencyOfHeight; i++)
        {
            if ((frequencyOfHeight[i].minHeight <= height) && (frequencyOfHeight[i].maxHeight > height))
            {
                localFrequencyOfHeight = frequencyOfHeight[i];
                break;
            }
        }

        selectionIntervalOfBlockType = Random.Range(0, 101);
        for (int i = 0; i < localFrequencyOfHeight.numberOfType; i++)
        {
            if (selectionIntervalOfBlockType <= localFrequencyOfHeight.frequencyOfBropoutOfType[i])
            {
                numberOfTypeOfThisBlock = i;
                break;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = localFrequencyOfHeight.spritesOfBlock[numberOfTypeOfThisBlock];
        blockStrength = localFrequencyOfHeight.blockStrength[numberOfTypeOfThisBlock];
        restBlockStrength = blockStrength;

        if ((restOfObjectsOnLeft > 1) && (isItUpperBlock))
        {
            createdObject = Instantiate(gameSettings.upperBlock, leftPoint.position, Quaternion.identity);
            createdObject.GetComponent<Block>().restOfObjectsOnLeft = restOfObjectsOnLeft - 1;
            createdObject.GetComponent<Block>().height = height;

            createdObject.GetComponent<Block>().Action();
        }

        if ((height > 1))
        {
            createdObject = Instantiate(gameSettings.lowerBlock, lowerPoint.position, Quaternion.identity);
            createdObject.GetComponent<Block>().height = height - 1;

            createdObject.GetComponent<Block>().Action();
        }
    }

    public int mineing(float damage)
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
}
