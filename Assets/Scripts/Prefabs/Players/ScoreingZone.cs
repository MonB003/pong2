using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreingZone : MonoBehaviour
{
        public EventTrigger.TriggerEvent onPlayerScored;

       private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        
        if(ball != null)
        {
            BaseEventData data = new BaseEventData(EventSystem.current);
            this.onPlayerScored.Invoke(data); // trigger an event on collision

        }
    }
}
