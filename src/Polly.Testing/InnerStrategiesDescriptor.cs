﻿namespace Polly.Testing;

/// <summary>
/// Describes the pipeline of a resilience strategy.
/// </summary>
public sealed class InnerStrategiesDescriptor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InnerStrategiesDescriptor"/> class.
    /// </summary>
    /// <param name="strategies">The strategies the pipeline is composed of.</param>
    /// <param name="hasTelemetry">Determines whether the pipeline has telemetry enabled.</param>
    /// <param name="isReloadable">Determines whether the resilience strategy is reloadable.</param>
    public InnerStrategiesDescriptor(IReadOnlyList<ResilienceStrategyDescriptor> strategies, bool hasTelemetry, bool isReloadable)
    {
        Strategies = strategies;
        HasTelemetry = hasTelemetry;
        IsReloadable = isReloadable;
    }

    /// <summary>
    /// Gets the strategies the pipeline is composed of.
    /// </summary>
    public IReadOnlyList<ResilienceStrategyDescriptor> Strategies { get; }

    /// <summary>
    /// Gets a value indicating whether the pipeline has telemetry enabled.
    /// </summary>
    public bool HasTelemetry { get; }

    /// <summary>
    /// Gets a value indicating whether the resilience strategy is reloadable.
    /// </summary>
    public bool IsReloadable { get; }
}
