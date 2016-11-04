using UnityEngine;
using UnityEngine.UI;

public class SendMessage : MonoBehaviour {

	public InputField _inputText = null;
	public GameObject _textPrefab = null;
	public Transform _parent = null;

	void Start()
	{
		_inputText.text = "";
	}

	public void SendMessageText()
	{
		if(!string.IsNullOrEmpty(_inputText.text)){
			string message = _inputText.text;
			GameObject ob = (GameObject)Instantiate(_textPrefab);
			ob.GetComponent<MessageText>().SetMessageText(message);
			ob.transform.SetParent(_parent);
			ob.transform.localScale = Vector3.one;
			ob.SetActive(true);
			float y = ob.GetComponent<Text>().preferredHeight;
			_parent.localPosition += new Vector3(0,y/2,0);
			_inputText.text = "";
		}
	}

}
