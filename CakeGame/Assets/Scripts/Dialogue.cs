using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public class InteractionEvent : UnityEvent { }

    [System.Serializable]
    public class Line
    {
        [TextArea(3, 10)]
        public string sentence;

        public InteractionEvent lineEvent;

        public Animator animator;
        public string clipName;
        public string soundName;

        public void ExecuteActions()
        {
            if (this.lineEvent != null)
            {
                this.lineEvent.Invoke();
            }
            if (this.animator != null && this.clipName != "")
            {
                this.animator.Play(this.clipName);
            }
            if (this.soundName != "")
            {
                FindObjectOfType<AudioManager>().Play(soundName);
            }
        }
    }

    public Line[] lines;
}
