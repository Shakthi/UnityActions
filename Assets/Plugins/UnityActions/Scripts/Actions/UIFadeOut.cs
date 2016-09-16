using UnityEngine;
using System.Collections;


namespace CC
{
    /// <summary>
    /// Class UIFadeOut. Class that makes the action of fading a Canvas Group.
    /// Warning This only works with UI elements, do not use 3D elements </summary>
    /// <remarks>
    /// Longer comments can be associated with a type or member 
    /// through the remarks tag</remarks>
    public class UIFadeOut : ActionInterval
    {
        private float startAlpha = 1.0f;
        private float endAlpha = 0.0f;
        private CanvasGroup canvasGroup;
        private bool setParentEnableAfterComplete;

        /// <summary>
        /// The class constructor. </summary>
        public UIFadeOut(float duration = 1.0f, bool setEnableAfterCompleted = true) : base(duration)
        {
            setParentEnableAfterComplete = setEnableAfterCompleted;
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
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, delta);
        }

        public override bool IsDone()
        {
            canvasGroup.alpha = endAlpha;
            Component.Destroy(target.GetComponent<Actor>());
            if (base.IsDone() && !setParentEnableAfterComplete)
                target.gameObject.SetActive(false);
            //Debug.Log("Completed Action");
            return base.IsDone();
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);
            if (inTarget.gameObject.GetComponent<CanvasGroup>() == null)
            {
                inTarget.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup = inTarget.gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1.0f;
        }
    }
}