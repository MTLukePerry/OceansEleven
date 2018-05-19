using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpriteDisplay : MonoBehaviour
{

    [SerializeField] private GameObject _spriteObject;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _spriteObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _spriteObject.SetActive(true);
        }
    }


}
