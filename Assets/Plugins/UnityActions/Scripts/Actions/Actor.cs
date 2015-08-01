using UnityEngine;
using System.Collections;


namespace CC
{



	public class Director:MonoBehaviour
	{

		public static Director Setup(Actor actor)
		{
			Director runner =  actor.transform.GetComponent<Director>();
			if(runner == null)
			{
				runner = actor.transform.gameObject.AddComponent<Director>();
			}

			
			return runner;
		}

	}






public class Actor  {




	public Transform transform;
	public  FiniteTimeAction action;

	public Actor(Transform transform)
	{
		this.transform=transform;
	}
	






		
		public IEnumerator YieldAction(FiniteTimeAction anAction)
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


		public Coroutine PerformAction(FiniteTimeAction anAction)
		{
			Director runner =Director.Setup(this);
			return runner.StartCoroutine(YieldAction(anAction));

		}

		public Coroutine MoveBy(float aduration,Vector3 diff)
		{
			return PerformAction(new CC.MoveBy(aduration,diff));

		}

		public Coroutine MoveTo(float duraction,Vector3 targetPos)
		{
			return PerformAction(new CC.MoveTo(duraction,targetPos));
		}


}
}