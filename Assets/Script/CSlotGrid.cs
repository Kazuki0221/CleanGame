using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CSlotGrid : MonoBehaviour
{
    [SerializeField] private GameObject[] slotPrefab;
    private int slotNumber = 3;

    [SerializeField] List<CItem> allItem = new List<CItem>();//所持アイテム

    [SerializeField] List<CItem> itemList = new List<CItem>();
    [SerializeField] int kindItemCount = 3;

    int index = 0;

    PlayerControl playerControl;
    

    void Start()
    {
    }

    private void Update()
    {
        if (playerControl == null) {
            playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
            return;
        }

        if (index > 2) index = 0;
        if (playerControl.Catch())
        {
            if (playerControl.itemName != null)//取得したアイテム名をデータから検索
            {
                Item(playerControl.itemName);
                playerControl.itemName = null;
            }
        }
        if (playerControl.Release())
        {
            allItem[index] = null;
        }
        
        for (int i = 0; i < slotNumber; i++)//取得したアイテムをインベントリに表示
        {

            CSlot slot = slotPrefab[i].GetComponent<CSlot>();

            if (i < 3)
            {
                slot.SetItem(allItem[i]);
            }
            //else
            //{
            //    slot.SetItem(null);
            //}
        }
    }

    public void Item(string itemName)//アイテムスロットに取得したアイテムを保持
    {
        for(int i = 0; i < kindItemCount; i++)
        {
            if(itemName == itemList[i].name)
            {
                if(index < slotNumber) 
                { 
                    allItem[index] = itemList[i];
                    index++;
                    break;
                }
            }
        }
    }
    public CItem ReleseItem(int num)//離すアイテム
    {
        index = num;
        return allItem[index];
    }

}