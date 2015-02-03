using UnityEngine;
using System.Collections;

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
