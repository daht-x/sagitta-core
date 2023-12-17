namespace Daht.Sagitta.Core.Monads;

/// <summary>Type that exposes a set of ways to initialize <see cref="Result{TFailure, TSuccess}"/>.</summary>
public static class ResultFactory
{
	/// <summary>Creates a new failed result if the value of <paramref name="createSuccess"/> throws <typeparamref name="TException"/>; otherwise, creates a new successful result.</summary>
	/// <typeparam name="TException">Type of possible exception.</typeparam>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <param name="createSuccess">
	///		<para>Creates the expected success.</para>
	///     <para>If <paramref name="createSuccess"/> is <see langword="null"/> or its value is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <param name="createFailure">
	///		<para>Creates the possible failure in combination with <typeparamref name="TException"/>.</para>
	///     <para>If <paramref name="createFailure"/> is <see langword="null"/> or its value is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new failed result if the value of <paramref name="createSuccess"/> throws <typeparamref name="TException"/>; otherwise, a new successful result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TSuccess, TFailure> Catch<TException, TSuccess, TFailure>(Func<TSuccess> createSuccess, Func<TException, TFailure> createFailure)
		where TException : Exception
	{
		try
		{
			return Succeed<TSuccess, TFailure>(createSuccess);
		}
		catch (TException exception)
		{
			ArgumentNullException.ThrowIfNull(createFailure);
			TFailure failure = createFailure(exception) ?? throw new ArgumentNullException(nameof(createFailure));
			return Fail<TSuccess, TFailure>(failure);
		}
	}

	/// <summary>Creates a new successful result.</summary>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <param name="createSuccess">
	///     <para>Creates the expected success.</para>
	///     <para>If <paramref name="createSuccess"/> is <see langword="null"/> or its value is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new successful result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TSuccess, TFailure> Succeed<TSuccess, TFailure>(Func<TSuccess> createSuccess)
	{
		ArgumentNullException.ThrowIfNull(createSuccess);
		TSuccess success = createSuccess() ?? throw new ArgumentNullException(nameof(createSuccess));
		return Succeed<TSuccess, TFailure>(success);
	}

	/// <summary>Creates a new successful result.</summary>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <param name="success">
	///		<para>The expected success.</para>
	///     <para>If <paramref name="success"/> is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new successful result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TSuccess, TFailure> Succeed<TSuccess, TFailure>(TSuccess success)
		=> success is null
			? throw new ArgumentNullException(nameof(success))
			: new Result<TSuccess, TFailure>()
			{
				IsSuccessful = true,
				Success = success
			};

	/// <summary>Creates a new failed result.</summary>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <param name="createFailure">
	///     <para>Creates the possible failure.</para>
	///     <para>If <paramref name="createFailure"/> is <see langword="null"/> or its value is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new failed result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TSuccess, TFailure> Fail<TSuccess, TFailure>(Func<TFailure> createFailure)
	{
		ArgumentNullException.ThrowIfNull(createFailure);
		TFailure failure = createFailure() ?? throw new ArgumentNullException(nameof(createFailure));
		return Fail<TSuccess, TFailure>(failure);
	}

	/// <summary>Creates a new failed result.</summary>
	/// <typeparam name="TSuccess">Type of expected success.</typeparam>
	/// <typeparam name="TFailure">Type of possible failure.</typeparam>
	/// <param name="failure">
	///     <para>The possible failure.</para>
	///     <para>If <paramref name="failure"/> is <see langword="null"/>, <seealso cref="ArgumentNullException"/> will be thrown.</para>
	/// </param>
	/// <returns>A new failed result.</returns>
	/// <exception cref="ArgumentNullException"/>
	public static Result<TSuccess, TFailure> Fail<TSuccess, TFailure>(TFailure failure)
		=> failure is null
			? throw new ArgumentNullException(nameof(failure))
			: new Result<TSuccess, TFailure>()
			{
				IsFailed = true,
				Failure = failure
			};
}