using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DracoLine : MonoBehaviour {

    [SerializeField] private float connectionSpeed;
    [SerializeField] private Vector2 minMaxWidth;
    [SerializeField] private ParticleSystem parSys, signalStart, signalEnd, collapseStart, collapseEnd;
    private DracoConstellationPoint start;
    private DracoConstellationPoint end;
    private LineRenderer lr;
    private EdgeCollider2D coll;

    public bool IsConnected { get; private set; }
    private enum State {
        Idle,
        Connecting,
        Disconneting,
    } private State state = State.Idle;

    private MaterialPropertyBlock lineMaterialPB;
    private readonly int shDensity = Shader.PropertyToID("_Density");
    private float lineDensity = 1;

    void Awake() {
        lineMaterialPB = new();
        lr = GetComponent<LineRenderer>();
        coll = GetComponent<EdgeCollider2D>();
        coll.enabled = false;
    }

    public void Connect(DracoConstellationPoint start, DracoConstellationPoint end) {
        IsConnected = true;
        this.start = start;
        this.end = end;
        state = State.Connecting;
        var emission = parSys.emission;
        emission.enabled = true;
        coll.enabled = true;
        SetLineColor(Color.white, 0.15f);
    }

    public void Disconnect() {
        IsConnected = false;
        state = State.Disconneting;
        var emission = parSys.emission;
        emission.enabled = false;
        coll.enabled = false;
        SpawnConnectionFlash(collapseStart, collapseEnd);
        SetLineColor(Color.red, 0.15f);
    }

    public bool IsRelated(DracoConstellationPoint cp) => cp == start || cp == end;

    void Update() {
        if (start == null || end == null) return;
        if (state == State.Connecting) {
            if (lineDensity > 0) {
                lineDensity = Mathf.MoveTowards(lineDensity, 0, Time.deltaTime * connectionSpeed);
                lineMaterialPB.SetFloat(shDensity, lineDensity / 2);
                lr.SetPropertyBlock(lineMaterialPB);
            } else {
                state = State.Idle;
                var emission = parSys.emission;
                emission.enabled = false;
                SpawnConnectionFlash(signalStart, signalEnd);
            }
            Vector3 startPos = start.transform.position;
            Vector3 endPos = Vector3.Lerp(startPos, end.transform.position, 1 - lineDensity);
            lr.SetPositions(new Vector3[] { startPos, endPos });
            parSys.transform.position = endPos;
        } else lr.SetPositions(new Vector3[] { start.transform.position, end.transform.position });
        if (state == State.Disconneting) {
            lineDensity = Mathf.MoveTowards(lineDensity, 1, Time.deltaTime * connectionSpeed / 2);
            lineMaterialPB.SetFloat(shDensity, lineDensity);
            lr.SetPropertyBlock(lineMaterialPB);
            if (Mathf.Approximately(lineDensity, 1)) {
                state = State.Idle;
                start = null;
                end = null;
            }
        }
        SetLineWidth(WidthLerp(Mathf.Abs(Mathf.Cos(Time.time * 2))));
        if (coll.enabled) AdjustCollider();
    }

    private float WidthLerp(float lerpVal) => Mathf.Lerp(minMaxWidth.x, minMaxWidth.y, lerpVal);
    private void SetLineWidth(float width) {
        lr.startWidth = width;
        lr.endWidth = width;
    }
    private void SetLineColor(Color color, float duration) {
        DOTween.To(() => lr.startColor, val => lr.startColor = val, color, duration);
        DOTween.To(() => lr.endColor, val => lr.endColor = val, color, duration);
    }

    private void AdjustCollider() {
        List<Vector2> edges = new();
        for (int i = 0; i < lr.positionCount; i++) {
            Vector2 lrPos = lr.GetPosition(i) - transform.position;
            edges.Add(new Vector2(lrPos.x, lrPos.y));
        }
        coll.SetPoints(edges);
    }

    private void SpawnConnectionFlash(ParticleSystem startPar, ParticleSystem endPar) {
        if (start == null || end == null) return;
        startPar.gameObject.SetActive(true);
        startPar.transform.SetParent(start.transform);
        startPar.transform.localPosition = Vector2.zero;
        startPar.Play();
        endPar.gameObject.SetActive(true);
        endPar.transform.SetParent(end.transform);
        endPar.transform.localPosition = Vector2.zero;
        endPar.Play();
    }
}