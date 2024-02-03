using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarNodeVisuals : MonoBehaviour
{
    public int nodeNum;
    [SerializeField] private float nodeHighlightTime;

    [SerializeField] private GameObject unselected;
    [SerializeField] private GameObject selected;
    [SerializeField] private GameObject goodOutline;
    [SerializeField] private GameObject badOutline;

    private void Awake() {
        DeselectNode();
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
        if(num != nodeNum) return;
        SelectNode();
    }

    private void SelectNode() {
        selected?.SetActive(true);
        unselected?.SetActive(false);
    }

    private void OnSpellCast(object sender, StarDrawLogic.OnSpellCastArgs e)
    {
        if(e.spellType == SpellType.NONE)
            StartCoroutine(OnFail());
        else
            StartCoroutine(OnSuccess());
    }

    private IEnumerator OnSuccess() {
        goodOutline?.SetActive(true);
        yield return new WaitForSeconds(nodeHighlightTime);
        goodOutline?.SetActive(false);
        DeselectNode();
    }

    private IEnumerator OnFail() {
        badOutline?.SetActive(true);
        yield return new WaitForSeconds(nodeHighlightTime);
        badOutline?.SetActive(false);
        DeselectNode();
    }

    private void DeselectNode() {
        selected?.SetActive(false);
        unselected?.SetActive(true);
        badOutline?.SetActive(false);
        badOutline?.SetActive(false);
    }
}
