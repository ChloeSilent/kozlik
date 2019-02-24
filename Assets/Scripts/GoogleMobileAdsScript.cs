using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleMobileAdsScript : MonoBehaviour
{
	private BannerView bannerView;

	public void Start()
	{
		#if UNITY_ANDROID
			string appId = "ca-app-pub-6835010280990929~1256467464";
		#elif UNITY_IPHONE
			string appId = "unexpected_platform";
		#else
			string appId = "unexpected_platform";
		#endif

		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);

		this.RequestBanner();
	}
	private void RequestBanner()
	{

		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //test unit
			//string adUnitId = "ca-app-pub-6835010280990929/3166514157"; //real unit
		#elif UNITY_IPHONE
			string adUnitId = "ca-app-pub-3940256099942544/2934735716";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
		.AddTestDevice("C5A74FCB9AB91559B78C81A174E2C2E6")
		.Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}
}


