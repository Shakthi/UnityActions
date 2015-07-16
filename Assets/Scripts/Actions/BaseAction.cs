using UnityEngine;
using System.Collections.Generic;



namespace CC
{

	public class BaseAction
	{
		protected const float epsilon = 1.192092896e-07F;

	}

	class ActionRunner:MonoBehaviour
	{
		public List<Action> actionsToRun;
		
		void Update()
		{

			for(int i=0;i<actionsToRun.Count ; i++)
			{
				Action currentAction = actionsToRun[i];
				if(!currentAction.IsStarted())
				{
					currentAction.StartWithTarget(transform);
						
				}

				if(!currentAction.IsDone())
				{
					currentAction.Step(Time.deltaTime);
				}else
				{
					currentAction.Stop();
					actionsToRun.RemoveAt(i);
					i--;

				}

			}

			if(actionsToRun.Count==0)
				Destroy(this);


		}
	}

	public class Action : BaseAction
	{
		protected Transform target;
		bool isStarted;


		public bool IsStarted()
		{
			return isStarted;
		}


		public static void Run(Transform targetTransform ,Action anAction)
		{	
			ActionRunner runner =  targetTransform.GetComponent<ActionRunner>();
			if(runner == null)
			{
				runner = targetTransform.gameObject.AddComponent<ActionRunner>();
				runner.actionsToRun = new List<Action>();
			}
			runner.actionsToRun.Add(anAction);

		}

		public virtual void ProgressUpdate(float delta)
		{
		}

		public virtual void Step(float delta)
		{

		}

		public virtual void StartWithTarget(Transform inTarget)
		{
			target = inTarget;
			isStarted = true;
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

	public class ActionInterval : FiniteTimeAction
	{
		float completedTime;
		bool isFirstTick;

		public ActionInterval(float duration)
		{
			completedTime = 0;
			this.duration = duration;
			isFirstTick = true;
			if (duration == 0)
			{
				this.duration = epsilon;
			}else
			{
				this.duration = duration;
			}


		}

		public override bool IsDone()
		{
			return (completedTime >= duration);
		}

		public override void Step(float delta)
		{
			if(isFirstTick)
			{
				isFirstTick = false;
				completedTime = 0;
			}
			else
			{
				completedTime += delta;
			}

			ProgressUpdate(Mathf.Max(0,Mathf.Min(1,completedTime /duration)));
		}
	}


/** @class MoveTo
 * @brief Moves a trasform to the position endposition

 */
	public class MoveTo : ActionInterval
	{
		Vector3 endPosition;
		Vector3 startPosition;

		public MoveTo(float duration ,Vector3 endPosition ):base(duration)
		{
			this.endPosition = endPosition;
		}
		
		public  override void ProgressUpdate(float deltaTime)
		{
			target.position = Vector3.Lerp(startPosition,endPosition,deltaTime);

		}

		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			startPosition = inTarget.position;
		}

	}


/** @class MoveBy
 * @brief Moves a trasform  by modifying it's position attribute.
 delta is  relative to the position of the object.
 Several MoveBy actions can be concurrently called, and the resulting
 movement will be the sum of individual movements.
 */
	public class MoveBy : ActionInterval
	{
		Vector3 startPosition;
		Vector3 delta;
		
		public MoveBy(float duration ,Vector3 delta ):base(duration)
		{
			this.delta = delta;
		}
		
		public  override void ProgressUpdate(float deltaTime)
		{
			target.position = ((startPosition + delta) * deltaTime );
		}
		
		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			startPosition = inTarget.position;
		}
		
	}




	/** @class RotateTo
 	* @brief Rotates a Transform to a certain angle by modifying it's rotation attribute.
 	The direction will be decided by the shortest angle.
	*/ 
	public class  RotateTo :  ActionInterval
	{
		public RotateTo(float duration, Vector3 dstAngle3D):base(duration)
		{
			_is3D=true;
			_dstAngle = Quaternion.Euler(dstAngle3D);

		}


		public  override void ProgressUpdate(float deltaTime)
		{
			target.rotation  = Quaternion.Lerp(_startRotation,_dstAngle,deltaTime);
		}
		
		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			_startRotation = inTarget.rotation;
			_diffAngle = _dstAngle * Quaternion.Inverse( _startRotation);
		}




		void RotateTocalculateAngles(ref float  startAngle,ref  float diffAngle, float dstAngle)
		{
			if (startAngle > 0)
			{
				startAngle = startAngle % 360.0f;
			}
			else
			{
				startAngle = startAngle % -360.0f;
			}
			
			diffAngle = dstAngle - startAngle;
			if (diffAngle > 180)
			{
				diffAngle -= 360;
			}
			if (diffAngle < -180)
			{
				diffAngle += 360;
			}
		}


		
		protected bool _is3D;
		protected 	Quaternion _dstAngle;
		protected  Quaternion _startRotation;
		protected  Quaternion _diffAngle;
		
	
	};



}
