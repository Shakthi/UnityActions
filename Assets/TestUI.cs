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
        Sequence q = new Sequence(new UIFadeOut() );
        Action.Run(textTest, q);

        //Actor anActor = Actor.GetActor(textTest);
        //anActor.UIFadeOut(1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
