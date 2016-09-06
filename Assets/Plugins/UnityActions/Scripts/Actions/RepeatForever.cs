using UnityEngine;
using System.Collections;


/** @class RepeatForever
 * @brief Repeats an action for ever.
 To repeat the an action for a limited number of times use the Repeat action.
 * @warning This action can't be Sequenceable because it is not an IntervalAction.
 */
namespace CC
{
    class RepeatForever : ActionInterval
    {
        /** Inner action */
        protected ActionInterval _innerAction;

        public RepeatForever(ActionInterval action)
        {
            _innerAction = action;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            _innerAction.StartWithTarget(target);
        }

        public override void Update(float dt)
        {
            _innerAction.Update(dt);
            if (_innerAction.IsDone())
            {
                float diff = _innerAction.GetCompletedTime() - _innerAction.GetDuration();
                if (diff > _innerAction.GetDuration())
                    diff = diff % _innerAction.GetDuration();
                _innerAction.StartWithTarget(target);
                // to prevent jerk. issue #390, 1247
                _innerAction.Update(0.0f);
                _innerAction.Update(diff);
            }
        }

        public override bool IsDone()
        {
            return false;
        }

        public override Action Reverse()
        {
            return new RepeatForever(_innerAction.Reverse() as ActionInterval);
        }

        public override Action Clone()
        {
            return new RepeatForever(_innerAction as ActionInterval);
        }

        public override void LerpAction(float delta)
        {
            throw new System.NotImplementedException();
        }
    };
}
