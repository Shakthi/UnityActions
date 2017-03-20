using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using CC;

public class TestUI : MonoBehaviour
{
    public Transform textTest;
    // Use this for initialization
    void Start()
    {
        Sequence q = new Sequence(new UIFadeTo(0.8f,0.5f),new UIFadeTo(0.5f,1.0f) );
        Action.Run(textTest,new RepeatForever (q) );

        //Actor anActor = Actor.GetActor(textTest);
        //anActor.UIFadeOut(1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
