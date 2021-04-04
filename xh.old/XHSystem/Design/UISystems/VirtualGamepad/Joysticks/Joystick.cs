using Godot;
using System;

public class Joystick : Control
{
    public enum VisibilityMode { ALWAYS, TOUCHSCREEN_ONLY }
    public enum JoystickMode { FIXED, DYNAMIC, FOLLOWING }
    public enum VectorMode {REAL, NORMALIZED}
    public bool IsWorking = false;
    public Vector2 output = Vector2.Zero;
    [Export] public VisibilityMode Visibility_Mode = VisibilityMode.TOUCHSCREEN_ONLY;
    [Export] public JoystickMode Joystick_Mode = JoystickMode.FIXED;
    [Export] public VectorMode Vector_Mode = VectorMode.REAL; 

    [Export] public Color PressedColor = Colors.Gray; 
    [Export(PropertyHint.Range, "0,12")] public int Directions = 0;
    [Export] public float SimmetryAngle = 90;
    [Export(PropertyHint.Range, "0,0.5")] public float DeadZone = 0.2f;
    [Export(PropertyHint.Range, "0.5,2")] public float ClampZone = 1;

    private TextureRect _background;
    private TextureRect _handle; 
    private Color _original_color;
    private Vector2 _original_position;
    private int _touch_index = -1;

    public override void _Ready()
    {
        Initial();
    }
    public override void _Input(InputEvent inputEvent)
    {
        if (!(inputEvent is InputEventScreenTouch || inputEvent is InputEventScreenDrag))
        {
            return;
        }

        if (inputEvent is InputEventScreenTouch inputEventScreenTouch)
        {
            if (HaveTouchStarted(inputEventScreenTouch) && HaveInsideControlRect(inputEventScreenTouch.Position, this))
            {
                if (Joystick_Mode == JoystickMode.DYNAMIC || Joystick_Mode == JoystickMode.FOLLOWING)
                {
                    ControlVector(_background, inputEventScreenTouch.Position);
                }
                if (HaveInsideControlCircle(inputEventScreenTouch.Position, _background))
                {
                    _touch_index = inputEventScreenTouch.Index;
                    _handle.SelfModulate = PressedColor;                
                }              
            }    
            else if (HaveTouchEnded(inputEventScreenTouch))
            {
                Reset();
                _handle.RectGlobalPosition = 
                    (_background.RectGlobalPosition + _background.RectSize/2) - ((_handle.RectSize) / 2);
            }     
        }
        
        else if (inputEvent is InputEventScreenDrag inputEventScreenDrag && _touch_index == inputEventScreenDrag.Index)
        {
            float radius = _background.RectSize.x / 2;
            float DeadSize = radius * DeadZone;
            float ClampSize = radius * ClampZone;

            Vector2 center = _background.RectGlobalPosition + (_background.RectSize / 2);
            Vector2 vector = inputEventScreenDrag.Position - center;

            if (vector.Length() > DeadZone)
            {
                if(Directions > 0)
                {
                    vector = DirectionalVector(vector, Directions, SimmetryAngle);
                }
                if (Vector_Mode == VectorMode.NORMALIZED)
                {
                    output = vector.Normalized();
                    _handle.RectGlobalPosition = (output * ClampSize + center) - (_handle.RectSize / 2);
                }    
                else if (Vector_Mode == VectorMode.REAL)
                {
                    Vector2 ClampedVector = vector.Clamped(ClampSize);
				    output = vector.Normalized() * (ClampedVector.Length() - DeadSize) / (ClampSize - DeadSize);
				    _handle.RectGlobalPosition = (output * ClampSize + center) - (_handle.RectSize / 2);
                }   
                IsWorking = true;
                if (Joystick_Mode == JoystickMode.FOLLOWING)
                {
                    Following(vector);
                }           
            }  
            else 
            {
                IsWorking = false;
                output = Vector2.Zero;
                _handle.RectGlobalPosition = 
                    (_background.RectGlobalPosition + _background.RectSize /2) - ((_handle.RectSize) / 2);
            }      
        }
    }

    #region 数据更新
    private void Initial()
    {
        HideJoystick();

        _background = GetChild<TextureRect>(0);
        _handle = _background.GetChild<TextureRect>(0);

        _background.RectPivotOffset = _background.RectSize / 2;
        _handle.RectPivotOffset = _handle.RectSize / 2;

        _original_color = _handle.SelfModulate;
        _original_position = _background.RectPosition;
    }

    private void HideJoystick()
    {
        bool isHide = !OS.HasTouchscreenUiHint() && Visibility_Mode == VisibilityMode.TOUCHSCREEN_ONLY;
        if (isHide) Hide();
    }

    private void ControlVector(Control control, Vector2 inputVector)
    {
        control.RectGlobalPosition = inputVector - control.RectSize / 2;
    }
    private void Reset()
    {
        _touch_index = -1;
        IsWorking = false;
        output = Vector2.Zero;
        _handle.SelfModulate = _original_color;
        _background.RectPosition = _original_position;
    }
    private void Following(Vector2 vector)
    {
        float ClampSize = ClampZone * _background.RectSize.x / 2;
        if (vector.Length() > ClampSize)
        {
            Vector2 radius = vector.Normalized() * ClampSize;
            Vector2 delta = vector - radius;
            Vector2 NewPos = _background.RectPosition + delta;           
            NewPos.x = Mathf.Clamp(NewPos.x, -_background.RectSize.x / 2, RectSize.x - _background.RectSize.x / 2);
            NewPos.y = Mathf.Clamp(NewPos.y, -_background.RectSize.y / 2, RectSize.x - _background.RectSize.y / 2);
            _background.RectPosition = NewPos;
        }	
    }
    #endregion
    #region 摇杆判断
    private bool HaveTouchStarted(InputEvent inputEvent)
    {
        bool isStarted = (inputEvent is InputEventScreenTouch inputEventScreenTouch && inputEventScreenTouch.Pressed) 
                    && _touch_index == -1; 
        return isStarted;
    }
    private bool HaveTouchEnded(InputEvent inputEvent)
    {
        bool isEnded = (inputEvent is InputEventScreenTouch inputEventScreenTouch && !inputEventScreenTouch.Pressed)
                    && _touch_index == inputEventScreenTouch.Index;
        return isEnded;
    }
    private bool HaveInsideControlRect(Vector2 inputPos, Control control)
    {
        bool isX = (inputPos.x > control.RectGlobalPosition.x)
                && (inputPos.x < (control.RectGlobalPosition.x + (control.RectSize.x * control.RectScale.x)));
        bool isY = (inputPos.y > control.RectGlobalPosition.y)
                && (inputPos.y < (control.RectGlobalPosition.y + (control.RectSize.y * control.RectScale.y)));
        return isX && isY;
    }
    private bool HaveInsideControlCircle(Vector2 inputPos, Control control)
    {
        float radius = (control.RectScale.x * control.RectSize.x) / 2;
        Vector2 center = control.RectGlobalPosition + new Vector2(radius, radius);
        Vector2 vector = inputPos - center;
        //return vector.Length() < radius ;
        return vector.LengthSquared() < (radius * radius);
    }
    private Vector2 DirectionalVector(Vector2 vector, int _directions, float simmetry_angle)
    {
        float s_angle = Mathf.Deg2Rad(simmetry_angle);
        float angel = (vector.Angle() + s_angle) / (Mathf.Pi / _directions);
        if(angel >= 0)
        {
            angel = Mathf.Floor(angel);
        } else
        {
            angel = Mathf.Ceil(angel);
        }
        if((Mathf.Abs(angel) % 2) == 1)
        {
            if(angel >= 0)
            {
                angel += 1;
            }else
            {
                angel -= 1;
            }
        }
        angel *= Mathf.Pi / _directions;
        angel -= s_angle;
        Vector2 directions = new Vector2(Mathf.Cos(angel), Mathf.Sin(angel));
        return directions * vector.Length();
    }
    #endregion
}
