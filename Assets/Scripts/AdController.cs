using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
	public string bannerPlacement = "top_banner";

	//#if UNITY_ANDROID
		public const string gameId = "3038717";
		public bool testMode = true;
	//#endif

	//#if UNITY_EDITOR
	//	public const string gameId = "0000000";
	//	public bool testMode = true;
	//#endif

	//#if UNITY_WEBGL
	//		public const string gameId = "0000000";
	//		public bool testMode = true;
	//#endif


	void Start()
    {
		Monetization.Initialize(gameId, testMode);
		StartCoroutine(ShowBannerWhenReady());
	}

	IEnumerator ShowBannerWhenReady()
	{
		while (!Monetization.IsReady("top_banner"))
		{
			Debug.Log("waiting");
			yield return new WaitForSeconds(0.5f);
			Debug.Log("ready");
		}

		Debug.Log(Monetization.version.ToString()); 
	}
}
