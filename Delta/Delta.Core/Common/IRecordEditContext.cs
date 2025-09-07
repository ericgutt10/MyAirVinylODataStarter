using Blazr.OneWayStreet.Core;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Core.Common;

public interface IRecordEditContext<TRecord>
    where TRecord : class
{
    public TRecord BaseRecord { get; }
    public TRecord AsRecord { get; }
    public bool IsDirty { get; }

    public IDataResult Load(TRecord record);
}
