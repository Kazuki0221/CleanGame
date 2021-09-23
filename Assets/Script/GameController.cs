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
    [SerializeField] GameObject playerPrefs;
    [SerializeField] GameObject itemSpownArea;
    public CItem[] item;
    public int itemCount = 0;
    bool itemZero = true;

    /// <summary>
    /// UI
    /// </summary>
    public static int m_score = 0;//スコア
    [SerializeField] Text scoreText;

    //タイム
    [SerializeField]float countTime = 10;
    [SerializeField] Color baseColor = new Color(0, 0, 0, 1);
    [SerializeField] Color dgColor = new Color(1, 0, 0, 1);
    [SerializeField] Text time_text;


    // Start is called before the first frame update
    void Start()
    {
        firstPlayerPos = new Vector3(0, 1, 0);
        playerPrefs = CharaSelectManager.chara.character;
        var charaObj = Instantiate(playerPrefs, firstPlayerPos, playerPrefs.transform.rotation);
        charaObj.name = playerPrefs.name;
        AddScore(0);

    }

    // Update is called once per frame
    void Update()
    {
        if(itemCount < 10 && itemZero)
        {
            Createitem();
            itemCount++;
        }

        if (itemCount == 0)
        {
            itemZero = true;
        }
        else if (itemCount == 10)
        {
            itemZero = false;
        }

        Timer();

    }

    void Createitem()
    {
        //ランダムの位置
        float x = Random.Range(itemSpownArea.transform.position.x - 5, itemSpownArea.transform.position.x + 5);
        float z = Random.Range(itemSpownArea.transform.position.z - 5, itemSpownArea.transform.position.z + 5);
        itemSpownPos = new Vector3(x, itemSpownArea.transform.position.y, z);

        //アイテムをランダムな位置に生成(アイテムの種類もランダム)
        int kind = Random.Range(0, item.Length);//アイテムのランダム化

        var obj = Instantiate(item[kind].ItemObj, itemSpownPos, item[kind].ItemObj.transform.rotation) as GameObject;
        obj.name = item[kind].name;

    }

    public void AddScore(int score)
    {
        m_score += score;
        scoreText.text = "Score：" + m_score.ToString() + " 点";
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
        
    }

    public float PushTime()
    {
        return countTime;
    }

}
