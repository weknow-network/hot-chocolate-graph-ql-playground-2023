
using System.Threading.Tasks.Dataflow;

using GreenDonut;

namespace Weknow.HotChocolatePlayground;

/// <summary>
/// Defines a batch dispatcher that immediately dispatches batch jobs.
/// </summary>
/// <remarks>
/// This is a variant of GreenDonut.AutoBatchScheduler
/// </remarks>
public class ImmediateBatchScheduler : IBatchScheduler
{
    private readonly ActionBlock<Func<ValueTask>> _actionBlock = new ActionBlock<Func<ValueTask>>(dispatch => dispatch());

    /// <summary>
    /// Schedules a new job to the dispatcher that is immediately executed.
    /// </summary>
    /// <param name="dispatch">
    /// The job that is being scheduled.
    /// </param>
    public async void Schedule(Func<ValueTask> dispatch)
        => _actionBlock.Post(dispatch);

    /// <summary>
    /// Gets the default instance if the <see cref="AutoBatchScheduler"/>.
    /// </summary>
    public static ImmediateBatchScheduler Default { get; } = new();
}