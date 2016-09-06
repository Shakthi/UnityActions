using UnityEngine;
using System.Collections;
namespace CC
{
    /** @class MoveTo
     * @brief Moves a trasform to the position endposition

     */
    public class MoveTo : ActionInterval
    {
        Vector3 endPosition;
        Vector3 startPosition;

        public MoveTo(float duration, Vector3 endPosition) : base(duration)
        {
            this.endPosition = endPosition;
        }

        public override Action Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override Action Clone()
        {
            return new MoveTo(duration, endPosition);
        }

        public override void LerpAction(float deltaTime)
        {
            target.position = Vector3.Lerp(startPosition, endPosition, deltaTime);
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            startPosition = inTarget.position;
        }
    }
}