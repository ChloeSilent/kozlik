using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseModeController : MonoBehaviour
{
	public ButtonsController objectPickerButtonsController;
	public ButtonsController categoryPickerButtonsController;
	public GoogleMobileAdsScript adController;

	public void EnterChoiseMode(Item item)
    {
		objectPickerButtonsController.FilterObjectPickerItemListTo(item);
		categoryPickerButtonsController.FilterCategoryPickerItemList();

		RandomizeObjectPickerSprites();
		RandomizeCategoryPickerSprites();

		objectPickerButtonsController.AddButtonsFromCurrentItemList();
		categoryPickerButtonsController.AddButtonsFromCurrentItemList();

		objectPickerButtonsController.TuneButtonsForMain();
		categoryPickerButtonsController.TuneButtonsForMain();

		adController.ShowAdsIfEnabled();
	}

	public void LeaveChoiseMode()
	{
		objectPickerButtonsController.RemoveAllButtons();
		categoryPickerButtonsController.RemoveAllButtons();

		objectPickerButtonsController.currentItemList.Clear();
		categoryPickerButtonsController.currentItemList.Clear();

		adController.TemporarilyHideAds();
	}

	// сбрасываем флаг, который отмечает, что для объекта  ранее  был выбран спрайт.
	// это приведет к рандомизации при следующем возвращении кнопки из пула
	public void RandomizeCategoryPickerSprites()
	{
		for (int i = 0; i < categoryPickerButtonsController.currentItemList.Count; i++)
		{
			categoryPickerButtonsController.currentItemList[i].spriteWasSelected = false;
		}
	}

	// сбрасываем флаг, который отмечает, что для объекта  ранее  был выбран спрайт.
	// это приведет к рандомизации при следующем возвращении кнопки из пула
	public void RandomizeObjectPickerSprites()
	{
		for (int i = 0; i < objectPickerButtonsController.currentItemList.Count; i++)
		{
			objectPickerButtonsController.currentItemList[i].spriteWasSelected = false;
		}
	}


}
