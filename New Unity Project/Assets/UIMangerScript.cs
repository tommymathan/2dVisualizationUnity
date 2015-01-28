using UnityEngine;
using System.Collections;

public class UIMangerScript : MonoBehaviour {

	public Animator dialog;
	private float mouseX;
	public GameObject panelOpen;
	public GameObject fileButton;
	public GameObject openBtn;
	public GameObject exitBtn;
	bool isPanelOpen = false;
	FileMouseOver fileBtn;
	FileMouseOver panel;
	FileMouseOver open;
	FileMouseOver exit;

	void Start()
	{
		fileBtn = fileButton.GetComponent<FileMouseOver> ();
		panel = panelOpen.GetComponent<FileMouseOver> ();
		open = openBtn.GetComponent<FileMouseOver> ();
		exit = exitBtn.GetComponent<FileMouseOver> ();
	}
	
	void Update()
	{

		bool mouseOverFile = fileBtn.getState();
		bool mouseOverPanel = panel.getState ();
		bool mouseOverOpenBtn = open.getState ();
		bool mouseOVerExitBtn = exit.getState ();


		if (mouseOverFile || mouseOverPanel || mouseOverOpenBtn || mouseOVerExitBtn) {

			animOpenFile(panelOpen,"OpenFileAnim", 1);
			animOpenFile(openBtn,"OpenFileAnim", 1);
			animOpenFile(exitBtn, "OpenFileAnim", 1);
		
			isPanelOpen = true;
		} 
		else if (isPanelOpen == true) {
			animOpenFile(panelOpen,"OpenFileAnim", -1);
			animOpenFile(openBtn,"OpenFileAnim", -1);
			animOpenFile(exitBtn,"OpenFileAnim", -1);

			isPanelOpen=  false;

		}

	}

	//set gameobject to do an Animation
	void animOpenFile(GameObject g, string anim, int speed)
	{
		g.animation [anim].speed = speed;
		g.animation.Play ();
	}
}
