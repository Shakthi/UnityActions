using UnityEngine;
using System.Collections;
using CC;

public class TestActions : MonoBehaviour 
{
	public  Transform movingObject;

	void Start ()
	{
		Action.Run(movingObject,new MoveBy(10 ,new Vector3(10,1,1)));
		Action.Run(movingObject,new RotateTo(5,new Vector3(90,0,0)));

	}

	void Update ()
	{

	}
}
