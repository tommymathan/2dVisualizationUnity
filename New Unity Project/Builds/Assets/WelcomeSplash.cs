using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WelcomeSplash : MonoBehaviour, IPointerClickHandler {

	public GameObject splash;

	void Start()
	{
		splash.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width * 2, Screen.height);
	}

	public void OnPointerClick(PointerEventData data)
	{

		splash.GetComponent<Animation>() ["splashOut"].speed = 1;
		splash.GetComponent<Animation>().Play ();
		Destroy (splash, 0.1f);
	}

}
