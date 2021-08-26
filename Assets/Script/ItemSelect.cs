using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{

    [SerializeField] GameObject[] slot;
    [Range(0, 2)]
    int num = 0;
    [SerializeField]Image[] images;
    int temp;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        //インベントリ選択
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            temp = num;
            num++;
            if (num > 2) num = 0;

            transform.position = slot[num].transform.position;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            temp = num;
            num--;
            if (num < 0) num = 2;

            transform.position = slot[num].transform.position;
        }

    }

    //選択されたとき
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == slot[num].name)
        {
            images[num].color = new Color(1, 1, 1, 1);
        }
    }

    //選択されていないとき
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == slot[temp].name)
        {
            images[temp].color = new Color(205f/255f, 205f/255f, 205f/ 255f, 1);
        }
    }

    public int SelectNum()
    {
        return num;
    }
}
