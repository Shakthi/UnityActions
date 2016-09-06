﻿using UnityEngine;

namespace CC
{
	public abstract class Action : BaseAction
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
		
		public abstract  void LerpAction(float delta);
		
		public abstract  void Update(float delta);
		
		public virtual void StartWithTarget(Transform inTarget)
		{
			target = inTarget;
			isStarted = true;
		}
		
		public virtual void Stop()
		{
			target = null;
		}
		
		public virtual bool IsDone()
		{
			return true;
		}	
    /** Returns a new action that performs the exactly the reverse action. 
     *
     * @return A new action that performs the exactly the reverse action.
     */
		public  abstract   Action Reverse() ;
		

		public  abstract   Action Clone(); 

	}	
}