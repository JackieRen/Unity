using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemLIst : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler {

	public GameObject _item = null;
	public Transform _parent = null;
	public Scrollbar _bar = null;

	private List<int> numList_ = new List<int>();
	private int mulriple_ = 0; 
	private int initialNum_ = 2; 
	private bool Create_ = true;

	void Start()
	{
		init();
	}

	private void init()
	{
		Evaluation();
		if(numList_.Count > 10)
		{
			for(int i = 0; i < 20; ++i)
			{
				CreateItem(i);
			}
			mulriple_ = (int)(numList_.Count / 10);
		}else{
			for(int i = 0; i < numList_.Count; ++i)
			{
				CreateItem(i);
				Create_ = false;
			}
		}
	}

	private void Evaluation()
	{
		for(int i = 0; i < 52; ++i)
		{
			numList_.Add(i);
		}
	}

	private void CreateItem(int i)
	{
		GameObject ob = (GameObject)Instantiate(_item);
		ob.transform.SetParent(_parent);
		ob.transform.localScale = Vector3.one;
		ob.name = i.ToString();
	}

	private void AddItem()
	{
		if(_bar.value < 0.5 && Create_ && initialNum_ <= mulriple_)
		{
			for(int j = initialNum_ * 10; j < (initialNum_ + 1) * 10; ++j)
			{
				if(j < numList_.Count)
				{
					CreateItem(j);
				}else{
					Create_ = false;
				}
			}
			++initialNum_;
		}
	}
	
	public void OnPointerDown(PointerEventData e)
	{
		AddItem();
	}

	public void OnPointerEnter(PointerEventData e)
	{
		AddItem();
	}

}
