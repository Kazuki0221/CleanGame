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
        if (Item != null)
        {
            ItemImage.sprite = item.ItemImage;
            ItemImage.color = Color.white;
        }
        else
        {
            ItemImage.sprite = null;
            ItemImage.color = new Color(0, 0, 0, 0);
        }
    }
}
