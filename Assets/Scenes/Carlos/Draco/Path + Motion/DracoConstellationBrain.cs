using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DracoConstellationBrain : MonoBehaviour {

    public event System.Action<bool> OnPuzzleEnd;
    public event System.Action<DracoConstellationNode> OnNodeCreated;

    [SerializeField] private GameObject headNodePrefab;
    [SerializeField] private GameObject constellationNodePrefab;
    [SerializeField] private DracoPath dracoPath;
    [SerializeField] private int nodeAmount;
    [SerializeField] private float nodeSpawnDelay;

    public void BeginPuzzle() => StartCoroutine(_DragonSpawn());

    private IEnumerator _DragonSpawn() {
        int tempAmount = nodeAmount;
        while (tempAmount > 0) {
            GameObject nodeGO = tempAmount == nodeAmount ? Instantiate(headNodePrefab, transform.position, transform.rotation, transform)
                                                         : Instantiate(constellationNodePrefab, transform.position, transform.rotation, transform);
            DracoConstellationNode node = nodeGO.GetComponentInChildren<DracoConstellationNode>(true);
            node.Init(dracoPath.GetStartPoint());
            OnNodeCreated?.Invoke(node);
            tempAmount--;
            yield return new WaitForSeconds(nodeSpawnDelay);
        }
    }

    public void DeclarePuzzleEnd(bool success) => OnPuzzleEnd?.Invoke(success);
}
