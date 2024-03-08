using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DracoConstellationBrain : MonoBehaviour {

    [SerializeField] private GameObject headNodePrefab;
    [SerializeField] private GameObject constellationNodePrefab;
    [SerializeField] private DracoPath dracoPath;
    [SerializeField] private int nodeAmount;
    [SerializeField] private float nodeSpawnDelay;

    void Start() {
        StartCoroutine(DragonSpawn());
    }

    private IEnumerator DragonSpawn() {
        int tempAmount = nodeAmount;
        while (tempAmount > 0) {
            GameObject nodeGO = tempAmount == nodeAmount ? Instantiate(headNodePrefab, transform.position, transform.rotation, transform)
                                                         : Instantiate(constellationNodePrefab, transform.position, transform.rotation, transform);
            DracoConstellationNode node = nodeGO.GetComponentInChildren<DracoConstellationNode>(true);
            node.Init(dracoPath.GetStartPoint());
            tempAmount--;
            yield return new WaitForSeconds(nodeSpawnDelay);
        }
    }
}
