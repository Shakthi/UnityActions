using UnityEngine;
using System.Collections;
using CC;

/** @class FiniteTimeAction
 * @brief
 * Base class actions that do have a finite time duration.
 * Possible actions:
 * - An action with a duration of 0 seconds.
 * - An action with a duration of 35.5 seconds.
 * Infinite time actions are valid.
 */

public abstract class FiniteTimeAction : Action
{
    protected float duration;

    public float GetDuration()
    {
        return duration;
    }

    public FiniteTimeAction(float aduration)
    {
        duration = aduration;
    }
}
