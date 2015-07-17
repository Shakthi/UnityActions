using UnityEngine;
using System.Collections.Generic;



namespace CC
{

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
					currentAction.Update(Time.deltaTime);
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



		public static void Setup(Transform inTarget,Action anAction)
		{
			ActionRunner runner =  inTarget.GetComponent<ActionRunner>();
			if(runner == null)
			{
				runner = inTarget.gameObject.AddComponent<ActionRunner>();
				runner.actionsToRun = new List<Action>();
			}
			runner.actionsToRun.Add(anAction);

			
			
		}


	}




	
	
}
