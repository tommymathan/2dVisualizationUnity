using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FileMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{


	public bool isMouseOver = false;
	public GameObject Obj1;
	public GameObject Obj2;
	public GameObject obj3;
	//public GameObject obj4;
	public string animation;
	

	public void OnPointerEnter(PointerEventData data)
	{

		isMouseOver = true;
		animObj (Obj1, 1);
		animObj (Obj2, 1);
		animObj (obj3, 1);
		//animObj (obj4, 1);
		//Debug.Log ("OVer");
	}

	public void OnPointerExit(PointerEventData data)
	{

		//Debug.Log ("Exit");
		isMouseOver = false;
		animObj (Obj1, -1);
		animObj (Obj2, -1);
		animObj (obj3, -1);
		//animObj (obj4, -1);
	}
	public void OnPointerClick(PointerEventData data)
	{
		isMouseOver = true;
		animObj (Obj1, 1);
		animObj (Obj2, 1);
		animObj (obj3, 1);
		Debug.Log ("Button Clicked");
	}


	public bool getState()
	{
		return isMouseOver;
	}
	//set gameobject to do an Animation
	void animObj(GameObject g, int speed)
	{
		g.animation [animation].speed = speed;
		g.animation.Play (animation);
		//g.animation [animation].weight = 1;
		//g.animation [animation].time = 0;
		//g.animation.Play (PlayMode.StopAll);
		//g.animation.PlayQueued (animation);
		//g.animation.CrossFadeQueued (animation, 0.3f, QueueMode.PlayNow);

		//g.animation.Stop ();
		//g.animation.CrossFadeQueued (animation, 0.3f, QueueMode.CompleteOthers);

	}

}
