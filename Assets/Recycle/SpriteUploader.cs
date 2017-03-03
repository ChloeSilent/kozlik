using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//этот скрипт наполняет кнопки контентом
public class SpriteUploader : MonoBehaviour {

	public GameObject myObject;
//	public Component myComponent;
	public Sprite newSprite;
//	public Sprite currentSprite;

	// Use this for initialization
	void Start () {
//		myObject= GameObject.Find ("Button1");
		//получаем ссылку на себя
		myObject= this.gameObject;


//		currentSprite = myObject.GetComponent<Image> ().sprite;

		newSprite = (Sprite) Resources.Load <Sprite> ("object001");

		myObject.GetComponent<Image> ().sprite = newSprite;
//		currentSprite = newSprite;


		//		Debug.Log ("myComponent.sprite is:" + myComponent.sprite);
		//		Debug.Log ("anotherComponent.sprite is:" + anotherComponent.sprite);
		//		Debug.Log ("myComponent.sprite is:" + myComponent.sprite);
		//		Debug.Log ("anotherComponent.sprite is:" + anotherComponent.sprite);
		//		Debug.Log ("myComponent.sprite is:" + myComponent.sprite);
				Debug.Log  ("myObject is:" + myObject);
		//		Debug.Log ("myComponent is:" + myComponent);
		//		Debug.Log ("anotherObject is:" + anotherObject);
		//		Debug.Log ("anotherComponent is:" + anotherComponent);
//				Debug.Log ("newSprite is:" + newSprite);
//				Debug.Log ("currentSprite is:" + currentSprite);

	}
	
	// Update is called once per frame
	void Update () {
//		currentSprite = newSprite;
//		Debug.Log ("NEW currentSprite is:" + currentSprite);
	}
}
