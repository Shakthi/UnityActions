using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

/// <summary>
/// 
/// </summary>
namespace CC
{
    public class TransitionScene : MonoBehaviour
    {
        private Image img;
        public float duration { get; set; }
        /// Use this for initialization

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void ChangeScene()
        {
            String sceneToLoad = "" + PlayerPrefs.GetString("SceneToLoad");
            bool result = sceneToLoad.Equals("", StringComparison.Ordinal);
            if (result)
            {
                sceneToLoad = SceneManager.GetActiveScene().name;
            }
            SceneManager.LoadScene(sceneToLoad);
        }
        public virtual void changeScene(String sceneName)
        {

        }
    }

    public class TransitionFade : TransitionScene
    {
        public static void create(float t, String sceneName)
        {
            //Lets create the img that 
            GameObject canvasScreen = new GameObject("canvasScreen");
            Canvas c = canvasScreen.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasScreen.AddComponent<CanvasScaler>();
            canvasScreen.AddComponent<GraphicRaycaster>();
            GameObject image = new GameObject("Image");
            //image.AddComponent<Image>().rectTransform.localScale = new Vector2(Screen.width, Screen.height);

            image.AddComponent<Image>().rectTransform.sizeDelta = new Vector2(Screen.width * 1.5f, Screen.height * 1.5f);
            image.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
            Image img = image.GetComponent<Image>();
            image.transform.SetParent(canvasScreen.transform);
            img.color = new Color(0f, 0f, 0f, 0.0f);
            //canvasScreen.transform.SetParent (  ) ;
            //DontDestroyOnLoad(canvasScreen);
            UnityEngine.GameObject.DontDestroyOnLoad(canvasScreen);
            CC.Sequence seq = new CC.Sequence(new CC.UImageFadeTo(t / 2, 1.0f)
                , new CC.CallFunc( () =>
                {
                    //String sceneToLoad = "" + PlayerPrefs.GetString("SceneToLoad");
                    String sceneToLoad = "" + sceneName;
                    SceneManager.LoadScene(sceneToLoad);
                    //base.
                })
                , new CC.UImageFadeTo(t / 2, 0f)
                , new CC.CallFunc(() => { Destroy(canvasScreen); })
                );
            CC.Action.Run(img.transform, seq);
        }

        public override void changeScene(String nameScene)
        {
            base.changeScene(nameScene);
        }
    }
}