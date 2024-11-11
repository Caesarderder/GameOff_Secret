using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public Camera rightCamera;           // 右侧玩家的相机
    public RawImage rightRawImage;       // 右侧玩家的 RawImage

    public GameObject Go1;
    public GameObject Go2;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 检查点击是否在 RawImage 上
            Vector2 localMousePos;
            RectTransform rectTransform = rightRawImage.rectTransform;
            Vector3 localPos = Input.mousePosition - Vector3.right * 960f;
            //    //用于将全局屏幕坐标转换为 RawImage 的局部坐标。
            //    //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localMousePos);

            //    //Debug.Log(Input.mousePosition + "  => " + localMousePos);

            if (rectTransform.rect.Contains(localPos))
            {
                //        //// 计算在右侧 RawImage 的比例位置
                //        //float normalizedX = (localMousePos.x - rectTransform.rect.x) / rectTransform.rect.width;
                //        //float normalizedY = (localMousePos.y - rectTransform.rect.y) / rectTransform.rect.height;

                //        //// 将比例位置转换为右侧相机的屏幕空间位置
                //        //Vector3 cameraScreenPos = new Vector3(normalizedX * Screen.width, normalizedY * Screen.height, 0);

                // 使用右侧相机从点击位置发射射线
                Ray ray = rightCamera.ScreenPointToRay(localPos);
                Debug.Log(ray.GetPoint(0f) + " " + ray.GetPoint(2f));
                Go1.transform.position = ray.GetPoint(0f);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit object: " + hit.transform.name);
                    Go2.transform.position = hit.transform.position;
                }
                else
                    Go2.transform.position = ray.GetPoint(2f);
            }
        }
    }
}

