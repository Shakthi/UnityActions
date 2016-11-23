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
        Sequence q = new Sequence(new UIFadeOut(),new UIFadeIn());
        Action.Run(textTest, q);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
