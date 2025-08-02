using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string text;

    private struct State
    {
        public float timer;
        public Vector3 mousePosition;
        public bool isUnderMouse;
    }

    private bool isVisible;
    private State state;

    void OnMouseEnter()
    {
        state = default;

        state.timer = 0f;
        state.isUnderMouse = true;
        state.mousePosition = Input.mousePosition;
        Debug.Log($"MouseEnter");
    }

    void OnMouseExit()
    {
        state = default;
        Hide();
        Debug.Log($"MousExit");
    }

    void Update()
    {
        if (!state.isUnderMouse)
        {
            Hide();
            return;
        }

        var isClicking = Input.GetMouseButton(0) || Input.GetMouseButton(1);
        if (isClicking)
            state.timer = 0;

        var newMousePos = Input.mousePosition;
        var diff = newMousePos - state.mousePosition;
        var isStatic = diff.sqrMagnitude < 0.01f;
        state.mousePosition = newMousePos;

        if (isStatic)
            state.timer += Time.deltaTime;

        if (state.timer > Consts.TOOLTIP_TIME)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        if (isVisible)
            return;

        isVisible = true;
        Debug.Log($"SHOW");

        UIController.Instance.tooltip.Show(text, transform.position);
    }

    private void Hide()
    {
        if (!isVisible)
            return;

        isVisible = false;
        Debug.Log($"HIDE");

        UIController.Instance.tooltip.Hide();
    }
}
