using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnCallAndroid : MonoBehaviour
{
    public Text num;
    public Text changedNum;
    public Text staticNum;
    public Text changedStaticNum;
    public Text returnStr;
    public Text returnStatic;



    // Start is called before the first frame update
    void Start()
    {
        //初始化交互类
        using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            //获取Activity对象,此时获取的是MainActivity
            using(AndroidJavaObject ajo=ajc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                //成员变量
                int i = ajo.Get<int>("testI");
                num.text=i.ToString();
                ajo.Set<int>("testI", 11);
                i = ajo.Get<int>("testI");
                changedNum.text=i.ToString();
                //静态变量
                int staticI = ajo.GetStatic<int>("testStaticI");
                staticNum.text=staticI.ToString();
                ajo.SetStatic<int>("testStaticI",22);
                staticI = ajo.GetStatic<int>("testStaticI");
                changedStaticNum.text=staticI.ToString();
                //成员方法
                string funStr =ajo.Call<string>("TestFun");
                returnStr.text=funStr;
                string staticStr = ajo.CallStatic<string>("TestStaticFun");
                returnStatic.text=staticStr;

            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
