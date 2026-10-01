using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool expandable = false;
    
    private void Start()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy)
                return child.gameObject;
        }

        if (expandable)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            return obj;
        }

        return null;
    }

}