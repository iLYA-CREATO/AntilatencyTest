using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    public Animator animator; 
    public string[] animationNames;

    private int currentAnimationIndex = 0;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found!");
                enabled = false; 
                return;
            }
        }

        if (animationNames == null || animationNames.Length == 0)
        {
            Debug.LogError("Animation names array is empty!");
            enabled = false; 
            return;
        }

        PlayAnimation(currentAnimationIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentAnimationIndex--;
            if (currentAnimationIndex < 0)
            {
                currentAnimationIndex = animationNames.Length - 1; 
            }
            PlayAnimation(currentAnimationIndex);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentAnimationIndex++;
            if (currentAnimationIndex >= animationNames.Length)
            {
                currentAnimationIndex = 0; 
            }
            PlayAnimation(currentAnimationIndex);
        }
    }

    private void PlayAnimation(int index)
    {
        if (index >= 0 && index < animationNames.Length)
        {
            animator.Play(animationNames[index]);
            Debug.Log("Playing animation: " + animationNames[index]);
        }
        else
        {
            Debug.LogError("Invalid animation index: " + index);
        }
    }
}