using UnityEngine;
using System.Collections;

public class DebugUI : MonoBehaviour {
	public GameObject parserObject;
	public DataParserScript parserScript;
	private static string title;
	private static string parsingText;

	private static Rect debugMenuRect;
	private static Rect parsingFieldRect;
	private static Rect parseButtonRect;

	void Start () {
		//dimensions for debugMenu
		debugMenuRect = new Rect((Screen.width/2)-300,Screen.height-220,600,200);
		//dimensions for parsingField depend on dimensions of debugMenu
		parsingFieldRect = new Rect(debugMenuRect.xMin, debugMenuRect.yMax-100f, debugMenuRect.width, 100f);
		//parse Button depends on dimensions of debug menu
		parseButtonRect = new Rect(debugMenuRect.xMin,debugMenuRect.yMax,debugMenuRect.width,20);

		title = "Debug Menu";
		parsingText = "0,0,1,3,3,2";

		//Linkup to the parserObject's parserScript
		parserScript = parserObject.GetComponent<DataParserScript>();
		if(parserScript != null){
			Debug.Log("Hookup made <" + parserObject + ">");
		}
	}
	
	void Update () {
	
	}

	void OnGUI(){
		//Debug Menu box
		GUI.Box (debugMenuRect, title);

		//Text Entry field box
		parsingText = GUI.TextField (parsingFieldRect, parsingText, 25);

		//parsing button box
		if(GUI.Button (parseButtonRect, "Parse Entry")){
			Debug.Log("Kicking Off Parse...\nEntered Data is: " + parsingText);
			Debug.Log (parserScript.ParseThis(parsingText));
		}
	}
}
