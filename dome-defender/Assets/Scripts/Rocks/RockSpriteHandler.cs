using UnityEngine;

[CreateAssetMenu(fileName = "RockSpriteHandler", menuName = "ScriptableObjects/RockSpriteHandler", order = 1)]
public class RockSpriteHander : ScriptableObject
{
    [Header("Extremes")]
    public Sprite none;
    public Sprite all;
    [Header("1 Border")]
    public Sprite top;
    public Sprite right;
    public Sprite bottom;
    public Sprite left;
    [Header("2 Borders")]
    public Sprite topLeft;
    public Sprite topRight;
    public Sprite topBottom;
    public Sprite rightBottom;
    public Sprite bottomLeft;
    public Sprite rightLeft;
    [Header("3 Borders")]
    public Sprite topRightBottom;
    public Sprite topRightLeft;
    public Sprite topBottomLeft;
    public Sprite rightBottomLeft;

    public Sprite GetRockSprite(Vector2 rockPos)
    {
        Vector2 topVec = rockPos + Vector2.up;
        Vector2 rightVec = rockPos + Vector2.right;
        Vector2 bottomVec = rockPos + Vector2.down;
        Vector2 leftVec = rockPos + Vector2.left;

        bool borderTop = !Terrain.HasRockAt(topVec);
        bool borderRight = !Terrain.HasRockAt(rightVec);
        bool borderbottom = !Terrain.HasRockAt(bottomVec);
        bool borderLeft = !Terrain.HasRockAt(leftVec);

        switch (borderTop, borderRight, borderbottom, borderLeft)
        {
            // Extremes
            case (false, false, false, false):
                return none;
            case (true, true, true, true):
                return all;
            // 1 Border
            case (true, false, false, false):
                return top;
            case (false, true, false, false):
                return right;
            case (false, false, true, false):
                return bottom;
            case (false, false, false, true):
                return left;
            // 2 Borders
            case (true, true, false, false):
                return topRight;
            case (true, false, true, false):
                return topBottom;
            case (false, true, false, true):
                return rightLeft;
            case (false, false, true, true):
                return bottomLeft;
            case (true, false, false, true):
                return topLeft;
            case (false, true, true, false):
                return rightBottom;
            // 3 Borders
            case (true, true, true, false):
                return topRightBottom;
            case (true, true, false, true):
                return topRightLeft;
            case (true, false, true, true):
                return topBottomLeft;
            case (false, true, true, true):
                return rightBottomLeft;
        }
    }
}
