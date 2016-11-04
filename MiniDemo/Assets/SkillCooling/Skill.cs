using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	private bool finished = true;

	public void DoSkill()
	{
		if(finished)
		{
			finished = false;
			StartCoroutine(SetSkillValue());
		}
	}

	private IEnumerator SetSkillValue()
	{
		for (int i = 0; i <= 100; ++i )
		{
			this.GetComponent<UnityEngine.UI.Image>().fillAmount = i*0.01f;
			yield return new WaitForSeconds(0.01f);
		}
		finished = true;
	}

}
