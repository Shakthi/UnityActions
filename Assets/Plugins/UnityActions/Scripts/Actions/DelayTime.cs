using UnityEngine;
using System.Collections;

namespace CC
{
    /** @class DelayTime
    * @brief Delays the action a certain amount of seconds.
*/
    public class DelayTime : ActionInterval
    {
        /** 
    * Creates the action.
    * @param d Duration time, in seconds.
*/
        public DelayTime(float d) : base(d)
        {

        }
        //
        // Overrides
        //
        /**
     * @param time In seconds.
     */
        public override void LerpAction(float time)
        {

        }

        public override Action Reverse()
        {
            return new DelayTime(duration);
        }

        public override Action Clone()
        {
            return new DelayTime(duration);
        }
    };
}
