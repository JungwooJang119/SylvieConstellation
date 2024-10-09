using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MetaballRender2D : ScriptableRendererFeature
{
    [System.Serializable]
    public class MetaballRender2DSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        [Range(0f, 1f), Tooltip("Outline size.")]
        public float outlineSize = 1.0f;

        [Tooltip("Gradient Texture")] public Texture2D gradient;
        [Tooltip("Noise")] public Texture2D noise;
        public Texture2D voronoi;
        public float wavelength;
        public float intensity;
        public float scale;

        public float scrollSpeed;
        public float simpleScale;

        public Vector4 playerPos;
        
        [Tooltip("Inner color.")]
        public Color innerColor = Color.white;

        [Tooltip("Outline color.")]
        public Color outlineColor = Color.black;
    }

    public MetaballRender2DSettings settings = new MetaballRender2DSettings();

    class MetaballRender2DPass : ScriptableRenderPass
    {
        private Material material;

        public float outlineSize;
        public Color innerColor;
        public Color outlineColor;

        public Vector4 playerPos;
        
        public Texture2D gradient;
        public Texture2D noise;
        public float wavelength;
        public float scale;
        public float intensity;

        public Texture2D voronoi;
        public float scrollSpeed;
        public float simpleScale;
        
        private bool isFirstRender = true;

        private RenderTargetIdentifier source;
        private string profilerTag;

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;

            material = new Material(Shader.Find("Hidden/Metaballs2D"));
        }

        public MetaballRender2DPass(string profilerTag)
        {
            this.profilerTag = profilerTag;
        }
        
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // Set up the render target for this pass (can be the camera or a custom render texture)
            ConfigureTarget(colorAttachmentHandle, depthAttachmentHandle);

            // Clear the color and depth buffers before rendering
            ConfigureClear(ClearFlag.All, Color.black); // You can set the clear color and choose which buffers to clear
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

            if(isFirstRender)
            {
                isFirstRender = false;
                cmd.SetGlobalVectorArray("_MetaballData", new Vector4[1000]);
            }

            List<Metaball2D> metaballs = MetaballSystem2D.Get();
            List<Vector4> metaballData = new List<Vector4>(metaballs.Count);

            for(int i = 0; i < metaballs.Count; ++i)
            {
                Vector2 pos = renderingData.cameraData.camera.WorldToScreenPoint(metaballs[i].transform.position);
                float radius = metaballs[i].GetRadius();
                metaballData.Add(new Vector4(pos.x, pos.y, radius, metaballs[i].isAntiball ? 1 : 0));
            }

            if(metaballData.Count > 0)
            {
                cmd.SetGlobalInt("_MetaballCount", metaballs.Count);
                cmd.SetGlobalVectorArray("_MetaballData", metaballData);
                cmd.SetGlobalFloat("_OutlineSize", outlineSize);
                cmd.SetGlobalColor("_InnerColor", innerColor);
                cmd.SetGlobalColor("_OutlineColor", outlineColor);
                cmd.SetGlobalFloat("_CameraSize", renderingData.cameraData.camera.orthographicSize);
                cmd.SetGlobalTexture("_GradientTex", gradient);
                cmd.SetGlobalTexture("_GradientNoise", noise);
                cmd.SetGlobalFloat("_Wavelength", wavelength);
                cmd.SetGlobalFloat("_Scale", scale);
                cmd.SetGlobalFloat("_Intensity", intensity);
                cmd.SetGlobalTexture("_Voronoi", voronoi);
                cmd.SetGlobalFloat("_ScrollSpeed", scrollSpeed);
                cmd.SetGlobalFloat("_SimpleScale", simpleScale);
                cmd.SetGlobalVector("_PlayerPos", playerPos);

                cmd.Blit(source, source, material);

                context.ExecuteCommandBuffer(cmd);
            }
            
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }

    MetaballRender2DPass pass;

    public override void Create()
    {
        name = "Metaballs (2D)";

        pass = new MetaballRender2DPass("Metaballs2D");

        pass.outlineSize = settings.outlineSize;
        pass.innerColor = settings.innerColor;
        pass.outlineColor = settings.outlineColor;
        pass.gradient = settings.gradient;
        pass.noise = settings.noise;
        pass.scale = settings.scale;
        pass.intensity = settings.intensity;
        pass.wavelength = settings.wavelength;
        pass.voronoi = settings.voronoi;
        pass.simpleScale = settings.simpleScale;
        pass.scrollSpeed = settings.scrollSpeed;
        pass.playerPos = settings.playerPos;

        pass.renderPassEvent = settings.renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        pass.Setup(renderer.cameraColorTargetHandle);
    }
}
