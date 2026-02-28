using System;
using System.Collections.Generic;
using Terraria.UI.Chat;

namespace Romert.AlienCode.LunarVielMod {
    public class RarityHelper {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tooltipLine"></param>
        /// <param name="glowColor">Bg color</param>
        /// <param name="textOuterColor">Tooltip color</param>
        /// <param name="textInnerColor"></param>
        /// <param name="glowTexture"></param>
        /// <param name="glowScaleOffset"></param>
        public static void DrawBaseTooltipTextAndGlow(DrawableTooltipLine tooltipLine, Color glowColor, Color textOuterColor, Color? textInnerColor = null, Texture2D glowTexture = null, Vector2? glowScaleOffset = null) {
            if (textInnerColor == null) {
                Color value = Color.Black;
                textInnerColor = new Color?(value);
            }
            if (glowScaleOffset == null) {
                glowScaleOffset = new Vector2?(glowScaleOffset.GetValueOrDefault());
            }
            string text = tooltipLine.Text;
            Vector2 vector = tooltipLine.Font.MeasureString(text);
            Vector2 textCenter = vector * 0.5f;
            Vector2 textPosition;
            textPosition = new(tooltipLine.X, tooltipLine.Y);
            Vector2 glowPosition;
            glowPosition = new(tooltipLine.X + textCenter.X, tooltipLine.Y + textCenter.Y / 1.5f);
            Vector2 glowScale = new Vector2(vector.X * 0.115f, 0.6f) * glowScaleOffset.Value;
            glowColor.A = 0;
            if (glowTexture != null) {
                Main.spriteBatch.Draw(glowTexture, glowPosition, null, glowColor * 0.85f, 0f, glowTexture.Size() * 0.5f, glowScale, 0, 0f);
            }
            float sine = (float)((1.0 + Math.Sin((double)(Main.GlobalTimeWrappedHourly * 2.5f))) / 2.0);
            float sineOffset = MathHelper.Lerp(0.5f, 1f, sine);
            for (int i = 0; i < 12; i++) {
                Vector2 afterimageOffset = (6.2831855f * (float)i / 12f).ToRotationVector2() * (2f * sineOffset);
                ChatManager.DrawColorCodedString(Main.spriteBatch, tooltipLine.Font, text, (textPosition + afterimageOffset).RotatedBy((double)(6.2831855f * (float)(i / 12)), default), textOuterColor * 0.9f, tooltipLine.Rotation, tooltipLine.Origin, tooltipLine.BaseScale, -1f, false);
            }
            Color mainTextColor = Color.Lerp(glowColor, textInnerColor.Value, 0.9f);
            ChatManager.DrawColorCodedString(Main.spriteBatch, tooltipLine.Font, text, textPosition, mainTextColor, tooltipLine.Rotation, tooltipLine.Origin, tooltipLine.BaseScale, -1f, false);
        }
        public class CircleSparkle : RaritySparkle {
            public CircleSparkle(int lifetime, float scale, float initialRotation, float rotationSpeed, Vector2 position, Vector2 velocity, string? texture = null) {
                Lifetime = lifetime;
                Scale = 0f;
                MaxScale = scale;
                Rotation = initialRotation;
                RotationSpeed = rotationSpeed;
                Position = position;
                Velocity = velocity;
                DrawColor = Color.Lerp(Color.White, Color.White, Main.rand.NextFloat(1f));
                Texture = texture != null ? Request<Texture2D>(RarityTextureRegistry.Path(texture), (ReLogic.Content.AssetRequestMode)2).Value : Request<Texture2D>(RarityTextureRegistry.Path("GoldRingParticle3"), (ReLogic.Content.AssetRequestMode)2).Value;
                BaseFrame = null;
            }
        }
        public static void SpawnAndUpdateTooltipParticles(DrawableTooltipLine tooltipLine, ref List<RaritySparkle> sparklesList, int spawnChance, SparkleType sparkleType, string? texture = null) {
            Vector2 textSize = tooltipLine.Font.MeasureString(tooltipLine.Text);
            if (Main.rand.NextBool(spawnChance)) {
                if (sparkleType != SparkleType.DefaultSparkle) {
                    if (sparkleType == SparkleType.MagicCircle) {
                        int lifetime = (int)Main.rand.NextFloat(45f, 70f);
                        float scale = Main.rand.NextFloat(0.015f, 0.03f);
                        Vector2 position = Main.rand.NextVector2FromRectangle(new Rectangle(-(int)(textSize.X * 0.5f), -(int)(textSize.Y * 0.4f), (int)textSize.X, (int)(textSize.Y * 0.35f)));
                        Vector2 velocity = Vector2.UnitY * Main.rand.NextFloat(0.1f, 0.25f);
                        sparklesList.Add(new CircleSparkle(lifetime, scale, 0f, 0f, position, velocity, texture));
                    }
                }
                else {
                    int lifetime = (int)Main.rand.NextFloat(45f, 70f);
                    float scale = Main.rand.NextFloat(0.15f, 0.3f);
                    Vector2 position = Main.rand.NextVector2FromRectangle(new Rectangle(-(int)(textSize.X * 0.5f), -(int)(textSize.Y * 0.4f), (int)textSize.X, (int)(textSize.Y * 0.35f)));
                    Vector2 velocity = Vector2.UnitY * Main.rand.NextFloat(0.1f, 0.25f);
                    sparklesList.Add(new DefaultSparkle(lifetime, scale, 0f, 0f, position, velocity));
                }
            }
            for (int i = 0; i < sparklesList.Count; i++) {
                sparklesList[i].Update();
            }
            sparklesList.RemoveAll(s => s.Time >= s.Lifetime);
            foreach (RaritySparkle sparkle in sparklesList) {
                sparkle.Draw(Main.spriteBatch, new Vector2((float)tooltipLine.X, (float)tooltipLine.Y) + textSize * 0.5f + sparkle.Position);
            }
        }
    }
}