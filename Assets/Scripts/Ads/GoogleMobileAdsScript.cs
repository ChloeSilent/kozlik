using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GoogleMobileAdsScript : MonoBehaviour
{
	private BannerView bannerView;
	public int delayBeforeAds;
	public string appId;
	public string realAdUnitId;
	public string testAdUnitId;
	public string currentAdUnitId;
	public bool adsEnabled = false;
	public bool successfullyPurchased = false;
	public Image disableAdsButtonImage;
	public Text disableAdsButtonText;

	public void Start()
	{
		this.SetupIds();
		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);
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

	public void SetupIds()
	{
#if UNITY_ANDROID
	//Do not change appId
	currentAdUnitId = realAdUnitId;
		Debug.Log("appId is" + appId);
#elif UNITY_EDITOR_WIN
	string appId = "Tha platform is editor";
	currentAdUnitId = testAdUnitId;
#elif UNITY_EDITOR
	string appId = "Tha platform is editor";
	currentAdUnitId = testAdUnitId;
#elif UNITY_IPHONE
	string appId = "The platform is iphone";
	currentAdUnitId = testAdUnitId;
#else
	string appId = "The platform is unexpected";
	currentAdUnitId = testAdUnitId;
#endif

	}

	private void RequesAndDisplayBanner()
	{
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(currentAdUnitId, AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
		.AddTestDevice("C5A74FCB9AB91559B78C81A174E2C2E6")
		.Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}

	public void ShowAdsIfEnabled() 
	{
		if (adsEnabled == true)
		{
			this.RequesAndDisplayBanner();
			this.UnhideButton();
		}
	}

	public void TemporarilyHideAdsAndButton()
	{
		//bannerView.Destroy();
		this.HideButton();
	}

	public void DisableAdsForever()
	{
		successfullyPurchased = true;
		bannerView.Destroy();
		this.HideButton();
	}

	private void HideButton()
	{
		disableAdsButtonImage.enabled = false;
		disableAdsButtonText.enabled = false;
	}

	private void UnhideButton()
	{
		disableAdsButtonImage.enabled = true;
		disableAdsButtonText.enabled = true;
	}
}


