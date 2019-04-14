using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdStatusChecker : MonoBehaviour
{
	public Image PlatformApllicableForAdsToggleCheckmark;
	public Image AdsEnabledToggleCheckmark;
	public Image PurchasedToggleCheckmark;
	public Image AdTestDeviceToggleCheckmark;
	public Text AdDeviceIdText;

	public GoogleMobileAdsScript adController;

	void Update()
	{
		if(adController.adsEnabled)
		{
			AdsEnabledToggleCheckmark.enabled = true;
		}
		else
		{
			AdsEnabledToggleCheckmark.enabled = false;
		}

		if(adController.successfullyPurchased)
		{
			PurchasedToggleCheckmark.enabled = true;
		}
		else
		{
			PurchasedToggleCheckmark.enabled = false;
		}

		if(adController.goodPlatformForAds)
		{
			PlatformApllicableForAdsToggleCheckmark.enabled = true;
		}
		else
		{
			PlatformApllicableForAdsToggleCheckmark.enabled = false;
		}

		if(adController.thisDeviseIsATestDevice)
		{
			AdTestDeviceToggleCheckmark.enabled = true;
		}
		else
		{
			AdTestDeviceToggleCheckmark.enabled = false;
		}
	}
}
