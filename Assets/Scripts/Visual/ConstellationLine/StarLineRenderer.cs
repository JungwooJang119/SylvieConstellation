using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLineRenderer : MonoBehaviour
{
    [SerializeField] private GameObject NodeParent;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float scale;
    [SerializeField] private float waitTime;
    private Dictionary<int, Vector2> nodeToPos = new Dictionary<int, Vector2>();

    private void Awake() {
        int numChildren = NodeParent.transform.childCount;
        for(int i=0; i < numChildren; i++){
            Transform child = NodeParent.transform.GetChild(i);
            if(!child.gameObject.activeSelf) break;
            int nodeNum = child.GetComponentInChildren<StarNodeVisuals>().nodeNum;
            Vector2 pos = child.GetComponent<RectTransform>().position;
            // pos += offset;
            // pos /= scale;
            nodeToPos[nodeNum] = pos;
        }

        ResetLR();
    }


     private void OnEnable() {
        StarDrawLogic.OnNodeSelected += OnNodeSelected;
        StarDrawLogic.OnSpellCast += OnSpellCast;
    }

    private void OnDisable() {
        StarDrawLogic.OnNodeSelected -= OnNodeSelected;
        StarDrawLogic.OnSpellCast -= OnSpellCast;
    }

    private void OnNodeSelected(object sender, int num)
    {
        AddLRPoint(num);
    }

    private void AddLRPoint(int nodeNum)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, nodeToPos[nodeNum]);
    }

    private void OnSpellCast(object sender, StarDrawLogic.OnSpellCastArgs e)
    {
        StartCoroutine(DelayLRReset());
    }

    private IEnumerator DelayLRReset()
    {
        yield return new WaitForSeconds(1f);
        ResetLR();
    }

    private void ResetLR()
    {
        lineRenderer.positionCount = 0;
    }

    // private void UndoLR() 
    // {
    //     if (lineRenderer.positionCount > 0)
    //     {
    //         lineRenderer.positionCount--;
    //     }
    // }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.U))
    //     {
    //         UndoLR();
    //     }
    // }
}
