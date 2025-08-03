using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using Unity.Burst.CompilerServices;
using TMPro;
using UnityEngine.Events;

public class ChickenProcessor : MonoBehaviour
{
    #region Singleton script
    public static ChickenProcessor Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log($"Init instance of Slot controller");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log($"Destroy instance of Slot controller");
            Destroy(gameObject);
        }
    }

    public static void Destroy()
    {
        Instance = null;
    }
    #endregion

    public TextMeshPro errorText;
    public AudioClip PlayClip;
    public AudioClip ErrorClip;
    int currentLine = 0, currentLoopRemainingRepeat = 0, loopStartLine = 0;
    DraggableObject_Loop lastActiveLoop = null;

    Vector3 initialScale;

    float speedMultiplier = 1f;

    void Start()
    {
        initialScale = this.transform.localScale;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            speedMultiplier *= 2f;
            speedMultiplier = Mathf.Clamp(speedMultiplier, .25f, 8f);
        }
        else if (scroll < 0f)
        {
            speedMultiplier /= 2f;
            speedMultiplier = Mathf.Clamp(speedMultiplier, .25f, 8f);
        }
    }

    void OnMouseEnter()
    {
        Tween.Scale(this.transform, initialScale * 1.2f, .25f);
    }

    void OnMouseExit()
    {
        Tween.Scale(this.transform, initialScale, .25f);
    }

    void OnMouseDown()
    {
        ShowError("");
        if (!CluckController.IsProcessing)
        {
            MusicController.Instance.PlaySFX(PlayClip);
            StartCoroutine(Process());
        }
    }

    public void ShowError(string text)
    {
        errorText.text = text;

        MusicController.Instance.PlaySFX(ErrorClip, true);
    }

    public float GetGameSpeed()
    {
        return speedMultiplier;
    }

    void Reset()
    {
        currentLine = 0;
        lastActiveLoop = null;
        currentLoopRemainingRepeat = 0;
        loopStartLine = 0;
        SlotController.Instance.MarkSlot(currentLine);
        CluckController.IsProcessing = false;
        CluckController.ResetClucks();
        EventController.TriggerEvent("CPUReset");
    }

    IEnumerator Process()
    {
        CluckController.IsProcessing = true;
        if (currentLine == 20)
        {
            Reset();
            yield break;
        }

        SlotController.Instance.MarkSlot(currentLine);

        //Speed
        float holdSpeed = .25f;

        DraggableObject_Loop loop = SlotController.Instance.GetLoopReference(currentLine);
        if (loop != null)
        {
            if (loop != lastActiveLoop) //New loop
            {
                lastActiveLoop = loop;
                currentLoopRemainingRepeat = loop.GetLoopCount() - 1;
                int clucksNeeded = loop.GetCluckCount();

                //Check clucks needed
                if (clucksNeeded <= CluckController.ClucksRemaining)
                {
                    CluckController.ClucksRemaining -= clucksNeeded;
                    loopStartLine = currentLine + 1;
                }
                else
                {
                    //Throw error if not enough clucks
                    ShowError("You need " + clucksNeeded + " clucks to run that loop but got " + CluckController.ClucksRemaining + "!");
                    Reset();
                    yield break;
                }
            }

            //Process this line
            int itemIndex = currentLine - loopStartLine; //Relative pos of the item
            if (itemIndex >= 0)
            {
                DraggableObject_Item item = loop.GetItem(itemIndex + 1); //Get the item, only if this isn't the loop header
                if (item != null)
                {
                    if (item.GetItemsInLoopRemaining() > 0)
                    {
                        item.Process();
                    }
                    else
                    {
                        holdSpeed = .05f;
                    }
                }
                else
                {
                    holdSpeed = 0.05f;
                }
            }

        }

        //Set time to wait
        if (loop == null)
        {
            holdSpeed = .05f;
        }

        yield return new WaitForSeconds(holdSpeed / speedMultiplier);

        //If next line is empty or another loop, check if we need to go back to the current loop
        DraggableObject_Loop nextLineLoop = null;
        if (currentLine < 19)
        {
            nextLineLoop = SlotController.Instance.GetLoopReference(currentLine + 1);
        }


        if (nextLineLoop == loop && loop != null) //Same loop
        {
            currentLine++;
        }

        if (nextLineLoop == null || nextLineLoop != loop) //If empty, or different loop
        {
            if (currentLoopRemainingRepeat > 0) //Do we need to repeat this loop?
            {
                currentLoopRemainingRepeat--;
                currentLine = loopStartLine;
            }
            else
            {
                currentLine++; //Go to next line
            }
        }

        StartCoroutine(Process());
        yield break;
    }

}
