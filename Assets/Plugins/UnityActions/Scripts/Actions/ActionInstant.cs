using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CC
{
    /// <summary>
    /// Instant actions are immediate actions. They don't have a duration like the IntervalAction actions.
    /// </summary>
    public class ActionInstant : FiniteTimeAction
    {
        public ActionInstant(float aduration) : base(aduration)
        {

        }

        public ActionInstant():base()
        {

        }

        public override Action Clone()
        {
            
            throw new NotImplementedException();
        }

        public override void LerpAction(float delta)
        {
            //throw new NotImplementedException();
        }

        public override Action Reverse()
        {
            throw new NotImplementedException();
        }

        public override void Update(float delta)
        {
            //throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Calls a 'callback'.
    /// </summary>
    public class CallFunc: ActionInstant
    {
        System.Action callFunc;

        public CallFunc(System.Action callBackF)
        {
            callFunc = callBackF;
        }

        public override Action Clone()
        {
            return new CallFunc(callFunc);
        }
        public override void LerpAction(float delta)
        {
            callFunc();
        }
        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
        }
    }
}
