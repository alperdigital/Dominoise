using UnityEngine;

[CreateAssetMenu(menuName="Game/Rules")]
public class GameRulesSo : ScriptableObject
{
    public int targetScore = 5;
    public int prepSeconds = 2;
    public int poseSeconds = 5;
}
