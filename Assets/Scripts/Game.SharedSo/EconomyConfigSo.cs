using UnityEngine;
namespace Game.SharedSo
{
[CreateAssetMenu(menuName="Game/EconomyConfig")]
public class EconomyConfigSo : ScriptableObject
{
    public int startGold = 5;
    public int roundCost = 5;
    public int adReward  = 5;
}
}