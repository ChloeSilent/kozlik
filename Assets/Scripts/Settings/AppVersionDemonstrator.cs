using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppVersionDemonstrator : MonoBehaviour
{
	public Text visibleText;

	void Start()
	{
		visibleText.text = Application.version;
	}
}
