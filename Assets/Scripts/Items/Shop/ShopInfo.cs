using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;


public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("Stats Fields")]
    public TMP_Text[] statsTexts;

    private RectTransform infoPanelRect;

    private RectTransform parentRect;
    private Canvas canvas;

    private void Awake()
    {
        infoPanelRect = infoPanel.GetComponent<RectTransform>();

        parentRect = infoPanelRect.parent as RectTransform;
        canvas = infoPanel.GetComponentInParent<Canvas>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;

        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();
        if(itemSO.currentHealth > 0)
        {
            stats.Add($"Health: {itemSO.currentHealth.ToString()}");
        }

        if (itemSO.damage > 0)
        {
            stats.Add($"Damage: {itemSO.damage.ToString()}");
        }

        if (itemSO.speed > 0)
        {
            stats.Add($"Speed: {itemSO.speed.ToString()}");
        }

        if (itemSO.duration > 0)
        {
            stats.Add($"Duration: {itemSO.duration.ToString()}");
        }

        if(stats.Count <= 0)
        {
            return;
        }

        for(int i = 0; i < statsTexts.Length; i++)
        {
            if(i < stats.Count)
            {
                statsTexts[i].text = stats[i];
                statsTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statsTexts[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;

        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    //public void FollowMouse()
    //{
    //    Vector3 mousePosition = Input.mousePosition;
    //    Vector3 offset = new Vector3(10, -10, 0);

    //    infoPanelRect.anchoredPosition = mousePosition + offset;
    //}

    public void FollowMouse()
    {
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect, Input.mousePosition, cam, out Vector2 localPoint))
            return;

        // localPoint tính theo pivot của cha, còn anchoredPosition tính theo tâm (anchor 0.5, 0.5)
        Vector2 pos = localPoint - parentRect.rect.center + new Vector2(10, -10);

        // Kẹp popup trong khung cha
        Rect bounds = parentRect.rect;
        Vector2 size = infoPanelRect.rect.size;
        Vector2 pivot = infoPanelRect.pivot;
        Vector2 center = bounds.center;

        float minX = bounds.xMin - center.x + size.x * pivot.x;
        float maxX = bounds.xMax - center.x - size.x * (1 - pivot.x);
        float minY = bounds.yMin - center.y + size.y * pivot.y;
        float maxY = bounds.yMax - center.y - size.y * (1 - pivot.y);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        infoPanelRect.anchoredPosition = pos;
    }
}
