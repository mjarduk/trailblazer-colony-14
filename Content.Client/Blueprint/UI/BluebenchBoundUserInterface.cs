﻿using Content.Client.VendingMachines.UI;
using Content.Shared.Blueprint;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client.Blueprint.UI;

[UsedImplicitly]
public sealed class BluebenchBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    [ViewVariables]
    private BluebenchMenu? _menu;

    protected override void Open()
    {
        base.Open();

        _menu = this.CreateWindow<BluebenchMenu>();
        _menu.Title = "AAA";
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing)
            return;

        _menu?.Dispose();
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is not BluebenchBoundUserInterfaceState newState)
            return;

        _menu?.UpdateResearchEntries(newState.AvailableResearchEntries);
    }
}
