using UnityEngine;

public class ObjectToSpawn : MonoBehaviour
{
    [SerializeField] private float _setfDestroyMaxDelay = 10f;
    [SerializeField] private MeshRenderer _meshRenderer;

    void Start()
    {
        GameManager._instance.IncreaseNumberObject();
        _meshRenderer.material.color = Random.ColorHSV(0f, 1f);

        Destroy(gameObject, Random.Range(0f, _setfDestroyMaxDelay));
    }

    private void OnDestroy()
    {
        GameManager._instance.DecreaseNumberObject();
    }
}
