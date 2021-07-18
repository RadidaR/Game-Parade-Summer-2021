using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MPUIKIT;
using MEC;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;
    [SerializeField] float uiSpeed;
    [SerializeField] Color usedColor;

    [SerializeField] GameEvent eUseTrampolinePressed;
    [SerializeField] GameEvent eRollPressed;
    [SerializeField] GameEvent eSwingPressed;

    InputActions playerInput;
    //public Button button;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new InputActions();

        //playerInput.Gameplay.MouseClick.
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TrampolineClick(Animator animator)
    {
        eUseTrampolinePressed.Raise();
        Timing.RunCoroutine(_CheckTrampoline(animator), Segment.Update);
        //Timing.RunCoroutine(_ButtonPressed(button, current.trampolineAvailable), Segment.Update);
    }

    public void RollClick(Animator animator)
    {
        eRollPressed.Raise();
        Timing.RunCoroutine(_CheckRoll(animator), Segment.Update);
    }

    public void SwingClick(Animator animator)
    {
        eSwingPressed.Raise();
        Timing.RunCoroutine(_CheckSwing(animator), Segment.FixedUpdate);
    }

    IEnumerator<float> _CheckTrampoline(Animator animator)
    {
        yield return Timing.WaitForSeconds(0.1f);

        if (!current.trampolineAvailable)
        {
            animator.Play("Expended_Anim");
        }
    }
    IEnumerator<float> _CheckRoll(Animator animator)
    {
        yield return Timing.WaitForSeconds(0.1f);

        if (!current.rollAvailable)
        {
            animator.Play("Expended_Anim");
        }
    }
    
    IEnumerator<float> _CheckSwing(Animator animator)
    {
        yield return Timing.WaitForSeconds(0.1f);

        if (!current.rollAvailable)
        {
            animator.Play("Expended_Anim");
        }

        while (playerInput.Gameplay.MouseClick.ReadValue<float>() == 1)
        {
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
            current.swingInput = playerInput.Gameplay.MouseClick.ReadValue<float>();
            if (playerInput.Gameplay.MouseClick.ReadValue<float>() == 0)
            {
                current.swingInput = playerInput.Gameplay.MouseClick.ReadValue<float>();
                break;
            }

        }
    }

  
}
