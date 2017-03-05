using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PanelsController : MonoBehaviour {

	void Start () 
	{
		//app start
		MakeMainVisible ();

	}
	

	public	void GoBrowseMode () 
	{
//		Debug.Log ("BrowseMode clicked");
		MakeBrowseVisible ();
		MakeMainInvisible ();



	}

	public	void GoMainMode () 
	{
//		Debug.Log ("GoMainMode clicked");
		MakeMainVisible ();
		MakeBrowseInvisible ();
	}


	public void MakeBrowseVisible ()
	{
		GameObject.Find("BrowseModePanel").gameObject.GetComponent<Image>().enabled =true;
		GameObject.Find("BrowseModePanel").gameObject.GetComponent<ButtonsList> ().AddButtons();
	}

	public void MakeBrowseInvisible ()
	{
		GameObject.Find("BrowseModePanel").gameObject.GetComponent<Image>().enabled =false;
		GameObject.Find("BrowseModePanel").gameObject.GetComponent<ButtonsList> ().RemoveAllButtons();
		
	}


	public void MakeMainInvisible ()
	{
		GameObject.Find("MainMenuPanel").gameObject.GetComponent<Image>().enabled =false;
		GameObject.Find("ObjectPickerPanel").gameObject.GetComponent<Image>().enabled =false;
		GameObject.Find("CategoryPickerPanel").gameObject.GetComponent<Image>().enabled =false;
		GameObject.Find("CategoryPickerPanel").gameObject.GetComponent<ButtonsList> ().RemoveAllButtons();
		GameObject.Find("ObjectPickerPanel").gameObject.GetComponent<ButtonsList> ().RemoveAllButtons();
	}
	public void MakeMainVisible ()
	{
		GameObject.Find("MainMenuPanel").gameObject.GetComponent<Image>().enabled =true;
		GameObject.Find("ObjectPickerPanel").gameObject.GetComponent<Image>().enabled =true;
		GameObject.Find("CategoryPickerPanel").gameObject.GetComponent<Image>().enabled =true;
		GameObject.Find("CategoryPickerPanel").gameObject.GetComponent<ButtonsList> ().AddButtons();
		GameObject.Find("ObjectPickerPanel").gameObject.GetComponent<ButtonsList> ().AddButtons();
	}

}
