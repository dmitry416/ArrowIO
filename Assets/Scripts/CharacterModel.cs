using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [SerializeField] private GameObject[] _skins;
    private int _curSkin = 0;

    public void ChangeSkin(int skinID)
    {
        _skins[_curSkin].SetActive(false);
        _skins[skinID].SetActive(true);
        _curSkin = skinID;
    }

    public int GetSkinsLength()
    {
        return _skins.Length;
    }
}
