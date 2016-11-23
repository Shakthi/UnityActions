using UnityEngine;
using System.Collections;

namespace CC
{
    /*
     *@class UIFadeIn. 
     *@brief This class will make the effect of the fadeIn on the UI elements.
     * Warning this cannot be used in 3D objects 
     */
    public class UIFadeIn : ActionInterval
    {
        private float startAlpha = 0.0f;
        private float endAlpha = 1.0f;
        private bool setEnableParentAfterCompleted;
        private CanvasRenderer canvasRenderer;

        //By default this will be 1 second
        public UIFadeIn(float duration = 1.0f, bool setEnableAfterCompleted = true) : base(duration)
        {
            this.setEnableParentAfterCompleted = setEnableAfterCompleted;
        }

        public override Action Reverse()
        {
            return null;
        }

        public override Action Clone()
        {
            return null;
        }

        public override void LerpAction(float delta)
        {
            canvasRenderer.SetAlpha(Mathf.Lerp(startAlpha, endAlpha, delta));
        }

        public override bool IsDone()
        {
            canvasRenderer.SetAlpha(endAlpha);
            Component.Destroy(target.GetComponent<Actor>());
            return base.IsDone();
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            if (inTarget.gameObject.GetComponent<CanvasRenderer>() == null)
            {
                Debug.LogError("This Action only works in UI elements");
            }
            canvasRenderer = inTarget.gameObject.GetComponent<CanvasRenderer>();
            canvasRenderer.SetAlpha(0.0f);
        }
    }
}