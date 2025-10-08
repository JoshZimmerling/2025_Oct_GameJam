using UnityEngine;

public static class Collider2DExtensions
{
    public static Vector2 GetRandomPointInside(this Collider2D collider)
    {
        Bounds bounds = collider.bounds;
        Vector2 point;
        
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            point = new Vector2(x, y);
        }
        while (!collider.OverlapPoint(point));

        return point;
    }
}