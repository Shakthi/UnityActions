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
        private CanvasGroup canvasGroup;
        private UISetup UIMode;

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
            if (UIMode == UISetup.canvasRenderer)
            {
                canvasRenderer.SetAlpha(Mathf.Lerp(startAlpha, endAlpha, delta));
            }
            else if (UIMode == UISetup.canvasGroup)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, delta);
            }
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            if (inTarget.GetComponent<CanvasRenderer>() != null)
            {
                UIMode = UISetup.canvasRenderer;
                canvasRenderer = inTarget.GetComponent<CanvasRenderer>();
                startAlpha = canvasRenderer.GetAlpha();
            }
            else if (inTarget.GetComponent<CanvasGroup>() != null)
            {
                UIMode = UISetup.canvasGroup;
                canvasGroup = inTarget.GetComponent<CanvasGroup>();
                startAlpha = canvasGroup.alpha;
            }
            else
            {
                Debug.LogError("This object doesn't have CanvasRenderer or CanvasGroup");
            }
        }
    }
}