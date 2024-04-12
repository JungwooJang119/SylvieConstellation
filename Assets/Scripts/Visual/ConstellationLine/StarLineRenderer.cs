using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLineRenderer : MonoBehaviour
{
    [SerializeField] private GameObject NodeParent;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float scale;
    [SerializeField] private float waitTime;
    private Dictionary<int, Vector2> nodeToPos;

    [SerializeField] private Color regularInsideColor;
    private List<int> constellationPositions;
    private List<GameObject> constellationLines;

    private void Awake() {
        constellationLines = new List<GameObject>();
        int numChildren = NodeParent.transform.childCount;
        nodeToPos = new Dictionary<int, Vector2>();
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
        constellationPositions.Add(nodeNum);
        if (constellationPositions.Count > 1)
        {
            GameObject lineGameObject = Instantiate(linePrefab, Camera.main.transform.position + Vector3.forward * 10, Quaternion.identity);
            constellationLines.Add(lineGameObject);
            ConstellationLines currentLine = lineGameObject.GetComponent<ConstellationLines>();
            currentLine.point1 = nodeToPos[constellationPositions[^2]];
            currentLine.point2 = nodeToPos[constellationPositions[^1]];
            currentLine.inverseSize = 6.5f;
            currentLine.color = regularInsideColor;
        }
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
        constellationPositions = new List<int>();
        foreach (GameObject c in constellationLines)
        {
            Destroy(c);
        }
        constellationLines = new List<GameObject>();
    }

    private void UndoLR() 
    {
        if (lineRenderer.positionCount > 0)
        {
            lineRenderer.positionCount--;
        }
        constellationPositions.RemoveAt(constellationPositions.Count - 1);
        Destroy(constellationLines[^1]);
        constellationLines.RemoveAt(constellationLines.Count - 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UndoLR();
        }
    }
}
