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
        private float startAlpha = 0.0f;//TODO:
        private float endAlpha = 1.0f;
        private CanvasRenderer canvasRenderer;
        private CanvasGroup canvasGroup;
        private UISetup UIMode;

        //By default this will be 1 second
        public UIFadeIn(float duration = 1.0f) : base(duration)
        {

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
                canvasRenderer.SetAlpha(0.0f);
            }
            else if (inTarget.GetComponent<CanvasGroup>() != null)
            {
                UIMode = UISetup.canvasGroup;
                canvasGroup = inTarget.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0.0f;
            }
            else
            {
                Debug.LogError("This object doesn't have CanvasRenderer or CanvasGroup");
            }
        }
    }
}