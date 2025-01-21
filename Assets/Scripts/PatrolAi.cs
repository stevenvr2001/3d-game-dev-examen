using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatrolAi : MonoBehaviour
{
    [Header("Waypoints")]
    public List<Transform> wayPoint;
    public int currentWaypointIndex = 0;

    [Header("Ground-Level Cone (Always Visible)")]
    public float groundConeAngle = 45f;
    public float groundConeRange = 10f;
    public int groundConeSegments = 20;

    [Header("Eye-Height Cone (Optional Visibility)")]
    public float eyeHeight = 1.8f;
    public float eyeConeAngle = 45f;
    public float eyeConeRange = 10f;
    public int eyeConeSegments = 20;
    [Tooltip("Toggle to show/hide the Eye Cone in the scene")]
    public bool showEyeCone = false;

    [Header("Player")]
    public Transform player;
    public string playerTag = "Player";

    [Header("Raycast & Layers")]
    [Tooltip("Layers to include in AI's line-of-sight checks. Exclude 'Powerups' layer here.")]
    public LayerMask detectionLayers = ~0; // Default: all layers included. Uncheck layers in Inspector to ignore them.

    [Header("References (Meshes)")]
    public MeshRenderer groundConeRenderer;  // Visible ground-level cone
    private Mesh groundConeMesh;
    private MeshRenderer eyeConeRenderer;    // Eye-level cone (optional visibility)
    private Mesh eyeConeMesh;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // 1) Create ground-level cone
        GenerateGroundCone();

        // 2) Create eye-level cone (we may or may not show it)
        GenerateEyeCone();
    }

    void Update()
    {
        // Patrol movement
        Walking();

        // Ground-level cone: update & detect
        UpdateGroundConeAndDetect();

        // Eye-level cone: update & detect
        UpdateEyeConeAndDetect();
    }

    /// <summary>
    /// Patrol between waypoints in a loop.
    /// </summary>
    private void Walking()
    {
        if (wayPoint == null || wayPoint.Count == 0) return;

        float distanceToWaypoint = Vector3.Distance(
            wayPoint[currentWaypointIndex].position,
            transform.position
        );

        if (distanceToWaypoint <= 2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoint.Count;
        }

        navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
    }

    // ------------------------------------------------------------------------------
    //  GROUND-LEVEL CONE (ALWAYS VISIBLE)
    // ------------------------------------------------------------------------------

    private void GenerateGroundCone()
    {
        groundConeMesh = new Mesh();

        // Create child GameObject
        GameObject groundConeObj = new GameObject("GroundDetectionCone");
        groundConeObj.transform.SetParent(transform);
        groundConeObj.transform.localPosition = Vector3.zero;
        groundConeObj.transform.localRotation = Quaternion.identity;

        groundConeRenderer = groundConeObj.AddComponent<MeshRenderer>();
        MeshFilter gf = groundConeObj.AddComponent<MeshFilter>();
        gf.mesh = groundConeMesh;

        // Semi-transparent red material
        groundConeRenderer.material = new Material(Shader.Find("Standard"))
        {
            color = new Color(1f, 0f, 0f, 0.5f)
        };
    }

    private void UpdateGroundConeAndDetect()
    {
        Vector3[] vertices = new Vector3[groundConeSegments + 2];
        int[] triangles = new int[groundConeSegments * 3];

        Vector3 aiFeetPos = transform.position;
        Vector3 coneOriginLocal = groundConeRenderer.transform.InverseTransformPoint(aiFeetPos);
        vertices[0] = coneOriginLocal;

        float angleStep = groundConeAngle / groundConeSegments;

        for (int i = 0; i <= groundConeSegments; i++)
        {
            float angle = -groundConeAngle / 2f + angleStep * i;
            float rad = Mathf.Deg2Rad * angle;

            // Horizontal direction
            Vector3 localDir = new Vector3(Mathf.Sin(rad), 0f, Mathf.Cos(rad)).normalized;
            Vector3 worldDir = transform.TransformDirection(localDir);

            float hitDistance = groundConeRange;

            // Use detectionLayers to ignore certain layers (like Powerups)
            RaycastHit hit;
            if (Physics.Raycast(aiFeetPos, worldDir, out hit, groundConeRange, detectionLayers))
            {
                hitDistance = hit.distance;

                if (hit.collider.CompareTag(playerTag))
                {
                    Debug.Log("Detected player with GROUND cone!");
                    Debug.Log($"Player detected by: {gameObject.name} (GROUND cone)");
                    EndGame();
                }
            }

            Vector3 worldHitPos = aiFeetPos + worldDir * hitDistance;
            Vector3 localHitPos = groundConeRenderer.transform.InverseTransformPoint(worldHitPos);
            vertices[i + 1] = localHitPos;
        }

        for (int i = 0; i < groundConeSegments; i++)
        {
            triangles[i * 3 + 0] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        groundConeMesh.Clear();
        groundConeMesh.vertices = vertices;
        groundConeMesh.triangles = triangles;
        groundConeMesh.RecalculateNormals();

        groundConeRenderer.transform.localPosition = Vector3.zero;
        groundConeRenderer.transform.localRotation = Quaternion.identity;
    }

    // ------------------------------------------------------------------------------
    //  EYE-HEIGHT CONE (TOGGLED VISIBILITY)
    // ------------------------------------------------------------------------------

    private void GenerateEyeCone()
    {
        eyeConeMesh = new Mesh();

        GameObject eyeConeObj = new GameObject("EyeDetectionCone");
        eyeConeObj.transform.SetParent(transform);
        eyeConeObj.transform.localPosition = Vector3.zero;
        eyeConeObj.transform.localRotation = Quaternion.identity;

        eyeConeRenderer = eyeConeObj.AddComponent<MeshRenderer>();
        MeshFilter mf = eyeConeObj.AddComponent<MeshFilter>();
        mf.mesh = eyeConeMesh;

        // Semi-transparent blue material, to distinguish from the red ground cone
        eyeConeRenderer.material = new Material(Shader.Find("Standard"))
        {
            color = new Color(0f, 0f, 1f, 0.5f)
        };
    }

    private void UpdateEyeConeAndDetect()
    {
        Vector3 aiEyePos = transform.position + Vector3.up * eyeHeight;
        Vector3[] vertices = new Vector3[eyeConeSegments + 2];
        int[] triangles = new int[eyeConeSegments * 3];

        // Convert the AI eye position to local space
        Vector3 coneOriginLocal = eyeConeRenderer.transform.InverseTransformPoint(aiEyePos);
        vertices[0] = coneOriginLocal;

        float angleStep = eyeConeAngle / eyeConeSegments;

        for (int i = 0; i <= eyeConeSegments; i++)
        {
            float angle = -eyeConeAngle / 2f + angleStep * i;
            float rad = Mathf.Deg2Rad * angle;

            Vector3 localDir = new Vector3(Mathf.Sin(rad), 0f, Mathf.Cos(rad)).normalized;
            Vector3 worldDir = transform.TransformDirection(localDir);

            float hitDistance = eyeConeRange;

            // Again, use detectionLayers to skip Powerups
            RaycastHit hit;
            if (Physics.Raycast(aiEyePos, worldDir, out hit, eyeConeRange, detectionLayers))
            {
                hitDistance = hit.distance;

                if (hit.collider.CompareTag(playerTag))
                {
                    Debug.Log("Detected player with EYE cone!");
                    Debug.Log($"Player detected by: {gameObject.name} (EYE cone)");
                    EndGame();
                }
            }

            // If showEyeCone, we build the mesh. Otherwise, we collapse it.
            if (showEyeCone)
            {
                Vector3 worldHitPos = aiEyePos + worldDir * hitDistance;
                Vector3 localHitPos = eyeConeRenderer.transform.InverseTransformPoint(worldHitPos);
                vertices[i + 1] = localHitPos;
            }
            else
            {
                // If not showing, let's just set all outer vertices to the same origin so it's invisible
                vertices[i + 1] = coneOriginLocal;
            }
        }

        if (showEyeCone)
        {
            for (int i = 0; i < eyeConeSegments; i++)
            {
                triangles[i * 3 + 0] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
            eyeConeMesh.Clear();
            eyeConeMesh.vertices = vertices;
            eyeConeMesh.triangles = triangles;
            eyeConeMesh.RecalculateNormals();
        }
        else
        {
            // Clear to hide
            eyeConeMesh.Clear();
        }

        // Toggle the renderer
        eyeConeRenderer.enabled = showEyeCone;

        eyeConeRenderer.transform.localPosition = Vector3.zero;
        eyeConeRenderer.transform.localRotation = Quaternion.identity;
    }

    private void EndGame()
    {
        SceneManager.LoadScene("Died");
    }
}
