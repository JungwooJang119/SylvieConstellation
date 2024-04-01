using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cyan;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class DrunkGoggles : MonoBehaviour
{
    /**
     * Chris' documentation:
     *
     * Call SetDrunkIntensity(int level) from any script. It's as easy as that :O
     *
     * The intensity level is an integer that scales from 0-4. The method catches OoB but try to keep it in that range.
     */
    [SerializeField] private Blit feature;
    [SerializeField] private NoiseSettings drunkNoise;
    [SerializeField] private Material shader;
    [Range(0, 4)] [SerializeField] private int intensity;

    private void Awake() {
        feature.SetActive(false);
        
        shader.SetFloat("_Scale1", 0f);
        shader.SetFloat("_Scale2", 0f);
        shader.SetFloat("_Scale3", 0f);
        shader.SetFloat("_Blend", 0f);
        shader.SetFloat("_DistScale", 0f);

        drunkNoise.PositionNoise[0].X.Frequency = 0f;
        drunkNoise.PositionNoise[0].X.Amplitude = 0f;
        drunkNoise.PositionNoise[0].Y.Frequency = 0f;
        drunkNoise.PositionNoise[0].Y.Amplitude = 0f;
        drunkNoise.OrientationNoise[0].Z.Frequency = 0;
        drunkNoise.OrientationNoise[0].Z.Amplitude = 0;
    }

    /**
     * CALL THIS METHOD
     */
    public void SetDrunkIntensity(int level) {
        level = level < 0 ? 0 : level;
        level = level > 4 ? 4 : level;
        
        Camera.main.transform.rotation = Quaternion.identity;
        switch (level) {
            case 0:
                if (Application.isPlaying) {
                    shader.DOFloat(0.01f, "_Scale1", 1f);
                    shader.DOFloat(0f, "_Scale2", 1f);
                    shader.DOFloat(0f, "_Scale3", 1f);
                    shader.DOFloat(0f, "_Blend", 1f);
                    shader.DOFloat(0f, "_DistScale", 1f);
                }
                else {
                    shader.SetFloat("_Scale1", 0f);
                    shader.SetFloat("_Scale2", 0f);
                    shader.SetFloat("_Scale3", 0f);
                    shader.SetFloat("_Blend", 0f);
                    shader.SetFloat("_DistScale", 1f);
                }

                feature.SetActive(false);
                drunkNoise.OrientationNoise[0].Z.Frequency = 0f;
                drunkNoise.OrientationNoise[0].Z.Amplitude = 0f;
                drunkNoise.PositionNoise[0].X.Frequency = 0f;
                drunkNoise.PositionNoise[0].X.Amplitude = 0f;
                drunkNoise.PositionNoise[0].Y.Frequency = 0f;
                drunkNoise.PositionNoise[0].Y.Amplitude = 0f;
                break;
            case 1:
                if (Application.isPlaying) {
                    shader.DOFloat(0.01f, "_Scale1", 1f);
                    shader.DOFloat(0f, "_Scale2", 1f);
                    shader.DOFloat(0f, "_Scale3", 1f);
                    shader.DOFloat(0.03f, "_Blend", 1f);
                    shader.DOFloat(4f, "_DistScale", 1f);
                }
                else {
                    shader.SetFloat("_Scale1", 0.01f);
                    shader.SetFloat("_Scale2", 0.0f);
                    shader.SetFloat("_Scale3", 0f);
                    shader.SetFloat("_Blend", 0.03f);
                    shader.SetFloat("_DistScale", 4f);
                }

                feature.SetActive(true);
                drunkNoise.OrientationNoise[0].Z.Frequency = 0.3f;
                drunkNoise.OrientationNoise[0].Z.Amplitude = 0.05f;
                drunkNoise.PositionNoise[0].X.Frequency = 1f;
                drunkNoise.PositionNoise[0].X.Amplitude = 0.3f;
                drunkNoise.PositionNoise[0].Y.Frequency = 1f;
                drunkNoise.PositionNoise[0].Y.Amplitude = 0.3f;
                break;
            case 2:
                if (Application.isPlaying) {
                    shader.DOFloat(0.01f, "_Scale1", 1f);
                    shader.DOFloat(0.01f, "_Scale2", 1f);
                    shader.DOFloat(0f, "_Scale3", 1f);
                    shader.DOFloat(0.01f, "_Blend", 1f);
                    shader.DOFloat(9.39f, "_DistScale", 1f);
                }
                else {
                    shader.SetFloat("_Scale1", 0.01f);
                    shader.SetFloat("_Scale2", 0.01f);
                    shader.SetFloat("_Scale3", 0f);
                    shader.SetFloat("_Blend", 0.1f);
                    shader.SetFloat("_DistScale", 9.39f);
                }

                feature.SetActive(true);
                drunkNoise.OrientationNoise[0].Z.Frequency = 0.5f;
                drunkNoise.OrientationNoise[0].Z.Amplitude = 0.1f;
                drunkNoise.PositionNoise[0].X.Frequency = 1f;
                drunkNoise.PositionNoise[0].X.Amplitude = 1f;
                drunkNoise.PositionNoise[0].Y.Frequency = 1f;
                drunkNoise.PositionNoise[0].Y.Amplitude = 1f;
                break;
            case 3:
                if (Application.isPlaying) {
                    shader.DOFloat(0.01f, "_Scale1", 1f);
                    shader.DOFloat(0.01f, "_Scale2", 1f);
                    shader.DOFloat(0.02f, "_Scale3", 1f);
                    shader.DOFloat(0.25f, "_Blend", 1f);
                    shader.DOFloat(10.39f, "_DistScale", 1f);   
                }
                else {
                    shader.SetFloat("_Scale1", 0.01f);
                    shader.SetFloat("_Scale2", 0.01f);
                    shader.SetFloat("_Scale3", 0.02f);
                    shader.SetFloat("_Blend", 0.25f);
                    shader.SetFloat("_DistScale", 10.39f);
                }
                feature.SetActive(true);
                drunkNoise.OrientationNoise[0].Z.Frequency = 0.8f;
                drunkNoise.OrientationNoise[0].Z.Amplitude = 0.15f;
                drunkNoise.PositionNoise[0].X.Frequency = 1f;
                drunkNoise.PositionNoise[0].X.Amplitude = 1.5f;
                drunkNoise.PositionNoise[0].Y.Frequency = 1f;
                drunkNoise.PositionNoise[0].Y.Amplitude = 1.5f;
                break;
            case 4:
                feature.SetActive(true);
                if (Application.isPlaying) {
                    shader.DOFloat(0.015f, "_Scale1", 1f);
                    shader.DOFloat(0.015f, "_Scale2", 1f);
                    shader.DOFloat(0.025f, "_Scale3", 1f);
                    shader.DOFloat(0.5f, "_Blend", 1f);
                    shader.DOFloat(12f, "_DistScale", 1f);   
                }
                else {
                    shader.SetFloat("_Scale1", 0.015f);
                    shader.SetFloat("_Scale2", 0.015f);
                    shader.SetFloat("_Scale3", 0.025f);
                    shader.SetFloat("_Blend", 0.5f);
                    shader.SetFloat("_DistScale", 12f);
                }

                drunkNoise.OrientationNoise[0].Z.Frequency = 0.8f;
                drunkNoise.OrientationNoise[0].Z.Amplitude = 0.3f;
                drunkNoise.PositionNoise[0].X.Frequency = 1f;
                drunkNoise.PositionNoise[0].X.Amplitude = 3f;
                drunkNoise.PositionNoise[0].Y.Frequency = 1f;
                drunkNoise.PositionNoise[0].Y.Amplitude = 3f;
                break;
        }
    }
}
