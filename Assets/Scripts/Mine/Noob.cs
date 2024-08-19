using UnityEngine;
using UnityEngine.UI;
using YG;

public class Noob : MonoBehaviour
{
    #region Food
    [Header("Food")]
    [SerializeField]
    private float restOfFoodShere = 1;
    [SerializeField]
    private Image FoodBar;
    [SerializeField]
    private Text[] foodNumbers;
    private float restOfFood;
    [SerializeField]
    private GameObject RestoreFood;
    private bool foodRestoreWasNot = true;
    #endregion

    #region Health
    [Header("Health")]
    [SerializeField]
    private float restOfHealthShere = 1;
    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private Text[] healthNumbers;
    private float restOfHealth;
    [SerializeField]
    private GameObject RestoreHealth;
    private bool healthRestoreWasNot = true;
    [SerializeField]
    private SpriteRenderer ArmorSetImage;
    [SerializeField]
    private Sprite[] ArmorSetSprites;
    #endregion

    #region Pickaxe
    [Header("Pickaxe")]
    [SerializeField]
    private Animator animatorOfStayPickaxe;
    [SerializeField]
    private Animator animatorOfLeftPickaxe;
    [SerializeField]
    private Animator animatorOfRightPickaxe;
    [SerializeField]
    private GameObject stayPickaxe;
    [SerializeField]
    private GameObject leftPickaxe;
    [SerializeField]
    private GameObject rightPickaxe;
    [SerializeField]
    private SpriteRenderer[] pickaxes;
    #endregion

    #region Blocks
    [Header("Blocks")]
    [SerializeField]
    private string[] namesOfBlocks;
    [SerializeField]
    private Text[] numbersOfBlocksText;
    #endregion

    #region finishScreen
    [Header("finishScreen")]
    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private GameObject vectoryScreen;
    #endregion

    #region interactionObject
    [Header("interactionObject")]
    [SerializeField]
    private GameObject upperObject;
    [SerializeField]
    private GameObject lowerObject;
    [SerializeField]
    private GameObject leftObject;
    [SerializeField]
    private GameObject rightObject;

    private int mineingStatus;
    private int numberOfTypeOfBrokenBlock;

    private float differenceUp;
    private float differenceDown;
    private float differenceRight;
    private float differenceLeft;
    #endregion

    #region MovementData
    [Header("MovementData")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float JumpHeight;
    [SerializeField]
    private float JumpTime;
    private Vector3 JumpVector = new Vector3(0, 1, 0);
    private bool TouchNow = false;
    private bool Jump = false;
    private float CurrentRemainingJumpTime;
    private bool loseWasNot = true;

    private bool moveUp;
    private bool moveDown;
    private bool moveRight;
    private bool moveLeft;
    #endregion

    #region Extras
    [Header("Extra")]
    [SerializeField]
    private GameManager GameManager;
    [SerializeField]
    private ImprovementData[] ImprovementDatas;
    [SerializeField]
    private GoalController GoalController;

    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private Text heightNumber;
    private int height;

    public bl_Joystick Joystick;
    private bool JoystickWas;

    [SerializeField]
    private int numberOfAssets;
    [SerializeField]
    private ShowDataController[] assets;
    private bool store = false;

    [SerializeField]
    private GameObject storeScreen;

    private int screenWidthThreshold;
    private int screenWidth;
    #endregion

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;


    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private void Rewarded(int id)
    {
        if (id == 1)
        {
            loseWasNot = true;
            foodRestoreWasNot = false;
            loseScreen.SetActive(false);
            restOfFood = PlayerPrefs.GetInt("Food");
            restOfFoodShere = 1;
            FoodBar.fillAmount = 1;
            UpdateFoodNumber();
        }
        else if (id == 2)
        {
            loseWasNot = true;
            healthRestoreWasNot = false;
            loseScreen.SetActive(false);
            restOfHealth = PlayerPrefs.GetInt("Health");
            restOfHealthShere = 1;
            HealthBar.fillAmount = 1;
            UpdateHealthNumber();
        }

    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("openStoreInStart") == 1)
        {
            StoreOpened();
            PlayerPrefs.SetInt("openStoreInStart", 0);
            storeScreen.SetActive(true);
        }

        if (PlayerPrefs.GetInt("notStart") == 0)
        {
            PlayerPrefs.SetInt("GoalRequest", 1);
            PlayerPrefs.SetInt("notStart", 1);
        }
        YandexGame.LoadProgress();
        animatorOfStayPickaxe.SetBool("rightDigging", true);
        animatorOfRightPickaxe.SetBool("rightDigging", true);
        animatorOfLeftPickaxe.SetBool("leftDigging", true);
        for (int i = 0; i < 3; i++)
        {
            pickaxes[i].sprite = ImprovementDatas[0].goodSprite[PlayerPrefs.GetInt("PickaxeImprovement")];
        }

        PlayerPrefs.SetInt("Food", Mathf.RoundToInt(ImprovementDatas[1].power[PlayerPrefs.GetInt("FoodImprovement")]));
        PlayerPrefs.SetInt("Health", Mathf.RoundToInt(ImprovementDatas[3].power[PlayerPrefs.GetInt("HealthImprovement")]));
        restOfFood = PlayerPrefs.GetInt("Food");
        restOfHealth = PlayerPrefs.GetInt("Health");
        UpdateFoodNumber();
        UpdateHealthNumber();

        for (int i = 2; i < 8; i++)
        {
            numbersOfBlocksText[i].text = PlayerPrefs.GetInt(namesOfBlocks[i]).ToString();
        }
        CurrentRemainingJumpTime = JumpTime;

        UpdateAssets();
    }

    private void Awake()
    {
        screenWidthThreshold = 1024;

        screenWidth = Screen.width;

    }

    public void UpdateAssets()
    {
        for (int i = 0; i < numberOfAssets; i++)
        {
            assets[i].UpdateScore();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Item>() != null)
        {
            if (other.gameObject.GetComponent<Item>().numberOfItem > 1)
            {
                PlayerPrefs.SetInt(namesOfBlocks[other.gameObject.GetComponent<Item>().numberOfItem], PlayerPrefs.GetInt(namesOfBlocks[other.gameObject.GetComponent<Item>().numberOfItem]) + 1);
                PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 1);
                GoalController.CheckGoal();
                numbersOfBlocksText[other.gameObject.GetComponent<Item>().numberOfItem].text = PlayerPrefs.GetInt(namesOfBlocks[other.gameObject.GetComponent<Item>().numberOfItem]).ToString();
            }
            Destroy(other.gameObject);
        }
        else
        {
            TouchNow = true;

            if (other.gameObject.transform.position.y < transform.position.y)
            {
                differenceDown = transform.position.y - other.gameObject.transform.position.y;
                differenceUp = -1;
            }
            else
            {
                differenceUp = other.gameObject.transform.position.y - transform.position.y;
                differenceDown = -1;
            }
            if (other.gameObject.transform.position.x < transform.position.x)
            {
                differenceLeft = transform.position.x - other.gameObject.transform.position.x;
                differenceRight = -1;
            }
            else
            {
                differenceRight = other.gameObject.transform.position.x - transform.position.x;
                differenceLeft = -1;
            }


            if (Mathf.Max(differenceUp, differenceDown, differenceRight, differenceLeft) == differenceUp)
            {
                upperObject = other.gameObject;
            }
            else if (Mathf.Max(differenceUp, differenceDown, differenceRight, differenceLeft) == differenceDown)
            {
                lowerObject = other.gameObject;
            }
            else if (Mathf.Max(differenceUp, differenceDown, differenceRight, differenceLeft) == differenceLeft)
            {
                leftObject = other.gameObject;
            }
            else if (Mathf.Max(differenceUp, differenceDown, differenceRight, differenceLeft) == differenceRight)
            {
                rightObject = other.gameObject;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        rightPickaxe.SetActive(false);
        leftPickaxe.SetActive(false);
        TouchNow = false;
        if (other.gameObject == lowerObject)
        {
            if (lowerObject.GetComponent<Block>() != null)
                lowerObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
            lowerObject = null;
        }
        if (other.gameObject == upperObject)
        {
            if (upperObject.GetComponent<Block>() != null)
                upperObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
            upperObject = null;
        }
        if (other.gameObject == leftObject)
        {
            if (leftObject.GetComponent<Block>() != null)
                leftObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
            leftObject = null;
        }
        if (other.gameObject == rightObject)
        {
            if (rightObject.GetComponent<Block>() != null)
                rightObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
            rightObject = null;
        }
    }

    private void Update()
    {
        if (!moveDown && lowerObject)
        {
            if (lowerObject.GetComponent<Block>() != null)
                lowerObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
        }
        if (!moveUp && upperObject)
        {
            if (upperObject.GetComponent<Block>() != null)
                upperObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
        }
        if (!moveLeft && leftObject)
        {
            if (leftObject.GetComponent<Block>() != null)
                leftObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
        }
        if (!moveRight && rightObject)
        {
            if (rightObject.GetComponent<Block>() != null)
                rightObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            pickaxes[i].sprite = ImprovementDatas[0].goodSprite[PlayerPrefs.GetInt("PickaxeImprovement")];
        }

        restOfFoodShere = restOfFood / PlayerPrefs.GetInt("Food");
        restOfHealthShere = restOfHealth / PlayerPrefs.GetInt("Health");

        if (JoystickWas)
        {
            JoystickWas = false;
            moveUp = false;
            moveRight = false;
            moveLeft = false;
            moveDown = false;
        }
        if ((Mathf.Abs(Joystick.Horizontal) > 4) || (Mathf.Abs(Joystick.Vertical) > 4))
        {
            JoystickWas = true;
            if (Mathf.Abs(Joystick.Horizontal) > Mathf.Abs(Joystick.Vertical))
            {
                if (Joystick.Horizontal > 4)
                {
                    moveRight = true;
                }
                else
                {
                    moveLeft = true;
                }
            }
            else
            {
                if (Joystick.Vertical > 4)
                {
                    moveUp = true;
                }
                else
                {
                    moveDown = true;
                }
            }
        }
        

        height = Mathf.RoundToInt(transform.position.y / 2.56f) * (-1) + 1;
        if (height < 0) { height = 0; }
        heightNumber.text = height.ToString();
        
        if (restOfFood > 0)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                moveUp = true;
            }
            else if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && moveUp)
            {
                moveUp = false;
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (moveRight == false))
            {
                moveRight = true;
            }
            else if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) && moveRight)
            {
                moveRight = false;
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (moveLeft == false))
            {
                moveLeft = true;
            }
            else if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) && moveLeft)
            {
                moveLeft = false;
            }

            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (moveDown == false))
            {
                moveDown = true;
            }
            else if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) && moveDown)
            {
                moveDown = false;
            }
        }
        else
        {
            moveUp = false;
            moveRight = false;
            moveLeft = false;
            moveDown = false;
        }

        if (store)
        {
            camera.transform.position = new Vector3(transform.position.x + 2, transform.position.y + 1, -5);
        }
        else
        {
            camera.transform.position = new Vector3(transform.position.x, transform.position.y - 2.56f, -27.5f);
        }

        if (Jump)
        {
            if (CurrentRemainingJumpTime == 0 || CurrentRemainingJumpTime < 0)
            {
                CurrentRemainingJumpTime = JumpTime;
                Jump = false;
            }
            else
            {
                CurrentRemainingJumpTime -= Time.deltaTime;
                transform.position += Time.deltaTime * JumpVector * (JumpHeight / JumpTime);
            }
        }

        if (moveDown)
        {
            if (lowerObject)
            {
                mineing(lowerObject, "lower");
            }
            else
            {
                transform.position = transform.position - new Vector3(0, Time.deltaTime * speed);
            }
        }
        else if (moveLeft)
        {
            if (leftObject)
            {
                if (leftObject.GetComponent<Block>() != null)
                {
                    mineing(leftObject, "left");
                }
            }
            else
            {
                transform.position = transform.position - new Vector3(Time.deltaTime * speed, 0);
            }
        }
        else if (moveRight)
        {
            if (rightObject)
            {
                mineing(rightObject, "right");
            }
            else
            {
                transform.position = transform.position + new Vector3(Time.deltaTime * speed, 0);
            }
        }
        else if (moveUp)
        {
            if (upperObject)
            {
                mineing(upperObject, "upper");
            }
            else if (TouchNow)
            {
                Jump = true;
            }
        }
        else
        {
            if (lowerObject != null)
            {
                if (lowerObject.GetComponent<Block>() != null)
                {
                    lowerObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
                }
            }
            if (leftObject != null)
            {
                if (leftObject.GetComponent<Block>() != null)
                {
                    leftObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
                }
            }
            if (rightObject != null)
            {
                if (rightObject.GetComponent<Block>() != null)
                {
                    rightObject.GetComponent<Block>().blockMiningAudioSource.SetActive(false);
                }
            }
        }
    }

    public void UpgradeHappened(int goodNumber)
    {
        if (goodNumber == 1)
        {
            PlayerPrefs.SetInt("Food", Mathf.RoundToInt(ImprovementDatas[1].power[PlayerPrefs.GetInt("FoodImprovement")]));
            restOfFood = restOfFoodShere * PlayerPrefs.GetInt("Food");
            restOfFood = Mathf.Ceil(restOfFood);
            UpdateFoodNumber();
        }
        else if (goodNumber == 3)
        {
            PlayerPrefs.SetInt("Health", Mathf.RoundToInt(ImprovementDatas[3].power[PlayerPrefs.GetInt("HealthImprovement")]));
            restOfHealth = restOfHealthShere * PlayerPrefs.GetInt("Health");
            restOfHealth = Mathf.Ceil(restOfHealth);
            UpdateHealthNumber();
        }
    }

    private void mineing(GameObject Block, string side)
    {
        if (Block.GetComponent<Block>() != null)
        {
            Block.GetComponent<Block>().blockMiningAudioSource.SetActive(true);
            PlayerPrefs.SetFloat("Luck", Mathf.RoundToInt(ImprovementDatas[2].power[PlayerPrefs.GetInt("LuckImprovement")]));
            //print((100 * PlayerPrefs.GetFloat("Luck")).ToString() + " " + Random.Range(0, 100).ToString());
            numberOfTypeOfBrokenBlock = Block.GetComponent<Block>().numberOfTypeOfThisBlock;
            mineingStatus = Block.GetComponent<Block>().mineing(Time.deltaTime * ImprovementDatas[0].power[PlayerPrefs.GetInt("PickaxeImprovement")]);
            if (mineingStatus == 0)
            {
                if (side == "right")
                    rightPickaxe.SetActive(true);
                else
                    leftPickaxe.SetActive(true);
            }
            else
            {
                if (side == "right")
                    rightPickaxe.SetActive(false);
                else
                    leftPickaxe.SetActive(false);
                if ((PlayerPrefs.GetFloat("Luck") > Random.Range(0, 100)))
                {
                    PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 2);
                    PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 2);
                }
                else
                {
                    PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 1);
                    PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 1);
                }
                GoalController.CheckGoal();
                numbersOfBlocksText[numberOfTypeOfBrokenBlock].text = PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]).ToString();
                LoseOneUnitOfFood();
                if (restOfFood < 1)
                {
                    Lose();
                    if (foodRestoreWasNot)
                    {
                        RestoreFood.SetActive(true);
                    }
                }
            }
        }
    }

    private void Lose()
    {
        loseWasNot = false;
        RestoreFood.SetActive(false);
        RestoreHealth.SetActive(false);
        YandexGame.NewLeaderboardScores("numberOfMinedBlocks", PlayerPrefs.GetInt("Blocks"));
        loseScreen.SetActive(true);
        moveRight = false;
        moveLeft = false;
        moveDown = false;
    }

    #region FoodMethods
    private void LoseOneUnitOfFood()
    {
        restOfFood -= 1;
        UpdateFoodNumber();
    }

    public void UpdateFoodNumber()
    {
        FoodBar.fillAmount = restOfFood / PlayerPrefs.GetInt("Food");
        for (int i = 0; i < 2; i++)
        {
            foodNumbers[i].text = restOfFood.ToString() + "/" + PlayerPrefs.GetInt("Food").ToString();
        }
    }
    #endregion

    #region HealthMethods
    public void LoseOneUnitOfHealth(int damage)
    {
        restOfHealth -= damage;
        UpdateHealthNumber();

        if (restOfHealth < 1)
        {
            Lose();
            if (healthRestoreWasNot)
            {
                RestoreHealth.SetActive(true);
            }
        }
    }

    public void UpdateHealthNumber()
    {
        HealthBar.fillAmount = restOfHealth / PlayerPrefs.GetInt("Health");
        ArmorSetImage.sprite = ArmorSetSprites[PlayerPrefs.GetInt("HealthImprovement")];
        for (int i = 0; i < 2; i++)
        {
            healthNumbers[i].text = restOfHealth.ToString() + "/" + PlayerPrefs.GetInt("Health").ToString();
        }
    }
    #endregion

    #region interactionStore
    public void StoreOpened()
    {
        store = true;
    }

    public void StoreClosed()
    {
        store = false;
    }
    #endregion

    public void Store()
    {
        PlayerPrefs.SetInt("openStoreInStart", 1);
        GameManager.SwichSceneTo("Mine");
    }
}
