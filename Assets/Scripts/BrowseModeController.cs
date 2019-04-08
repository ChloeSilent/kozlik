using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseModeController : MonoBehaviour
{
	public ButtonsController browseModeButtonsController;

	public void EnterBrowseMode(Item item)
	{
		browseModeButtonsController.currentItemList.Add(item);
		browseModeButtonsController.AddButtonsFromCurrentItemList();
		browseModeButtonsController.TuneButtonsForBrowse();
	}

	public void LeaveBrowseMode(Item item)
	{
		browseModeButtonsController.RemoveAllButtons();
		browseModeButtonsController.currentItemList.Clear();
	}

}
