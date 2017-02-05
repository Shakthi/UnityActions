using UnityEngine;
using System.Collections;
using CC;

public class TestActions : MonoBehaviour
{
    public Transform movingObject;

    IEnumerator anAnimation()
    {
        Actor anActor = Actor.GetActor(movingObject);
        yield return anActor.MoveBy(3, new Vector3(10, 10, 10));

        Debug.Log("Now cube must be at the top of the screen");

        yield return anActor.MoveTo(4, new Vector3(0, 0, 0));

        Debug.Log("Both the action completed now must be completed at 4+3=7 seconds");
    }


    IEnumerator ShowDemo()
    {
        yield return StartCoroutine(anAnimation());

        Debug.Log("Actor demo done. Now exicuting two actions parallely ");

        Action.Run(movingObject, new MoveBy(10, new Vector3(10, 1, 1)));
        Action.Run(movingObject, new RotateBy(5, new Vector3(180, 0, 0)));

        yield return new WaitForSeconds(11);

        Debug.Log(" Now executing sequence and repeat-2 actions  ");
        ResetMovingObject();
        Sequence q = new Sequence(new MoveBy(3, new Vector3(3, 1, 1)),
                                              new RotateBy(5, new Vector3(180, 0, 0)),
                                              new MoveBy(3, new Vector3(-3, 1, 1)),
                                                    new RotateBy(3, new Vector3(0, 60, 0))

                                  );
        Action.Run(movingObject, new Repeat(q, 2));
        yield return new WaitForSeconds(30);

        Debug.Log(" Now exicuting Spawn actions  ");
        ResetMovingObject();
        Action.Run(movingObject, new Spawn(new MoveBy(10, new Vector3(2, 1, 1)), new MoveBy(10, new Vector3(0, 3, 0)), new RotateBy(10, new Vector3(0, 180, 0))));
        yield return new WaitForSeconds(10);

        Debug.Log(" All tests are completed ");
    }

    IEnumerator StartTestingActions()
    {
        //yield return StartCoroutine(anAnimation());
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Testing action fade to..");
        Action.Run(movingObject, new FadeTo(1.0f, 0.0f));

    }

    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;

    void ResetMovingObject()
    {
        movingObject.position = originalPosition;
        movingObject.rotation = originalRotation;
        movingObject.localScale = originalScale;
    }


    void Start()
    {
        originalPosition = movingObject.position;
        originalRotation = movingObject.rotation;
        originalScale = movingObject.localScale;

        StartCoroutine(ShowDemo());
        //StartCoroutine(StartTestingActions());
        //Action.Run(movingObject, new FadeTo(1.0f, 0.0f));
    }
}
