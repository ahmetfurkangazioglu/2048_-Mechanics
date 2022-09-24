using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bomb : MonoBehaviour
{
    [SerializeField] int CurrentValue;
    [SerializeField] TextMeshProUGUI CurrentValueText;
    [SerializeField] GameManager gameManager;
    List<Collider2D> colliders = new List<Collider2D>();
    void Start()
    {
        CurrentValueText.text = CurrentValue.ToString();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CurrentValue.ToString()))
          Explosion();
    }

   void Explosion()
    {
        var contactFilter2D = new ContactFilter2D
        {
            useTriggers = true
        };
        SoundManager.Instance.AllSounds[0].Play();
        Physics2D.OverlapBox(transform.position, transform.localScale * 3, 25f, contactFilter2D, colliders);
        gameManager.ExplosionEffect(transform.position);
        foreach (var item in colliders)
        {
            if (item.gameObject.CompareTag("Box"))
            {
                item.GetComponent<Box>().BoxOperation();
            }
            else
            {
                item.gameObject.GetComponent<Rigidbody2D>().AddForce(90 * new Vector2(0, 6), ForceMode2D.Force);
            }
        }        gameObject.SetActive(false);
    }
}
