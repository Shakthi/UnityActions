using UnityEngine;
using System.Collections;

namespace CC 
{
	/** 
@brief Instant actions are immediate actions. They don't have a duration like
the CCIntervalAction actions.
*/ 

	public abstract class ActionInstant :  FiniteTimeAction 
	{
		bool completed = false;


		public override bool IsDone()
		{
			return completed;
		}

		public ActionInstant():base(0)
		{
			
		}

	
		public override void Update(float delta)
		{
			LerpAction(1f);
			completed = true;
		}

		public abstract void  DoAction();

		public override void LerpAction(float delta)
		{
			DoAction();

		}

	
	}

	public class Show :  ActionInstant 
	{

		public override void DoAction()
		{

			target.GetComponent<Renderer>().enabled = true;

		}

		public override Action  Clone()
		{
			return new Show();
		}

		public override Action  Reverse()
		{
			return new Hide();
		}


	}



	public class Hide :  ActionInstant 
	{

		public override void DoAction()
		{

			target.GetComponent<Renderer>().enabled = false;

		}

		public override Action  Clone()
		{
			return new Hide();
		}

		public override Action  Reverse()
		{
			return new Show();
		}


	}


	public class ToggleVisiblilty :  ActionInstant 
	{

		public override void DoAction()
		{

			target.GetComponent<Renderer>().enabled = !target.GetComponent<Renderer>().enabled;

		}

		public override Action  Clone()
		{
			return new ToggleVisiblilty();
		}

		public override Action  Reverse()
		{
			return new ToggleVisiblilty();
		}


	}

	public class RemoveSelf :  ActionInstant 
	{
		public override Action  Clone()
		{
			return new RemoveSelf();
		}

		public override Action  Reverse()
		{
			return new RemoveSelf();
		}

		public override void  DoAction()
		{
			GameObject.Destroy(target.gameObject);

		}



	}



	public class Place :  ActionInstant 
	{
		Vector3 targetPosition;

		public Place(Vector3 position)
		{
			targetPosition = position;
		}
		public override Action  Clone()
		{
			return new Place(targetPosition);
		}

		public override Action  Reverse()
		{
			return new Place(targetPosition);
		}

		public override void  DoAction()
		{
			target.position = targetPosition;

		}



	}




	public class FlipX :  ActionInstant 
	{
		bool flip=false;

		public FlipX(bool isFlip)
		{
			flip = isFlip;
		}
		public override Action  Clone()
		{
			return new FlipX(flip);
		}

		public override Action  Reverse()
		{
			return new FlipX(!flip);
		}

		public override void  DoAction()
		{
			target.GetComponent<SpriteRenderer>().flipX = flip;

		}



	}


	public class FlipY :  ActionInstant 
	{
		bool flip=false;

		public FlipY(bool isFlip)
		{
			flip = isFlip;
		}
		public override Action  Clone()
		{
			return new FlipY(flip);
		}

		public override Action  Reverse()
		{
			return new FlipY(!flip);
		}

		public override void  DoAction()
		{
			target.GetComponent<SpriteRenderer>().flipY = flip;

		}



	}



	public class Call :  ActionInstant 
	{
		public delegate void CallDelegate(Transform atransform);
		CallDelegate function;

		public Call(CallDelegate aFunction)
		{
			function = aFunction;
		}
		public override Action  Clone()
		{
			return new Call(function);
		}

		public override Action  Reverse()
		{
			return new Call(function);
		}

		public override void  DoAction()
		{
			function(target);
		}



	}



}

