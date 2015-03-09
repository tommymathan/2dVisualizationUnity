using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class textValueButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	
	public Button button;
	public Text nameLabel;
	public int row;
	public bool isMouseOver = false;
	public GlobalSettings gs;

	void Start()
	{
		gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
	}

	public void OnPointerEnter(PointerEventData data)
	{

		gs.mouseOverUI = true;
		isMouseOver = true;
		foreach (GameObject go in gs.camList) {
			gs.hoverlist.add(go.transform.GetChild(row+1);
				}
	}
	
	/* 
	 * Mouse exited this object and is not hovering it
	 */
	public void OnPointerExit(PointerEventData data)
	{
		gs.mouseOverUI = false;
		isMouseOver = false;
		gs.hoverlist.clear ();
	}


}



