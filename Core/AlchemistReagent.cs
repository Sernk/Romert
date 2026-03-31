using Romert.Content.Reagents.Flask;
using Romert.Content.Reagents.Rarity;
using Romert.Core.Exceptions;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent;
using static System.Net.Mime.MediaTypeNames;

namespace Romert.Core;

public abstract class AlchemistReagent : ModType, ILocalizedModType {
    public string LocalizationCategory => "Reagents";
    public string SearchName { get; internal set; }
    public bool Synergy { get; internal set; }
    public AlchemistReagentData CurrentType { get; internal set; }
    public ReagentRarity Rarity { get; internal set; }

    public void SetDefaults() {
        SearchName = LocalizationName;
        SetStaticDefaults();
    }
    public void Draw(SpriteBatch sb, Vector2 pos, Vector2 heling, int posY) {
        sb.Draw(GetTexture("HoverReagent_Bg"), position: new(pos.X + 6, pos.Y), new(200, 200, (int)heling.X + 4, posY), Color.White);
        sb.Draw(GetTexture("HoverReagent_Frame"), position: new(pos.X - 2, pos.Y), GetTexture("HoverReagent_Frame").Frame(4, 1, 0, 0), Color.White);
        sb.Draw(GetTexture("HoverReagent_Left"), destinationRectangle: new Rectangle((int)(pos.X + 10), (int)pos.Y, (int)heling.X, GetTexture("HoverReagent_Left").Height), null, Color.White);
        sb.Draw(GetTexture("HoverReagent_Frame"), new(pos.X + heling.X + 29, pos.Y + 6), GetTexture("HoverReagent_Frame").Frame(4, 1, 1, 0), Color.White, 0, GetTexture("HoverReagent_Frame").Size() / 2f, 1, SpriteEffects.None, 1);
        sb.Draw(GetTexture("HoverReagent_Down"), destinationRectangle: new Rectangle((int)(pos.X - 2), (int)pos.Y + 12, 8, posY - 17), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
        sb.Draw(GetTexture("HoverReagent_Down"), destinationRectangle: new Rectangle((int)(pos.X + heling.X + 9), (int)pos.Y + 12, 8, posY - 17), null, Color.White);
        sb.Draw(GetTexture("HoverReagent_Frame"), position: new(pos.X - 2, pos.Y + posY - 5), GetTexture("HoverReagent_Frame").Frame(4, 1, 2, 0), Color.White);
        sb.Draw(GetTexture("HoverReagent_Left"), destinationRectangle: new Rectangle((int)(pos.X + 10), (int)pos.Y + posY - 1, (int)heling.X, GetTexture("HoverReagent_Left").Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 1);
        sb.Draw(GetTexture("HoverReagent_Frame"), position: new(pos.X + heling.X + 5, pos.Y + posY - 5), GetTexture("HoverReagent_Frame").Frame(4, 1, 3, 0), Color.White);
        static Texture2D GetTexture(string name) => Request<Texture2D>("Romert/Asset/Textures/UI" + ShortCat[0] + name).Value;
    }
    public void DrawElement2(SpriteBatch sb, Vector2 pos) {
        Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(LocalizationName);
        Vector2 rareSize = FontAssets.MouseText.Value.MeasureString(Rarity.Tooltips);
        Vector2 heling;

        if (nameSize.X > rareSize.X) { heling = nameSize; }
        else { heling = rareSize; }
        heling.X += 20;
        int posY = (int)heling.Y * 2 + 30;

        Draw(sb, pos, heling, posY);
        Vector2 posText = new(pos.X + 20, pos.Y + 20);

        Color color;
        if (Rarity.IsAnimated) { color = Rarity.AnimatedColor(Rarity.Colors, Rarity.Time); }
        else { color = Rarity.Color; }

        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, LocalizationName, posText.X, posText.Y, color, Rarity.BorderColor, Vector2.Zero, 1f);
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Rarity.Tooltips, posText.X, posText.Y + 30, Color.White, Color.Black, Vector2.Zero, 1f);
    }
    public void DrawElementInInventory2(SpriteBatch sb, Vector2 pos, AlchemistReagent[] reagents = null, AlchemistReagent reagent = null) {
        List<string> names = [];
        string text;

        if (reagents != null) {
            for (int i = 0; i < reagents.Length; i++) {
                if (reagents[i].Name != GetReagent<NoN>().Name && reagents[i].Name != GetReagent<Look>().Name) { names.Add(reagents[i].LocalizationName); }
            }
            text = Loc("Alchemist", "Tooltips.ActiveReagent") + " " + string.Join(", ", names);
        }
        else {
            text = Loc("Alchemist", "Tooltips.Add") + " " + reagent.Name;
        }

        Vector2 tooltips = FontAssets.MouseText.Value.MeasureString(text);
        Vector2 heling = tooltips; heling.X += 30;
        Vector2 posText = new(pos.X + 20, pos.Y + 20);

        int posY = (int)heling.Y + 30;

        Draw(sb, pos, heling, posY);

        Color color;

        if (Rarity.IsAnimated) { color = Rarity.AnimatedColor(Rarity.Colors, Rarity.Time); }
        else { color = Rarity.Color; }

        if (names.Count != 0) {
            Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Loc("Alchemist", "Tooltips.ActiveReagent"), posText.X, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
            float scale = 0;
            for (int i = 0; i < names.Count; i++) {
                Vector2 fistTextSize = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.ActiveReagent"));
                Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(names[i]);
                string subString = i < names.Count - 1 ? ", " : ".";

                Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {names[i]}", posText.X + fistTextSize.X + scale, posText.Y, color, Rarity.BorderColor, Vector2.Zero, 1f);
                Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {subString}", posText.X + nameSize.X + fistTextSize.X + scale, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
                scale += nameSize.X + 10;
            }
        }
        else {
            Vector2 fistTextSize = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.Add"));
            Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Loc("Alchemist", "Tooltips.Add"), posText.X, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
            Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {reagent.LocalizationName}", posText.X + fistTextSize.X, posText.Y, color, Rarity.BorderColor, Vector2.Zero, 1f);
        }
    }
    public void DrawElementInInventoryReagent(SpriteBatch sb, Vector2 pos, AlchemistReagent reagent) {
        Vector2 tooltips = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.Has") + " " + reagent.LocalizationName);
        Vector2 heling = tooltips; heling.X += 30;
        Vector2 posText = new(pos.X + 20, pos.Y + 20);

        int posY = (int)heling.Y + 30;

        Draw(sb, pos, heling, posY);

        Color color;

        if (Rarity.IsAnimated) { color = Rarity.AnimatedColor(Rarity.Colors, Rarity.Time); }
        else { color = Rarity.Color; }

        Vector2 fistTextSize = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.Has"));
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Loc("Alchemist", "Tooltips.Has"), posText.X, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {reagent.LocalizationName}", posText.X + fistTextSize.X, posText.Y, color, Rarity.BorderColor, Vector2.Zero, 1f);
    }

    public sealed override void Load() {
        AlchemistReagentManager.ReagentsData.Add(this);
        AlchemistReagentManager.ReagentID.Add(Name, this);
        _ = this.GetLocalization("Name").Value;
        _ = this.GetLocalization("Descriptions").Value;
        if (HasTexture) {
            if (!HasAsset(TexturePatch)) {
                throw new NoTexture(Name);
            }
        }
    }
    protected sealed override void Register() => ModTypeLookup<AlchemistReagent>.Register(this);
    
    public virtual bool HasTexture => true;

    public virtual string LocalizationName => this.GetLocalization("Name").Value;
    public virtual string Descriptions => this.GetLocalization("Descriptions").Value;
    public virtual string TexturePatch => (GetType().Namespace + "." + Name).Replace('.', '/');

    public virtual void Register(RegisterReagent register) { }
    public virtual void Buff(Player player, AlchemistData buff) { }
    public virtual FlaskItemData EditItem() => new("");
    public virtual bool CanNewShot(bool synergy) => false;
    public virtual bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => true;
    public virtual bool CanBySynergia(AlchemistReagent reagent) => false;
    public virtual void Synergia(Player player, Item item, AlchemistReagent reagent) { }
    public virtual void Recipe(Alchemy alchemy) { }
    public virtual void DrawElement(SpriteBatch sb, Vector2 pos) => DrawElement2(sb, pos);
    public virtual void DrawElementInInventory(SpriteBatch sb, Vector2 pos, bool reagentItem = false, AlchemistReagent[] reagents = null, AlchemistReagent reagent = null) {
        if (reagentItem) { DrawElementInInventoryReagent(sb, pos, reagent); }
        else { DrawElementInInventory2(sb, pos, reagents, reagent); }      
    }
    public virtual void SetRarity(ReagentRarity rarity) => Rarity = GetReagentRarity<BaseRarity>();
    public override string ToString() => Name;
}