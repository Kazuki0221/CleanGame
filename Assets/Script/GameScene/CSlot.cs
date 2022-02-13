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
        }
    }

    public void ReleseItem(CItem item)
    {
        Item = item;
        ItemImage.color = new Color(204, 204, 204, 255);

    }
}
