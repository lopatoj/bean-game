using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private GameObject Cat;

    [SerializeField] private int health;

    private float _wave;
    
    // Start is called before the first frame update
    void Start()
    {
        _wave = 1;
        Death();
    }

    private void AddCat()
    {
        GameObject c = Instantiate(Cat, Vector3.zero, Quaternion.identity);
        c.GetComponent<Enemy>().health = health;
        c.GetComponent<Enemy>().Handler = this;
    }

    public void Death()
    {
        if (_wave < 100)
        {
            for (int i = 0; i < _wave || i < 50; i++)
            {
                AddCat();
            }

            _wave += .1f;
        }
    }
}