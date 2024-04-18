using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DracoConstellationBrain))]
public class DracoConstellationHandler : MonoBehaviour {

    public event System.Action<HashSet<DracoConstellationPoint>> OnNodeSelection;
    private DracoConstellationBrain puzzleBrain;

    [SerializeField] private DracoLine prefabLine;
    private DracoConstellationPoint previousNode;
    private Dictionary<DracoConstellationLink, DracoLine> connectionMap = new();
    private int nodeCount;
    private DracoConstellationPoint selectedNode;
    public DracoConstellationPoint SelectedNode => selectedNode;

    void Start() {
        puzzleBrain = GetComponent<DracoConstellationBrain>();
        puzzleBrain.OnNodeCreated += InitializePoint;
    }

    private void ValidatePuzzleStatus() {
        if (connectionMap.All(kvp => kvp.Value.IsConnected)) puzzleBrain.DeclarePuzzleEnd(true);
    }

    private void InitializePoint(DracoConstellationNode node) {
        DracoConstellationPoint[] nodePoints = node.GetComponentsInChildren<DracoConstellationPoint>(true);
        if (nodePoints.Length == 0) Debug.LogWarning("Attempted to initialize a node without puzzle logic");
        foreach (DracoConstellationPoint nodePoint in nodePoints) { nodePoint.Init(this, nodeCount++); }
        foreach (DracoConstellationPoint nodePoint in nodePoints) { ProcessLinks(nodePoint); }
    }

    private void ProcessLinks(DracoConstellationPoint nodePoint) {
        if (nodePoint.Connections != null) {
            foreach (var connection in nodePoint.Connections) { Link(nodePoint, connection); }
        }
        if (!nodePoint.IsHead) {
            Link(previousNode, nodePoint);
            previousNode = nodePoint;
        }
    }

    private void Link(DracoConstellationPoint nodePoint1, DracoConstellationPoint nodePoint2) {
        if (nodePoint1 == null || nodePoint2 == null) return;
        DracoConstellationLink key = nodePoint1.HashLink(nodePoint2);
        if (!connectionMap.ContainsKey(key)) connectionMap[key] = Instantiate(prefabLine, transform);
    }

    public void SignalNode(DracoConstellationPoint cp) {
        if (selectedNode == null) SetSelectedNode(cp);
        else {
            if (selectedNode.Connections.Contains(cp)) {
                DracoLine connectionLine = connectionMap[selectedNode.HashLink(cp)];
                if (!connectionLine.IsConnected) {
                    connectionLine.Connect(selectedNode, cp);
                    selectedNode = null;
                    OnNodeSelection?.Invoke(null);
                    ValidatePuzzleStatus();
                } else SetSelectedNode(cp);
            } else SetSelectedNode(cp);
        }
    }

    public void SilentSignal() => OnNodeSelection?.Invoke(selectedNode == null ? null : selectedNode.Connections);

    public bool CertifyConnection(DracoConstellationPoint cp1, DracoConstellationPoint cp2) {
        return connectionMap[new DracoConstellationLink(cp1, cp2)].IsConnected;
    }

    private void SetSelectedNode(DracoConstellationPoint selectedNode) {
        this.selectedNode = selectedNode;
        HashSet<DracoConstellationPoint> pendingConnections = selectedNode.Connections
                                         .Where(node => !connectionMap[new DracoConstellationLink(node, SelectedNode)].IsConnected)
                                         .ToHashSet();
        OnNodeSelection?.Invoke(pendingConnections);
    }
}
