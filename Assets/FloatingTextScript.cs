using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextScript : MonoBehaviour
{
    public Text floatingText;
    float time = 0;
    public float maxTime = 2;
    public float speed = 2;
    Color32 targetColor;
    public void Init() {
        time = 0;
        floatingText.color = new Color32(255, 0, 0, 255);
    }
    void Start()
    {
        floatingText.color = new Color32(255, 0, 0, 255);
        targetColor = new Color32(255, 0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position += Vector3.up * speed * Time.deltaTime;
        floatingText.color=Color.Lerp(floatingText.color, targetColor, Time.deltaTime*2);
        if (time > maxTime) {
            DestroyGameObject();
        }
    }
    public void DestroyGameObject() {
        ObjectPoolManager.instance.floatingText.Destroy(gameObject);
    }
}
