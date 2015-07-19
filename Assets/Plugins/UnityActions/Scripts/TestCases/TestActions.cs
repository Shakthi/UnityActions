using UnityEngine;
using System.Collections;
using CC;

public class TestActions : MonoBehaviour 
{
	public  Transform movingObject;


	IEnumerator anANimtion()
	{
		var  anActor = new Actor(movingObject);
		yield return anActor.MoveBy(10,new Vector3(10,10,10));
		yield return anActor.MoveTo(4,new Vector3(10,10,10));
	}

	void Start ()
	{
//		Action.Run(movingObject,new MoveBy(10 ,new Vector3(10,1,1)));
//		Action.Run(movingObject,new RotateBy(5,new Vector3(10,0,0)));
//


//		Action.Run(movingObject,new Spawn(new MoveBy(10 ,new Vector3(10,1,1)),new MoveBy(10,new Vector3(0,30,0)),new RotateBy(10,new Vector3(0,180,0))));
//		//		Action.Run(movingObject,new RotateBy(5,new Vector3(10,0,0)));
//		//

//		Sequence q= new Sequence(new MoveBy(3 ,new Vector3(10,1,1)),
//		                                     new RotateBy(5,new Vector3(180,0,0)),
//		            						 new MoveBy(3 ,new Vector3(-10,1,1)),
//		           								new RotateBy(3,new Vector3(0,60,0))
//
//		                         );
//		           
//
//
//		Action.Run(movingObject, new Repeat(q,2));



		//Action.Run(movingObject,new RepeatForever(new RotateBy(0.5f,new Vector3(10,0,0))));




		StartCoroutine(anANimtion);

	}

	void Update ()
	{

	}
}
