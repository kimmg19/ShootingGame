using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*화면을 16:9로 고정시키는 스크립트.
이해보다는 사용위주이다.
Gamescene-Main camera
MenuScene-Main camera
 */

public class CameraScript : MonoBehaviour
{ 
    private void Awake()
    {
        Camera camera=GetComponent<Camera>();
        Rect rect = camera.rect;
        float ratio=(float)Screen.width/Screen.height;     //현재 화면의 해상도
        float scaleWidth = ratio / ((float)16 / 9);
        float scaleHeight=1 / scaleWidth;
        //화면이 세로로 길쭉하게 -5:4 등등 
        if (scaleWidth < 1)
        {
            rect.height = scaleWidth;
            rect.y = (1 - scaleWidth) / 2;

        }
        //화면이 가로로 넓게 -19:9 등등
        else
        {
            rect.width = scaleHeight;
            rect.x = (1 - scaleHeight) / 2;
        }
        camera.rect = rect;
    }
}
