using UnityEngine;
using System.Collections;


namespace CC
{



public class Actor:MonoBehaviour  {

	public  FiniteTimeAction action;

	
	
	static public Actor GetActor(Transform intransform)
	{
			Actor actor =  intransform.GetComponent<Actor>();
			if(actor == null)
			{
				actor = intransform.gameObject.AddComponent<Actor>();
			}

			return actor;
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
			return StartCoroutine(YieldAction(anAction));
		}
		
		public Coroutine Sequence(params FiniteTimeAction[] list)
		{
			return PerformAction(new CC.Sequence(list));
		}
		
		public Coroutine Spawn(params FiniteTimeAction[] list)
		{
			return PerformAction(new CC.Spawn(list));
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

	public static class ActorExtensions {
		
		public static Coroutine RunAction(this MonoBehaviour component, FiniteTimeAction action) {
			return Actor.GetActor (component.transform).PerformAction (action);
		}
		
		public static Coroutine RunSequence(this MonoBehaviour component, params FiniteTimeAction[] actions) {
			return Actor.GetActor (component.transform).Sequence (actions);
		}
		
		public static Coroutine RunSpawn(this MonoBehaviour component, params FiniteTimeAction[] actions) {
			return Actor.GetActor (component.transform).Spawn (actions);
		}

	}
	
}