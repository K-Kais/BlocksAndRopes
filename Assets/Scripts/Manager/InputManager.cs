using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance { get => instance; }

    [SerializeField] protected Vector3 mouseWorldPos;
    public Vector3 MouseWorldPos { get => mouseWorldPos; }

    [SerializeField] protected bool onMouseDrag = false;
    public bool OnMouseDrag { get => onMouseDrag; }

    [SerializeField] protected bool onMouseDown = false;
    public bool OnMouseDown { get => onMouseDown; }

    private void Awake()
    {
        if (InputManager.instance != null) Debug.LogError("Only one InputManager allow to exist");
        InputManager.instance = this;
    }
    private void Update()
    {
        this.GetMousePos();
        this.GetOnMouseAll();
    }

    protected virtual void GetMousePos()
    {
        this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void GetOnMouseAll()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.onMouseDown = true;
            this.onMouseDrag = true;
        }
        else if (Input.GetMouseButton(0) && this.onMouseDown) this.onMouseDown = false;
        else if (Input.GetMouseButtonUp(0)) this.onMouseDrag = false;
    }
}
