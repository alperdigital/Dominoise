using UnityEngine;
namespace Game.SharedSo
{
[CreateAssetMenu(menuName="Game/EconomyConfig")]
public class EconomyConfigSo : ScriptableObject
{
    public int startGold = 10;  // ✅ 10 altın başlangıç
    public int roundCost = 5;   // ✅ 5 altın oyun
    public int adReward  = 5;   // ✅ 5 altın reklam
}
}