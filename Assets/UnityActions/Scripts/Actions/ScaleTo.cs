using UnityEngine;
using System.Collections;
using System;

namespace CC
{

	public class ScaleTo : ActionInterval
    {
        protected float startScale;

        protected float endScale_x;
        protected float endScale_y;
        protected float endScale_z;

        protected Vector3 _startScale;
        protected Vector3 _endScale;

        public ScaleTo(float duration, float scaleTo): base(duration)
        {
            _endScale = new Vector3(scaleTo, scaleTo, scaleTo);
        }
        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override void LerpAction(float delta)
        {
            target.localScale = Vector3.Lerp(_startScale,_endScale,delta);
        }

        public override Action Reverse()
        {
            throw new NotImplementedException();
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            _startScale = inTarget.localScale;
        }
    }
}