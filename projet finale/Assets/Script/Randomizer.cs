using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveElement;
    [SerializeField] private GameObject _meleeEnemyGenerator;
    [SerializeField] private GameObject _rangeEnemyGenerator;

    private List<Vector3> _allPosition = new List<Vector3>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            _allPosition.Add(child.position);
        }

        
        int randomIndex = Random.Range(0, _allPosition.Count);
        Vector3 randomPos = _allPosition[randomIndex];
        Instantiate(_meleeEnemyGenerator, randomPos, Quaternion.identity, transform);
        _allPosition.RemoveAt(randomIndex);

        randomIndex = Random.Range(0, _allPosition.Count);
        randomPos = _allPosition[randomIndex];
        Instantiate(_rangeEnemyGenerator, randomPos, Quaternion.identity, transform);
        _allPosition.RemoveAt(randomIndex);

        foreach (var position in _allPosition)
        {
            Instantiate(_interactiveElement, position, Quaternion.identity, transform);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
