using System.Collections.Generic;
using UnityEngine;

public class SkillVFXPool : MonoBehaviour
{
    [SerializeField] private GameObject _vfxPrefab;
    private Queue<GameObject> _pool = new Queue<GameObject>();

    public GameObject GetVFX()
    {
        if (_pool.Count > 0) return _pool.Dequeue();
        return Instantiate(_vfxPrefab);
    }

    public void ReturnVFX(GameObject vfx)
    {
        vfx.SetActive(false);
        _pool.Enqueue(vfx);
    }
}
