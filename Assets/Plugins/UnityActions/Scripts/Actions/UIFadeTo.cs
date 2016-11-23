using UnityEngine;
using System.Collections;
using System;

namespace CC
{
    public class UIFadeTo : ActionInterval
    {
        private float startAlpha;
        private float endAlpha;
        private CanvasRenderer canvasRenderer;

        public UIFadeTo(float duration = 1.0f, float alphaEndp = 0.5f) : base(duration)
        {
            endAlpha = alphaEndp;
        }

        public override CC.Action Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override CC.Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override void LerpAction(float delta)
        {
            canvasRenderer.SetAlpha(Mathf.Lerp(startAlpha, endAlpha, delta));
        }

        public override bool IsDone()
        {
            if (base.IsDone())
            {
                Component.Destroy(target.GetComponent<Actor>());
                return base.IsDone();
            }
            return base.IsDone();
        }
        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            if (inTarget.gameObject.GetComponent<CanvasRenderer>() == null)
            {
                Debug.LogError("This Action should be applied only to UI elements");
            }
            canvasRenderer = inTarget.gameObject.GetComponent<CanvasRenderer>();
            startAlpha = canvasRenderer.GetAlpha();
        }
    }
}

