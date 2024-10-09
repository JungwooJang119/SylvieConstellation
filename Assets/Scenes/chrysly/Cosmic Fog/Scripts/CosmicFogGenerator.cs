using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CosmicFogGenerator : MonoBehaviour {
    public bool enableGeneration = true;
    
    [Header("Path Fog (Function)")]
    [SerializeField] private Transform goal;
    [SerializeField] private CosmicFogAgent agent;
    #region Object Pool

    [SerializeField] private int pathFogCount = 15;
    [SerializeField] private int steps = 10;
    [SerializeField] private float pathSpawnDelay = 0.5f;
    [SerializeField] private GameObject pathFogPrefab;
    [SerializeField] private float pathLength = 20f;
    [SerializeField] private Vector2 pathSizeRange = new Vector2(1f, 3f);
    [SerializeField] private Vector2 pathLifetimeRange = new Vector2(3f, 6f);
    private CosmicFog[] _pathFogList;
    private IEnumerator _pathFogSpawnAction;
    private int _fogIndex = 0;

    [Header("Emitter Fog (Extra)")] [SerializeField]
    private int emitterCountPerPath = 3;
    private CosmicFog[] _emitterFogList;
    private int _emitterIndex = 0;
    private int _emitterMax;

    #endregion Object Pool

    private void Start() {
        _pathFogList = new CosmicFog[pathFogCount];
        _emitterMax = pathFogCount * emitterCountPerPath;
        _emitterFogList = new CosmicFog[_emitterMax];
        for (int i = 0; i < pathFogCount; i++) {
            _pathFogList[i] = Instantiate(pathFogPrefab, transform).GetComponent<CosmicFog>();
        }
        for (int i = 0; i < _emitterMax; i++) {
            _emitterFogList[i] = Instantiate(pathFogPrefab, transform).GetComponent<CosmicFog>();
        }
    }

    private void Update() {
        if (enableGeneration) ActivateFog();
    }

    private Vector3 CalculateStepSize() {
        Vector3 normalizedDistance = (goal.position - agent.transform.position).normalized;
        normalizedDistance *= pathLength;
        return normalizedDistance / steps;
    }

    private void ActivateFog() {
        if (_pathFogSpawnAction == null) {
            _pathFogSpawnAction = ActivateFogAction();
            StartCoroutine(_pathFogSpawnAction);
        }
    }

    private IEnumerator ActivateFogAction() {
        if (_fogIndex >= pathFogCount - 1) {
            _fogIndex = 0;
        }
        
        if (_pathFogList[_fogIndex] != null && !_pathFogList[_fogIndex].Active()) {
            _pathFogList[_fogIndex].Activate(Random.Range(pathSizeRange.x, pathSizeRange.y), Random.Range(pathLifetimeRange.x, pathLifetimeRange.y), new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), agent.transform.position +
                CalculateStepSize() * (_fogIndex % steps) + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f));
        }

        for (int i = 0; i < emitterCountPerPath; i++) {
            if (_emitterIndex >= emitterCountPerPath * pathFogCount - 1) {
                _emitterIndex = 0;
            }
            
            if (_emitterFogList[_emitterIndex] != null && !_emitterFogList[_emitterIndex].Active()) {
                _emitterFogList[_emitterIndex].Activate(Random.Range(0.1f, 0.4f), Random.Range(3f, 6f), new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)),
                    agent.transform.position + CalculateStepSize() * (_fogIndex % steps) + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0f));
            }
            _emitterIndex++;
        }

        _fogIndex++;
        yield return new WaitForSeconds(pathSpawnDelay);
        _pathFogSpawnAction = null;
        yield return null;
    }
}
