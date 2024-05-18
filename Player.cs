using Godot;
using System;

public partial class Player : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 input = Vector3.Zero;
		input.X = Input.GetAxis("move_left", "move_right");
		input.Z = Input.GetAxis("move_forward", "move_back");

		input = input.Normalized();

		ApplyCentralForce(input * 2000f * (float)delta);
	}
}
