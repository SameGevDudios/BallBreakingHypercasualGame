using UnityEngine;
public class StartSkinProgression : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinManager;
    public void GameEnded() => _skinManager.GameEnded();
    public void StartProgression() => _skinManager.ProgressSkins();
}
