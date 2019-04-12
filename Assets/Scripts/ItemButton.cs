using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour 
{
	public Button button;
	public GameObject letterPanel;
	public GameObject namePanel;
	public Text nameLabel;
	public Text initialLetter;
	public Image iconImage;
	public Item item;
	private bool noClicksWasMadeYet;
	public AudioClip letterClip;
	public AudioClip nameClip;
	private Mask maskComponent;

	private void Awake()
	{
		// через инспектор нельзя назначить значение больше трёхсот, поэтому назначим тут
		initialLetter.fontSize = 710;
		maskComponent = transform.GetComponent<Mask>();
	}

	void Update()
	{
		// на десктопе можно стрелками листать спрайты
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
		button.gameObject.name = item.itemName; 
		AudioSource[] allAudioSourcesOfButton = item.GetComponents<AudioSource>(); // TODO а точно нужен отдельный компонент AudioSource ? может сделать звуковой файл просто полем в  Item
		if (allAudioSourcesOfButton.Length>0)
		{
			nameClip = allAudioSourcesOfButton[0].clip;
		}
		if (allAudioSourcesOfButton.Length > 1)
		{
			letterClip = allAudioSourcesOfButton[1].clip;
		}
	}

	public void SetupSprite ()
	{
		if (item.spriteWasSelected == false) //если номер спрайта не был выбран ранее
		{
			// то выберем новый спрайт
			int numberOfRandomPicture = Random.Range (0, item.pictureList.Count);	
		
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
		// Если выбран последний спрайт, то переключаем на первый, т.к. листаем по кругу
		if(item.savedNumberOfSelectedPicture==(item.pictureList.Count-1))
		{
			item.savedNumberOfSelectedPicture = 0;
		}
        // Для всех остальных кроме последнего переключимся на следующий, увеличив номер на единицу
		else
		{
			item.savedNumberOfSelectedPicture++;			
		}
		Setup (item); 
	}

	public void SwitchToPreviousSprite()
	{
        //если выбран первый спрайт, то переключим на последний, т.к. листаем по кругу
        if (item.savedNumberOfSelectedPicture==0)
		{
			item.savedNumberOfSelectedPicture = (item.pictureList.Count-1);
		}
        // Для всех остальных кроме первого перенключимся на предыдущий, уменьшив номер на единицу
        else
        {
			item.savedNumberOfSelectedPicture--;			
		}
		Setup (item);
	}

	public void HandleClick()
	{
		// Lets find out who is the parent of clicked button
		switch (transform.parent.name) 
		{
		case("CategoryPickerPanel"): 
			EventManager.TriggerEvent ("CategoryPickerButtonClicked", this);
			break;

		case("ObjectPickerPanel"): 
			if(item.gameObject.name == "Quiz")
			{
				EventManager.TriggerEvent ("QuizButtonClicked", this);	
			}
			// if it is not quiz button that was clicked, then this is regular item button
			else
			{
				EventManager.TriggerEvent ("ObjectPickerButtonClicked", this);
			}
			break;

		case("BrowseModePanel"):
				if (noClicksWasMadeYet==true) 
				{
					EventManager.TriggerEvent ("FirstClickInBrowseMode", this);
					noClicksWasMadeYet = false;
				}
				else
				{
					EventManager.TriggerEvent ("SecondClickInBrowseMode", this);
				}
				break;

		case("QuizButtonsPanel"):
			EventManager.TriggerEvent ("QuizVariantClicked", this);
			break;

		default:
			Debug.LogError ("Ошибка. У кнопки неверный или несуществующий parent" + transform.parent.name);
			break;
		}
	}

    public void TuneButtonForPool()
    {
        namePanel.SetActive(false);
        letterPanel.SetActive(false);
        MoveRedCrossBackward();
    }

    public void TuneButtonForMain()
	{
		maskComponent.enabled = true;
		namePanel.SetActive (false);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
		MakeButtonNotFullscreen();
	}

	public void TuneButtonForBrowse()
	{
		maskComponent.enabled = false;
		namePanel.SetActive (false);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
		noClicksWasMadeYet = true;
		MakeButtonFullscreen();
	}

	public void TuneButtonForQuiz()
	{
		maskComponent.enabled = true;
		namePanel.SetActive (false);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
		MakeButtonNotFullscreen();
	}

	public void MoveRedCrossForward()
	{
		transform.Find ("WrongImage").SetAsLastSibling (); 
	}

	public void MoveRedCrossBackward()
	{
		transform.Find ("WrongImage").SetAsFirstSibling (); 
	}

	public void DisplayInitialLetterFullscreeen()
	{
		namePanel.SetActive(true);
		letterPanel.SetActive(true);
	}

	// Resize picture to screen resolution
	private void MakeButtonFullscreen ()
	{
		AspectRatioFitter arf = GetComponent<AspectRatioFitter>();
		arf.enabled = true;
	}

	// Disable ratio control witn AspectRatioFitter. 
	// Further managenent will be made by layout group component in parent panel.
	private void MakeButtonNotFullscreen()
	{
		AspectRatioFitter arf = GetComponent<AspectRatioFitter>();
		arf.enabled = false;
	}
}
