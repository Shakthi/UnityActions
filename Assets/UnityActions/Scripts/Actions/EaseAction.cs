using UnityEngine;
using System.Collections;

namespace CC
{

public abstract class  ActionEase : ActionInterval 

{	protected ActionInterval inner;

		public ActionInterval getInnerAction()
		{
			return inner;
		}



		public ActionEase(ActionInterval inner )
		{
			this.inner = inner;
			this.duration = inner.GetDuration();
		}

	

		public override void StartWithTarget(Transform inTarget)
		{
			base.StartWithTarget(inTarget);
			inner.StartWithTarget(inTarget);
		}

		public override void Stop()
		{
			
			inner.Stop();
			base.Stop();
		}





}


	/** 
 @brief Base class for Easing actions with rate parameters
 @ingroup Actions
 */
	public abstract class  EaseRateAction :  ActionEase
	{
		
		/** set rate value for the actions */
		public float Rate
		{
			get { return m_fRate; }
			set { m_fRate = value; }
		}


		/** Initializes the action with the inner action and the rate parameter */
		public EaseRateAction(ActionInterval pAction, float rate):base(pAction)
		{
			m_fRate = rate;
			
		}

		public ActionInterval reverse()
		{
			return null;
		}


		float m_fRate;
	}




public	class  EaseIn :  EaseRateAction
{

	/** 
     @brief Create the action with the inner action and the rate parameter.
     @param action The pointer of the inner action.
     @param rate The value of the rate parameter.
     @return A pointer of EaseIn action. If creation failed, return nil.
    */
	public EaseIn(ActionInterval action, float rate):base(action, rate)
	{

	}




	public override Action Clone() 
	{
		// no copy constructor
			if (inner!=null)
				return new EaseIn((ActionInterval)inner.Clone(), this.Rate);

		return null;
	}

	public override void LerpAction(float dt)
	{
			inner.LerpAction(tweenfunc.easeIn(dt, this.Rate));
	}

		public override Action Reverse() 
	{
			return new EaseIn((ActionInterval)inner.Reverse(), 1f / this.Rate);
	}


};





	/** 
 @class EaseOut
 @brief EaseOut action with a rate.
 @details The timeline of inner action will be changed by:
         \f${ time }^ { (1/rate) }\f$.
 @ingroup Actions
 */
	public class  EaseOut :  EaseRateAction
	{
		

		public EaseOut(ActionInterval action, float rate):base(action, rate)
		{

		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseOut((ActionInterval)inner.Clone(), this.Rate);

			return null;
		}

		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.easeOut(dt, this.Rate));
		}

		public override Action Reverse() 
		{
			return new EaseOut((ActionInterval)inner.Reverse(), 1f / this.Rate);
		}
	};






	/** 
 @class EaseInOut
 @brief EaseInOut action with a rate
 @details If time * 2 < 1, the timeline of inner action will be changed by:
         \f$0.5*{ time }^{ rate }\f$.
         Else, the timeline of inner action will be changed by:
         \f$1.0-0.5*{ 2-time }^{ rate }\f$.
 @ingroup Actions
 */
	public class  EaseInOut :  EaseRateAction
	{

		/** 
     @brief Create the action with the inner action and the rate parameter.
     @param action The pointer of the inner action.
     @param rate The value of the rate parameter.
     @return A pointer of EaseInOut action. If creation failed, return nil.
    */
		public EaseInOut(ActionInterval action, float rate):base(action, rate)
		{}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseInOut((ActionInterval)inner.Clone(), this.Rate);

			return null;
		}

		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.easeInOut(dt, this.Rate));
		}

		public override Action Reverse() 
		{
			return new EaseInOut((ActionInterval)inner.Reverse(), 1f / this.Rate);
		}
	};







/** 
 @class EaseExponentialIn
 @brief Ease Exponential In action.
 @details The timeline of inner action will be changed by:
         \f${ 2 }^{ 10*(time-1) }-1*0.001\f$.
 @ingroup Actions
 */
public class  EaseExponentialIn :  ActionEase
{

	/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseExponentialIn action. If creation failed, return nil.
    */
	public EaseExponentialIn(ActionInterval action):base(action)
	{}

	public override Action Clone() 
	{
		// no copy constructor
		if (inner!=null)
			return new EaseExponentialIn((ActionInterval)inner.Clone());

		return null;
	}

	public override void LerpAction(float dt)
	{
			inner.LerpAction(tweenfunc.expoEaseIn(dt));
	}

	public override Action Reverse() 
	{
		return new EaseExponentialIn((ActionInterval)inner.Reverse());
	}
};




	/** 
 @class EaseExponentialOut
 @brief Ease Exponential Out
 @details The timeline of inner action will be changed by:
         \f$1-{ 2 }^{ -10*(time-1) }\f$.
 @ingroup Actions
 */
	public class  EaseExponentialOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseExponentialOut action. If creation failed, return nil.
    */
		public EaseExponentialOut(ActionInterval action):base(action)
		{}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseExponentialOut((ActionInterval)inner.Clone());

			return null;
		}

		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.expoEaseOut(dt));
		}

		public override Action Reverse() 
		{
			return new EaseExponentialOut((ActionInterval)inner.Reverse());
		}
	};








/** 
 @class EaseExponentialInOut
 @brief Ease Exponential InOut
 @details If time * 2 < 1, the timeline of inner action will be changed by:
         \f$0.5*{ 2 }^{ 10*(time-1) }\f$.
         else, the timeline of inner action will be changed by:
         \f$0.5*(2-{ 2 }^{ -10*(time-1) })\f$.
 @ingroup Actions
 */
public class  EaseExponentialInOut :  ActionEase
{

	/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseExponentialInOut action. If creation failed, return nil.
    */
	public EaseExponentialInOut(ActionInterval action):base(action)
	{}

	public override Action Clone() 
	{
		// no copy constructor
		if (inner!=null)
			return new EaseExponentialInOut((ActionInterval)inner.Clone());

		return null;
	}

	public override void LerpAction(float dt)
	{
		inner.LerpAction(tweenfunc.expoEaseInOut(dt));
	}

	public override Action Reverse() 
	{
		return new EaseExponentialInOut((ActionInterval)inner.Reverse());
	}
};





	/** 
 @class EaseElastic
 @brief Ease Elastic abstract class
 @since v0.8.2
 @ingroup Actions
 */
	public abstract  class  EaseElastic :  ActionEase
	{

		/**
     @brief Set period of the wave in radians.
     @param fPeriod The value will be set.
    */
		public float Period
		{
			get {
				return _period;
			}
			set {
				_period = value;
			}
		}

		/**ram action The pointer of the inner action.
     @param period Period of the wave in radians. Default is 0.3.
     @return Return true when the initialization success, otherwise return false.
    */
		public	EaseElastic(ActionInterval interval,  float period=5f):base(interval)
		{
			_period = period;
		}


		protected float _period;


	};






	/** 
 @class EaseElasticIn
 @brief Ease Elastic In action.
 @details If time == 0 or time == 1, the timeline of inner action will not be changed.
         Else, the timeline of inner action will be changed by:
         \f$-{ 2 }^{ 10*(time-1) }*sin((time-1-\frac { period }{ 4 } )*\pi *2/period)\f$.
 @warning This action doesn't use a bijective function.
          Actions like Sequence might have an unexpected result when used with this action.
 @since v0.8.2
 @ingroup Actions
 */
	public class  EaseElasticIn :  EaseElastic
	{

		/** 
     @brief Create the EaseElasticIn action with the inner action and the period in radians.
     @param action The pointer of the inner action.
     @param period Period of the wave in radians.
     @return A pointer of EaseElasticIn action. If creation failed, return nil.
    */


		public EaseElasticIn(ActionInterval interval,  float period):base(interval,period)
		{
			
		}




		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.expoEaseIn(dt));
		}



		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseElasticIn((ActionInterval)inner.Clone(),this.Period);

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseElasticIn((ActionInterval)inner.Reverse(),this.Period);
		}


	};





	public class  EaseElasticOut :  EaseElastic
	{

		/** 
     @brief Create the EaseElasticIn action with the inner action and the period in radians.
     @param action The pointer of the inner action.
     @param period Period of the wave in radians.
     @return A pointer of EaseElasticIn action. If creation failed, return nil.
    */


		public EaseElasticOut(ActionInterval interval,  float period):base(interval,period)
		{

		}




		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.expoEaseOut(dt));
		}



		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseElasticOut((ActionInterval)inner.Clone(),this.Period);

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseElasticOut((ActionInterval)inner.Reverse(),this.Period);
		}


	};







	public class  EaseElasticInOut :  EaseElastic
	{

		/** 
     @brief Create the EaseElasticIn action with the inner action and the period in radians.
     @param action The pointer of the inner action.
     @param period Period of the wave in radians.
     @return A pointer of EaseElasticIn action. If creation failed, return nil.
    */


		public EaseElasticInOut(ActionInterval interval,  float period):base(interval,period)
		{

		}




		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.expoEaseInOut(dt));
		}



		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseElasticInOut((ActionInterval)inner.Clone(),this.Period);

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseElasticInOut((ActionInterval)inner.Reverse(),this.Period);
		}


	};













	public abstract class  EaseBounce :  ActionEase
	{


		public	EaseBounce(ActionInterval interval):base(interval)
		{
		}

		// Overrides







	};





	public  class  EaseBounceIn :  ActionEase
	{


		public	EaseBounceIn(ActionInterval interval):base(interval)
		{
		}

		// Overrides



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.bounceEaseIn(dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseBounceIn((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseBounceIn((ActionInterval)inner.Reverse());
		}

	};





	public  class  EaseBounceOut :  ActionEase
	{


		public	EaseBounceOut(ActionInterval interval):base(interval)
		{
		}

		// Overrides



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.bounceEaseOut(dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseBounceOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseBounceOut((ActionInterval)inner.Reverse());
		}

	};







	public  class  EaseBounceInOut :  ActionEase
	{


		public	EaseBounceInOut(ActionInterval interval):base(interval)
		{
		}

		// Overrides



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.bounceEaseInOut(dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseBounceInOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseBounceInOut((ActionInterval)inner.Reverse());
		}

	};



	/** 
 @class EaseBackIn
 @brief EaseBackIn action.
 @warning This action doesn't use a bijective function.
          Actions like Sequence might have an unexpected result when used with this action.
 @since v0.8.2
 @ingroup Actions
 */
	public class  EaseBackIn :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseBackIn action. If creation failed, return nil.
    */
		public	EaseBackIn(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.backEaseIn(dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseBackIn((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseBackIn((ActionInterval)inner.Reverse());
		}

	};

	/** 
 @class EaseBackOut
 @brief EaseBackOut action.
 @warning This action doesn't use a bijective function.
          Actions like Sequence might have an unexpected result when used with this action.
 @since v0.8.2
 @ingroup Actions
 */
	public class  EaseBackOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseBackOut action. If creation failed, return nil.
    */
		public	EaseBackOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.backEaseOut(dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseBackOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseBackOut((ActionInterval)inner.Reverse());
		}
	};

	/** 
 @class EaseBackInOut
 @brief EaseBackInOut action.
 @warning This action doesn't use a bijective function.
          Actions like Sequence might have an unexpected result when used with this action.
 @since v0.8.2
 @ingroup Actions
 */
	public class  EaseBackInOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseBackInOut action. If creation failed, return nil.
    */
		public	EaseBackInOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.backEaseInOut(dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseBackInOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseBackInOut((ActionInterval)inner.Reverse());
		}
	};






	/** 
@class EaseBezierAction
@brief Ease Bezier
@ingroup Actions
*/
	public class  EaseBezierAction :  ActionEase
	{


		/**
    @brief Set the bezier parameters.
    */

		public void SetBezierParamer( float p0, float p1, float p2, float p3)
		{
			_p0 = p0;
			_p1 = p1;
			_p2 = p2;
			_p3 = p3;
		}





		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseBezierAction action. If creation failed, return nil.
    */
		public	EaseBezierAction(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.bezieratFunction(_p0,_p1,_p2,_p3,dt));
		}

		public override Action Clone() 
		{
			// no copy constructor



			if (inner!=null)
			{
				EaseBezierAction action = new EaseBezierAction((ActionInterval)inner.Clone());
				action.SetBezierParamer(_p0,_p1,_p2,_p3);
				return action;
			}

			return null;
		}



		public override Action Reverse() 
		{

			if (inner!=null)
			{
				EaseBezierAction action = new EaseBezierAction((ActionInterval)inner.Clone());
				action.SetBezierParamer(_p3,_p2,_p1,_p0);
				return action;
			}

			return null;

		}

		float _p0;
		float _p1;
		float _p2;
		float _p3;


	};





/** 
@class EaseQuadraticActionIn
@brief Ease Quadratic In
@ingroup Actions
*/
public class  EaseQuadraticActionIn :  ActionEase
{

	/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuadraticActionIn action. If creation failed, return nil.
    */
	public	EaseQuadraticActionIn(ActionInterval interval):base(interval)
	{
	}



	public override void LerpAction(float dt)
	{
			inner.LerpAction(tweenfunc.quadEaseIn (dt));
	}

	public override Action Clone() 
	{
		// no copy constructor
		if (inner!=null)
			return new EaseQuadraticActionIn((ActionInterval)inner.Clone());

		return null;
	}



	public override Action Reverse() 
	{
		return new EaseQuadraticActionIn((ActionInterval)inner.Reverse());
	}

};





	/** 
@class EaseQuadraticActionOut
@brief Ease Quadratic Out
@ingroup Actions
*/
	public	class  EaseQuadraticActionOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuadraticActionOut action. If creation failed, return nil.
    */
		public	EaseQuadraticActionOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quadEaseOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuadraticActionOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuadraticActionOut((ActionInterval)inner.Reverse());
		}

	};





	/** 
@class EaseQuadraticActionInOut
@brief Ease Quadratic InOut
@ingroup Actions
*/
	public	class  EaseQuadraticActionInOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuadraticActionInOut action. If creation failed, return nil.
    */

		public	EaseQuadraticActionInOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quadEaseInOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuadraticActionInOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuadraticActionInOut((ActionInterval)inner.Reverse());
		}
	};





	/** 
@class EaseQuarticActionIn
@brief Ease Quartic In
@ingroup Actions
*/
	public	class  EaseQuarticActionIn :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionIn action. If creation failed, return nil.
    */
		public	EaseQuarticActionIn(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quartEaseIn (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuarticActionIn((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuarticActionIn((ActionInterval)inner.Reverse());
		}
	};




	/** 
@class EaseQuarticActionOut
@brief Ease Quartic Out
@ingroup Actions
*/
	public class  EaseQuarticActionOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionOut action. If creation failed, return nil.
    */
		public	EaseQuarticActionOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quartEaseIn (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuarticActionOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuarticActionOut((ActionInterval)inner.Reverse());
		}
	};





	/** 
@class EaseQuarticActionInOut
@brief Ease Quartic InOut
@ingroup Actions
*/
	public class  EaseQuarticActionInOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseQuarticActionInOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quartEaseInOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuarticActionInOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuarticActionInOut((ActionInterval)inner.Reverse());
		}
	};





	public class  EaseQuinticActionIn :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseQuinticActionIn(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quintEaseIn (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuinticActionIn((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuinticActionIn((ActionInterval)inner.Reverse());
		}
	};

	public class  EaseQuinticActionOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseQuinticActionOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quintEaseOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuinticActionOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuinticActionOut((ActionInterval)inner.Reverse());
		}
	};



	public class  EaseQuinticActionInOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseQuinticActionInOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.quintEaseInOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseQuinticActionInOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseQuinticActionInOut((ActionInterval)inner.Reverse());
		}
	};






	public class  EaseCircleActionIn :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseCircleActionIn(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.circEaseIn (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseCircleActionIn((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseCircleActionIn((ActionInterval)inner.Reverse());
		}
	};

	public class  EaseCircleActionOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseCircleActionOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.circEaseOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseCircleActionOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseCircleActionOut((ActionInterval)inner.Reverse());
		}
	};








	public class  EaseCircleActionInOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseCircleActionInOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.circEaseInOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseCircleActionInOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseCircleActionInOut((ActionInterval)inner.Reverse());
		}
	};






	public class  EaseCubicActionIn :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseCubicActionIn(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.cubicEaseIn (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseCubicActionIn((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseCubicActionIn((ActionInterval)inner.Reverse());
		}
	};




	public class  EaseCubicActionOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseCubicActionOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.cubicEaseOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseCubicActionOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseCubicActionOut((ActionInterval)inner.Reverse());
		}
	};



	public class  EaseCubicActionInOut :  ActionEase
	{

		/** 
     @brief Create the action with the inner action.
     @param action The pointer of the inner action.
     @return A pointer of EaseQuarticActionInOut action. If creation failed, return nil.
    */
		public	EaseCubicActionInOut(ActionInterval interval):base(interval)
		{
		}



		public override void LerpAction(float dt)
		{
			inner.LerpAction(tweenfunc.cubicEaseInOut (dt));
		}

		public override Action Clone() 
		{
			// no copy constructor
			if (inner!=null)
				return new EaseCubicActionInOut((ActionInterval)inner.Clone());

			return null;
		}



		public override Action Reverse() 
		{
			return new EaseCubicActionInOut((ActionInterval)inner.Reverse());
		}
	};


}

