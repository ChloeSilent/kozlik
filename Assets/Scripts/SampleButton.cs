﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SampleButton : MonoBehaviour 
{
	public Button button;
	public Text nameLabel;
	public Image iconImage;
	private Item item;

	void Start () 
	{
		button.onClick.AddListener (HandleClick);
	}

	void Update()
	{
		if (Input.GetKeyUp("right"))
		{
			SwitchToNextSprite ();
		}

		if (Input.GetKeyUp("left"))
		{
			SwitchToPreviousSprite ();
		}
	}

	public void Setup (Item currentItem)
	{
		item = currentItem; //TODO это хуйня. переделать
		nameLabel.text = item.itemName;
		if (transform.parent.name == "CategoryPickerPanel") 
		{
			SetupCategorySprite (currentItem);
		}
		else
		{
			SetupNonCategorySprite (currentItem);
		}
	}

	public void SetupNonCategorySprite (Item currentItem)
	{
		if (item.spriteWasSelected == true) //если номер спрайта был выбран ранее
		{
			// то используем выбранный ранее
			iconImage.sprite = item.pictureList [item.savedNumberOfSelectedPicture];
		} 
		else // иначе выберем новый спрайт
		{
				int numberOfRandomPicture = Random.Range (0, item.pictureList.Capacity);	

				//пропишем выбранный спрайт в кнопку 
				iconImage.sprite = item.pictureList [numberOfRandomPicture];

				//пропишем выбранный спрайт в айтем 
				item.savedNumberOfSelectedPicture = numberOfRandomPicture;

				// и отметим, что у этого айтема спрайт уже выбран
				item.spriteWasSelected = true;
		}
	}

	public void SetupCategorySprite (Item currentItem)
	{
//				int numberOfRandomItemOfCategory = Random.Range (0, 11);
	}




	public void SwitchToNextSprite()
	{
		//если выбран последний спрайт
		if(item.savedNumberOfSelectedPicture==(item.pictureList.Count-1))
		{
			item.savedNumberOfSelectedPicture = 0;
		}
		else
		{
			item.savedNumberOfSelectedPicture++;			
		}
		Setup (item);
	}

	public void SwitchToPreviousSprite()
	{
		//если выбран первый спрайт
		if(item.savedNumberOfSelectedPicture==0)
		{
			item.savedNumberOfSelectedPicture = (item.pictureList.Count-1);
		}
		else
		{
			item.savedNumberOfSelectedPicture--;			
		}
		Setup (item);
	}

	//обработка нажатий
	public void HandleClick()
	{
		//храним ссылку на родителей
		PanelsController panelsController = GameObject.Find ("MainCanvas").GetComponent<PanelsController> (); //todo fix find
		ButtonsController currentButtonsController = this.transform.parent.GetComponent<ButtonsController> ();

		//выясняем кто parent нажатой кнопки, реагируем соответственно
		switch (transform.parent.name) 
		{
		case("CategoryPickerPanel"): 
			panelsController.ChangeObjectPickerItemListCategoryTo (item.Category);
			panelsController.RepopulateObjectPicker ();
			break;

		case("ObjectPickerPanel"): 
			if(item.gameObject.name == "Quiz") //12 кнопка quiz
			{
				panelsController.GoQuizMode (); 
			}
			else //1-11 кнопка
			{
				panelsController.ChangeBrowseModeItemListTo (item);
				panelsController.GoBrowseMode ();
			}
			break;

		case("BrowseModePanel"):
			panelsController.RefreshMainModeItemLists (item.Category);
			panelsController.GoMainMode(); 
			break;

		case("QuizButtonsPanel"):
			panelsController.CheckIfWinner(item, button.gameObject); 
			break;

		default:
			Debug.LogError ("new or unknown category" + transform.parent.name);
			break;
		}
	}

	public void MoveRedCrossForward()
	{
		this.transform.FindChild ("WrongImage").SetAsLastSibling ();
	}

	public void MoveRedCrossBackward()
	{
		this.transform.FindChild ("WrongImage").SetAsFirstSibling ();
	}
}
