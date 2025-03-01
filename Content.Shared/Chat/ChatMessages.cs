using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared.Chat;

[Serializable, NetSerializable]
public sealed class OOCMessageEvent : EntityEventArgs
{
    public string Message { get; }

    public OOCMessageEvent(string message)
    {
        Message = message;
    }
}
