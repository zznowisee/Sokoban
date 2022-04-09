using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    Transform transform { get; }
    void LeftClick();
    void LeftRelease();
    void RightClick();
    void Dragging();
}
