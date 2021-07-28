using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CSlotGrid : MonoBehaviour
{
    [SerializeField] private GameObject[] slotPrefab;
    private int slotNumber = 3;

    [SerializeField] CItem[] allItem = new CItem[3];//所持アイテム
    int index;

    PlayerControl pc;
    GameManager gm;

    void Start()
    {
        pc = GetComponent<PlayerControl>();
        gm = GetComponent<GameManager>();
    }

    private void Update()
    {
        //GetItem();
        for (int i = 0; i < slotNumber; i++)//取得したアイテムをインベントリに表示
        {

            CSlot slot = slotPrefab[i].GetComponent<CSlot>();

            if (i < allItem.Length)
            {
                slot.SetItem(allItem[i]);
            }
            else
            {
                slot.SetItem(null);
            }
        }
    }

    //public void GetItem()//アイテムスロットに取得したアイテムを保持
    //{
    //    for (int i = 0; i < slotNumber; i++)
    //    {
    //        if (gm.item[i].ItemName == pc.GetItemName())
    //        {
    //            allItem[index] = gm.item[i];
    //            if (index < 2)
    //            {
    //                index++;
    //            }
    //        }
    //    }
    //}


}