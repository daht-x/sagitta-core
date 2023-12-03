namespace Daht.Sagitta.Core.Monads;

/// <summary>Reference point to initialize <see cref="Result{TFailure, TSuccess}"/>.</summary>
public static class Result
{
	/// <summary>Creates a new failed result.</summary>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <param name="failure">
	///     <para>The possible failure.</para>
	///     <para>If <paramref name="failure"/> is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new failed result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TFailure, TSuccess> Fail<TFailure, TSuccess>(TFailure failure)
		where TFailure : notnull
		where TSuccess : notnull
		=> failure is null
			? throw new ArgumentNullException(nameof(failure))
			: new Result<TFailure, TSuccess>()
			{
				IsFailed = true,
				Failure = failure
			};

	/// <summary>Creates a new successful result.</summary>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <param name="createSuccess">
	///     <para>Creates the expected success.</para>
	///     <para>If <paramref name="createSuccess"/> is <see langword="null"/> or its value is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new successful result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TFailure, TSuccess> Succeed<TFailure, TSuccess>(Func<TSuccess> createSuccess)
		where TFailure : notnull
		where TSuccess : notnull
	{
		ArgumentNullException.ThrowIfNull(createSuccess);
		TSuccess success = createSuccess() ?? throw new ArgumentNullException(nameof(createSuccess));
		return Succeed<TFailure, TSuccess>(success);
	}

	/// <summary>Creates a new successful result.</summary>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <param name="success">
	///		<para>The expected success.</para>
	///     <para>If <paramref name="success"/> is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new successful result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TFailure, TSuccess> Succeed<TFailure, TSuccess>(TSuccess success)
		where TFailure : notnull
		where TSuccess : notnull
		=> success is null
			? throw new ArgumentNullException(nameof(success))
			: new Result<TFailure, TSuccess>()
			{
				IsSuccessful = true,
				Success = success
			};
}

/// <summary>Type that encapsulates both the possible failure and the expected success of a given action</summary>
///	<typeparam name="TFailure">Type of possible failure.</typeparam>
/// <typeparam name="TSuccess">Type of expected success.</typeparam>
public readonly record struct Result<TFailure, TSuccess>
	where TFailure : notnull
	where TSuccess : notnull
{
	/// <summary>Indicates whether the status is failed or <see langword="default"/>.</summary>
	public bool IsFailedOrDefault => IsFailed || IsDefault;

	/// <summary>Indicates whether the status is successful or <see langword="default"/>.</summary>
	public bool IsSuccessfulOrDefault => IsSuccessful || IsDefault;

	/// <summary>Indicates whether the status is <see langword="default"/>.</summary>
	public bool IsDefault => this is
	{
		IsFailed: false,
		IsSuccessful: false
	};

	/// <summary>Indicates whether the status is failed.</summary>
	public bool IsFailed { get; internal init; }

	/// <summary>The possible failure.</summary>
	public TFailure Failure { get; internal init; }

	/// <summary>Indicates whether the status is successful.</summary>
	public bool IsSuccessful { get; internal init; }

	/// <summary>The expected success.</summary>
	public TSuccess Success { get; internal init; }
}
