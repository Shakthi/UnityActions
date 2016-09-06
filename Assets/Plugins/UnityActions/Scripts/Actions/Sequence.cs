using UnityEngine;
using System.Collections;

/** @class Sequence
 * @brief Runs actions sequentially, one after another.
 */
namespace CC
{
    class Sequence : ActionInterval
    {
        int _last;
        float _split;

        FiniteTimeAction[] finiteTimeActions = new FiniteTimeAction[2];
        //List<FiniteTimeAction> finiteTimeActions=new List<FiniteTimeAction>();
        public Sequence(params FiniteTimeAction[] list) : this(false, list)
        {

        }

        void AccumulateDuration()
        {
            duration = finiteTimeActions[0].GetDuration() + finiteTimeActions[1].GetDuration();
        }

        private Sequence(bool dummy, FiniteTimeAction[] list) : base(1)
        {
            if (list.Length == 0)
            {
                finiteTimeActions[0] = new ExtraAction();
                finiteTimeActions[1] = new ExtraAction();
                AccumulateDuration();
            }
            else if (list.Length == 1)
            {
                finiteTimeActions[0] = list[0];
                finiteTimeActions[1] = new ExtraAction();

                AccumulateDuration();
            }
            else if (list.Length == 2)
            {
                finiteTimeActions[0] = list[0];
                finiteTimeActions[1] = list[1];
            }
            else
            {// GREATER THAN 2
                Sequence last = new Sequence(list[list.Length - 2], list[list.Length - 1]);

                for (int i = list.Length - 3; i >= 1; i--)
                {
                    last = new Sequence(list[i], last);
                }

                finiteTimeActions[0] = list[0];
                finiteTimeActions[1] = last;

                AccumulateDuration();
            }
        }

        public override void Stop()
        {
            if (_last != -1)
            {
                finiteTimeActions[_last].Stop();
            }
            base.Stop();
        }

        public override void StartWithTarget(Transform inTarget)
        {
            base.StartWithTarget(inTarget);

            _split = finiteTimeActions[0].GetDuration() / GetDuration();
            _last = -1;
        }


        public override void LerpAction(float t)
        {
            int found = 0;
            float new_t = 0.0f;

            if (t < _split)
            {
                // action[0]
                found = 0;
                if (_split != 0)
                    new_t = t / _split;
                else
                    new_t = 1;
            }
            else
            {
                // action[1]
                found = 1;
                if (_split == 1)
                    new_t = 1;
                else
                    new_t = (t - _split) / (1 - _split);
            }
            if (found == 1)
            {
                if (_last == -1)
                {
                    // action[0] was skipped, execute it.
                    finiteTimeActions[0].StartWithTarget(target);
                    finiteTimeActions[0].LerpAction(1.0f);
                    finiteTimeActions[0].Stop();
                }
                else if (_last == 0)
                {
                    // switching to action 1. Stop action 0.
                    finiteTimeActions[0].LerpAction(1.0f);
                    finiteTimeActions[0].Stop();
                }
            }
            else if (found == 0 && _last == 1)
            {
                // Reverse mode ?
                // FIXME: Bug. this case doesn't contemplate when _last==-1, found=0 and in "reverse mode"
                // since it will require a hack to know if an action is on reverse mode or not.
                // "step" should be overriden, and the "reverseMode" value propagated to inner Sequences.
                finiteTimeActions[1].LerpAction(0);
                finiteTimeActions[1].Stop();
            }
            // Last action found and it is done.
            if (found == _last && finiteTimeActions[found].IsDone())
            {
                return;
            }
            // Last action found and it is done
            if (found != _last)
            {
                finiteTimeActions[found].StartWithTarget(target);
            }
            finiteTimeActions[found].LerpAction(new_t);
            _last = found;
        }

        public Sequence(FiniteTimeAction action1, FiniteTimeAction action2)
        {
            float totalduration = action1.GetDuration() + action2.GetDuration();
            duration = totalduration;

            finiteTimeActions[0] = action1;
            finiteTimeActions[1] = action2;
        }


        public override Action Reverse()
        {
            FiniteTimeAction action1 = finiteTimeActions[1].Reverse() as FiniteTimeAction;
            FiniteTimeAction action0 = finiteTimeActions[0].Reverse() as FiniteTimeAction;

            return new Sequence(action1, action0);
        }

        public override Action Clone()
        {
            return new Sequence(finiteTimeActions[0].Clone() as FiniteTimeAction, finiteTimeActions[1].Clone() as FiniteTimeAction);
        }
    }
}