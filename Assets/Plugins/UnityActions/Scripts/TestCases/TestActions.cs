using UnityEngine;
using System.Collections;
using CC;

public class TestActions : MonoBehaviour 
{
	public  Transform movingObject;

	void Start ()
	{
//		Action.Run(movingObject,new MoveBy(10 ,new Vector3(10,1,1)));
//		Action.Run(movingObject,new RotateBy(5,new Vector3(10,0,0)));
//

		Action.Run(movingObject,new Sequence(new MoveBy(3 ,new Vector3(10,1,1)),
		                                     new RotateBy(5,new Vector3(180,0,0)),
		            						 new MoveBy(3 ,new Vector3(-10,1,1)),
		           								new RotateBy(3,new Vector3(0,60,0))

		          							 )
		           );


	}

	void Update ()
	{

	}
}
