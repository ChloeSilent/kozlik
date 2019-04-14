using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class AuthorInfoDemonstrator : MonoBehaviour
{
	public string cvUrl;
	public void OpenCvUrl()
	{
		Application.OpenURL(cvUrl);
	}


}
