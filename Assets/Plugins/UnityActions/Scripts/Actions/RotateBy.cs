using UnityEngine;
using System.Collections;

/** @class RotateBy
* @brief Rotates a Node object clockwise a number of degrees by modifying it's rotation attribute.
*/
namespace CC
{
    public class RotateBy : ActionInterval
    {
        public RotateBy(float duration, Vector3 deltaAngle3D) : base(duration)
        {
            _diffAngle = Quaternion.Euler(deltaAngle3D);
        }

        public RotateBy(float duration, Quaternion deltaAngle3D) : base(duration)
        {
            _diffAngle = deltaAngle3D;
        }

        public override void LerpAction(float deltaTime)
        {
            target.rotation = Quaternion.Lerp(_startRotation, _dstAngle, deltaTime);
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            _startRotation = inTarget.rotation;
            _dstAngle = _diffAngle * _startRotation;
        }

        public override Action Reverse()
        {
            return new RotateTo(duration, Quaternion.Inverse(_diffAngle));
        }

        public override Action Clone()
        {
            return new RotateTo(duration, _diffAngle);
        }

        protected Quaternion _dstAngle;
        protected Quaternion _startRotation;
        protected Quaternion _diffAngle;
    };
}