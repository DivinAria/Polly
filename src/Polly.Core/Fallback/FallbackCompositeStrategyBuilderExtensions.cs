using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Polly.Fallback;

namespace Polly;

/// <summary>
/// Provides extension methods for configuring fallback resilience strategies for <see cref="CompositeStrategyBuilder"/>.
/// </summary>
public static class FallbackCompositeStrategyBuilderExtensions
{
    /// <summary>
    /// Adds a fallback resilience strategy with the provided options to the builder.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="builder">The resilience strategy builder.</param>
    /// <param name="options">The options to configure the fallback resilience strategy.</param>
    /// <returns>The builder instance with the fallback strategy added.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="builder"/> or <paramref name="options"/> is <see langword="null"/>.</exception>
    /// <exception cref="ValidationException">Thrown when <paramref name="options"/> are invalid.</exception>
    public static CompositeStrategyBuilder<TResult> AddFallback<TResult>(this CompositeStrategyBuilder<TResult> builder, FallbackStrategyOptions<TResult> options)
    {
        Guard.NotNull(builder);
        Guard.NotNull(options);

        builder.AddFallbackCore<TResult, FallbackStrategyOptions<TResult>>(options);
        return builder;
    }

    /// <summary>
    /// Adds a fallback resilience strategy with the provided options to the builder.
    /// </summary>
    /// <param name="builder">The resilience strategy builder.</param>
    /// <param name="options">The options to configure the fallback resilience strategy.</param>
    /// <returns>The builder instance with the fallback strategy added.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="builder"/> or <paramref name="options"/> is <see langword="null"/>.</exception>
    /// <exception cref="ValidationException">Thrown when <paramref name="options"/> are invalid.</exception>
    internal static CompositeStrategyBuilder AddFallback(this CompositeStrategyBuilder builder, FallbackStrategyOptions options)
    {
        Guard.NotNull(builder);
        Guard.NotNull(options);

        builder.AddFallbackCore<object, FallbackStrategyOptions>(options);
        return builder;
    }

    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
        Justification = "All options members preserved.")]
    internal static void AddFallbackCore<TResult, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TOptions>(
        this CompositeStrategyBuilderBase builder,
        FallbackStrategyOptions<TResult> options)
    {
        builder.AddStrategy(context =>
        {
            var handler = new FallbackHandler<TResult>(
                options.ShouldHandle!,
                options.FallbackAction!);

            return new FallbackResilienceStrategy<TResult>(
                handler,
                options.OnFallback,
                context.Telemetry);
        },
        options);
    }
}
