using Godot;

namespace Pokemon.UserInput
{
    public class UserInputReader
    {
        public bool DownKey { get; private set; }
        public bool UpKey { get; private set; }
        public bool LeftKey { get; private set; }
        public bool RightKey { get; private set; }

        public bool PrimaryAttack { get; private set; }

        public bool IsKeyDown
        {
            get
            {
                return DownKey || UpKey || LeftKey || RightKey;
            }
        }

        public Vector2 NormalizedInputVector { get; private set; } = Vector2.Zero;

        public void DetectInput()
        {
            DownKey = Input.IsActionPressed("ui_down");
            RightKey = Input.IsActionPressed("ui_right");
            LeftKey = Input.IsActionPressed("ui_left");
            UpKey = Input.IsActionPressed("ui_up");

            PrimaryAttack = Input.IsActionJustPressed("ui_accept");

            NormalizedInputVector = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();
        }
    }
}