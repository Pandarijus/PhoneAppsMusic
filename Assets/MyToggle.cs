using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Toggle))]
public abstract class MyToggle : MonoBehaviour
{
    private Toggle toggle;

    void OnEnable()
    {
        GetToggle().onValueChanged.AddListener(OnToggleChange);
    }

    void OnDisable()
    {
        GetToggle().onValueChanged.RemoveListener(OnToggleChange);
    }

    private Toggle GetToggle()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }
        return toggle;
    }

    public void SetToggle(bool isOn)
    {
        GetToggle().isOn = isOn;
    }
    protected abstract void OnToggleChange(bool isOn);

}