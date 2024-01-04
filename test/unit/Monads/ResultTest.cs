namespace Daht.Sagitta.Core.UnitTest.Monads;

public sealed class ResultTest
{
	private const string root = nameof(Result<object, object>);

	private const string constructor = "Constructor";

	private const string implicitOperator = "Implicit Operator";

	private const string ensure = nameof(Result<object, object>.Ensure);

	#region Constructor

	#region Overload

	[Fact]
	[Trait(root, constructor)]
	public void Constructor_Success_SuccessfulResult()
	{
		// Arrange
		Constellation expectedSuccess = ResultFixture.Success;

		// Act
		Result<Constellation, string> actualResult = new(expectedSuccess);

		// Assert
		ResultAsserter.AreSuccessful(expectedSuccess, actualResult);
	}

	#endregion

	#region Overload

	[Fact]
	[Trait(root, constructor)]
	public void Constructor_Failure_FailedResult()
	{
		// Arrange
		const string expectedFailure = ResultFixture.Failure;

		// Act
		Result<Constellation, string> actualResult = new(expectedFailure);

		// Assert
		ResultAsserter.AreFailed(expectedFailure, actualResult);
	}

	#endregion

	#endregion

	#region Implicit Operator

	#region Overload

	[Fact]
	[Trait(root, implicitOperator)]
	public void ImplicitOperator_Success_SuccessfulResult()
	{
		// Arrange
		Constellation expectedSuccess = ResultFixture.Success;

		// Act
		Result<Constellation, string> actualResult = expectedSuccess;

		// Assert
		ResultAsserter.AreSuccessful(expectedSuccess, actualResult);
	}

	#endregion

	#region Overload

	[Fact]
	[Trait(root, implicitOperator)]
	public void ImplicitOperator_Failure_FailedResult()
	{
		// Arrange
		const string expectedFailure = ResultFixture.Failure;

		// Act
		Result<Constellation, string> actualResult = expectedFailure;

		// Assert
		ResultAsserter.AreFailed(expectedFailure, actualResult);
	}

	#endregion

	#endregion

	#region Ensure

	#region Overload

	[Fact]
	[Trait(root, ensure)]
	public void Ensure_FailedResultPlusTruePredicatePlusFailure_FailedResult()
	{
		// Arrange
		const string expectedFailure = ResultFixture.Failure;
		Func<Constellation, bool> predicate = static _ => true;
		string failure = ResultFixture.RandomFailure;

		// Act
		Result<Constellation, string> actualResult = ResultMother.Fail(expectedFailure)
			.Ensure(predicate, failure);

		// Assert
		ResultAsserter.AreFailed(expectedFailure, actualResult);
	}

	[Fact]
	[Trait(root, ensure)]
	public void Ensure_SuccessfulResultPlusTruePredicatePlusFailure_FailedResult()
	{
		// Arrange
		Func<Constellation, bool> predicate = static _ => true;
		const string expectedFailure = ResultFixture.Failure;

		// Act
		Result<Constellation, string> actualResult = ResultMother.Succeed()
			.Ensure(predicate, expectedFailure);

		// Assert
		ResultAsserter.AreFailed(expectedFailure, actualResult);
	}

	[Fact]
	[Trait(root, ensure)]
	public void Ensure_SuccessfulResultPlusFalsePredicatePlusFailure_SuccessfulResult()
	{
		// Arrange
		Constellation expectedSuccess = ResultFixture.Success;
		Func<Constellation, bool> predicate = static _ => false;
		const string failure = ResultFixture.Failure;

		// Act
		Result<Constellation, string> actualResult = ResultMother.Succeed(expectedSuccess)
			.Ensure(predicate, failure);

		// Assert
		ResultAsserter.AreSuccessful(expectedSuccess, actualResult);
	}

	#endregion

	#region Overload

	[Fact]
	[Trait(root, ensure)]
	public void Ensure_FailedResultPlusTruePredicatePlusCreateFailure_FailedResult()
	{
		// Arrange
		const string expectedFailure = ResultFixture.Failure;
		Func<Constellation, bool> predicate = static _ => true;
		Func<Constellation, string> createFailure = static _ => ResultFixture.RandomFailure;

		// Act
		Result<Constellation, string> actualResult = ResultMother.Fail(expectedFailure)
			.Ensure(predicate, createFailure);

		// Assert
		ResultAsserter.AreFailed(expectedFailure, actualResult);
	}

	[Fact]
	[Trait(root, ensure)]
	public void Ensure_SuccessfulResultPlusTruePredicatePlusCreateFailure_FailedResult()
	{
		// Arrange
		Func<Constellation, bool> predicate = static _ => true;
		const string expectedFailure = ResultFixture.Failure;
		Func<Constellation, string> createFailure = static _ => expectedFailure;

		// Act
		Result<Constellation, string> actualResult = ResultMother.Succeed()
			.Ensure(predicate, createFailure);

		// Assert
		ResultAsserter.AreFailed(expectedFailure, actualResult);
	}

	[Fact]
	[Trait(root, ensure)]
	public void Ensure_SuccessfulResultPlusFalsePredicatePlusCreateFailure_SuccessfulResult()
	{
		// Arrange
		Constellation expectedSuccess = ResultFixture.Success;
		Func<Constellation, bool> predicate = static _ => false;
		Func<Constellation, string> createFailure = static _ => ResultFixture.Failure;

		// Act
		Result<Constellation, string> actualResult = ResultMother.Succeed(expectedSuccess)
			.Ensure(predicate, createFailure);

		// Assert
		ResultAsserter.AreSuccessful(expectedSuccess, actualResult);
	}

	#endregion

	#endregion
}
