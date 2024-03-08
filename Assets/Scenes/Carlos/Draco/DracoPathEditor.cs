using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DracoPath))]
public class DracoPathEditor : Editor {

    public override void OnInspectorGUI() {
        DracoPath dp = target as DracoPath;

        if (dp.path.Count < 2) EditorGUILayout.HelpBox("At least two nodes must exist to display a path!", MessageType.Warning);
        if (GUILayout.Button("Add Node")) {
            GameObject newNodeGO = Instantiate(dp.EDITOR_nodePrefab, dp.transform.position, dp.transform.rotation, dp.transform);
            Selection.objects = new Object[] { newNodeGO };
            DracoPathNode newNode = newNodeGO.GetComponentInChildren<DracoPathNode>(true);
            dp.path.Add(newNode);
            newNode.Init(dp);
            dp.EDITOR_ChainList();
        }
        if (GUILayout.Button("Chain List")) dp.EDITOR_ChainList();

        base.OnInspectorGUI();
        GUI.color = new Vector4(0.99f, 0.825f, 0.825f, 1);
        if (GUILayout.Button("Chain Children")) {
            dp.path = new();
            foreach (Transform child in dp.transform) {
                DracoPathNode potentialNode = child.GetComponentInChildren<DracoPathNode>(true);
                if (potentialNode != null) dp.path.Add(potentialNode);
            }
            dp.EDITOR_ChainList();
        }
        GUI.color = Color.white;
    }
}