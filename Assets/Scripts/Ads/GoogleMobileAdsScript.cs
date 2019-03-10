using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GoogleMobileAdsScript : MonoBehaviour
{
	public string currentAppId;
	public string androidAppId;
	public string iosAppId;

	public string currentAdUnitId;
	public string testAdUnitIdForAndroid;
	public string realAdUnitIdForAndroid;
	public string testAdUnitIdForIos;
	public string realAdUnitIdForIos;

	private AdRequest request;
	private BannerView bannerView;

	public bool adsEnabled = false;
	public bool successfullyPurchased = false;
	public int delayBeforeAds;

	public void Start()
	{
		SetupIds();
		CreateBannerView();
		CreateAdRequest();
		LoadRequestedBannerToView();
	}

	private void SetupIds()
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

	private void CreateBannerView()
	{
		// Create a 320x50 banner view at the top of the screen.
			bannerView = new BannerView(currentAdUnitId, AdSize.Banner, AdPosition.Top);
			Debug.Log("ads: CreateBannerView called");
	}

	private void CreateAdRequest()
	{
			request = new AdRequest.Builder()
			.AddTestDevice("31642AF81FC95C9F60ED1975BD0207B6") //mart
			.AddTestDevice("4AC2811559563C8348D03C6D9DE61104") //son
			.TagForChildDirectedTreatment(true)
			.Build();
			Debug.Log("ads: CreateAdRequest called");
	}

	private void LoadRequestedBannerToView()
	{
			bannerView.LoadAd(request);

			Debug.Log("ads: LoadRequestedBannerToView called");
	}

	public void ShowAdsIfEnabled() 
	{
		// if delay from appstart has already passed AND user didnt acquire disabling ads, 
		// then set  adsEnabled flag to true
		if (Time.realtimeSinceStartup > delayBeforeAds && successfullyPurchased == false)
		{
			adsEnabled = true;
			Debug.Log("ads: adsEnabled set to true");
		}
		else
		{
			adsEnabled = false;
			Debug.Log("ads: adsEnabled set to false");
		}

		// show ads if flag allows, or do nothing if doesnt
		if (adsEnabled == true)
		{
			bannerView.Show();
		}
	}

	public void TemporarilyHideAds()
	{
		if (bannerView != null)
		{
			bannerView.Hide();
			Debug.Log(" ads: TemporarilyHideAds called");
		}
	}

	public void DisableAdsForever()
	{
		if (bannerView != null)
		{
			bannerView.Destroy();
		}
		successfullyPurchased = true;
		adsEnabled = false;
		Debug.Log(" ads: DisableAdsForever called");
	}
}


