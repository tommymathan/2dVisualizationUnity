using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FileMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{


	public bool isMouseOver = false;
	public GameObject Obj1;
	public GameObject Obj2;
	public GameObject obj3;
	public string animation;
	public bool isPlaying;
	int counter = 0;
	bool mouseOverObj1 = false;
	bool mouseOverObj2 = false;
	bool mouseOverObj3 = false;
	public float animationTimer;
	float time;
	float timeClick;


	
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
		//mouse clicked set flag
		counter = 1;

		//play each objects animation
		isMouseOver = true;
		animObj (Obj1, 1);
		animObj (Obj2, 1);
		animObj (obj3, 1);

		//Time click plus animation time for checking when to change animation
		timeClick = time + animationTimer;
	}

	/*
	 * Updates this script every frame
	 */
	void Update()
	{
		//Get other objects in this script scripts
		FileMouseOver obj1Mouse = Obj1.GetComponent<FileMouseOver> ();
		FileMouseOver obj2Mouse = Obj2.GetComponent<FileMouseOver> ();
		FileMouseOver obj3Mouse = obj3.GetComponent<FileMouseOver> ();

		//get their mouse mouse over state
		mouseOverObj1 = obj1Mouse.getState ();
		mouseOverObj2 = obj2Mouse.getState ();
		mouseOverObj3 = obj3Mouse.getState ();

		/*
		 * If mouse has been clicked and it still hovers the objects in this script then dont play the reverse animation
		 * 	else the mouse isn't over any object and its time is up then play the reverse animation
		 */
		if(counter ==1 && mouseOverObj1 == false && mouseOverObj2 == false && mouseOverObj3 == false && timeClick < time){
			animObj (Obj1, -1);
			animObj (Obj2, -1);
			animObj (obj3, -1);
			
			//reset mouseclick flag
			counter = 0;
		}



		//Get current time
		time = Time.time;
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
		//g.animation.Play (animation);

		//No queue of animation, Plays it now
		g.GetComponent<Animation>() [animation].weight = 1;
		g.GetComponent<Animation>() [animation].time = 0;
		g.GetComponent<Animation>().Play ();
		//g.animation.Play (PlayMode.StopAll);
		//g.animation.PlayQueued (animation);
		//g.animation.CrossFadeQueued (animation, 0.3f, QueueMode.PlayNow);

		//g.animation.Stop ();
		//g.animation.CrossFadeQueued (animation, 0.3f, QueueMode.CompleteOthers);

	}

	/* 
	 * Method to pause an animation
	 */
	void animObjPause(GameObject g)
	{
		g.GetComponent<Animation>() [animation].speed = 0;

	}

}
