using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WobblyLine : MonoBehaviour {

    private DracoConstellationPoint start;
    private DracoConstellationPoint end;
    private LineRenderer lr;

    private void Start() {
        lr = GetComponent<LineRenderer>();
    }

    public void Connect() {
        
    }

    void Update() {
        
    }
}

public class DracoConstellationHandler : MonoBehaviour {

}

public class DracoConstellationPoint : MonoBehaviour {

    private Button button;
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick() {

    }
}