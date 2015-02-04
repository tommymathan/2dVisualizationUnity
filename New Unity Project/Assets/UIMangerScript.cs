using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class UIMangerScript : MonoBehaviour {

	//Location of file selected
	private string address;
	protected FileBrowser m_fileBrowser;

	/*
	 * Open button clicked and Open dialog pops up
	 */
	
	protected void OnGUI () {
				if (m_fileBrowser != null) {
						m_fileBrowser.OnGUI ();
				}
		}

	public void HelpButtonClicked(){
		Application.OpenURL(Environment.CurrentDirectory +@"\Documentation\index.html");


	}
	public void openButtonClicked()
	{
		m_fileBrowser = new FileBrowser(
			new Rect(100, 100, 600, 500),
			"Choose Text File",
			FileSelectedCallback
			);
		m_fileBrowser.SelectionPattern = "*.csv";
	}

	protected void FileSelectedCallback(string path) {
		m_fileBrowser = null;
		address = path;
		GameObject dataManagerObject = GameObject.FindGameObjectWithTag ("DataManagerTag");
		//TODO: CHange method name to reflect function
		dataManagerObject.GetComponent<DataManager> ().SetDataPath (path);
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
