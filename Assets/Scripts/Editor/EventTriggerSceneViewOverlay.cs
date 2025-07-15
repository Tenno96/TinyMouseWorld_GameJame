using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[InitializeOnLoad]
public static class EventTriggerSceneViewOverlay
{
    static EventTriggerSceneViewOverlay()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        TriggerEvent[] eventTriggers = Object.FindObjectsOfType<TriggerEvent>();

        foreach (TriggerEvent eventTrigger in eventTriggers)
        {
            Collider2D collider2D = eventTrigger.GetComponent<Collider2D>();

            if (collider2D != null && collider2D.enabled && collider2D.gameObject.activeInHierarchy)
            {
                Handles.color = new Color(1, 0.92f, 0.016f, .75f);
                Handles.matrix = eventTrigger.transform.localToWorldMatrix; // Applica la trasformazione dell'oggetto

                if (collider2D is BoxCollider2D)
                {
                    BoxCollider2D boxCollider = (BoxCollider2D)collider2D;
                    Vector3[] corners = new Vector3[4];
                    corners[0] = boxCollider.offset + new Vector2(-boxCollider.size.x, -boxCollider.size.y) * 0.5f;
                    corners[1] = boxCollider.offset + new Vector2(boxCollider.size.x, -boxCollider.size.y) * 0.5f;
                    corners[2] = boxCollider.offset + new Vector2(boxCollider.size.x, boxCollider.size.y) * 0.5f;
                    corners[3] = boxCollider.offset + new Vector2(-boxCollider.size.x, boxCollider.size.y) * 0.5f;
                    Handles.DrawSolidRectangleWithOutline(corners, Handles.color * new Color(1f, 1f, 1f, 0.2f), Handles.color);
                }
                else if (collider2D is CircleCollider2D)
                {
                    CircleCollider2D circleCollider = (CircleCollider2D)collider2D;
                    Handles.DrawWireDisc(circleCollider.offset, Vector3.forward, circleCollider.radius);
                }
            }
        }
    }
}

#endif