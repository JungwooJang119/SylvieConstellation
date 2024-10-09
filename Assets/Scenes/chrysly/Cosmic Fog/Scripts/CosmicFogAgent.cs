using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicFogAgent : MonoBehaviour {
    private Metaball2D _metaball;
    private void Awake() {
        _metaball = gameObject.AddComponent<Metaball2D>();
        _metaball.isAntiball = true;
        GetComponent<CircleCollider2D>().radius = 0.5f;
    }
}
