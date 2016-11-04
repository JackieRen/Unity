using UnityEngine;

public class DropDownList : MonoBehaviour {

	public GameObject _itemList = null;

	void Start () 
	{
		_itemList.SetActive (false);
	}

	public void Show()
	{
		_itemList.SetActive (!_itemList.activeSelf);
	}
}
