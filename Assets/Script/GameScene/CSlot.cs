using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSlot : MonoBehaviour
{
    private CItem Item;
    [SerializeField] Image ItemImage;

    public void SetItem(CItem item)
    {
        Item = item;
        if (item != null)
        {
            ItemImage.sprite = item.ItemImage;
            ItemImage.color = new Color(255, 255, 255, 255);
        }
        else
        {
            ItemImage.sprite = null;
            //ItemImage.color = new Color(255, 255, 255, 255);

        }
    }
}
