using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private ShotCountText shotCountText;
    [SerializeField] Sprite[] cannons;
    public Text ballsCountText;

    public GameObject[] block;

    public List<GameObject> levels;

    private GameObject level1;
    private GameObject level2;

    private MainMenu mainMenu;

    private Vector2 level1Pos;
    private Vector2 level2Pos;

    private GameObject cannon;

    public int shotCount;
    public int score;
    public int ballsCount;

    private GameObject ballsContainer;


    public bool isBought;
    private bool firstShot;
    private bool gameOverControl = false;
    void Awake()
    {
        Time.timeScale = 1;
        //Application.targetFrameRate = 60;
      
        cannon = GameObject.FindGameObjectWithTag("Cannon");
        shotCountText = GameObject.Find("ShotCountText").GetComponent<ShotCountText>();
        ballsCountText = GameObject.Find("BallCountText").GetComponent<Text>();
        ballsContainer = GameObject.Find("BallsContainer");
        mainMenu = GameObject.Find("MainCanvas").GetComponent<MainMenu>();
    }

    void Start()
    {
        mainMenu.levelNumber.text = PlayerPrefs.GetInt("Level").ToString();
        cannon.GetComponent<SpriteRenderer>().sprite = cannons[PlayerPrefs.GetInt("cannonIndex", 11)];
        ballsCount = PlayerPrefs.GetInt("BallsCount", 5);
        ballsCountText.text = ballsCount.ToString();

        Physics2D.gravity = new Vector2(0, -17);

        SpawnLevel();
        GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", true);

    }

    void Update() {
        if(ballsContainer.transform.childCount == 0 && shotCount == 4 && !gameOverControl )
        {
            gameOverControl = true;
            mainMenu.ShowAds.SetActive(true);
            Time.timeScale = 0;
        }

        if (shotCount > 2)
            firstShot = false;
        else
            firstShot = true;

        CheckBlocks();
        if (isBought == true)
        {
            StartCoroutine(SpawnNewCannon());
            isBought = false;
        }

        GameObject level = GameObject.FindGameObjectWithTag("Level");
        if (level.GetComponentsInChildren<Block>().Length == 0)
            Destroy(level);
    }

    private IEnumerator SpawnNewCannon()
    {
        cannon.GetComponent<SpriteRenderer>().sprite = Resources.Load(PlayerPrefs.GetString("cannon")) as Sprite;
        yield return null;
    }

    void SpawnNewLevel(int numberLevel1, int numberLevel2, int min, int max)
    {
        if(shotCount > 1)
            Camera.main.GetComponent<CameraTransitions>().RotateCameraToFront();

        shotCount = 1;

        level1Pos = new Vector2(3.5f, 1);
        level2Pos = new Vector2(3.5f, -3.4f);

        level1 = levels[numberLevel1];
        level2 = levels[numberLevel2];

        Instantiate(level1, level1Pos, Quaternion.identity);
        Instantiate(level2, level2Pos, Quaternion.identity);

        SetBlocksCount(min, max);

    }

    void SpawnLevel()
    {
      
        if(PlayerPrefs.GetInt("Level", 0) == 0)
            SpawnNewLevel(0, 17, 3, ball());

        if (PlayerPrefs.GetInt("Level") == 1)
            SpawnNewLevel(1, 18, 3, ball());

        if (PlayerPrefs.GetInt("Level") == 2)
            SpawnNewLevel(2, 19, 3, ball());

        if (PlayerPrefs.GetInt("Level") == 3)
            SpawnNewLevel(5, 20, 4, ball());

        if (PlayerPrefs.GetInt("Level") == 4)
            SpawnNewLevel(12, 28, 5, ball());

        if (PlayerPrefs.GetInt("Level") == 5)
            SpawnNewLevel(14, 29, 7, ball());

        if (PlayerPrefs.GetInt("Level") == 6)
            SpawnNewLevel(15, 30, 6, ball());

        if (PlayerPrefs.GetInt("Level") == 7)
            SpawnNewLevel(6, 20, 7, ball());

        if (PlayerPrefs.GetInt("Level") == 8)
            SpawnNewLevel(7, 22, 9, ball());

        if (PlayerPrefs.GetInt("Level") == 9)
            SpawnNewLevel(24, 34, 10, ball());

        if (PlayerPrefs.GetInt("Level") == 10)
            SpawnNewLevel(12, 24, 11, ball());

        if (PlayerPrefs.GetInt("Level") == 11)
            SpawnNewLevel(14, 25, 12, ball());

        if (PlayerPrefs.GetInt("Level") == 12)
            SpawnNewLevel(15, 30, 13, ball());

        if (PlayerPrefs.GetInt("Level") == 13)
            SpawnNewLevel(22, 30, 15, ball());

        if (PlayerPrefs.GetInt("Level") == 14)
            SpawnNewLevel(17, 31, 15, ball());

        if (PlayerPrefs.GetInt("Level") == 15)
            SpawnNewLevel(32, 18, 15, ball());

        if (PlayerPrefs.GetInt("Level") == 16)
            SpawnNewLevel(32, 19, 15, ball());

        if (PlayerPrefs.GetInt("Level") == 17)
            SpawnNewLevel(24, 34, 15, ball());

        if (PlayerPrefs.GetInt("Level") == 18)
            SpawnNewLevel(34, 14, 15, ball());

        if (PlayerPrefs.GetInt("Level") == 19)
            SpawnNewLevel(21, 30, 15, ball());


        if (PlayerPrefs.GetInt("Level") == 20)
            SpawnNewLevel(22, 30, ball() - 14, ball());

        if (PlayerPrefs.GetInt("Level") == 21)
            SpawnNewLevel(17, 31, ball() - 14, ball());

        if (PlayerPrefs.GetInt("Level") == 22)
            SpawnNewLevel(32, 18, ball() - 14, ball());

        if (PlayerPrefs.GetInt("Level") == 23)
            SpawnNewLevel(19, 32, ball() - 14, ball());

        if (PlayerPrefs.GetInt("Level") == 24)
            SpawnNewLevel(14, 34, ball() - 14, ball());

        if (PlayerPrefs.GetInt("Level") == 25)
            SpawnNewLevel(21, 34, ball() - 14, ball());

        if (PlayerPrefs.GetInt("Level") == 26)
            SpawnNewLevel(21, 30, ball() - 14, ball());


        if (PlayerPrefs.GetInt("Level") == 27)
            SpawnNewLevel(22, 30, ball() - 10, ball());

        if (PlayerPrefs.GetInt("Level") == 28)
            SpawnNewLevel(17, 31, ball() - 10, ball());

        if (PlayerPrefs.GetInt("Level") == 29)
            SpawnNewLevel(32, 18, ball() - 10, ball());

        if (PlayerPrefs.GetInt("Level") == 30)
            SpawnNewLevel(32, 19, ball() - 10, ball());

        if (PlayerPrefs.GetInt("Level") == 31)
            SpawnNewLevel(14, 34, ball() - 10, ball());

        if (PlayerPrefs.GetInt("Level") == 32)
            SpawnNewLevel(21, 24, ball() - 10, ball());

        if (PlayerPrefs.GetInt("Level") == 33)
            SpawnNewLevel(21, 30, ball() - 10, ball());


        if (PlayerPrefs.GetInt("Level") == 34)
            SpawnNewLevel(44, 30, ball() - 5, ball());

        if (PlayerPrefs.GetInt("Level") == 35)
            SpawnNewLevel(21, 24, ball() - 5, ball());

        if (PlayerPrefs.GetInt("Level") == 36)
            SpawnNewLevel(17, 31, ball() - 5, ball());

        if (PlayerPrefs.GetInt("Level") == 37)
            SpawnNewLevel(27, 33, ball() - 5, ball());

        if (PlayerPrefs.GetInt("Level") == 38)
            SpawnNewLevel(0, 35, ball() - 5, ball());

        if (PlayerPrefs.GetInt("Level") == 39)
            SpawnNewLevel(3, 35, ball() - 5, ball());

        if (PlayerPrefs.GetInt("Level") == 40)
            SpawnNewLevel(36, 24, ball()-5, ball());

    }

    void SetBlocksCount(int min, int max)
    {
        block = GameObject.FindGameObjectsWithTag("Block");

        for (int i = 0; i < block.Length; i++)
        {
            int count = Random.Range(min, max);
            block[i].GetComponent<Block>().SetStartingCount(count);
        }
    }

    public void CheckBlocks()
    {
        block = GameObject.FindGameObjectsWithTag("Block");
        
        if (block.Length < 1) {
            RemoveBalls();
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            mainMenu.levelNumber.text = PlayerPrefs.GetInt("Level").ToString();
            SpawnLevel();

            if (ballsCount >= PlayerPrefs.GetInt("BallsCount", 5))
                PlayerPrefs.SetInt("BallsCount", ballsCount);

            if (firstShot)
                score += 5;
            else
                score += 3;
        }
      
    }

    void RemoveBalls()
    {
        if (GameObject.FindGameObjectWithTag("Cannon").GetComponent<ShootScript>().shoot == true) return;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }

    }

    public void CheckShotCount()
    {
        if(shotCount == 1)
        {
            shotCountText.SetTopText("SHOT");
            shotCountText.SetBottomText("1/3");
            shotCountText.Flash();
        }
        if (shotCount == 2)
        {
            shotCountText.SetTopText("SHOT");
            shotCountText.SetBottomText("2/3");
            shotCountText.Flash();
        }
        if (shotCount == 3)
        {
            shotCountText.SetTopText("FINAL");
            shotCountText.SetBottomText("SHOT");
            shotCountText.Flash();
        }
    }

    int ball()
    {
        return PlayerPrefs.GetInt("BallsCount")+3;
    }
}
