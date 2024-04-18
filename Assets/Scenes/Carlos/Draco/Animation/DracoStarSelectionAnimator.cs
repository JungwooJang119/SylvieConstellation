using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class DracoStarSelectionAnimator : MonoBehaviour {

    [SerializeField] private StarSelectionPropertyBlock[] statePropertyArray;

    private DracoConstellationPoint cp;
    private Image image;
    private float rotationSpeed;

    public enum State {
        HoverOn,
        HoverOut,
        Allude,
        SelectOn,
        SelectOut,
    } private State state = State.HoverOut;
    private Dictionary<State, StarSelectionPropertyBlock> stateMap = new();

    private class LapseCore {
        public event System.Action OnLapseComplete;

        TweenerCore<Color, Color, ColorOptions> colorCore;
        TweenerCore<Vector3, Vector3, VectorOptions> scaleCore;
        TweenerCore<float, float, FloatOptions> rotationCore;

        public LapseCore(TweenerCore<Color, Color, ColorOptions> colorCore,
                                 TweenerCore<Vector3, Vector3, VectorOptions> scaleCore,
                                 TweenerCore<float, float, FloatOptions> rotationCore) {
            this.colorCore = colorCore;
            this.scaleCore = scaleCore;
            this.rotationCore = rotationCore;
            this.scaleCore.onComplete += ScaleCore_OnComplete;
        }

        public void Sync() {
            colorCore.Kill();
            scaleCore.Kill();
            rotationCore.Kill();
            scaleCore.onComplete -= ScaleCore_OnComplete;
        } private void ScaleCore_OnComplete() => OnLapseComplete?.Invoke();
    } private LapseCore lapseCore;

    void Awake() {
        cp = GetComponentInParent<DracoConstellationPoint>(true);
        if (!cp) throw new System.InvalidOperationException("Selection Animator must be childed to a Constellation Point;");
        cp.OnMouseEvent += ConstellationPoint_OnMouseEvent;
        image = GetComponent<Image>();
        BuildStateMap(statePropertyArray);
        ForceState(State.HoverOut);
    }

    void Update() => transform.Rotate(new Vector3(0, 0, rotationSpeed));

    /// <summary> 
    /// Control Signal for a Moore State Machine;
    /// <br></br> Will include a link to the diagram later;
    /// </summary>
    private void ConstellationPoint_OnMouseEvent(MouseEvent eventType) {
        if (cp.IsDone) DOState(State.HoverOut);
        else {
            State defaultState = cp.IsSelected ? State.SelectOut
                                   : cp.IsAlluded ? State.Allude
                                                  : State.HoverOut;
            switch (eventType) {
                case MouseEvent.Enter:
                    switch (defaultState) {
                        case State.HoverOut:
                        case State.Allude:
                            // Go to Hover On with I;
                            state = State.HoverOn;
                            DOState(State.HoverOn);
                            break;
                        case State.SelectOut:
                            // Go to Select On with I;
                            state = State.SelectOn;
                            DOState(State.SelectOn);
                            break;
                    }
                    break;
                case MouseEvent.Exit:
                    // Go to Default State with I;
                    state = defaultState;
                    DOState(defaultState);
                    break;
                case MouseEvent.Click:
                    // Initialize Bump protocol regardless of case;
                    switch (defaultState) {
                        case State.HoverOut:
                            if (state == State.SelectOn) {
                                // Go to Hover Out with I, and S (and *puff* VFXs);
                                state = State.HoverOut;
                                DOState(State.HoverOut);
                            } else goto case State.Allude;
                            break;
                        case State.Allude:
                            // Go to Default State with I;
                            state = defaultState;
                            DOState(defaultState);
                            break;
                        case State.SelectOut:
                            if (state == State.HoverOn) {
                                // Go to Select On with I, R, and S;
                                state = State.SelectOn;
                                DOState(State.SelectOn);
                            }
                            break;
                    }
                    break;
            }
        }
    }

    private void DOState(State state) {
        if (lapseCore != null) {
            lapseCore.Sync();
            lapseCore.OnLapseComplete -= LapseCore_OnLapseComplete;
        } StarSelectionPropertyBlock nsspb = stateMap[state];
        TweenerCore<Color, Color, ColorOptions> colorCore = image.DOColor(nsspb.Color, nsspb.LapseTime);
        TweenerCore<Vector3, Vector3, VectorOptions> scaleCore = transform.DOScale(Vector2.one * nsspb.Scale, nsspb.LapseTime);
        if (nsspb.Scale < stateMap[State.HoverOut].Scale) scaleCore.SetEase(Ease.OutBounce);
        TweenerCore<float, float, FloatOptions> rotationCore = DOTween.To(() => rotationSpeed, val => rotationSpeed = val, nsspb.RotationSpeed, nsspb.LapseTime);
        lapseCore = new LapseCore(colorCore, scaleCore, rotationCore);
        lapseCore.OnLapseComplete += LapseCore_OnLapseComplete;
    }

    private void LapseCore_OnLapseComplete() {
        if (image.sprite != stateMap[state].Sprite) {
            image.sprite = stateMap[state].Sprite;
            StartCoroutine(_SmallBump(transform.parent));
        }
    }

    private IEnumerator _SmallBump(Transform transform) {
        if (transform == null) yield break;
        transform.DOScale(transform.localScale * 1.25f, 0.15f);
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(transform.localScale / 1.25f, 0.25f);
    }

    private void BuildStateMap(StarSelectionPropertyBlock[] arr) {
        foreach (StarSelectionPropertyBlock sspb in arr) {
            stateMap[sspb.State] = sspb;
        }
    }

    private void ForceState(State state) {
        this.state = state;
        StarSelectionPropertyBlock sspb = stateMap[state];
        image.color = sspb.Color;
        transform.localScale = Vector2.one * sspb.Scale;
        rotationSpeed = sspb.RotationSpeed;
    }
}

[System.Serializable]
public class StarSelectionPropertyBlock {
    [SerializeField] private DracoStarSelectionAnimator.State state;
    public DracoStarSelectionAnimator.State State => state;
    [SerializeField] private float lapseTime;
    public float LapseTime => lapseTime;
    [SerializeField] private Color color;
    public Color Color => color;
    [SerializeField] private float scale;
    public float Scale => scale;
    [SerializeField] private float rotationSpeed;
    public float RotationSpeed => rotationSpeed;
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
}