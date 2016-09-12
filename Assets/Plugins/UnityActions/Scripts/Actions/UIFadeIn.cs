using UnityEngine;
using System.Collections;
using CC;

/*
 *@class UIFadeIn. 
 *@brief This class will make the effect of the fadeIn on the UI elements.
 * Warning this cannot be used in 3D objects 
 * 
 */
public class UIFadeIn : ActionInterval
{
    private float startAlpha = 0.0f;
    private float endAlpha = 1.0f;
    private bool setEnableParentAfterCompleted;
    private CanvasGroup canvasGroup;

    //By default this will be 1 second
    public UIFadeIn(float duration = 1.0f, bool setEnableAfterCompleted = true):base(duration)
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
        canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, delta);
    }

    public override bool IsDone()
    {
        canvasGroup.alpha = endAlpha;
        Component.Destroy(target.GetComponent<Actor>());
        return base.IsDone();
    }

    public override void StartWithTarget(Transform inTarget)
    {
        base.StartWithTarget(inTarget);
        if(inTarget.gameObject.GetComponent<CanvasGroup>() == null)
        {
            CanvasGroup g = createCanvasGroup(inTarget.gameObject, 0.0f);
        }
        canvasGroup = inTarget.gameObject.GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 0.0f;
    }

    public static CanvasGroup createCanvasGroup(GameObject where, float initialAlpha)
    {
        CanvasGroup canvasG = where.AddComponent<CanvasGroup>();
        canvasG.alpha = initialAlpha;
        return canvasG;
    }

}
