using UnityEngine;
using System.Collections;
using CC;

/** @class Repeat
 * @brief Repeats an action a number of times.
 * To repeat an action forever use the RepeatForever action.
 */
namespace CC
{
    class Repeat : ActionInterval
    {
        protected int _times;
        protected int _total;
        protected float _nextDt;
        /** Inner action */
        protected FiniteTimeAction _innerAction;

        public Repeat(FiniteTimeAction action, int times)
        {
            _times = times;
            duration = action.GetDuration() * times;

            _innerAction = action;
        }

        public override void StartWithTarget(Transform target)
        {
            _total = 0;
            _nextDt = _innerAction.GetDuration() / duration;
            base.StartWithTarget(target);
            _innerAction.StartWithTarget(target);
        }

        public override void Stop()
        {
            _innerAction.Stop();
            base.Stop();
        }

        public override void LerpAction(float dt)
        {
            if (dt >= _nextDt)
            {
                while (dt > _nextDt && _total < _times)
                {

                    _innerAction.LerpAction(1.0f);
                    _total++;

                    _innerAction.Stop();
                    _innerAction.StartWithTarget(target);
                    _nextDt = _innerAction.GetDuration() / duration * (_total + 1);
                }

                // fix for issue #1288, incorrect end value of repeat
                if (dt >= 1.0f && _total < _times)
                {
                    _total++;
                }

                // don't set an instant action back or update it, it has no use because it has no duration
                if (_total == _times)
                {
                    _innerAction.LerpAction(1);
                    _innerAction.Stop();
                }
                else
                {
                    // issue #390 prevent jerk, use right update
                    _innerAction.LerpAction(dt - (_nextDt - _innerAction.GetDuration() / duration));
                }
            }
            else
            {
                _innerAction.LerpAction(dt * _times % 1.0f);
            }
        }

        public override bool IsDone()
        {
            return _total == _times;
        }

        public override Action Reverse()
        {
            return new Repeat(_innerAction.Reverse() as FiniteTimeAction, _times);
        }

        public override Action Clone()
        {
            return new Repeat(_innerAction.Clone() as FiniteTimeAction, _times);
        }
    };
}
