using Blazr.OneWayStreet.Core;
using Delta.Core.Common;
using Delta.Core.Libraries.Toasts;
using Microsoft.AspNetCore.Components.Forms;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Presentation.Presenters;

public class EditPresenter<TRecord, TIdentity, TEditContext> : IEditPresenter<TRecord, TIdentity, TEditContext>
    where TRecord : class, new()
    where TEditContext : IRecordEditContext<TRecord>, new()
{
    private readonly IDataBroker _dataBroker;
    private readonly IAppToastService _toastService;
    private readonly INewRecordProvider<TRecord> _newRecordProvider;
    private readonly string _recordName;

    public IDataResult LastDataResult { get; private set; } = DataResult.Success();
    public EditContext EditContext { get; private set; } = default!;
    public TEditContext RecordEditContext { get; private set; } = default!;
    public bool IsNew { get; private set; }
    public bool IsInvalid => EditContext?.GetValidationMessages().Any() ?? false;

    internal EditPresenter(IDataBroker dataBroker, INewRecordProvider<TRecord> newRecordProvider,
        IAppToastService toastService)
    {
        _dataBroker = dataBroker;
        _toastService = toastService;
        _newRecordProvider = newRecordProvider;
        _recordName = typeof(TRecord).Name;
    }

    // When called with an Id that doesn't exist the method sets the LastResult
    // to the returned failure result and loads a new entity.
    // It's up to the UI to decide hoe to handle that context
    internal async Task LoadAsync(TIdentity id, bool isNew)
    {
        LastDataResult = DataResult.Success();

        TRecord? item = null;

        // The Update Path.  Get the requested record if it exists
        if (!isNew)
        {
            var request = ItemQueryRequest<TIdentity>.Create(id);
            var result = await _dataBroker.ExecuteQueryAsync<TRecord, TIdentity>(request);
            LastDataResult = result;
            if (LastDataResult.Successful)
            {
                item = result.Item;
                IsNew = false;
            }
        }

        // isNew is true or there's no record with the provided Id
        if (item is null)
        {
            item = _newRecordProvider.NewRecord();
            IsNew = true;
        }

        RecordEditContext = new();
        RecordEditContext.Load(item);
        EditContext = new(RecordEditContext);
        return;
    }

    public async Task<IDataResult> SaveItemAsync()
    {
        if (!RecordEditContext.IsDirty)
        {
            LastDataResult = DataResult.Failure($"The {_recordName} has not changed and therefore has not been updated.");
            _toastService.ShowWarning($"The {_recordName} has not changed and therefore has not been updated.");
            return LastDataResult;
        }

        var record = RecordEditContext.AsRecord;
        var command = new CommandRequest<TRecord>(record, IsNew ? CommandState.Add : CommandState.Update);
        var result = await _dataBroker.ExecuteCommandAsync(command);

        var stateText = IsNew ? "Added" : "saved";
        if (result.Successful)
            _toastService.ShowSuccess($"The {_recordName} was {stateText}.");
        else
            _toastService.ShowError(result.Message ?? $"The {_recordName} could not be {stateText}.");

        LastDataResult = result;
        return result;
    }
}
