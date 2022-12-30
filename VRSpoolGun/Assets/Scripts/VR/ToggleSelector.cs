using Oculus.Interaction;
using Oculus.Interaction.Input;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class ToggleSelector : MonoBehaviour, ISelector
{
    public enum ControllerSelectorLogicOperator
    {
        Any = 0,
        All = 1
    }

    [SerializeField, Interface(typeof(IController))]
    private MonoBehaviour _controller;

    [SerializeField]
    private ControllerButtonUsage _controllerButtonUsage;

    [SerializeField]
    private ControllerSelectorLogicOperator _requireButtonUsages =
        ControllerSelectorLogicOperator.Any;

    #region Properties
    public ControllerButtonUsage ControllerButtonUsage
    {
        get
        {
            return _controllerButtonUsage;
        }
        set
        {
            _controllerButtonUsage = value;
        }
    }

    public ControllerSelectorLogicOperator RequireButtonUsages
    {
        get
        {
            return _requireButtonUsages;
        }
        set
        {
            _requireButtonUsages = value;
        }
    }
    #endregion

    public IController Controller { get; private set; }

    public event Action WhenSelected = delegate { };
    public event Action WhenUnselected = delegate { };

    private bool wasSelectedLastFrame;

    protected virtual void Awake()
    {
        Controller = _controller as IController;
    }

    protected virtual void Start()
    {
        Assert.IsNotNull(Controller);
    }

    bool toggle = false;
    protected virtual void Update()
    {
        bool buttonSelected = _requireButtonUsages == ControllerSelectorLogicOperator.All
            ? Controller.IsButtonUsageAllActive(_controllerButtonUsage)
            : Controller.IsButtonUsageAnyActive(_controllerButtonUsage);

        // Detect a change
        if (buttonSelected)
        {
            if (!wasSelectedLastFrame)
            {
                toggle = !toggle;

                // Check on/off
                if (toggle)
                    WhenSelected();
                else
                    WhenUnselected();
            }
        }

        wasSelectedLastFrame = buttonSelected;
    }

    #region Inject

    public void InjectAllControllerSelector(IController controller)
    {
        InjectController(controller);
    }

    public void InjectController(IController controller)
    {
        _controller = controller as MonoBehaviour; ;
        Controller = controller;
    }

    #endregion
}
