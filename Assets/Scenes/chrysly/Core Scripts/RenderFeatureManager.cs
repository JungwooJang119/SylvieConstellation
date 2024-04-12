using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct RenderFeatureToggle {
    public ScriptableRendererFeature feature;
    public bool isEnabled;
}

[ExecuteAlways]
public class RenderFeatureManager : MonoBehaviour {
    [SerializeField] private List<RenderFeatureToggle> renderFeatures = new List<RenderFeatureToggle>();
    //[SerializeField] private UniversalRenderPipelineAsset pipelineAsset;

    private void Update() {
        foreach (RenderFeatureToggle feature in renderFeatures) {
            feature.feature.SetActive(feature.isEnabled);
        }
    }
}
