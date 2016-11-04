using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
	
	public Transform _parent = null;
	public Transform _substituteParent = null;
	
	private GameObject substitute_ = null;
	private Vector3 position_ = Vector3.zero;
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
		substitute_ = new GameObject();
		_substituteParent = this.transform.parent;
		substitute_.transform.SetParent(_substituteParent);
		LayoutElement le = substitute_.AddComponent<LayoutElement>();
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.flexibleHeight = 0;
		le.flexibleWidth = 0;
		substitute_.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		this.transform.SetParent(_parent.parent);
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		position_ = Camera.main.ScreenToWorldPoint(eventData.position);
		position_.z = 0f;
		this.transform.position = position_;
		for(int i = 0; i < _parent.childCount; ++i)
		{
			if(this.transform.position.x < _parent.GetChild(i).position.x)
			{
				substitute_.transform.SetSiblingIndex(i);
				break;
			}
		}
		substitute_.transform.SetParent(_substituteParent);
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
		this.transform.SetParent(_parent.transform);
		this.transform.SetSiblingIndex(substitute_.transform.GetSiblingIndex());
		if(substitute_ != null){
			DestroyObject(substitute_);
		}
	}

}
