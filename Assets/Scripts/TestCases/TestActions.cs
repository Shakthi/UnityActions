using UnityEngine;
using System.Collections;
using CC;

public class TestActions : MonoBehaviour 
{
	public  Transform movingObject;

	void Start ()
	{
		Action.Run(movingObject,new MoveTo(10 ,new Vector3(10,10,10)));
	}

	void Update ()
	{

	}
}
