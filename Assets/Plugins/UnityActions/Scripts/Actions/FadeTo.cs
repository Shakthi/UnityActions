using UnityEngine;
using System.Collections;
using System;

namespace CC
{
    /** @class FadeTo
    * @brief Changes the Material Alpha. In order to get this work 
    * The material must be setup in Rendering Mode Fade, otherwise it will not work.
    * 
    */
    public class FadeTo : ActionInterval
    {

        private float alphaStart;
        private float alphaEnd;
        private Material material;
        private Color color;

        public FadeTo(float duration, float alphaEnd) : base(duration)
        {
            this.alphaEnd = alphaEnd;
        }

        public override Action Reverse()
        {
            throw new NotImplementedException();
        }
        public override Action Clone()
        {
            throw new NotImplementedException();
        }
        public override void LerpAction(float delta)
        {
            color.a = Mathf.Lerp(alphaStart, alphaEnd, delta);
            material.color = color;
            Debug.Log(material.color.a);
        }
        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            material = target.GetComponent<Renderer>().material;
            alphaStart = material.color.a;
            color = material.color;
        }
    }
}