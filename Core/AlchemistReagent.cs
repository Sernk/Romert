using Romert.Content.Reagents.Rarity;
using Romert.Core.Exceptions;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Romert.Core;

public abstract class AlchemistReagent : ModType, ILocalizedModType {
    public string LocalizationCategory => "Reagents";
    public string SearchName { get; internal set; }
    public bool Synergy { get; internal set; }
    public AlchemistReagentData CurrentType { get; internal set; }
    public ReagentRarity Rarity { get; internal set; }

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
    protected override void Register() {
        ModTypeLookup<AlchemistReagent>.Register(this);
    }
    // В каком предмете он находится и сколько нужно для открытия в книге!
    public virtual void Register(RegisterReagent register) { }
    public void SetDefaults() {
        SetStaticDefaults();
        SearchName = LocalizationName;
    }
    public void Draw(SpriteBatch sb, Vector2 pos) {
        Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(LocalizationName);
        Vector2 rareSize = FontAssets.MouseText.Value.MeasureString(Rarity.Tooltips);
        Vector2 heling;
        if (nameSize.X > rareSize.X) { heling = nameSize; }
        else { heling = rareSize; }
        heling.X += 20;
        int posY = (int)heling.Y * 2 + 30;

        sb.Draw(GetTexture("HoverReagent_Bg"), position: new(pos.X + 6, pos.Y), new(200, 200, (int)heling.X + 4, posY), Color.White);
        sb.Draw(GetTexture("HoverReagent_Frame"), position: new(pos.X - 2, pos.Y), GetTexture("HoverReagent_Frame").Frame(4, 1, 0, 0), Color.White);
        sb.Draw(GetTexture("HoverReagent_Left"), destinationRectangle: new Rectangle((int)(pos.X + 10), (int)pos.Y, (int)heling.X, GetTexture("HoverReagent_Left").Height), null, Color.White);
        sb.Draw(GetTexture("HoverReagent_Frame"), new(pos.X + heling.X + 29, pos.Y + 6), GetTexture("HoverReagent_Frame").Frame(4, 1, 1, 0), Color.White, 0, GetTexture("HoverReagent_Frame").Size() / 2f, 1, SpriteEffects.None, 1);
        sb.Draw(GetTexture("HoverReagent_Down"), destinationRectangle: new Rectangle((int)(pos.X -2), (int)pos.Y + 12, 8, posY - 17), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
        sb.Draw(GetTexture("HoverReagent_Down"), destinationRectangle: new Rectangle((int)(pos.X + heling.X + 9), (int)pos.Y + 12, 8, posY - 17), null, Color.White);
        sb.Draw(GetTexture("HoverReagent_Frame"), position: new(pos.X - 2, pos.Y + posY -5), GetTexture("HoverReagent_Frame").Frame(4, 1, 2, 0), Color.White);
        sb.Draw(GetTexture("HoverReagent_Left"), destinationRectangle: new Rectangle((int)(pos.X + 10), (int)pos.Y + posY - 1, (int)heling.X, GetTexture("HoverReagent_Left").Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 1);
        sb.Draw(GetTexture("HoverReagent_Frame"), position: new(pos.X + heling.X + 5, pos.Y + posY - 5), GetTexture("HoverReagent_Frame").Frame(4, 1, 3, 0), Color.White);

        Vector2 posText = new(pos.X + 20, pos.Y + 20);

        Color color;
        if (Rarity.IsAnimated) { color = Rarity.AnimatedColor(Rarity.Colors, Rarity.Time); }
        else { color = Rarity.Color; }

        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, LocalizationName, posText.X, posText.Y, color, Rarity.BorderColor, Vector2.Zero, 1f);
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Rarity.Tooltips, posText.X, posText.Y + 30, Color.White, Color.Black, Vector2.Zero, 1f);

        static Texture2D GetTexture(string name) => Request<Texture2D>("Romert/Asset/Textures/UI" + ShortCat[0] + name).Value;
    }

    public virtual bool HasTexture => true;

    public virtual string LocalizationName => this.GetLocalization("Name").Value;
    public virtual string Descriptions => this.GetLocalization("Descriptions").Value;
    public virtual string TexturePatch => (GetType().Namespace + "." + Name).Replace('.', '/');

    // use only add item debuff and add player buff
    public virtual void Buff(Player player, AlchemistData buff) { }
    public virtual FlaskItemData EditItem() => new("");
    public virtual bool CanNewShot(bool synergy) => false;
    public virtual bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => true;
    public virtual bool CanBySynergia(AlchemistReagent reagent) => false;
    public virtual void Synergia(Player player, Item item, AlchemistReagent reagent) { }
    public virtual void Recipe(Alchemy alchemy) { }
    // use only for draw in book Draw(sb, pos) for vanilla draw
    public virtual void DrawElement(SpriteBatch sb, Vector2 pos) => Draw(sb, pos);
    public virtual void SetRarity(ReagentRarity rarity) {
        Rarity = GetReagentRarity<BaseRarity>();
    }
    public override string ToString() => Name;
}