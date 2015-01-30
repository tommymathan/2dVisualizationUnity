using UnityEngine;
using System.Collections;
using UnityEditor;

public class UIMangerScript : MonoBehaviour {

	//Location of file selected
	private string address;

	/*
	 * Open button clicked and Open dialog pops up
	 */
	public void openButtonClicked()
	{
		address = EditorUtility.OpenFilePanel ("Open File", "", "csv");
	}

	/*
	 * Returns the address or location of the file selected
	 */
	public string getAddress()
	{
		return address;
	}

	/*
	 * Exits application when the exit button is clicked
	 */
	public void exitButtonClicked()
	{
		Application.Quit ();
	}

	//set gameobject to do an Animation
	void animOpenFile(GameObject g, string anim, int speed)
	{
		g.animation [anim].speed = speed;
		g.animation.Play ();
	}
}
