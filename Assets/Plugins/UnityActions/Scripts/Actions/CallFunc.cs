using UnityEngine;
using System.Collections;
using System;

namespace CC
{
    public class CallFunc : ActionInterval
    {
        System.Action call;

        public CallFunc (System.Action callback)
        {
            call = callback;
        }

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override void LerpAction(float delta)
        {
            //throw new NotImplementedException();
            call();
        }

        public override Action Reverse()
        {
            throw new NotImplementedException();
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);

        }

        private IEnumerator waitThenCallback(float time, System.Action Callback)
        {
            yield return new WaitForSeconds(time);
            Callback();
        }
    }
}
