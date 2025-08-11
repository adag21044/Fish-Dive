using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem; // New Input System
#endif

public class TimeScaleController : MonoBehaviour
{
    public static TimeScaleController Instance;

    [Range(0f, 10f)]
    [SerializeField] private float timeScale = 1f;
    private bool isFrozen = false;

    private void Awake()
    {
        // Robust singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("TimeScaleController: Duplicate found, destroying this one.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("TimeScaleController: Awake (alive).");
    }

    private void Start()
    {
        Apply(timeScale); // match inspector at startup
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null; // safe for Domain Reload off
    }

    private void Update()
    {
        // Decrease
        if (IsMinusDown())
        {
            Apply(timeScale - 0.5f);
            return;
        }

        // Increase
        if (IsPlusDown())
        {
            Debug.Log("Increasing time scale.");
            Apply(timeScale + 0.5f);
            return;
        }

        // Toggle freeze with I
        if (KeyboardToggleFreezeDown())
        {
            isFrozen = !isFrozen;
            Apply(isFrozen ? 0f : 1f);
        }
    }

    // --- Input helpers (Legacy + keypad + fallback keys) ---
    private bool IsPlusDown()
    {
        // main '+' is Shift + '=' on most layouts; also support keypad '+', and ']' as a simple fallback
        bool legacy =
            Input.GetKeyDown(KeyCode.KeypadPlus) ||
            (Input.GetKeyDown(KeyCode.Equals) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) ||
            Input.GetKeyDown(KeyCode.RightBracket);

#if ENABLE_INPUT_SYSTEM
        // New Input System path (works even if Legacy is disabled)
        var k = Keyboard.current;
        bool newInput =
            (k != null) && (
                (k.equalsKey.wasPressedThisFrame && (k.leftShiftKey.isPressed || k.rightShiftKey.isPressed)) ||
                (k.numpadPlusKey != null && k.numpadPlusKey.wasPressedThisFrame) ||
                k.rightBracketKey.wasPressedThisFrame
            );
        return legacy || newInput;
#else
        return legacy;
#endif
    }

    private bool IsMinusDown()
    {
        // support main '-' + keypad '-' and '[' as fallback
        bool legacy =
            Input.GetKeyDown(KeyCode.Minus) ||
            Input.GetKeyDown(KeyCode.KeypadMinus) ||
            Input.GetKeyDown(KeyCode.LeftBracket);

#if ENABLE_INPUT_SYSTEM
        var k = Keyboard.current;
        bool newInput =
            (k != null) && (
                k.minusKey.wasPressedThisFrame ||
                (k.numpadMinusKey != null && k.numpadMinusKey.wasPressedThisFrame) ||
                k.leftBracketKey.wasPressedThisFrame
            );
        return legacy || newInput;
#else
        return legacy;
#endif
    }

    private bool KeyboardToggleFreezeDown()
    {
#if ENABLE_INPUT_SYSTEM
        var k = Keyboard.current;
        if (k != null && k.iKey.wasPressedThisFrame) return true;
#endif
        return Input.GetKeyDown(KeyCode.I);
    }

    // --- Core ---
    private void Apply(float scale)
    {
        timeScale = Mathf.Clamp(scale, 0f, 10f);
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // keep physics in sync
        Debug.Log($"⏱️ Time scale set to {timeScale}");
    }

    public float GetTimeScale() => timeScale;
}
