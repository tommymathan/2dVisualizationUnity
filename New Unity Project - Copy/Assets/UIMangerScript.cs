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
	public GUISkin fileBrowserSkin;
	public Texture2D folderTexture;
	public Texture2D fileTexture;

	/*
	 * Open button clicked and Open dialog pops up
	 */
	
	protected void OnGUI () {

		GUI.skin = fileBrowserSkin;

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
			new Rect(200, 20, Screen.width/2, Screen.height-20),
			"Choose Text File",
			FileSelectedCallback
			);
		m_fileBrowser.SelectionPattern = "*.csv";
		m_fileBrowser.setBrowserSkin (fileBrowserSkin);
		m_fileBrowser.DirectoryImage = folderTexture;
		m_fileBrowser.FileImage = fileTexture;

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
		g.GetComponent<Animation>() [anim].speed = speed;
		g.GetComponent<Animation>().Play ();
	}
}
