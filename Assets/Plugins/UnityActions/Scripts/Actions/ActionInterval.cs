using UnityEngine;



namespace CC
{


/** @class ActionInterval
@brief An interval action is an action that takes place within a certain period of time.
It has an start time, and a finish time. The finish time is the parameter
duration plus the start time.
These ActionInterval actions have some interesting properties, like:
- They can run normally (default)
- They can run reversed with the reverse method
- They can run with the time altered with the Accelerate, AccelDeccel and Speed actions.
For example, you can simulate a Ping Pong effect running the action normally and
then running it again in Reverse mode.
Example:
Action *pingPongAction = Sequence::actions(action, action->reverse(), nullptr);
*/
	public class ActionInterval : FiniteTimeAction
	{
		float completedTime;
		bool isFirstTick;

		public ActionInterval(float duration)
		{
			this.duration =duration;
		}
	}


	
	
	
	
	/** @class MoveTo
 * @brief Moves a trasform to the position endposition

 */
	public class MoveTo : ActionInterval
	{
		Vector3 endPosition;
		Vector3 startPosition;
		
		public MoveTo(float duration ,Vector3 endPosition ):base(duration)
		{
			this.endPosition = endPosition;
		}
		
		public  override void LerpAction(float deltaTime)
		{
			target.position = Vector3.Lerp(startPosition,endPosition,deltaTime);
			
		}
		
		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			startPosition = inTarget.position;
		}
		
	}
	
	
	/** @class MoveBy
 * @brief Moves a trasform  by modifying it's position attribute.
 delta is  relative to the position of the object.
 Several MoveBy actions can be concurrently called, and the resulting
 movement will be the sum of individual movements.
 */
	public class MoveBy : ActionInterval
	{
		Vector3 startPosition;
		Vector3 delta;
		
		public MoveBy(float duration ,Vector3 delta ):base(duration)
		{
			this.delta = delta;
		}
		
		public  override void LerpAction(float deltaTime)
		{
			target.position = startPosition + delta * deltaTime ;
		}
		
		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			startPosition = inTarget.position;
		}
		
	}
	
	
	
	
	/** @class RotateTo
 	* @brief Rotates a Transform to a certain angle by modifying it's rotation attribute.
 	The direction will be decided by the shortest angle.
	*/ 
	public class  RotateTo :  ActionInterval
	{
		public RotateTo(float duration, Vector3 dstAngle3D):base(duration)
		{
			_dstAngle = Quaternion.Euler(dstAngle3D);
			
		}
		
		
		public  override void LerpAction(float deltaTime)
		{
			target.rotation  = Quaternion.Lerp(_startRotation,_dstAngle,deltaTime);
		}
		
		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			_startRotation = inTarget.rotation;
			_diffAngle = _dstAngle * Quaternion.Inverse( _startRotation);
		}
		
		
		
		
		protected 	Quaternion _dstAngle;
		protected  Quaternion _startRotation;
		protected  Quaternion _diffAngle;
		
		
	};
	
	
	
	/** @class RotateBy
* @brief Rotates a Node object clockwise a number of degrees by modifying it's rotation attribute.
*/
	
	public class  RotateBy :  ActionInterval
	{
		public RotateBy(float duration, Vector3 deltaAngle3D):base(duration)
		{
			_diffAngle = Quaternion.Euler(deltaAngle3D);
			
		}
		
		
		public  override void LerpAction(float deltaTime)
		{
			target.rotation  = Quaternion.Lerp(_startRotation,_dstAngle,deltaTime);
		}
		
		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			_startRotation = inTarget.rotation;
			_dstAngle = _diffAngle * _startRotation;
		}
		
		
		
		
		protected 	Quaternion _dstAngle;
		protected  Quaternion _startRotation;
		protected  Quaternion _diffAngle;
		
		
	};
	
	



}