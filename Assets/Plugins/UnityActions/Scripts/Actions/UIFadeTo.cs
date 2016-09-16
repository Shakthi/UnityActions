using UnityEngine;
using System.Collections;
using System;

namespace CC
{
    public class UIFadeTo : ActionInterval
    {
        private float startAlpha;
        private float endAlpha;
        private CanvasGroup canvasGroup;

        public UIFadeTo(float duration = 1.0f) : base(duration)
        {

        }

        public override CC.Action Reverse()
        {
            return null;
        }

        public override CC.Action Clone()
        {
            return null;
        }

        public override void LerpAction(float delta)
        {

        }

        public override bool IsDone()
        {
            return base.IsDone();
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
        }
    }
}

