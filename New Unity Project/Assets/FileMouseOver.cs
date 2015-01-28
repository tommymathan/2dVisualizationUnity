using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FileMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{


	public bool isMouseOver = false;
	

	public void OnPointerEnter(PointerEventData data)
	{
		isMouseOver = true;
		//Debug.Log ("OVer");
	}

	public void OnPointerExit(PointerEventData data)
	{
		//Debug.Log ("Exit");
		isMouseOver = false;
	}

	public bool getState()
	{
		return isMouseOver;
	}

}
