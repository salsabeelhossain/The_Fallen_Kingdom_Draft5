using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour 
{
    public PolygonCollider2D mapBoundary;
    public Direction direction;
    public Transform teleportTarget;
    private CinemachineConfiner2D confiner;

    public enum Direction { Up, Down, Left, Right, Teleport }

    private void Awake() 
    {
        confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Player")) return;

        UpdatePlayerPosition(collision.gameObject);

        if (confiner != null && mapBoundary != null) 
        {
            confiner.BoundingShape2D = mapBoundary;
            confiner.InvalidateBoundingShapeCache();
        }
    }

    private void UpdatePlayerPosition(GameObject player) 
    {
        if (direction == Direction.Teleport && teleportTarget != null) 
        {
            player.transform.position = teleportTarget.position;
            return;
        }

        Vector3 newPosition = player.transform.position;
        switch (direction) 
        {
            case Direction.Up: newPosition.y += 4; break;
            case Direction.Down: newPosition.y -= 4; break;
            case Direction.Left: newPosition.x -= 4; break;
            case Direction.Right: newPosition.x += 4; break;
        }
        player.transform.position = newPosition;
    }
}
