using UnityEngine;
using System.Collections;


namespace CC
{
    public class Actor : MonoBehaviour
    {
        public FiniteTimeAction action;

        static public Actor GetActor(Transform intransform)
        {
            Actor actor = intransform.GetComponent<Actor>();
            if (actor == null)
            {
                actor = intransform.gameObject.AddComponent<Actor>();
            }
            return actor;
        }

        public IEnumerator YieldAction(FiniteTimeAction anAction)
        {
            if (action != null)
                Debug.LogError("An action is already running");
            else
            {
                action = anAction;

                Action.Run(transform, action);
                yield return new WaitForSeconds(action.GetDuration());
                while (!action.IsDone())
                {
                    yield return null;
                }
                action = null;
            }
        }

        public Coroutine PerformAction(FiniteTimeAction anAction)
        {
            return StartCoroutine(YieldAction(anAction));
        }

        public Coroutine MoveBy(float aduration, Vector3 diff)
        {
            return PerformAction(new CC.MoveBy(aduration, diff));
        }

        public Coroutine MoveTo(float duraction, Vector3 targetPos)
        {
            return PerformAction(new CC.MoveTo(duraction, targetPos));
        }

        public Coroutine UIFadeIn(float duration)
        {
            return PerformAction(new UIFadeOut(duration));
        }

        public Coroutine UIFadeOut(float duration)
        {
            return PerformAction(new UIFadeOut(duration));
        }
    }
}