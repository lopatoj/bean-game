using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private GameObject Cat;

    [SerializeField] private int health;

    private int _wave;
    
    // Start is called before the first frame update
    void Start()
    {
        _wave = 5;
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
        for (int i = 0; i < _wave; i++)
        {
            AddCat();
        }

        _wave++;
    }
}