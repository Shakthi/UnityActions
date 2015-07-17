using UnityEngine;
using System.Collections;
using CC;

public class TestActions : MonoBehaviour 
{
	public  Transform movingObject;

	void Start ()
	{
		Action.Run(movingObject,new MoveBy(10 ,new Vector3(10,1,1)));
		Action.Run(movingObject,new RotateBy(5,new Vector3(10,0,0)));

	}

	void Update ()
	{

	}
}
