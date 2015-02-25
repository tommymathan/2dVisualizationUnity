using UnityEngine;
using System.Collections;

public class GlobalSettings : MonoBehaviour {

	public float lineR;
	public float lineG;
	public float lineB;
	public float envR;
	public float envG;
	public float envB;

	// Use this for initialization
	void Start () {
		lineR = 0.6f;
		lineG = 0.6f;
		lineB = 0.6f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateLineRed(float f){
		lineR = f;
	}

	public void UpdateLineBlue(float f){
		lineB = f;
	}

	public void UpdateLineGreen(float f){
		lineG = f;
	}
}
