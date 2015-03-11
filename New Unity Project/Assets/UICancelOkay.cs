using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UICancelOkay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{

	public GameObject Obj1;
	public string animation;
	public bool isMouseOver;
	public int speed;
	GlobalSettings gs;
	
	
	/*
	 * Mouse is hovering this object
	 */
	public void OnPointerEnter(PointerEventData data)
	{
		isMouseOver = true;
	}
	
	/* 
	 * Mouse exited this object and is not hovering it
	 */
	public void OnPointerExit(PointerEventData data)
	{
		isMouseOver = false;
	}
	
	/*
	 * Mouse clicked this object
	 */
	public void OnPointerClick(PointerEventData data)
	{
		animObj (Obj1, speed);
	}
	
	/*
	 * Updates this script every frame
	 */
	void Start(){
		gs = GameObject.FindGameObjectWithTag("GlobalSettingsObject").GetComponent<GlobalSettings>();
	}
	
	void Update()
	{
	}
	
	/*
	 * Return a boolean value of whether the mouse is hovering this object or not
	 */
	public bool getState()
	{
		return isMouseOver;
	}
	
	/*
	 * set gameobject to do an Animation, speed identifies which direction either forward or backward
	 *  speed = 1;  plays animation forward
	 *  speed = -1; plays animation in reverse
	 * 	speed = 0; pauses animation
	 */
	void animObj(GameObject g, int speed)
	{
		g.GetComponent<Animation>() [animation].speed = speed;
		
		//No queue of animation, Plays it now
		g.GetComponent<Animation>() [animation].weight = 1;
		g.GetComponent<Animation>() [animation].time = 0;
		g.GetComponent<Animation>().Play ();

		
	}
	
	/* 
	 * Method to pause an animation
	 */
	void animObjPause(GameObject g)
	{
		g.GetComponent<Animation>() [animation].speed = 0;
		
	}
	
}

