using UnityEngine;
using System.Collections;

/** @class RotateTo
 	* @brief Rotates a Transform to a certain angle by modifying it's rotation attribute.
 	The direction will be decided by the shortest angle.
	*/
namespace CC
{
    public class RotateTo : ActionInterval
    {
        public RotateTo(float duration, Vector3 dstAngle3D) : base(duration)
        {
            _dstAngle = Quaternion.Euler(dstAngle3D);
        }

        public RotateTo(float duration, Quaternion deltaAngle3D) : base(duration)
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
            _diffAngle = _dstAngle * Quaternion.Inverse(_startRotation);
        }


        public override Action Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override Action Clone()
        {
            return new RotateTo(duration, _dstAngle);
        }

        protected Quaternion _dstAngle;
        protected Quaternion _startRotation;
        protected Quaternion _diffAngle;
    };
}