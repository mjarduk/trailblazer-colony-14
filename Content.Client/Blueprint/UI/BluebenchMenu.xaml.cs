﻿using Content.Shared.Blueprint;
using Robust.Client.AutoGenerated;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Utility;

namespace Content.Client.Blueprint.UI;

[GenerateTypedNameReferences]
public sealed partial class BluebenchMenu : DefaultWindow
{
    [Dependency] private readonly IEntityManager _entityManager = default!;
    [Dependency] private readonly ILocalizationManager Loc = default!;
    private SpriteSystem _spriteSystem;

    public BluebenchMenu()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);

        _spriteSystem = _entityManager.System<SpriteSystem>();
    }

    public void UpdateResearchEntries(HashSet<BluebenchResearchPrototype> researchEntries)
    {
        ResearchList.RemoveAllChildren();
        foreach (var entry in researchEntries)
        {
            var message = new FormattedMessage();
            message.AddMarkupOrThrow(entry.Description!);
            message.PushNewline();

            foreach (var (key, value) in entry.StackRequirements)
            {
                message.PushNewline();
                message.AddMarkupOrThrow($"{value}x {key.Id}");
            }
            foreach (var (key, value) in entry.TagRequirements)
            {
                message.PushNewline();
                message.AddMarkupOrThrow($"{value.Amount}x {key.Id}");
            }
            foreach (var (key, value) in entry.ComponentRequirements)
            {
                message.PushNewline();
                message.AddMarkupOrThrow($"{value.Amount}x {key}");
            }

            ResearchList.AddChild(new BluebenchResearchEntry(entry.Name!, message, _spriteSystem.Frame0(entry.Icon!), entry.ID));
        }
    }
}
