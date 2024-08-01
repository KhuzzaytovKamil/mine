using UnityEngine;
using UnityEngine.UI;
using YG;

public class Noob : MonoBehaviour
{
    [SerializeField]
    private Task task;

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
    private PickaxeData pickaxeData;
    [SerializeField]
    private SpriteRenderer[] pickaxes;

    private float restOfEnergy;
    [SerializeField]
    private Image ProgressBar;

    private int mineingStatus;
    private int numberOfTypeOfBrokenBlock;

    private float differenceDown;
    private float differenceRight;
    private float differenceLeft;

    private bool moveDown;
    private bool moveRight;
    private bool moveLeft;

    [SerializeField]
    private string[] namesOfBlocks;
    [SerializeField]
    private Text[] numbersOfBlocksText;

    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private GameObject vectoryScreen;

    [SerializeField]
    private GameObject lowerObject;
    private GameObject leftObject;
    private GameObject rightObject;

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

    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private Text heightNumber;
    private int height;

    public bl_Joystick Joystick;
    private bool JoystickWas;
    [SerializeField]
    private GameObject DesktopHelper;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;


    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;


    private void Rewarded(int id)
    {
        if (id == 1)
        {
            loseScreen.SetActive(false);
            restOfEnergy = PlayerPrefs.GetInt("Energy");
            ProgressBar.fillAmount = 1;
        }

    }

    private void Start()
    {
        YandexGame.LoadProgress();

        if (PlayerPrefs.GetInt("Energy") == 0)
        {
            PlayerPrefs.SetInt("Energy", 20);
        }
        animatorOfStayPickaxe.SetBool("rightDigging", true);
        animatorOfRightPickaxe.SetBool("rightDigging", true);
        animatorOfLeftPickaxe.SetBool("leftDigging", true);
        for (int i = 0; i < 3; i++)
        {
            pickaxes[i].sprite = pickaxeData.pickaxeSprite[PlayerPrefs.GetInt("numberOfPickaxe")];
        }

        restOfEnergy = PlayerPrefs.GetInt("Energy");

        for (int i = 2; i < 9; i++)
        {
            numbersOfBlocksText[i].text = PlayerPrefs.GetInt(namesOfBlocks[i]).ToString();
        }
        CurrentRemainingJumpTime = JumpTime;

        if (SystemInfo.deviceType != DeviceType.Desktop)    {DesktopHelper.SetActive(false);}
        }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "VictoryTrigger")
        {
            vectoryScreen.SetActive(true);
        }
        else
        {
            TouchNow = true;

            if (other.gameObject.transform.position.y < transform.position.y)
            {
                differenceDown = transform.position.y - other.gameObject.transform.position.y;
            }
            else
            {
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


            if (Mathf.Max(differenceDown, differenceRight, differenceLeft) == differenceDown)
            {
                lowerObject = other.gameObject;
            }
            else if (Mathf.Max(differenceDown, differenceRight, differenceLeft) == differenceLeft)
            {
                leftObject = other.gameObject;
            }
            else if (Mathf.Max(differenceDown, differenceRight, differenceLeft) == differenceRight)
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
            lowerObject = null;
        }
        else if (other.gameObject == leftObject)
        {
            leftObject = null;
        }
        else if (other.gameObject == rightObject)
        {
            rightObject = null;
        }
    }

    public void JUMP()
    {
        if (TouchNow)
        {
            Jump = true;
        }
        DesktopHelper.SetActive(false);
    }

    public void MoveRight(bool status)
    {
        moveRight = status;
        DesktopHelper.SetActive(false);
    }

    public void MoveLeft(bool status)
    {
        moveLeft = status;
        DesktopHelper.SetActive(false);
    }

    public void MoveDown(bool status)
    {
        moveDown = status;
        DesktopHelper.SetActive(false);
    }

    private void Update()
    {
        if (JoystickWas)
        {
            JoystickWas = false;
            moveRight = false;
            moveLeft = false;
            moveDown = false;
        }
        if ((Mathf.Abs(Joystick.Horizontal) > 4) || (Mathf.Abs(Joystick.Vertical) > 4))
        {
            DesktopHelper.SetActive(false);
            JoystickWas = true;
            moveRight = false;
            moveLeft = false;
            moveDown = false;
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
                    Jump = true;
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

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && TouchNow)
            {
                Jump = true;
                DesktopHelper.SetActive(false);
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (moveRight == false))
            {
                moveRight = true;
                DesktopHelper.SetActive(false);
            }
            else if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) && moveRight)
            {
                moveRight = false;
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (moveLeft == false))
            {
                moveLeft = true;
                DesktopHelper.SetActive(false);
            }
            else if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) && moveLeft)
            {
                moveLeft = false;
            }

            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (moveDown == false))
            {
                moveDown = true;
                DesktopHelper.SetActive(false);
            }
            else if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) && moveDown)
            {
                moveDown = false;
            }
        }

        if (transform.position.x < (-137))
        {
            camera.transform.position = new Vector3(-137, camera.transform.position.y, -27.5f);
        }
        else if (transform.position.x > (3.3f))
        {
            camera.transform.position = new Vector3(3.3f, camera.transform.position.y, -27.5f);
        }
        else
        {
            camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, -27.5f);
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

                if (lowerObject.GetComponent<Block>() != null)
                {
                    numberOfTypeOfBrokenBlock = lowerObject.GetComponent<Block>().numberOfTypeOfThisBlock;
                    mineingStatus = lowerObject.GetComponent<Block>().mineing(Time.deltaTime * pickaxeData.powerOfPickaxe[PlayerPrefs.GetInt("numberOfPickaxe")]);
                    if (mineingStatus == 0)
                    {
                        rightPickaxe.SetActive(true);
                        leftPickaxe.SetActive(false);
                    }
                    else
                    {
                        rightPickaxe.SetActive(false);
                        if ((PlayerPrefs.GetFloat("Luck") * 100 > Random.Range(0, 100)))
                        {
                            PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 2);
                            PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 2);
                        }
                        else
                        {
                            PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 1);
                            PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 1);
                        }
                        //task.UpdateStatus();
                        numbersOfBlocksText[numberOfTypeOfBrokenBlock].text = PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]).ToString();
                        restOfEnergy -= 1;
                        ProgressBar.fillAmount = restOfEnergy / PlayerPrefs.GetInt("Energy");
                        if (restOfEnergy == 0)
                        {
                            YandexGame.NewLeaderboardScores("numberOfMinedBlocks", PlayerPrefs.GetInt("Blocks"));
                            loseScreen.SetActive(true);
                            moveRight = false;
                            moveLeft = false;
                            moveDown = false;
                        }
                    }
                }
                else
                {
                    mineingStatus = lowerObject.GetComponent<Border>().mineing(Time.deltaTime * pickaxeData.powerOfPickaxe[PlayerPrefs.GetInt("numberOfPickaxe")]);
                    if (mineingStatus == 0)
                    {
                        leftPickaxe.SetActive(true);
                        rightPickaxe.SetActive(false);
                    }
                    else
                    {
                        leftPickaxe.SetActive(false);
                        //task.UpdateStatus();
                        restOfEnergy -= 1;
                        ProgressBar.fillAmount = restOfEnergy / PlayerPrefs.GetInt("Energy");
                        if (restOfEnergy == 0)
                        {
                            YandexGame.NewLeaderboardScores("numberOfMinedBlocks", PlayerPrefs.GetInt("Blocks"));
                            loseScreen.SetActive(true);
                            moveRight = false;
                            moveLeft = false;
                            moveDown = false;
                        }
                    }
                }
            }
            else
            {
                transform.position = transform.position - new Vector3(0, Time.deltaTime * speed);
            }
        }

        if (moveLeft)
        {
            if (leftObject)
            {
                if (leftObject.GetComponent<Block>() != null)
                {
                    numberOfTypeOfBrokenBlock = leftObject.GetComponent<Block>().numberOfTypeOfThisBlock;
                    mineingStatus = leftObject.GetComponent<Block>().mineing(Time.deltaTime * pickaxeData.powerOfPickaxe[PlayerPrefs.GetInt("numberOfPickaxe")]);
                    if (mineingStatus == 0)
                    {
                        leftPickaxe.SetActive(true);
                        rightPickaxe.SetActive(false);
                    }
                    else
                    {
                        leftPickaxe.SetActive(false);
                        if ((PlayerPrefs.GetFloat("Luck") * 100 > Random.Range(0, 100)))
                        {
                            PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 2);
                            PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 2);
                        }
                        else
                        {
                            PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 1);
                            PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 1);
                        }
                        //task.UpdateStatus();
                        numbersOfBlocksText[numberOfTypeOfBrokenBlock].text = PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]).ToString();
                        restOfEnergy -= 1;
                        ProgressBar.fillAmount = restOfEnergy / PlayerPrefs.GetInt("Energy");
                        if (restOfEnergy == 0)
                        {
                            YandexGame.NewLeaderboardScores("numberOfMinedBlocks", PlayerPrefs.GetInt("Blocks"));
                            loseScreen.SetActive(true);
                            moveRight = false;
                            moveLeft = false;
                            moveDown = false;
                        }
                    }
                }
                else
                {
                    mineingStatus = leftObject.GetComponent<Border>().mineing(Time.deltaTime * pickaxeData.powerOfPickaxe[PlayerPrefs.GetInt("numberOfPickaxe")]);
                    if (mineingStatus == 0)
                    {
                        leftPickaxe.SetActive(true);
                        rightPickaxe.SetActive(false);
                    }
                    else
                    {
                        leftPickaxe.SetActive(false);
                        //task.UpdateStatus();
                        restOfEnergy -= 1;
                        ProgressBar.fillAmount = restOfEnergy / PlayerPrefs.GetInt("Energy");
                        if (restOfEnergy == 0)
                        {
                            YandexGame.NewLeaderboardScores("numberOfMinedBlocks", PlayerPrefs.GetInt("Blocks"));
                            loseScreen.SetActive(true);
                            moveRight = false;
                            moveLeft = false;
                            moveDown = false;
                        }
                    }
                }
            }
            else
            {
                transform.position = transform.position - new Vector3(Time.deltaTime * speed, 0);
            }
        }

        if (moveRight)
        {
            if (rightObject)
            {
                if (rightObject.GetComponent<Block>() != null)
                {
                    numberOfTypeOfBrokenBlock = rightObject.GetComponent<Block>().numberOfTypeOfThisBlock;
                    mineingStatus = rightObject.GetComponent<Block>().mineing(Time.deltaTime * pickaxeData.powerOfPickaxe[PlayerPrefs.GetInt("numberOfPickaxe")]);
                    if (mineingStatus == 0)
                    {
                        rightPickaxe.SetActive(true);
                    }
                    else
                    {
                        rightPickaxe.SetActive(false);
                        if ((PlayerPrefs.GetFloat("Luck") * 100 > Random.Range(0, 100)))
                        {
                            PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 2);
                            PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 2);
                        }
                        else
                        {
                            PlayerPrefs.SetInt(namesOfBlocks[numberOfTypeOfBrokenBlock], PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]) + 1);
                            PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + 1);
                        }
                        //task.UpdateStatus();
                        numbersOfBlocksText[numberOfTypeOfBrokenBlock].text = PlayerPrefs.GetInt(namesOfBlocks[numberOfTypeOfBrokenBlock]).ToString();
                        restOfEnergy -= 1;
                        ProgressBar.fillAmount = restOfEnergy / PlayerPrefs.GetInt("Energy");
                        if (restOfEnergy == 0)
                        {
                            YandexGame.NewLeaderboardScores("numberOfMinedBlocks", PlayerPrefs.GetInt("Blocks"));
                            loseScreen.SetActive(true);
                            moveRight = false;
                            moveLeft = false;
                            moveDown = false;
                        }
                    }
                }
                else
                {
                    mineingStatus = rightObject.GetComponent<Border>().mineing(Time.deltaTime * pickaxeData.powerOfPickaxe[PlayerPrefs.GetInt("numberOfPickaxe")]);
                    if (mineingStatus == 0)
                    {
                        leftPickaxe.SetActive(true);
                        rightPickaxe.SetActive(false);
                    }
                    else
                    {
                        leftPickaxe.SetActive(false);
                        //task.UpdateStatus();
                        restOfEnergy -= 1;
                        ProgressBar.fillAmount = restOfEnergy / PlayerPrefs.GetInt("Energy");
                        if (restOfEnergy == 0)
                        {
                            YandexGame.NewLeaderboardScores("numberOfMinedBlocks", PlayerPrefs.GetInt("Blocks"));
                            loseScreen.SetActive(true);
                            moveRight = false;
                            moveLeft = false;
                            moveDown = false;
                        }
                    }
                }
            }
            else
            {
                transform.position = transform.position + new Vector3(Time.deltaTime * speed, 0);
            }
        }
    }
}
