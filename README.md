# Unity Actions

Unity Actions is a port of Cocos2d actions to unity.

Actions are one of the powerfull feature of cocos2d that I always missed in Unity3d. I never satisfied with iTween or related library. This code line by line port of original cocos2d  (Cosos2d-X actually) code.

With actiobs making an animation may not be simpler than this.
```c#
Action.Run(aTransform,new MoveBy(10 ,new Vector3(10,1,1)));
```
Above code move transforms 'aTransform' by Vector3(10,1,1) in 10 seconds. Now this basic one, more complex example follows.
```c#
  Sequence q= new Sequence(new MoveBy(3 ,new Vector3(10,1,1)),
											new RotateBy(5,new Vector3(180,0,0)),
		            						 new MoveBy(3 ,new Vector3(-10,1,1)),
		           								new RotateBy(3,new Vector3(0,60,0))

		                         );
		Action.Run(movingObject, new Repeat(q,2));

```