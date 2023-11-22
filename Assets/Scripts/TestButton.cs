using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestButton : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            print("按钮点击了");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
