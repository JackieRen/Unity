using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ***************************************************
/// *****contentRectTransform的对齐方式一定要顶端对齐*****
/// **********itemPrefab的对齐方式一定要顶端对齐**********
/// ***************************************************
/// </summary>
public class LoopScroll : UIBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform contentRectTransform;
    [SerializeField]
    private RectTransform itemPrefab;
    [SerializeField]
    private float offset = 0;
    [SerializeField]
    private int initItemNum = 10;
    [SerializeField]
    private int maxNum = 20;

    [System.NonSerialized]
    public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

    public Direction direction;
    public ListType listType;
    public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

    [System.Serializable]
    public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }

    public enum Direction
    {
        Vertical,
        Horizontal,
    }
    public enum ListType
    {
        Infinite,
        Limit,
    }
    
    private float m_diffPreFramePosition = 0;
    private float m_itemSpace = 0;
    private int m_currentItemNo = 0;
    private bool m_isMove = true;

    private float anchoredPosition
    {
        get
        {
            return direction == Direction.Vertical ? -contentRectTransform.anchoredPosition.y : contentRectTransform.anchoredPosition.x;
        }
    }

    private float itemSpace
    {
        get
        {
            if (itemPrefab != null)
            {
                m_itemSpace = direction == Direction.Vertical ? itemPrefab.sizeDelta.y + offset : itemPrefab.sizeDelta.x + offset;
            }
            return m_itemSpace;
        }
    }

    protected override void Start()
    {
        scrollRect.horizontal = direction == Direction.Horizontal;
        scrollRect.vertical = direction == Direction.Vertical;
        scrollRect.content = contentRectTransform;

        itemPrefab.gameObject.SetActive(false);

        if (listType == ListType.Infinite)
        {
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        } else {
            scrollRect.movementType = ScrollRect.MovementType.Elastic;
            var delta = contentRectTransform.sizeDelta;
            delta.y = itemSpace * maxNum;
            contentRectTransform.sizeDelta = delta;
        }

        for (int i = 0; i < initItemNum; i++)
        {
            var item = GameObject.Instantiate(itemPrefab) as RectTransform;
            item.SetParent(transform, false);
            item.name = i.ToString();
            item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -itemSpace * i) : new Vector2(itemSpace * i, 0);
            itemList.AddLast(item);
            item.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        //Item向下移动
        while (anchoredPosition - m_diffPreFramePosition < -itemSpace * 2)
        {
            if (m_currentItemNo >= maxNum - initItemNum && listType == ListType.Limit)
                return;
            m_diffPreFramePosition -= itemSpace;

            var item = itemList.First.Value;
            itemList.RemoveFirst();
            itemList.AddLast(item);

            var pos = itemSpace * initItemNum + itemSpace * m_currentItemNo;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

            onUpdateItem.Invoke(m_currentItemNo + initItemNum, item.gameObject);

            m_currentItemNo++;
        }
        //Item向上移动
        while (anchoredPosition - m_diffPreFramePosition > 0)
        {
            if (m_currentItemNo <= 0 && listType == ListType.Limit)
                return;
            m_diffPreFramePosition += itemSpace;

            var item = itemList.Last.Value;
            itemList.RemoveLast();
            itemList.AddFirst(item);

            m_currentItemNo--;

            var pos = itemSpace * m_currentItemNo;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
            onUpdateItem.Invoke(m_currentItemNo, item.gameObject);
        }
    }

}
