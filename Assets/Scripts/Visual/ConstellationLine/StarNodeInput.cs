using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarNodeInput : NodeInput
{
    public int nodeNum;

    public void OnClickButton() {
        base.InvokeInputPress(this, nodeNum);
        print("yeah");
    }

    public void OnHoverButton() {
        base.InvokeInputPress(this, nodeNum);
    }
}
