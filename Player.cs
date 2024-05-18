using Godot;
using System;

public partial class Player : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 moveVector = Vector3.Zero;
		moveVector.X = Input.GetAxis("move_left", "move_right");
		moveVector.Z = Input.GetAxis("move_forward", "move_back");

		moveVector = moveVector.Normalized();

		ApplyCentralForce(moveVector * 2000f * (float)delta);

		if (Input.IsActionJustPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
}
