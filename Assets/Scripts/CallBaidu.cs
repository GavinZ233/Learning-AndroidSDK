using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallBaidu : MonoBehaviour
{
    public Text lat;
    public Text lng;
    public Text country;
    public Text province;
    public Text city;
    public Text addr;
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    ajo.Call<string>("GetAddr");
                }
            }

        });
    }

    public void SetInfo(string info)
    {
        string[] ss = info.Split('_');
        lat.text = ss[0];
        lng.text = ss[1];
        country.text = ss[2];
        province.text = ss[3];
        city.text = ss[4];
        addr.text = ss[5];

    }

}
