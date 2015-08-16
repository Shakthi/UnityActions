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
	public abstract class ActionInterval : FiniteTimeAction
	{
		float completedTime;
		bool isFirstTick;

		public float GetCompletedTime()
		{
			return completedTime;
		}
		protected ActionInterval():base(0)
		{

		}
		public ActionInterval(float duration):base(duration)
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
		public ExtraAction():base(0)
		{}

		 public override void LerpAction (float delta)
		{

		}

		public override void Update (float delta)
		{
			
		}

		public override Action Reverse ()
		{
			return new ExtraAction();
		}

		public override Action Clone ()
		{
			return new ExtraAction();
		}
	
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
		


		public Sequence( FiniteTimeAction  action1,FiniteTimeAction  action2)
		{
			float totalduration = action1.GetDuration()+action2.GetDuration();
			duration = totalduration;
			
			finiteTimeActions[0]=action1;
			finiteTimeActions[1]=action2;
		}

		
		public override Action Reverse ()
		{
			FiniteTimeAction action1 = finiteTimeActions[1].Reverse() as FiniteTimeAction;
			FiniteTimeAction action0 = finiteTimeActions[0].Reverse() as FiniteTimeAction ;

			return new Sequence(action1,action0);
		}

		public override Action Clone ()
		{
			return  new Sequence(finiteTimeActions[0].Clone() as FiniteTimeAction,finiteTimeActions[1].Clone() as FiniteTimeAction);
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

		public override Action Reverse() 
		{
			return new Repeat(_innerAction.Reverse() as FiniteTimeAction, _times);
		}

		public override Action Clone() 
		{
			return new Repeat(_innerAction.Clone() as FiniteTimeAction, _times);
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
		
		public override Action Reverse() 
		{
			return new RepeatForever(_innerAction.Reverse() as ActionInterval);
		}


		public override Action Clone() 
		{
			return new RepeatForever(_innerAction as ActionInterval);
		}

		public override void LerpAction (float delta)
		{
			throw new System.NotImplementedException ();
		}
		
		//
	};


	/** @class DelayTime
 * @brief Delays the action a certain amount of seconds.
*/
	public	class  DelayTime :  ActionInterval
	{
			/** 
     * Creates the action.
     * @param d Duration time, in seconds.
     */
		public DelayTime(float d):base(d)
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




	/** @class Spawn
 * @brief Spawn a new action immediately
 */
	public class  Spawn :  ActionInterval
	{


		public Spawn(params FiniteTimeAction[] arrayOfActions):this(false,arrayOfActions)
		{

		}



		Spawn(bool dummy,FiniteTimeAction[] list)
		{


			if(list.Length<3)
				Debug.LogError("Array length should greater than 2");

				Spawn last = new Spawn (list[list.Length-2],list[list.Length-1]);
			
			for(int i= list.Length-3;i>=1;i--)
			{
				last = new Spawn(list[i],last);
			}
			
			
			_one= list[0];
			_two= last;
			
			AccumulateDuration(_one,_two);



	}

		void AccumulateDuration (FiniteTimeAction action1, FiniteTimeAction action2)
		{
			float d1 = _one.GetDuration ();
			float d2 = _two.GetDuration ();
			duration = Mathf.Max (d1, d2);
			if (d1 > d2) {
				_two = new Sequence (action2, new DelayTime (d1 - d2));
			}
			else
				if (d1 < d2) {
					_one = new Sequence (action1, new DelayTime (d2 - d1));
				}
		}


		public Spawn(FiniteTimeAction action1, FiniteTimeAction action2)
		{
			//CCASSERT(action1 != nullptr, "action1 can't be nullptr!");
			//CCASSERT(action2 != nullptr, "action2 can't be nullptr!");
			



			_one = action1;
			_two = action2;

			AccumulateDuration (action1, action2);



				

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
		public  override void Stop() 
		{
			_one.Stop();
			_two.Stop();
			base.Stop();


		}
		/**
     * @param time In seconds.
     */
		public  override void LerpAction(float time)
		{
			if (_one !=null)
			{
				_one.LerpAction(time);
			}
			if (_two!=null)
			{
				_two.LerpAction(time);
			}

		}
		
	
		protected FiniteTimeAction _one;
		protected FiniteTimeAction _two;


		public override Action Clone() 
		{
			return new Spawn(_one.Clone() as FiniteTimeAction,_two.Clone() as FiniteTimeAction);
		}

		public override Action Reverse() 
		{
			return new Spawn(_one.Reverse() as FiniteTimeAction ,_two.Reverse() as FiniteTimeAction );
		}


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

		public override Action Reverse ()
		{
			throw new System.NotImplementedException ();
		}


		public override Action Clone ()
		{
			return new MoveTo(duration,endPosition);
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
 @brief Moves a trasform  by modifying it's position attribute.
 delta is  relative to the position of the object.
 Several MoveBy actions can be concurrently called, and the resulting
 movement will be the sum of individual movements.
 */
	public class MoveBy : ActionInterval
	{
		Vector3 startPosition,_previousPosition;
		Vector3 _positionDelta;
		
		public MoveBy(float duration ,Vector3 delta ):base(duration)
		{
			 _positionDelta=delta;
		}
		
		public  override void LerpAction(float deltaTime)
		{
			Vector3 currentPos = target.position;
			Vector3 diff = currentPos - _previousPosition;
			startPosition = startPosition + diff;
			Vector3 newPos =  startPosition + (_positionDelta * deltaTime);
			target.position = newPos;
			_previousPosition = newPos;



			//target.position = startPosition + delta * deltaTime ;
		}
		
		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			_previousPosition = startPosition = inTarget.position;
		}

		public override Action Reverse ()
		{
			return new MoveBy(duration,-_positionDelta);
		}
		
		
		public override Action Clone ()
		{
			return new MoveBy(duration,_positionDelta);
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

		public RotateTo(float duration, Quaternion deltaAngle3D):base(duration)
		{
			_diffAngle = deltaAngle3D;
			
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
		
		
		public override Action Reverse ()
		{
			throw new System.NotImplementedException ();
		}

		public override Action Clone ()
		{
			return new RotateTo(duration,_dstAngle);
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

		public RotateBy(float duration, Quaternion deltaAngle3D):base(duration)
		{
			_diffAngle = deltaAngle3D;
			
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
		
		public override Action Reverse ()
		{
			return new RotateTo(duration,Quaternion.Inverse( _diffAngle));
		}
		
		public override Action Clone ()
		{
			return new RotateTo(duration,_diffAngle);
		}
		
		
		protected 	Quaternion _dstAngle;
		protected  Quaternion _startRotation;
		protected  Quaternion _diffAngle;
		
		
	};




	public class  JumpBy :  ActionInterval
	{
	
			/** 
     * Creates the action.
     * @param duration Duration time, in seconds.
     * @param position The jumping distance.
     * @param height The jumping height.
     * @param jumps The jumping times.
     * 
     */
		public	JumpBy(float aduration,  Vector3  position, float height, int jumps):base(aduration)
		{
			_delta = position;
			_height = height;
			_jumps = jumps;

		}
		
		//
		// Overrides
		//
		public override Action Clone ()
		{
			return new JumpBy(duration,_delta,_height,_jumps);
		}



		public override Action Reverse ()
		{
			return new JumpBy(duration,-_delta,_height,_jumps);
		}
		public override void StartWithTarget(Transform aTransform) 
		{
			base.StartWithTarget(aTransform);
			_previousPos = _startPosition = aTransform.position;

		}
		/**
     * @param time In seconds.
     */
		public override void LerpAction(float t) 
		{
			//TODO: Implement 3d jump
			// parabolic jump (since v0.8.2)
			if (target)
			{
				float frac = (t * _jumps)% 1.0f ;
				float y = _height * 4 * frac * (1 - frac);
				y += _delta.y * t;
				float x = _delta.x * t;
				//#if CC_ENABLE_STACKABLE_ACTIONS
				Vector3 currentPos = target.position;
				Vector3 diff = currentPos - _previousPos;
				_startPosition = diff + _startPosition;
				Vector3 newPos = _startPosition + new Vector3(x,y,0);
				target.position= newPos;
				_previousPos = newPos;
//				#else
//				_target->setPosition(_startPosition + Vec2(x,y));
//				#endif // !CC_ENABLE_STACKABLE_ACTIONS
			}
		}
		

	
	protected	Vector3           _startPosition;
		protected	Vector3           _delta;
		protected float           _height;
		protected int             _jumps;
		protected Vector3           _previousPos;
		
	};







	




}