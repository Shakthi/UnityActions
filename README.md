# Unity Actions

Unity Actions is a port of Cocos2d actions to unity.

Actions are one of the powerfull feature of cocos2d that I always missed in Unity3d. I never satisfied with iTween or related library. This code line by line port of original cocos2d  (Cosos2d-X actually) code.

With actions, making an animation may not be simpler than this.
```c#
Action.Run(aTransform,new MoveBy(10 ,new Vector3(10,1,1)));
```
Above code move/transforms 'aTransform' by Vector3(10,1,1) units in 10 seconds. Now this is basic one, more complex example follows.
```c#
  Sequence seq= new Sequence(new MoveBy(3 ,new Vector3(10,1,1)),
								new RotateBy(5,new Vector3(180,0,0)),
								    new MoveBy(3 ,new Vector3(-10,1,1)),
								       new RotateBy(3,new Vector3(0,60,0))
							);
							
  Action.Run(movingObject, new Repeat(seq,2));

```
That must be self-explanatory.

# Welcome Actors!
Power of cocos2d actions and Unity3d coroutines can be nicely combined to produce yet more powerfull feature called **Actors**. Right now it can be thought as syntax sugar over action.   

```c#
public  Transform movingObject;

IEnumerator anAnimation()
{
	Actor  anActor = Actor.GetActor(movingObject);
	yield return anActor.MoveBy(3,new Vector3(10,10,10));

	Debug.Log("Now cube must be at the top of the screen");

	yield return anActor.MoveTo(4,new Vector3(0,0,0));

	Debug.Log("Both the action completed now. Must be completed at 4+3=7 seconds");
}

void Start()
{
    StartCoroutine(anAnimation());
}
```
Code  you see here is *actually* part of **Actor** testcase! Isn't it intutive, and as simple as *screenwriting*.
## Contribution
There is a  lot of sugar in cocos2d in waiting to be ported to Unity3d! All contribution are eagerly accepted.
## Todos

 - Write Testcases
 - Port CCInstantActions,CCTransitions...
 - Add Code Comments


## License
Apache License
---
**Free Software, Hell Yeah!** -- hehe markdown porked from http://dillinger.io


- [Cocos2d iphone project ](https://github.com/cocos2d/cocos2d-objc)
- [Cosco2d-X project](https://github.com/cocos2d/cocos2d-x)

