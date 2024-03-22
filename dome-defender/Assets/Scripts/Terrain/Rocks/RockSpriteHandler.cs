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

    public Sprite GetRockSprite(TerrainData terrain, Vector2 rockPos)
    {
        Vector2 topVec = rockPos + Vector2.up;
        Vector2 rightVec = rockPos + Vector2.right;
        Vector2 bottomVec = rockPos + Vector2.down;
        Vector2 leftVec = rockPos + Vector2.left;

        bool borderTop = !terrain.HasRockAt(topVec);
        bool borderRight = !terrain.HasRockAt(rightVec);
        bool borderbottom = !terrain.HasRockAt(bottomVec);
        bool borderLeft = !terrain.HasRockAt(leftVec);

        return (borderTop, borderRight, borderbottom, borderLeft) switch
        {
            // Extremes
            (false, false, false, false) => none,
            (true, true, true, true) => all,
            // 1 Border
            (true, false, false, false) => top,
            (false, true, false, false) => right,
            (false, false, true, false) => bottom,
            (false, false, false, true) => left,
            // 2 Borders
            (true, true, false, false) => topRight,
            (true, false, true, false) => topBottom,
            (false, true, false, true) => rightLeft,
            (false, false, true, true) => bottomLeft,
            (true, false, false, true) => topLeft,
            (false, true, true, false) => rightBottom,
            // 3 Borders
            (true, true, true, false) => topRightBottom,
            (true, true, false, true) => topRightLeft,
            (true, false, true, true) => topBottomLeft,
            (false, true, true, true) => rightBottomLeft,
        };
    }
}
