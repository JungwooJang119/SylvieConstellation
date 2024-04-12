using UnityEngine;

[System.Serializable]
public class DracoPathNode : MonoBehaviour {
    public DracoPath dp;
    public DracoPathNode next;
    public Vector2 position => transform.position;

    public void Init(DracoPath dp) => this.dp = dp;

    public DracoPathNode GetNext() => next == null 
                                      ? (dp.path.Count > 0 
                                         ? dp.path[0] : null) : next;

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if (dp == null) {
            UnityEditor.Handles.Label(transform.position, "No Path Assigned");
        } else dp.OnDrawGizmosSelected();
    }
    #endif
}