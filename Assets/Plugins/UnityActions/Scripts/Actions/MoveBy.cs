using UnityEngine;
using System.Collections;

/** @class MoveBy
 @brief Moves a trasform  by modifying it's position attribute.
 delta is  relative to the position of the object.
 Several MoveBy actions can be concurrently called, and the resulting
 movement will be the sum of individual movements.
 */
namespace CC
{
    public class MoveBy : ActionInterval
    {
        Vector3 startPosition, _previousPosition;
        Vector3 _positionDelta;

        public MoveBy(float duration, Vector3 delta) : base(duration)
        {
            _positionDelta = delta;
        }

        public override void LerpAction(float deltaTime)
        {
            Vector3 currentPos = target.position;
            Vector3 diff = currentPos - _previousPosition;
            startPosition = startPosition + diff;
            Vector3 newPos = startPosition + (_positionDelta * deltaTime);
            target.position = newPos;
            _previousPosition = newPos;

            //target.position = startPosition + delta * deltaTime ;
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            _previousPosition = startPosition = inTarget.position;
        }

        public override Action Reverse()
        {
            return new MoveBy(duration, -_positionDelta);
        }


        public override Action Clone()
        {
            return new MoveBy(duration, _positionDelta);
        }
    }
}