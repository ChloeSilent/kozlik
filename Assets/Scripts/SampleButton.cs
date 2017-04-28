using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SampleButton : MonoBehaviour 
{
	public Button button;
	public GameObject letterPanel;
	public GameObject namePanel;
	public Text nameLabel;
	public Text initialLetter;
	public Image iconImage;
	private Item item;

	void Start () 
	{
		button.onClick.AddListener (HandleClick);
	}

	void Update()
	{
		if (transform.parent.name == "BrowseModePanel") 
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
	}

	public void Setup (Item itemToSetupWith)
	{
		item = itemToSetupWith; 
		nameLabel.text = item.itemName;
		initialLetter.text = item.initialLetter.ToString ();
		SetupSprite ();
	}

	public void SetupSprite ()
	{
		if (item.spriteWasSelected == false) //если номер спрайта не был выбран ранее
		{
			// то выберем новый спрайт
			int numberOfRandomPicture = Random.Range (0, item.pictureList.Capacity);	
		
			// отметим флажком, что у этого айтема спрайт уже выбран
			item.spriteWasSelected = true;

			//пропишем выбранный спрайт в айтем 
			item.savedNumberOfSelectedPicture = numberOfRandomPicture;
		} 

		//пропишем выбранный спрайт в кнопку 
		iconImage.sprite = item.pictureList [item.savedNumberOfSelectedPicture];
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
		Setup (item); //TODO setup sprite    ?
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
		PanelsController panelsController = GameObject.Find ("MainCanvas").GetComponent<PanelsController> ();
		QuizController quizController = GameObject.Find ("QuizController").GetComponent<QuizController> ();
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
			panelsController.RandomizeObjectPickerSprites ();
			panelsController.GoMainMode(); 
			break;

		case("QuizButtonsPanel"):
			bool guessWasSuccessfull = quizController.CheckIfWinner (item); 
			if (guessWasSuccessfull == true) 
			{
				panelsController.ChangeBrowseModeItemListTo (item);
				panelsController.GoBrowseMode ();
			}
			else 
			{
				this.MoveRedCrossForward (); //TODO CHECK IF THIS needed
			}
			break;

		default:
			Debug.LogError ("new or unknown category" + transform.parent.name);
			break;
		}
	}

	public void TuneButtonForMain()
	{
		namePanel.SetActive (true);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
	}

	public void TuneButtonForBrowse()
	{
		namePanel.SetActive (true);
		letterPanel.SetActive (true);
		MoveRedCrossBackward ();
	}

	public void TuneButtonForQuiz()
	{
		namePanel.SetActive (false);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
	}

	public void MoveRedCrossForward()
	{
		this.transform.FindChild ("WrongImage").SetAsLastSibling (); //TODO no find
	}

	public void MoveRedCrossBackward()
	{
		this.transform.FindChild ("WrongImage").SetAsFirstSibling (); //TODO no find
	}
}
