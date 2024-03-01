using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DracoPath : MonoBehaviour {
    public List<DracoPathNode> path = new();

    public DracoPathNode GetStartPoint() {
        if (path == null || path.Count == 0) throw new System.Exception("The path does not exist or is empty;");
        return path[0];
    }

    #if UNITY_EDITOR
    public GameObject EDITOR_nodePrefab;
    public void OnDrawGizmosSelected() {
        if (path.Count < 2) return;
        UnityEditor.Handles.color = Color.green;
        for (int i = 0; i < path.Count; i++) {
            if (path[i] == null) {
                path.Remove(path[i]);
                continue;
            } 
            if (path[i].GetNext() == null) continue;
            if (i > 0) UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawDottedLine(path[i].transform.position, path[i].GetNext().transform.position, 5f);
            if (path[i].next == null) {
                UnityEditor.Handles.color = Color.blue;
            } UnityEditor.Handles.DrawSolidDisc(path[i].GetNext().transform.position, Vector3.forward, 0.2f);
            if (path[i].next == null) {
                UnityEditor.Handles.Label(path[i].GetNext().transform.position, "Start", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
            }
        }
        Gizmos.color = Color.white;
    }

    public void EDITOR_ChainList() {
        for (int i = 0; i < path.Count; i++) {
            if (i + 1 == path.Count) {
                path[i].next = null;
            } else path[i].next = path[i + 1];
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
    #endif
}