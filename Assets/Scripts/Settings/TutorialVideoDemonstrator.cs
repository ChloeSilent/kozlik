using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVideoDemonstrator : MonoBehaviour
{
	public string tutorialUrl;
 
    public void OpenTutorialLink()
    {
		 Application.OpenURL(tutorialUrl);
	}
}
