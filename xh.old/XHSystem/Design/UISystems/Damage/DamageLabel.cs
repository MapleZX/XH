using Godot;
using System;

public class DamageLabel : Label
{
    Tween tween;
    private Color initalColor;

    public override void _Ready()
    {
        tween = GetChild<Tween>(0);
        tween.Connect("tween_all_completed", this, nameof(tween_all_completed));

        initalColor = Modulate;
    }
    public void ShowDamageLabel(DamageEventArgs e)
    {
        Text = e.damage.ToString();
        var random = new RandomNumberGenerator();
        var min = (double)(- e.spread / 2);
        var max = (double)( e.spread / 2);
        var movement =  e.travel.Rotated((float)GD.RandRange(min, max));
        
        RectPivotOffset = RectSize / 2;

        tween.InterpolateProperty(
            this, "rect_position", RectPosition, RectPosition + movement, e.duration, Tween.TransitionType.Linear, Tween.EaseType.InOut);

        if (e.crit)
        {
            Modulate = e.color;
            tween.InterpolateProperty(
                this, "rect_scale", RectScale * e.multiple, RectScale, e.duration, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        }

        tween.InterpolateProperty(
            this, "modulate:a", 1.0, 0.0, e.duration, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        tween.Start();
    }
    
    void tween_all_completed()
    {
        Hide();
        Modulate = initalColor;
    }
}
