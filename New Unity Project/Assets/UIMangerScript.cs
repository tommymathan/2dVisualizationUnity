using UnityEngine;
using System.Collections;

public class UIMangerScript : MonoBehaviour {


	//set gameobject to do an Animation
	void animOpenFile(GameObject g, string anim, int speed)
	{
		g.animation [anim].speed = speed;
		g.animation.Play ();
	}
}
