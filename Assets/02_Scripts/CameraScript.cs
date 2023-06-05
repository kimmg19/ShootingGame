using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ȭ���� 16:9�� ������Ű�� ��ũ��Ʈ.
���غ��ٴ� ��������̴�.

 */
public class CameraScript : MonoBehaviour
{
    private void Awake()
    {
        Camera camera=GetComponent<Camera>();
        Rect rect = camera.rect;
        float ratio=(float)Screen.width/Screen.height;     //���� ȭ���� �ػ�
        float scaleWidth = ratio / ((float)16 / 9);
        float scaleHeight=1 / scaleWidth;
        if(scaleWidth < 1)
        {
            rect.height = scaleWidth;
            rect.y = (1 - scaleWidth) / 2;

        }
        else
        {
            rect.width = scaleHeight;
            rect.x = (1 - scaleHeight) / 2;
        }
        camera.rect = rect;
    }
}
