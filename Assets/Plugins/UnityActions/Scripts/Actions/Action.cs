using UnityEngine;

namespace CC
{


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
			ActionRunner.Setup(targetTransform,anAction);
		}
		
		public virtual void LerpAction(float delta)
		{
		}
		
		public virtual void Update(float delta)
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


	/** @class FiniteTimeAction
 * @brief
 * Base class actions that do have a finite time duration.
 * Possible actions:
 * - An action with a duration of 0 seconds.
 * - An action with a duration of 35.5 seconds.
 * Infinite time actions are valid.
 */

	public class FiniteTimeAction : Action
	{
		protected float duration;
	}
	
}

