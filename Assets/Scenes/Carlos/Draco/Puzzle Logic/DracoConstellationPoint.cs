using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MouseEvent { Enter, Exit, Click }

public class DracoConstellationPoint : MonoBehaviour, IPointerEnterHandler,
    IPointerExitHandler, IPointerClickHandler {

    public event System.Action<MouseEvent> OnMouseEvent;

    #region | Puzzle Logic |
    [SerializeField] private List<DracoConstellationPoint> defaultConnections;
    private HashSet<DracoConstellationPoint> connections;
    [SerializeField] private bool isHead;
    public HashSet<DracoConstellationPoint> Connections => connections;
    public bool IsHead => isHead;

    private DracoConstellationHandler handler;
    public int NodeID { get; private set; }
    #endregion

    #region | Animation Reference |
    public bool IsSelected => handler.SelectedNode == this;
    public bool IsAlluded { get; private set; }
    public bool IsDone { get; private set; }
    #endregion

    void Awake() {
        connections = defaultConnections == null ? new HashSet<DracoConstellationPoint>()
                                                 : new HashSet<DracoConstellationPoint>(defaultConnections);
    }

    public void Init(DracoConstellationHandler handler, int nodeID) {
        this.handler = handler;
        handler.OnNodeSelection += Handler_OnNodeSelection;
        NodeID = nodeID;
    }

    private void Handler_OnNodeSelection(HashSet<DracoConstellationPoint> set) {
        IsAlluded = set == null ? false : set.Contains(this);
        IsDone = connections.All(node => handler.CertifyConnection(this, node));
        OnMouseEvent?.Invoke(MouseEvent.Click);
    }

    public void OnPointerEnter(PointerEventData data) => OnMouseEvent?.Invoke(MouseEvent.Enter);

    public void OnPointerExit(PointerEventData data) => OnMouseEvent?.Invoke(MouseEvent.Exit);

    public void OnPointerClick(PointerEventData data) {
        if (!IsDone) handler.SignalNode(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        DracoLine dr;
        if ((dr = collider.GetComponent<DracoLine>()) != null
            && !dr.IsRelated(this)) {
            dr.Disconnect();
            handler.SilentSignal();
        }
    }

    public DracoConstellationLink HashLink(DracoConstellationPoint nodePoint) {
        nodePoint.Connections.Add(this);
        connections.Add(nodePoint);
        return new DracoConstellationLink(this, nodePoint);
    }
}