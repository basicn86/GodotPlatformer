using Godot;
using System;

public partial class Player : RigidBody3D
{
	public static float MouseSensitivity = 0.1f;
	public static float TwistDegrees = 0.0f;
	public static float PitchDegrees = 0.0f;
	Node3D TwistPivot; //cache the pivot node for the twist rotation
	Node3D PitchPivot;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		TwistPivot = GetNode<Node3D>("TwistPivot");
		PitchPivot = GetNode<Node3D>("TwistPivot/PitchPivot");
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

		TwistPivot.Set(Node3D.PropertyName.RotationDegrees, new Vector3(0.0f, TwistDegrees, 0.0f));
		PitchPivot.Set(Node3D.PropertyName.RotationDegrees, new Vector3(PitchDegrees, 0.0f, 0.0f));
	}

	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseMotion)
		{
			if(Input.MouseMode == Input.MouseModeEnum.Captured)
			{
				TwistDegrees += MouseSensitivity * -((InputEventMouseMotion)@event).Relative.X;
				if(TwistDegrees > 360.0f) TwistDegrees -= 360.0f;
				if(TwistDegrees < 0.0f) TwistDegrees += 360.0f;
				PitchDegrees += MouseSensitivity * -((InputEventMouseMotion)@event).Relative.Y;
				PitchDegrees = Math.Clamp(PitchDegrees, -85.0f, 85.0f);
			}
		} 
	}
}
