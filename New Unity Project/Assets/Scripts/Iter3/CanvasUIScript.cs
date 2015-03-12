using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CanvasUIScript :  MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{

	GlobalSettings gs;
	// Use this for initialization
	void Start () {
		gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnPointerEnter(PointerEventData data)
	{
		gs.mouseOverUI = true;
	}
	
	/* 
	 * Mouse exited this object and is not hovering it
	 */
	public void OnPointerExit(PointerEventData data)
	{
		gs.mouseOverUI = false;

	}
	
	/*
	 * Mouse clicked this object
	 */
	public void OnPointerClick(PointerEventData data)
	{

	}

}
