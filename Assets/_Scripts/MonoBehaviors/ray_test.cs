using UnityEngine;
using System.Collections;

public class ray_test: MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {//判断鼠标是否按下
            // 从摄像机开始，到屏幕触摸点，发出一条射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 撞击到了哪个3D物体
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);//打印出碰撞到的节点的名字
            }
        }
    }
}