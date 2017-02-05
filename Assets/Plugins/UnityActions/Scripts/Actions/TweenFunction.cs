using UnityEngine;
using System.Collections;

namespace CC
{

    static public class tweenfunc
    {

        public enum TweenType
        {
            CUSTOM_EASING = -1,

            Linear,

            Sine_EaseIn,
            Sine_EaseOut,
            Sine_EaseInOut,


            Quad_EaseIn,
            Quad_EaseOut,
            Quad_EaseInOut,

            Cubic_EaseIn,
            Cubic_EaseOut,
            Cubic_EaseInOut,

            Quart_EaseIn,
            Quart_EaseOut,
            Quart_EaseInOut,

            Quint_EaseIn,
            Quint_EaseOut,
            Quint_EaseInOut,

            Expo_EaseIn,
            Expo_EaseOut,
            Expo_EaseInOut,

            Circ_EaseIn,
            Circ_EaseOut,
            Circ_EaseInOut,

            Elastic_EaseIn,
            Elastic_EaseOut,
            Elastic_EaseInOut,

            Back_EaseIn,
            Back_EaseOut,
            Back_EaseInOut,

            Bounce_EaseIn,
            Bounce_EaseOut,
            Bounce_EaseInOut,

            TWEEN_EASING_MAX = 10000
        };




        public static float tweenTo(float time, TweenType type, float[] easingParam)
        {
            float delta = 0;

            switch (type)
            {
                case TweenType.CUSTOM_EASING:
                    delta = customEase(time, easingParam);
                    break;

                case TweenType.Linear:
                    delta = linear(time);
                    break;

                case TweenType.Sine_EaseIn:
                    delta = sineEaseIn(time);
                    break;
                case TweenType.Sine_EaseOut:
                    delta = sineEaseOut(time);
                    break;
                case TweenType.Sine_EaseInOut:
                    delta = sineEaseInOut(time);
                    break;

                case TweenType.Quad_EaseIn:
                    delta = quadEaseIn(time);
                    break;
                case TweenType.Quad_EaseOut:
                    delta = quadEaseOut(time);
                    break;
                case TweenType.Quad_EaseInOut:
                    delta = quadEaseInOut(time);
                    break;

                case TweenType.Cubic_EaseIn:
                    delta = cubicEaseIn(time);
                    break;
                case TweenType.Cubic_EaseOut:
                    delta = cubicEaseOut(time);
                    break;
                case TweenType.Cubic_EaseInOut:
                    delta = cubicEaseInOut(time);
                    break;

                case TweenType.Quart_EaseIn:
                    delta = quartEaseIn(time);
                    break;
                case TweenType.Quart_EaseOut:
                    delta = quartEaseOut(time);
                    break;
                case TweenType.Quart_EaseInOut:
                    delta = quartEaseInOut(time);
                    break;

                case TweenType.Quint_EaseIn:
                    delta = quintEaseIn(time);
                    break;
                case TweenType.Quint_EaseOut:
                    delta = quintEaseOut(time);
                    break;
                case TweenType.Quint_EaseInOut:
                    delta = quintEaseInOut(time);
                    break;

                case TweenType.Expo_EaseIn:
                    delta = expoEaseIn(time);
                    break;
                case TweenType.Expo_EaseOut:
                    delta = expoEaseOut(time);
                    break;
                case TweenType.Expo_EaseInOut:
                    delta = expoEaseInOut(time);
                    break;

                case TweenType.Circ_EaseIn:
                    delta = circEaseIn(time);
                    break;
                case TweenType.Circ_EaseOut:
                    delta = circEaseOut(time);
                    break;
                case TweenType.Circ_EaseInOut:
                    delta = circEaseInOut(time);
                    break;

                case TweenType.Elastic_EaseIn:
                    {
                        float period = 0.3f;
                        if (null != easingParam)
                        {
                            period = easingParam[0];
                        }
                        delta = elasticEaseIn(time, period);
                    }
                    break;
                case TweenType.Elastic_EaseOut:
                    {
                        float period = 0.3f;
                        if (null != easingParam)
                        {
                            period = easingParam[0];
                        }
                        delta = elasticEaseOut(time, period);
                    }
                    break;
                case TweenType.Elastic_EaseInOut:
                    {
                        float period = 0.3f;
                        if (null != easingParam)
                        {
                            period = easingParam[0];
                        }
                        delta = elasticEaseInOut(time, period);
                    }
                    break;


                case TweenType.Back_EaseIn:
                    delta = backEaseIn(time);
                    break;
                case TweenType.Back_EaseOut:
                    delta = backEaseOut(time);
                    break;
                case TweenType.Back_EaseInOut:
                    delta = backEaseInOut(time);
                    break;

                case TweenType.Bounce_EaseIn:
                    delta = bounceEaseIn(time);
                    break;
                case TweenType.Bounce_EaseOut:
                    delta = bounceEaseOut(time);
                    break;
                case TweenType.Bounce_EaseInOut:
                    delta = bounceEaseInOut(time);
                    break;

                default:
                    delta = sineEaseInOut(time);
                    break;
            }

            return delta;
        }

        // Linear
        public static float linear(float time)
        {
            return time;
        }


        // Sine Ease
        public static float sineEaseIn(float time)
        {
            return -1 * Mathf.Cos(time * (float)(Mathf.PI / 2f)) + 1;
        }

        public static float sineEaseOut(float time)
        {
            return Mathf.Sin(time * (float)(Mathf.PI / 2f));
        }

        public static float sineEaseInOut(float time)
        {
            return -0.5f * (Mathf.Cos((float)(Mathf.PI) * time) - 1);
        }


        // Quad Ease
        public static float quadEaseIn(float time)
        {
            return time * time;
        }

        public static float quadEaseOut(float time)
        {
            return -1 * time * (time - 2);
        }

        public static float quadEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time;
            --time;
            return -0.5f * (time * (time - 2) - 1);
        }



        // Cubic Ease
        public static float cubicEaseIn(float time)
        {
            return time * time * time;
        }
        public static float cubicEaseOut(float time)
        {
            time -= 1;
            return (time * time * time + 1);
        }
        public static float cubicEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time * time;
            time -= 2;
            return 0.5f * (time * time * time + 2);
        }


        // Quart Ease
        public static float quartEaseIn(float time)
        {
            return time * time * time * time;
        }

        public static float quartEaseOut(float time)
        {
            time -= 1;
            return -(time * time * time * time - 1);
        }

        public static float quartEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time * time * time;
            time -= 2;
            return -0.5f * (time * time * time * time - 2);
        }


        // Quint Ease
        public static float quintEaseIn(float time)
        {
            return time * time * time * time * time;
        }

        public static float quintEaseOut(float time)
        {
            time -= 1;
            return (time * time * time * time * time + 1);
        }

        public static float quintEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time * time * time * time;
            time -= 2;
            return 0.5f * (time * time * time * time * time + 2);
        }


        // Expo Ease
        public static float expoEaseIn(float time)
        {
            return time == 0 ? 0 : Mathf.Pow(2, 10 * (time / 1 - 1)) - 1 * 0.001f;
        }
        public static float expoEaseOut(float time)
        {
            return time == 1 ? 1 : (-Mathf.Pow(2, -10 * time / 1) + 1);
        }
        public static float expoEaseInOut(float time)
        {
            time /= 0.5f;
            if (time < 1)
            {
                time = 0.5f * Mathf.Pow(2, 10 * (time - 1));
            }
            else
            {
                time = 0.5f * (-Mathf.Pow(2, -10 * (time - 1)) + 2);
            }

            return time;
        }


        // Circ Ease
        public static float circEaseIn(float time)
        {
            return -1 * (Mathf.Sqrt(1 - time * time) - 1);
        }
        public static float circEaseOut(float time)
        {
            time = time - 1;
            return Mathf.Sqrt(1 - time * time);
        }
        public static float circEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return -0.5f * (Mathf.Sqrt(1 - time * time) - 1);
            time -= 2;
            return 0.5f * (Mathf.Sqrt(1 - time * time) + 1);
        }


        // Elastic Ease
        public static float elasticEaseIn(float time, float period)
        {

            float newT = 0;
            if (time == 0 || time == 1)
            {
                newT = time;
            }
            else
            {
                float s = period / 4;
                time = time - 1;
                newT = -Mathf.Pow(2, 10 * time) * Mathf.Sin((time - s) * ((Mathf.PI) * 2f) / period);
            }

            return newT;
        }
        public static float elasticEaseOut(float time, float period)
        {

            float newT = 0;
            if (time == 0 || time == 1)
            {
                newT = time;
            }
            else
            {
                float s = period / 4;
                newT = Mathf.Pow(2, -10 * time) * Mathf.Sin((time - s) * ((Mathf.PI) * 2f) / period) + 1;
            }

            return newT;
        }
        public static float elasticEaseInOut(float time, float period)
        {

            float newT = 0;
            if (time == 0 || time == 1)
            {
                newT = time;
            }
            else
            {
                time = time * 2;
                if (period == 0)
                {
                    period = 0.3f * 1.5f;
                }

                float s = period / 4;

                time = time - 1;
                if (time < 0)
                {
                    newT = -0.5f * Mathf.Pow(2, 10 * time) * Mathf.Sin((time - s) * ((Mathf.PI) * 2f) / period);
                }
                else
                {
                    newT = Mathf.Pow(2, -10 * time) * Mathf.Sin((time - s) * ((Mathf.PI) * 2f) / period) * 0.5f + 1;
                }
            }
            return newT;
        }


        // Back Ease
        public static float backEaseIn(float time)
        {
            float overshoot = 1.70158f;
            return time * time * ((overshoot + 1) * time - overshoot);
        }
        public static float backEaseOut(float time)
        {
            float overshoot = 1.70158f;

            time = time - 1;
            return time * time * ((overshoot + 1) * time + overshoot) + 1;
        }
        public static float backEaseInOut(float time)
        {
            float overshoot = 1.70158f * 1.525f;

            time = time * 2;
            if (time < 1)
            {
                return (time * time * ((overshoot + 1) * time - overshoot)) / 2;
            }
            else
            {
                time = time - 2;
                return (time * time * ((overshoot + 1) * time + overshoot)) / 2 + 1;
            }
        }



        // Bounce Ease
        public static float bounceTime(float time)
        {
            if (time < 1 / 2.75)
            {
                return 7.5625f * time * time;
            }
            else if (time < 2 / 2.75)
            {
                time -= 1.5f / 2.75f;
                return 7.5625f * time * time + 0.75f;
            }
            else if (time < 2.5 / 2.75)
            {
                time -= 2.25f / 2.75f;
                return 7.5625f * time * time + 0.9375f;
            }

            time -= 2.625f / 2.75f;
            return 7.5625f * time * time + 0.984375f;
        }
        public static float bounceEaseIn(float time)
        {
            return 1 - bounceTime(1 - time);
        }

        public static float bounceEaseOut(float time)
        {
            return bounceTime(time);
        }

        public static float bounceEaseInOut(float time)
        {
            float newT = 0;
            if (time < 0.5f)
            {
                time = time * 2;
                newT = (1 - bounceTime(1 - time)) * 0.5f;
            }
            else
            {
                newT = bounceTime(time * 2 - 1) * 0.5f + 0.5f;
            }

            return newT;
        }


        // Custom Ease
        public static float customEase(float time, float[] easingParam)
        {
            if (easingParam != null)
            {
                float tt = 1 - time;
                return easingParam[1] * tt * tt * tt + 3 * easingParam[3] * time * tt * tt + 3 * easingParam[5] * time * time * tt + easingParam[7] * time * time * time;
            }
            return time;
        }

        public static float easeIn(float time, float rate)
        {
            return Mathf.Pow(time, rate);
        }

        public static float easeOut(float time, float rate)
        {
            return Mathf.Pow(time, 1 / rate);
        }

        public static float easeInOut(float time, float rate)
        {
            time *= 2;
            if (time < 1)
            {
                return 0.5f * Mathf.Pow(time, rate);
            }
            else
            {
                return (1.0f - 0.5f * Mathf.Pow(2 - time, rate));
            }
        }

        public static float quadraticIn(float time)
        {
            return Mathf.Pow(time, 2);
        }

        public static float quadraticOut(float time)
        {
            return -time * (time - 2);
        }

        public static float quadraticInOut(float time)
        {

            float resultTime = time;
            time = time * 2;
            if (time < 1)
            {
                resultTime = time * time * 0.5f;
            }
            else
            {
                --time;
                resultTime = -0.5f * (time * (time - 2) - 1);
            }
            return resultTime;
        }

        public static float bezieratFunction(float a, float b, float c, float d, float t)
        {
            return (Mathf.Pow(1 - t, 3) * a + 3 * t * (Mathf.Pow(1 - t, 2)) * b + 3 * Mathf.Pow(t, 2) * (1 - t) * c + Mathf.Pow(t, 3) * d);
        }

    }

}