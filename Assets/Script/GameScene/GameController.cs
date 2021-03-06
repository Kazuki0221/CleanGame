using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// 選ばれたプレイヤーを初期値に生成
    /// アイテムをランダムな位置に生成
    /// アイテムはランダムに選ばれる
    /// </summary>
    Vector3 firstPlayerPos;
    Vector3 itemSpownPos;
    [SerializeField] GameObject playerPrefs; //選択キャラ
    [SerializeField] GameObject itemSpownArea;//アイテム生成位置
    GameObject charaObj;

    public CItem[] item;
    public int itemCount = 0;

    public static string stageName;

    AudioSource source;

    /// <summary>
    /// UI
    /// </summary>
    public static int m_score = 0;//スコア
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip[] sound = new AudioClip[2];
    int tempScore;
    bool getScore = false;


    //タイム
    float countDown = 4;
    [SerializeField]float totalTime = 60;
    [SerializeField] Color baseColor = new Color(0, 0, 0, 1);
    [SerializeField] Color dgColor = new Color(1, 0, 0, 1);
    [SerializeField] Text time_text;
    [SerializeField] Text countDown_text;
    [SerializeField] GameObject timeUpText;
    [SerializeField] AudioClip stop;
    bool timeUp;
    int count;

    void Start()
    {
        FindObjectOfType<GameManager>().mode = GameMode.Game;

        firstPlayerPos = new Vector3(0, 1, 0);

        playerPrefs = FindObjectOfType<GameManager>().chara.Game;

        charaObj = Instantiate(playerPrefs, firstPlayerPos, playerPrefs.transform.rotation);
        charaObj.name = playerPrefs.name;
        AddScore(0);
        timeUpText.SetActive(false);

        source = GetComponent<AudioSource>();
        stageName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

    }

    void Update()
    {

        if (itemCount == 0)
        {
            Createitem();
        }

        //Score
        if (getScore)
        {
            if(tempScore > 0)
            {
                source.PlayOneShot(sound[0]);
            }
            else if(tempScore < 0)
            {
                source.PlayOneShot(sound[1]);
            }
            getScore = false;
        }

        //Timer
        if (countDown >= 0)
        {
            
            countDown -= Time.deltaTime;
            count = (int)countDown;
            countDown_text.text = count.ToString();

        }
        if(countDown < 0)
        {
            countDown_text.text = "";
            Timer();
        }
        if(totalTime < 0 && !timeUp)
        {
            source.PlayOneShot(stop);
            timeUpText.SetActive(true);
            timeUp = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            charaObj.transform.position = firstPlayerPos;
        }

    }

    void Createitem()
    {
        for (int iCount = 0; iCount < 10; iCount++)
        {
            //ランダムの位置
            float x = Random.Range(itemSpownArea.transform.position.x - 8, itemSpownArea.transform.position.x + 8);
            float z = Random.Range(itemSpownArea.transform.position.z - 8, itemSpownArea.transform.position.z + 8);
            itemSpownPos = new Vector3(x, itemSpownArea.transform.position.y, z);

            //アイテムをランダムな位置に生成(アイテムの種類もランダム)
            int kind = Random.Range(0, item.Length);//アイテムのランダム化

            var obj = Instantiate(item[kind].ItemObj, itemSpownPos, item[kind].ItemObj.transform.rotation) as GameObject;
            obj.name = item[kind].name;

            itemCount = iCount;
        }

    }

    public void AddScore(int score)
    {
        tempScore = score;
        m_score += score;
        scoreText.text = "Score：" + m_score.ToString() + " 点";
        getScore = true;

    }

    void Timer()
    {
        totalTime -= Time.deltaTime;
        time_text.text = totalTime.ToString("F0");
        if (totalTime <= 10)
        {
            time_text.color = dgColor;
        }
        else
        {
            time_text.color = baseColor;
        }
        
    }

    public float CountDown()
    {
        return countDown;
    }

    public float PushTime()
    {
        return totalTime;
    }

}
