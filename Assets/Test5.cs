using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test5 : MonoBehaviour {


	public GameObject myObject;
	public SpriteRenderer myComponent;

	public GameObject anotherObject;
	public SpriteRenderer anotherComponent;

	public Sprite newSprite;

	// Use this for initialization
	void Start () {



		myObject= GameObject.Find ("Test5");
		myComponent = myObject.GetComponent<SpriteRenderer> ();


		anotherObject= GameObject.Find ("Test6");
		anotherComponent = anotherObject.GetComponent<SpriteRenderer> ();


		newSprite = (Sprite) Resources.Load <Sprite> ("object001");

				myComponent.sprite = newSprite;

	



//		Debug.Log ("myComponent.sprite is:" + myComponent.sprite);
//		Debug.Log ("anotherComponent.sprite is:" + anotherComponent.sprite);
//		Debug.Log ("myComponent.sprite is:" + myComponent.sprite);
//		Debug.Log ("anotherComponent.sprite is:" + anotherComponent.sprite);
		Debug.Log ("myComponent.sprite is:" + myComponent.sprite);
//		Debug.Log ("myObject is:" + myObject);
//		Debug.Log ("myComponent is:" + myComponent);
//		Debug.Log ("anotherObject is:" + anotherObject);
//		Debug.Log ("anotherComponent is:" + anotherComponent);
		Debug.Log ("newSprite is:" + newSprite);
		
	}
	
	// Update is called once per frame
	void Update () {
//		myComponent.sprite = anotherComponent.sprite;
	}
}
