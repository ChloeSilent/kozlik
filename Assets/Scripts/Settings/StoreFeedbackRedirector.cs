using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFeedbackRedirector : MonoBehaviour
{
	public string commonStoreUrl;
	public string androidStoreUrl;
	public string currentStoreUrl;

    public void OpenStoreLink()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
			currentStoreUrl = androidStoreUrl;
        }
		else
		{
			currentStoreUrl = commonStoreUrl;
		}

		Application.OpenURL(currentStoreUrl);
    }
}
