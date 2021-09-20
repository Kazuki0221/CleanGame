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
    [SerializeField]GameObject playerPrefs;
    [SerializeField]GameObject itemSpownArea;
    public CItem [] item;
    public int itemCount = 0;

    /// <summary>
    /// UI
    /// </summary>
    public static int  m_score = 0;//スコア
    [SerializeField] Text scoreText;

    //タイム
    float countTime = 10;
    [SerializeField] Color baseColor = new Color(0, 0, 0, 1);
    [SerializeField] Color dgColor = new Color(1, 0, 0, 1);
    [SerializeField]Text time_text;


    // Start is called before the first frame update
    void Start()
    {
        firstPlayerPos = new Vector3(0, -0.01000017f, 0);
        playerPrefs = CharaSelectManager.chara.character;
        var charaObj = Instantiate(playerPrefs, firstPlayerPos, playerPrefs.transform.rotation);
        charaObj.name = playerPrefs.name;
        AddScore(0);

    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Createitem", 2, 1);
        Timer();

    }

    void Createitem()
    {
        //ランダムの位置
        float x = Random.Range(itemSpownArea.transform.position.x - 5, itemSpownArea.transform.position.x + 5);
        float z = Random.Range(itemSpownArea.transform.position.z - 5, itemSpownArea.transform.position.z + 5);
        itemSpownPos = new Vector3(x, itemSpownArea.transform.position.y, z);
        if (itemCount < 10)
        {
            //アイテムをランダムな位置に生成(アイテムの種類もランダム)
            int kind = Random.Range(0, item.Length);//アイテムのランダム化

            var obj = Instantiate(item[kind].ItemObj, itemSpownPos, item[kind].ItemObj.transform.rotation) as GameObject;
            obj.name = item[kind].name;
            itemCount++;
        }
    }

    public void AddScore(int score)
    {
        m_score += score;
        scoreText.text ="Score：" + m_score.ToString();
    }

    public int GetScore()
    {
        return m_score;
    }

    void Timer()
    {
        countTime -= Time.deltaTime;
        time_text.text = countTime.ToString("F0");
        if (countTime <= 10)
        {
            time_text.color = dgColor;
        }
        else
        {
            time_text.color = baseColor;
        }
        if (countTime < 0)
        {
            Time.timeScale = 0;
        }
    }

    public float PushTime()
    {
        return countTime;
    }

}
