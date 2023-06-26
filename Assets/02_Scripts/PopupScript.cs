using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

public class PopupScript : MonoBehaviour {

    private event Onclick onYesClick;
    private event Onclick onNoClick;
    public Button yesButton;
    public Button noButton;
    public Text titleText;
    public Text detailText;


    public void SetYesListener(Onclick onClick) {
        this.onYesClick += onClick;
    }

    public void SetNoListener(Onclick onClick) {
        this.onNoClick += onClick;
    }
    void PopupYesAction() {
        onYesClick();
        Destroy(gameObject);
    }

    void PopupNoAction() {
        onNoClick();
        Destroy(gameObject);
    }

    private void Start() {
        yesButton.onClick.AddListener(PopupYesAction);
        noButton.onClick.AddListener(PopupNoAction);

    }

}
