using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleLoot : MonoBehaviour
{

    private Transform target;
    public float speed;
    
    private Transform _parent;

    public float minModifier;
    public float maxModifier;

    private Vector3 velocity = Vector3.zero;
    private bool _isFollowing = false;
    // Start is called before the first frame update

    public void Start()
    {
        _parent = transform.parent;
        target = playerController.Instance.lootCollector;
    }

    public void StartFollowing()
    {
        _isFollowing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isFollowing)
        {
            /*_parent.position = Vector3.MoveTowards(
                _parent.position, target.position, Random.Range(minModifier, maxModifier)
            );*/
            _parent.LookAt(target.position);
            _parent.position += _parent.forward * speed * Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(_parent.gameObject);
        }
    }
}
