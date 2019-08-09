using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : BasicManager<SwipeController>
{
    public void MoveUp()
    {
        SpaceshipController.instance.Move(SpaceshipController.Direction.Up);
    }

    public void MoveDown()
    {
        SpaceshipController.instance.Move(SpaceshipController.Direction.Down);
    }

    public void MoveLeft()
    {
        SpaceshipController.instance.Move(SpaceshipController.Direction.Left);
    }

    public void MoveRight()
    {
        SpaceshipController.instance.Move(SpaceshipController.Direction.Right);
    }
}
