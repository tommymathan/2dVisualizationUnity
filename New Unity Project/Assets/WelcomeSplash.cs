using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WelcomeSplash : MonoBehaviour, IPointerClickHandler {

	public GameObject splash;

	public void OnPointerClick(PointerEventData data)
	{

		splash.animation ["splashOut"].speed = 1;
		splash.animation.Play ();
	}

}
