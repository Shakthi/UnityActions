using UnityEngine;
using System.Collections;

/** @class Spawn
* @brief Spawn a new action immediately
*/
namespace CC
{ 
public class Spawn : ActionInterval
{
    public Spawn(params FiniteTimeAction[] arrayOfActions) : this(false, arrayOfActions)
    {

    }
        
    Spawn(bool dummy, FiniteTimeAction[] list)
    {

        if (list.Length < 3)
            Debug.LogError("Array length should greater than 2");

        Spawn last = new Spawn(list[list.Length - 2], list[list.Length - 1]);

        for (int i = list.Length - 3; i >= 1; i--)
        {
            last = new Spawn(list[i], last);
        }


        _one = list[0];
        _two = last;

        AccumulateDuration(_one, _two);



    }

    void AccumulateDuration(FiniteTimeAction action1, FiniteTimeAction action2)
    {
        float d1 = _one.GetDuration();
        float d2 = _two.GetDuration();
        duration = Mathf.Max(d1, d2);
        if (d1 > d2)
        {
            _two = new Sequence(action2, new DelayTime(d1 - d2));
        }
        else
            if (d1 < d2)
        {
            _one = new Sequence(action1, new DelayTime(d2 - d1));
        }
    }


    public Spawn(FiniteTimeAction action1, FiniteTimeAction action2)
    {
        //CCASSERT(action1 != nullptr, "action1 can't be nullptr!");
        //CCASSERT(action2 != nullptr, "action2 can't be nullptr!");




        _one = action1;
        _two = action2;

        AccumulateDuration(action1, action2);





    }



    //
    // Overrides
    //
    public override void StartWithTarget(Transform target)
    {
        base.StartWithTarget(target);
        _one.StartWithTarget(target);
        _two.StartWithTarget(target);

    }
    public override void Stop()
    {
        _one.Stop();
        _two.Stop();
        base.Stop();


    }
    /**
 * @param time In seconds.
 */
    public override void LerpAction(float time)
    {
        if (_one != null)
        {
            _one.LerpAction(time);
        }
        if (_two != null)
        {
            _two.LerpAction(time);
        }

    }


    protected FiniteTimeAction _one;
    protected FiniteTimeAction _two;


    public override Action Clone()
    {
        return new Spawn(_one.Clone() as FiniteTimeAction, _two.Clone() as FiniteTimeAction);
    }

    public override Action Reverse()
    {
        return new Spawn(_one.Reverse() as FiniteTimeAction, _two.Reverse() as FiniteTimeAction);
    }
};
}