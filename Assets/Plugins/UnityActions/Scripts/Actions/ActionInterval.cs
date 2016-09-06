using UnityEngine;
using System.Collections.Generic;

namespace CC
{
    /** @class ActionInterval
@brief An interval action is an action that takes place within a certain period of time.
It has an start time, and a finish time. The finish time is the parameter
duration plus the start time.
These ActionInterval actions have some interesting properties, like:
- They can run normally (default)
- They can run reversed with the reverse method
- They can run with the time altered with the Accelerate, AccelDeccel and Speed actions.
For example, you can simulate a Ping Pong effect running the action normally and
then running it again in Reverse mode.
Example:
Action *pingPongAction = Sequence::actions(action, action.reverse(), nullptr);
*/
    public abstract class ActionInterval : FiniteTimeAction
    {
        float completedTime;
        bool isFirstTick;

        public float GetCompletedTime()
        {
            return completedTime;
        }
        protected ActionInterval() : base(0)
        {

        }
        public ActionInterval(float duration) : base(duration)
        {
            completedTime = 0;
            this.duration = duration;
            isFirstTick = true;
            if (duration == 0)
            {
                this.duration = epsilon;
            }
            else
            {
                this.duration = duration;
            }
        }
        public override bool IsDone()
        {
            return (completedTime >= duration);
        }

        public override void Update(float delta)
        {
            if (isFirstTick)
            {
                isFirstTick = false;
                completedTime = 0;
            }
            else
            {
                completedTime += delta;
            }

            LerpAction(Mathf.Max(0, Mathf.Min(1, completedTime / Mathf.Max(duration, epsilon))));
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            isFirstTick = true;
        }

    }
    // Extra action for making a Sequence or Spawn when only adding one action to it.
    class ExtraAction : FiniteTimeAction
    {
        public ExtraAction() : base(0)
        { }

        public override void LerpAction(float delta)
        {

        }

        public override void Update(float delta)
        {

        }

        public override Action Reverse()
        {
            return new ExtraAction();
        }

        public override Action Clone()
        {
            return new ExtraAction();
        }
    };
}