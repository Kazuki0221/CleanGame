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
    //[SerializeField]GameObject playerPrefs;
    [SerializeField]GameObject itemSpownArea;
    public CItem [] item;
    public int itemCount = 0;

    //PlayerControl pc;

    int m_score = 0;

    [SerializeField] Text scoreText;

    
    // Start is called before the first frame update
    void Start()
    {
        firstPlayerPos = new Vector3(0, -0.01000017f, 0);
        //Instantiate(PlayerPrefs, FirstPlayerPos, PlayerPrefs.transform.rotation);
        //pc = GetComponent<PlayerControl>();
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Createitem", 2, 1);
        //itemCount = pc.itemCount();
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

}
