using UnityEngine;
using System.Collections.Generic;



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
Action *pingPongAction = Sequence::actions(action, action.reverse(), nullptr);
*/
	public class ActionInterval : FiniteTimeAction
	{
		float completedTime;
		bool isFirstTick;

		public float GetCompletedTime()
		{
			return completedTime;
		}
		protected ActionInterval()
		{

		}
		public ActionInterval(float duration)
		{
			completedTime = 0;
			this.duration = duration;
			isFirstTick = true;
			if (duration == 0)
			{
				this.duration = epsilon;
			}else
			{
				this.duration = duration;
			}
			
			
		}

		
		public override bool IsDone()
		{
			return (completedTime >= duration);
		}
		
		public override void Update(float delta)
		{
			if(isFirstTick)
			{
				isFirstTick = false;
				completedTime = 0;
			}
			else
			{
				completedTime += delta;
			}
			
			LerpAction(Mathf.Max(0,Mathf.Min(1,completedTime / Mathf.Max(duration, epsilon))));
		}

		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			isFirstTick=true;
		}

	}



	// Extra action for making a Sequence or Spawn when only adding one action to it.
	class ExtraAction :  FiniteTimeAction
	{
		public ExtraAction()
		{}
	
	};


	
	/** @class Sequence
 * @brief Runs actions sequentially, one after another.
 */
	class  Sequence :  ActionInterval
	{
	






		int _last;
		float _split;


		FiniteTimeAction[ ] finiteTimeActions = new FiniteTimeAction[2];


		//List<FiniteTimeAction> finiteTimeActions=new List<FiniteTimeAction>();


		public Sequence(params FiniteTimeAction[] list):this(false,list)
		{

		}


		void AccumulateDuration()
		{
			duration= finiteTimeActions[0].GetDuration()+finiteTimeActions[1].GetDuration();

		}

		private Sequence( bool dummy,FiniteTimeAction[] list):base(1)
		{
			if(list.Length==0)
			{
				finiteTimeActions[0]= new ExtraAction();
				finiteTimeActions[1]= new ExtraAction();
				AccumulateDuration();
			}
			else if(list.Length==1)
			{
				finiteTimeActions[0]= list[0];
				finiteTimeActions[1]= new ExtraAction();

				AccumulateDuration();
			}else if(list.Length==2)
			{
				finiteTimeActions[0]= list[0];
				finiteTimeActions[1]= list[1];


			}else
			{// GREATER THAN 2



				Sequence last = new Sequence (list[list.Length-2],list[list.Length-1]);

				for(int i= list.Length-3;i>=1;i--)
				{
					last = new Sequence(list[i],last);
				}


				finiteTimeActions[0]= list[0];
				finiteTimeActions[1]= last;

				AccumulateDuration();
			}



	
		}


		public Sequence( FiniteTimeAction  action1,FiniteTimeAction  action2):base(1)
		{
			float totalduration = action1.GetDuration()+action2.GetDuration();
			duration = totalduration;

			finiteTimeActions[0]=action1;
			finiteTimeActions[1]=action2;
		}



		public override void Stop()
		{
			if( _last != - 1)
			{
				finiteTimeActions[_last].Stop();
			}
			base.Stop();
		}

		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			
			_split = finiteTimeActions[0].GetDuration()/ GetDuration();
			_last = -1;
		}


		public override void LerpAction(float t)
		{
			int found = 0;
			float new_t = 0.0f;
			
			if( t < _split ) {
				// action[0]
				found = 0;
				if( _split != 0 )
					new_t = t / _split;
				else
					new_t = 1;
				
			} else {
				// action[1]
				found = 1;
				if ( _split == 1 )
					new_t = 1;
				else
					new_t = (t-_split) / (1 - _split );
			}
			
			if ( found==1 ) {
				
				if( _last == -1 ) {
					// action[0] was skipped, execute it.
					finiteTimeActions[0].StartWithTarget(target);
					finiteTimeActions[0].LerpAction(1.0f);
					finiteTimeActions[0].Stop();
				}
				else if( _last == 0 )
				{
					// switching to action 1. Stop action 0.
					finiteTimeActions[0].LerpAction(1.0f);
					finiteTimeActions[0].Stop();
				}
			}
			else if(found==0 && _last==1 )
			{
				// Reverse mode ?
				// FIXME: Bug. this case doesn't contemplate when _last==-1, found=0 and in "reverse mode"
				// since it will require a hack to know if an action is on reverse mode or not.
				// "step" should be overriden, and the "reverseMode" value propagated to inner Sequences.
				finiteTimeActions[1].LerpAction(0);
				finiteTimeActions[1].Stop();
			}
			// Last action found and it is done.
			if( found == _last && finiteTimeActions[found].IsDone() )
			{
				return;
			}
			
			// Last action found and it is done
			if( found != _last )
			{
				finiteTimeActions[found].StartWithTarget(target);
			}
			
			finiteTimeActions[found].LerpAction(new_t);
			_last = found;
		}
		




		



	}


	
	/** @class Repeat
 * @brief Repeats an action a number of times.
 * To repeat an action forever use the RepeatForever action.
 */

class  Repeat :  ActionInterval
{


		protected  int _times;
		protected	 int _total;
		protected float _nextDt;
	/** Inner action */
		protected FiniteTimeAction _innerAction;
	
		public 	Repeat(FiniteTimeAction action, int times)
		{
			_times=times;
			duration = action.GetDuration() * times;

			_innerAction=action;
		}




		public override void StartWithTarget(Transform target)
		{
			_total = 0;
			_nextDt = _innerAction.GetDuration()/duration;
			base.StartWithTarget(target);
			_innerAction.StartWithTarget(target);
		}


		public override void Stop()
		{
			_innerAction.Stop();
			base.Stop();
		}


		public override void LerpAction(float dt)
		{
			if (dt >= _nextDt)
			{
				while (dt > _nextDt && _total < _times)
				{
					
					_innerAction.LerpAction(1.0f);
					_total++;
					
					_innerAction.Stop();
					_innerAction.StartWithTarget(target);
					_nextDt = _innerAction.GetDuration()/duration * (_total+1);
				}
				
				// fix for issue #1288, incorrect end value of repeat
				if(dt >= 1.0f && _total < _times) 
				{
					_total++;
				}
				
				// don't set an instant action back or update it, it has no use because it has no duration
				if (_total == _times)
				{
					_innerAction.LerpAction(1);
					_innerAction.Stop();
				}
				else
				{
					// issue #390 prevent jerk, use right update
					_innerAction.LerpAction(dt - (_nextDt - _innerAction.GetDuration()/duration));
				}

			}
			else
			{
				_innerAction.LerpAction(dt * _times % 1.0f);
			}
		}
		
		public override bool IsDone() 
		{
			return _total == _times;
		}





};




	
	/** @class RepeatForever
 * @brief Repeats an action for ever.
 To repeat the an action for a limited number of times use the Repeat action.
 * @warning This action can't be Sequenceable because it is not an IntervalAction.
 */
	class  RepeatForever :  ActionInterval
	{
	
			/** Inner action */
		protected		ActionInterval _innerAction;
		
	
		public RepeatForever(ActionInterval action)
		{
			_innerAction = action;
		}

		public  override void StartWithTarget(Transform target)
		{
			base.StartWithTarget(target);
			_innerAction.StartWithTarget(target);
		}
		
		public override  void Update(float dt)
		{
			_innerAction.Update(dt);
			if (_innerAction.IsDone())
			{
				float diff = _innerAction.GetCompletedTime() - _innerAction.GetDuration();
				if (diff > _innerAction.GetDuration())
					diff = diff % _innerAction.GetDuration();
				_innerAction.StartWithTarget(target);
				// to prevent jerk. issue #390, 1247
				_innerAction.Update(0.0f);
				_innerAction.Update(diff);
			}
		}
		
		public override bool IsDone() 
		{
			return false;
		}
		
//		RepeatForever Reverse() const
//		{
//			return RepeatForever::create(_innerAction->reverse());
//		}
		
		//
	};


	
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