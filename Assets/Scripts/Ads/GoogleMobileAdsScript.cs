using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GoogleMobileAdsScript : MonoBehaviour
{
	private BannerView bannerView;
	public int delayBeforeAds;

	public string currentAppId;
	public string androidAppId;
	public string iosAppId;

	public string currentAdUnitId;
	public string testAdUnitIdForAndroid;
	public string realAdUnitIdForAndroid;
	public string testAdUnitIdForIos;
	public string realAdUnitIdForIos;

	public bool adsEnabled = false;
	public bool successfullyPurchased = false;

	public void Start()
	{
		InitializeAds();
	}
	
	void Update()
	{
		//если не куплен отказ от рекламы, и прошло некторое время со старта приложения, то включаем рекламу
		if (Time.realtimeSinceStartup > delayBeforeAds && successfullyPurchased == false)
		{
			adsEnabled = true;
		}
		else
		{
			adsEnabled = false;
		}
	}


	private void InitializeAds()
	{
		#if UNITY_EDITOR
			currentAppId = "no currentAppId  available for UNITY_EDITOR";
			currentAdUnitId = "no currentAdUnitId available for UNITY_EDITOR";
			Debug.Log("Google ads is not available for UNITY_EDITOR, so we will skip its initialization");
		#elif UNITY_ANDROID
			currentAppId = androidAppId;
			currentAdUnitId = realAdUnitIdForAndroid;
			Debug.Log("Start initilization of Google Ads at UNITY_ANDROID platform");
			MobileAds.Initialize(currentAppId);
		#elif UNITY_WEBGL
			currentAppId = "no currentAppId available for UNITY_WEBGL";
			currentAdUnitId = "no currentAdUnitId available for UNITY_WEBGL";
			Debug.Log("Google ads is not available for UNITY_WEBGL, so we will skip its initialization");
		#elif UNITY_IOS
			currentAppId = iosAppId;
			currentAdUnitId = testAdUnitIdForIos;
			Debug.Log("Start initilization of Google Ads at UNITY_IOS platform");
			MobileAds.Initialize(currentAppId);
		#else
			currentAppId = "WARNING! UNEXPECTED PLATFORM";
			currentAdUnitId = "WARNING! UNEXPECTED PLATFORM";
			Debug.Log("Google ads is not available for UNEXPECTED PLATFORM, so we will skip its initialization");
		#endif
	}

	private void RequesAndDisplayBanner()
	{
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(currentAdUnitId, AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
		.TagForChildDirectedTreatment(true)
		.Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}

	public void ShowAdsIfEnabled() 
	{
		if (adsEnabled == true)
		{
			this.RequesAndDisplayBanner();
		}
	}

	public void TemporarilyHideAds()
	{
		if (bannerView != null)
		{
			bannerView.Destroy();
		}
	}

	public void DisableAdsForever()
	{
		this.TemporarilyHideAds();
		successfullyPurchased = true;
		adsEnabled = false;
	}


}


