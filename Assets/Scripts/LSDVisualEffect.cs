using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LSDVisualEffect : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private ColorGrading colorGrading;
    private Vignette vignette;

    [Header("Animation Smoothing")]
    public int smoothingBufferSize = 120; // 2 seconden aan frames bij 60 FPS
    public float smoothingWeight = 0.05f;
    private float[] distortionBuffer;
    private float[] saturationBuffer;
    private float[] bloomBuffer;
    private int bufferIndex = 0;

    [Header("Breathing Effect")]
    public float breathingSpeed = 0.15f;
    public float breathingDepth = 15f;

    [Header("Color Settings")]
    public float hueShiftSpeed = 0.1f;
    public float saturationMin = 100f;
    public float saturationMax = 150f;
    public float saturationBreathingSpeed = 0.2f;

    [Header("Bloom Settings")]
    public float bloomMin = 2f;
    public float bloomMax = 5f;
    public float bloomBreathingSpeed = 0.25f;

    [Header("Geometric Patterns")]
    public int patternComplexity = 6;    // Aantal lagen in het patroon
    public float patternSpeed = 0.1f;    // Rotatiesnelheid
    public float patternScale = 0.8f;    // Schaal van de patronen

    private float timer;
    private Vector2[] patternOffsets;
    private float[] patternRotations;

    void Start()
    {
        InitializePostProcessing();
        InitializeBuffers();
        InitializePatterns();
        Application.targetFrameRate = 60;
    }

    void InitializePostProcessing()
    {
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out bloom);
            postProcessVolume.profile.TryGetSettings(out chromaticAberration);
            postProcessVolume.profile.TryGetSettings(out lensDistortion);
            postProcessVolume.profile.TryGetSettings(out colorGrading);
            postProcessVolume.profile.TryGetSettings(out vignette);
        }
    }

    void InitializeBuffers()
    {
        distortionBuffer = new float[smoothingBufferSize];
        saturationBuffer = new float[smoothingBufferSize];
        bloomBuffer = new float[smoothingBufferSize];
    }

    void InitializePatterns()
    {
        patternOffsets = new Vector2[patternComplexity];
        patternRotations = new float[patternComplexity];
        for (int i = 0; i < patternComplexity; i++)
        {
            patternOffsets[i] = Random.insideUnitCircle;
            patternRotations[i] = Random.value * 360f;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateBreathingEffect();
        UpdatePatterns();
        UpdateColors();
    }

    void UpdateBreathingEffect()
    {
        // Gebruik verschillende sinusgolven voor een complexer breathing effect
        float mainBreathing = Mathf.Sin(timer * breathingSpeed * Mathf.PI) * breathingDepth;
        float secondaryBreathing = Mathf.Sin(timer * breathingSpeed * 1.3f * Mathf.PI) * (breathingDepth * 0.3f);
        float finalBreathing = mainBreathing + secondaryBreathing;

        // Update buffers voor smooth interpolatie
        distortionBuffer[bufferIndex] = finalBreathing;
        
        // Bereken gewogen gemiddelde voor ultra-smooth beweging
        float smoothDistortion = 0;
        float totalWeight = 0;
        for (int i = 0; i < smoothingBufferSize; i++)
        {
            float weight = 1f - (i * smoothingWeight);
            if (weight < 0) break;
            
            int index = (bufferIndex - i + smoothingBufferSize) % smoothingBufferSize;
            smoothDistortion += distortionBuffer[index] * weight;
            totalWeight += weight;
        }
        smoothDistortion /= totalWeight;

        // Pas de effecten toe met extra smoothing
        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Lerp(
                lensDistortion.intensity.value,
                smoothDistortion,
                Time.deltaTime * 5f
            );
        }

        bufferIndex = (bufferIndex + 1) % smoothingBufferSize;
    }

    void UpdatePatterns()
    {
        for (int i = 0; i < patternComplexity; i++)
        {
            // Update pattern rotaties met verschillende snelheden per laag
            float layerSpeed = patternSpeed * (1f + i * 0.2f);
            patternRotations[i] += layerSpeed * Time.deltaTime * 360f;
            
            // Creëer vloeiende beweging voor de patronen
            float t = timer * (0.1f + i * 0.05f);
            patternOffsets[i] = new Vector2(
                Mathf.Sin(t) * patternScale,
                Mathf.Cos(t * 1.3f) * patternScale
            );

            // Pas de patronen toe op de vignette en distortion
            if (vignette != null)
            {
                float vignetteX = 0.5f + Mathf.Sin(patternRotations[i] * Mathf.Deg2Rad) * 0.1f * patternScale;
                float vignetteY = 0.5f + Mathf.Cos(patternRotations[i] * Mathf.Deg2Rad) * 0.1f * patternScale;
                vignette.center.value = new Vector2(vignetteX, vignetteY);
            }
        }
    }

    void UpdateColors()
    {
        if (colorGrading != null)
        {
            // Smooth kleurveranderingen
            float hueShift = Mathf.Sin(timer * hueShiftSpeed) * 180f;
            float saturationBreathing = Mathf.Sin(timer * saturationBreathingSpeed) * 0.5f + 0.5f;
            float saturation = Mathf.Lerp(saturationMin, saturationMax, saturationBreathing);

            colorGrading.hueShift.value = Mathf.Lerp(
                colorGrading.hueShift.value,
                hueShift,
                Time.deltaTime * 2f
            );

            colorGrading.saturation.value = Mathf.Lerp(
                colorGrading.saturation.value,
                saturation,
                Time.deltaTime * 2f
            );
        }

        if (bloom != null)
        {
            float bloomBreathing = Mathf.Sin(timer * bloomBreathingSpeed) * 0.5f + 0.5f;
            float targetBloom = Mathf.Lerp(bloomMin, bloomMax, bloomBreathing);
            
            bloom.intensity.value = Mathf.Lerp(
                bloom.intensity.value,
                targetBloom,
                Time.deltaTime * 3f
            );
        }

        if (chromaticAberration != null)
        {
            float aberrationIntensity = Mathf.Sin(timer * 0.2f) * 0.3f + 0.3f;
            chromaticAberration.intensity.value = Mathf.Lerp(
                chromaticAberration.intensity.value,
                aberrationIntensity,
                Time.deltaTime * 2f
            );
        }
    }

    void OnDisable()
    {
        // Reset alle effecten
        if (bloom != null) bloom.intensity.value = 0;
        if (chromaticAberration != null) chromaticAberration.intensity.value = 0;
        if (lensDistortion != null) lensDistortion.intensity.value = 0;
        if (colorGrading != null)
        {
            colorGrading.saturation.value = 100;
            colorGrading.hueShift.value = 0;
        }
        if (vignette != null)
        {
            vignette.intensity.value = 0;
            vignette.center.value = new Vector2(0.5f, 0.5f);
        }
    }
}