using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DracoPathNode))]
public class DracoPathNodeEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        DracoPathNode node = target as DracoPathNode;
        if (node.dp == null) {
            EditorGUILayout.HelpBox("Parent Path is Not Assigned. Node is invalid!"
                                    + "Please assign a parent path below ↓", MessageType.Error);
            node.dp = EditorGUILayout.ObjectField("Draco Path", node.dp, typeof(DracoPath), true) as DracoPath;
            if (GUILayout.Button("Assign From Parent")) node.dp = node.GetComponentInParent<DracoPath>(true);

            if (node.dp != null) {
                if (!node.dp.path.Contains(node)) node.dp.path.Add(node);
                node.dp.EDITOR_ChainList();
            }
        } else {
            if (GUILayout.Button("Select Draco Path")) Selection.objects = new Object[] { node.dp.gameObject };
        }
    }
}