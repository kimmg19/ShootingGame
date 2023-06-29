using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//11-8~11
public class ScrollViewSnap : MonoBehaviour {
    public List<GameObject> item;
    public float[] distance;
    public GameObject center;
    public float minDistance;
    public int minItemNum;
    float itemDistance;
    bool isDragging = true;
    public RectTransform contentRect;
    public int menuSelect;
    bool firstStrat = true;
    void Start() {
        int buttonLenght = item.Count;
        distance = new float[buttonLenght];
    }

    void Update() {
        float item1Pos = item[1].GetComponent<RectTransform>().anchoredPosition.x;
        float item0Pos = item[0].GetComponent<RectTransform>().anchoredPosition.x;
        itemDistance = Mathf.Abs(item0Pos - item1Pos);
        if (itemDistance == 0) {
            return;
        }
        if (firstStrat == true) {
            menuSelect = GameDataScript.instance.select;
            minItemNum = menuSelect;
            if (minItemNum != 0) {
                contentRect.anchoredPosition =
                    new Vector3(minItemNum * -itemDistance, 0, 0);
            }
            firstStrat = false;
        }
        for (int i = 0; i < item.Count; i++) {
            //Abs-절댓값 찾는 함수.i=0일떄 center 값0, item값 5...distance[i]는 -5의 절댓값
            distance[i] = Mathf.Abs(center.transform.position.x -
                item[i].transform.position.x);

        }
        //배열 중 가장 작은 값and center와 가장 가까운 값
        minDistance = Mathf.Min(distance);

        for (int i = 0; i < item.Count; i++) {
            if (minDistance == distance[i]) {
                minItemNum = i;
            }
        }
        if (minItemNum != menuSelect) {
            GameDataScript.instance.select = minItemNum;
            menuSelect = minItemNum;
        }
        if (!isDragging) {
            float targetPos = minItemNum * -itemDistance;
            float newX = Mathf.Lerp(contentRect.anchoredPosition.x, targetPos,
                Time.deltaTime * 10);
            Vector2 newPosition = new Vector2(newX, contentRect.anchoredPosition.y);
            contentRect.anchoredPosition = newPosition;
        }

    }
    public void StartDrag() {
        isDragging = true;
    }
    public void EndDrag() {
        isDragging = false;
    }
}
