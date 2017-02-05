using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CC
{
    public class UImageFadeTo : ActionInterval
    {
        private float startAlpha;
        private float endAlpha;
        private UnityEngine.UI.Image image;
        public UImageFadeTo(float duration, float alphaEnd) : base(duration)
        {
            this.duration = duration;
            this.endAlpha = alphaEnd;
        }

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override void LerpAction(float delta)
        {
            Color c = image.color;
            c.a = Mathf.Lerp(startAlpha, endAlpha, delta);
            image.color = c;
        }

        public override Action Reverse()
        {
            throw new NotImplementedException();
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            if (inTarget.GetComponent<UnityEngine.UI.Image>() != null)
            {
                image = inTarget.GetComponent<UnityEngine.UI.Image>();
                startAlpha = image.color.a;
                Debug.Log("Breakpoint Tracking");
            }
        }
    }
}
