using UnityEngine;
using System.Collections;


namespace CC
{

public class Actor  {

	 Transform transform;
	 FiniteTimeAction action;

	public Actor(Transform transform)
	{
		this.transform=transform;
	}
	

	public IEnumerator RunAction(FiniteTimeAction anAction)
	{
			if(action !=null)
				Debug.LogError("An action is already running");
			else
			{
				action = anAction;
			
				Action.Run(transform,action);
				yield return new WaitForSeconds(action.GetDuration());
				while(!action.IsDone())
				{
					yield return null;
				}
				action=null;
			}

	}

	public IEnumerator MoveBy(float duraction,Vector3 diff)
	{



			 return RunAction(new CC.MoveBy(duraction,diff));
	}

	public IEnumerator MoveTo(float duraction,Vector3 destination)
	{
			 return RunAction(new CC.MoveTo(duraction,destination));
	}


}
}