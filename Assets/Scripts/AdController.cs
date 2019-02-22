using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdController : MonoBehaviour
{
	public int delayBeforeAds = 10;
	public string placementId = "top_banner";
	public bool adsEnabled = false;
	public Text adStatusText;
	public bool successfullyPurchased = false;

	#if UNITY_ANDROID
		public const string gameId = "3038717";
		public bool testMode = false;
	#elif UNITY_EDITOR
		public const string gameId = "0000000";
		public bool testMode = true;
	#elif UNITY_WEBGL
		public const string gameId = "0000000";
		public bool testMode = true;
	#endif

	void Start()
    {
		Advertisement.Initialize(gameId, testMode);
	}

	// если не куплен отказ от рекламы , и прошло некторое время со старта приложения, то включаем рекламу
	void Update()
	{
		if (Time.realtimeSinceStartup > delayBeforeAds && successfullyPurchased==false) 
		{
			adsEnabled = true;
		}
		else
		{
			adsEnabled = false;
		}
		TellAdStatus();
	}
	
	IEnumerator ShowBannerWhenReady()
	{
		while (!Advertisement.IsReady(placementId))
		{
			yield return new WaitForSeconds(0.5f);
		}

		Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER); 
		Advertisement.Banner.Show();
	}
	public void  HideAds()
	{
		Advertisement.Banner.Hide();
	}

	public void ShowAds()
	{
		if (adsEnabled==true)
		{
			StartCoroutine(ShowBannerWhenReady());
		}
	}

	private void TellAdStatus()
	{
		if (adsEnabled == true)
		{
			adStatusText.text = "ads enabled";
		}
		else
		{
			adStatusText.text = "ads disabled";
		}
	}

	public void DisableAdsForever()
	{
		Debug.Log("DisableAdsForever");
		successfullyPurchased = true;
		Advertisement.Banner.Hide();
	}
}
