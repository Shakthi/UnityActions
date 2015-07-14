using UnityEngine;
using System.Collections;


namespace CC
{

	public class BaseAction
	{

	}

	class ActionRunner:MonoBehaviour
	{
		public Action actionToRun;
		
		void Update()
		{
			if(!actionToRun.IsDone())
			{
				actionToRun.Step(Time.deltaTime);
		
			}
			else
			{
				Destroy(this);
			}

		}
	}

	public class Action : BaseAction
	{
		protected Transform target;

		public static void Run(Transform movingObject ,Action aAction)
		{	
			ActionRunner runner =  movingObject.GetComponent<ActionRunner>();
			if(runner == null)
			{
				runner = movingObject.gameObject.AddComponent<ActionRunner>();
				runner.actionToRun = aAction;
			}
			aAction.StartWithTarget(movingObject);

		}

		public virtual void Update(float delta)
		{
		}

		public virtual void Step(float delta)
		{

		}

		public virtual void StartWithTarget(Transform inTarget)
		{
			target = inTarget;
		}

		public void Stop()
		{
			target = null;
		}

		public virtual bool IsDone()
		{
			return true;
		}
	}

	public class FiniteTimeAction : Action
	{
		protected float duration;
	}

	public class ActionIntervel : FiniteTimeAction
	{
		float completedTime;
		bool isFirstTick;

		public ActionIntervel(float duration)
		{
			completedTime = 0;
			this.duration = duration;
			isFirstTick = true;
		}

		public override bool IsDone()
		{
			return (completedTime >= duration);
		}

		public override void Step(float delta)
		{
			Debug.Log("Step is called");

			if(isFirstTick)
			{
				isFirstTick = false;
				completedTime = 0;
			}
			else
			{
				completedTime += delta;
			}

			Update(Mathf.Max(0,Mathf.Min(1,completedTime /duration)));
		}
	}



	public class MoveTo : ActionIntervel
	{
		Vector3 endPosition;
		Vector3 startPosition;
		Vector3 delta;
		
		public MoveTo(float duration ,Vector3 endPosition ):base(duration)
		{
			this.endPosition = endPosition;
		}
		
		public  override void Update(float deltaTime)
		{
			target.position = ((startPosition + delta) * deltaTime );
		}

		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			startPosition = inTarget.position;
			delta = endPosition - startPosition;
		}

	}
}