using System;

namespace Romert.AlienCode.LunarVielMod {
    public class RaritySparkle {
        public int Time;
        public int Lifetime;
        public float MaxScale;
        public float Scale;
        public float Rotation;
        public float RotationSpeed;
        public Vector2 Position;
        public Vector2 Velocity;
        public Color DrawColor;
        public Texture2D Texture;
        public Rectangle? BaseFrame;
        public bool UseSingleFrame;
        public float TimeLeft { get { return Lifetime - Time; } }
        public float LifetimeRatio { get { return Time / Lifetime; } }

        public void Update() {
            Position += Velocity;
            if (!CustomUpdate()) {
                Time++;
                return;
            }
            if (Time <= 20) {
                Scale = MathHelper.Lerp(0f, MaxScale, Time / 20f);
            }
            if (TimeLeft <= 20f) {
                Scale = MathHelper.Lerp(0f, MaxScale, TimeLeft / 20f);
            }
            Rotation += RotationSpeed;
            Time++;
        }
        public virtual bool CustomUpdate() {
            return true;
        }
        public virtual bool CustomDraw(SpriteBatch spriteBatch, Vector2 drawPosition) {
            return true;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition) {
            if (!CustomDraw(spriteBatch, drawPosition)) {
                return;
            }
            Rectangle? frame = null;
            if (BaseFrame != null) {
                if (UseSingleFrame) {
                    frame = new Rectangle?(BaseFrame.Value);
                }
                else {
                    int animationFrame = (int)Math.Floor((double)(Time / (Lifetime / 6f)));
                    frame = new Rectangle?(new Rectangle(0, BaseFrame.Value.Y * animationFrame, BaseFrame.Value.Width, BaseFrame.Value.Height));
                }
            }
            Color drawColor = DrawColor;
            drawColor.A = 0;
            spriteBatch.Draw(Texture, drawPosition, frame, drawColor, Rotation, (frame == null) ? (Texture.Size() * 0.5f) : (frame.Value.Size() * 0.5f), Scale, 0, 0f);
        }
    }
}
