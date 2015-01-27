using UnityEngine;
using System.Collections;

public class UIMangerScript : MonoBehaviour {

	public Animator dialog;
	private float mouseX;
	public GameObject panelOpen;
	public GameObject fileButton;

	public void OpenFilePanel()
	{
		//dialog.
		dialog.enabled = true;
		//dialog.SetBool ("isHidden", false);
		//dialog.SetBool ("isOpen",true);	

	}

	public void CloseFilePanel()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		Vector3 vec = dialog.bodyPosition;
		/*
		if (Input.mousePosition.x < 150) {
			dialog.SetBool ("isHidden", true);
			dialog.SetBool ("isOpen",false);	
		
		}
	*/
	}
	void Update()
	{
		//dialog.enabled = true;

		mouseX = Input.mousePosition.x;
		Transform panelObj = panelOpen.transform;
		Transform buttonObj = fileButton.transform;


		Debug.Log ("X: " + mouseX);
		Debug.Log ("X: " + mouseX);
		Debug.Log ("Dialog X: "  + panelObj.position.x);

		if (Input.mousePosition.x > panelObj.position.x) {
			//dialog.SetBool ("isHidden", true);
			//dialog.SetBool ("isOpen",false);	
			
		}


		//Vector3 pos = new Vector3 (obj.position);

	}
}
