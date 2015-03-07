using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WelcomeSplash : MonoBehaviour, IPointerClickHandler {

	public GameObject splash;

	public void OnPointerClick(PointerEventData data)
	{

		splash.GetComponent<Animation>() ["splashOut"].speed = 1;
		splash.GetComponent<Animation>().Play ();
		Destroy (splash, 0.1f);
	}

}
