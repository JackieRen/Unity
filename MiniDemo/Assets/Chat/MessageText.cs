using UnityEngine;
using UnityEngine.UI;

public class MessageText : MonoBehaviour {

	public Text _text = null;

	public void SetMessageText(string message)
	{
		_text.text = message;
	}

}
