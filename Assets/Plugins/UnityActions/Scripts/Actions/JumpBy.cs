using UnityEngine;
using System.Collections;

namespace CC
{
    public class JumpBy : ActionInterval
    {

        /** 
        * Creates the action.
        * @param duration Duration time, in seconds.
        * @param position The jumping distance.
        * @param height The jumping height.
        * @param jumps The jumping times.
        * 
        */
        public JumpBy(float aduration, Vector3 position, float height, int jumps) : base(aduration)
        {
            _delta = position;
            _height = height;
            _jumps = jumps;
        }
        //
        // Overrides
        //
        public override Action Clone()
        {
            return new JumpBy(duration, _delta, _height, _jumps);
        }

        public override Action Reverse()
        {
            return new JumpBy(duration, -_delta, _height, _jumps);
        }
        public override void StartWithTarget(Transform aTransform)
        {
            base.StartWithTarget(aTransform);
            _previousPos = _startPosition = aTransform.position;

        }
        /**
     * @param time In seconds.
     */
        public override void LerpAction(float t)
        {
            //TODO: Implement 3d jump
            // parabolic jump (since v0.8.2)
            if (target)
            {
                float frac = (t * _jumps) % 1.0f;
                float y = _height * 4 * frac * (1 - frac);
                y += _delta.y * t;
                float x = _delta.x * t;
                //#if CC_ENABLE_STACKABLE_ACTIONS
                Vector3 currentPos = target.position;
                Vector3 diff = currentPos - _previousPos;
                _startPosition = diff + _startPosition;
                Vector3 newPos = _startPosition + new Vector3(x, y, 0);
                target.position = newPos;
                _previousPos = newPos;
                //				#else
                //				_target->setPosition(_startPosition + Vec2(x,y));
                //				#endif // !CC_ENABLE_STACKABLE_ACTIONS
            }
        }
        protected Vector3 _startPosition;
        protected Vector3 _delta;
        protected float _height;
        protected int _jumps;
        protected Vector3 _previousPos;
    };
}
