using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;
using SolidFrame.Resources.Helpers;
using SolidFrame.Validation.Logics;
using SolidFrame.Validation.Test.Stubs;
using SolidFrame.Validation.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolidFrame.Validation.Test
{
	[TestFixture]
	public class DescribeRegistration
	{
		private IValidationService<CanBeValidatedStub> _validationService;
		private Mock<INotificationService> _notificationServiceMock;
		private Mock<IValidationRule<CanBeValidatedStub>> _validationRuleMock;

		[SetUp]
		public void BeforeEach()
		{
			var validationServiceDependenciesMock = new Mock<IValidationServiceDependencies>();

			var propertyNameHelperMock = new Mock<IPropertyNameHelper>();
			propertyNameHelperMock.Setup(h => h.GetPropertyName(It.IsAny<Expression<Func<CanBeValidatedStub, int>>>()))
				.Returns<Expression<Func<CanBeValidatedStub, int>>>(e => new PropertyNameHelper().GetPropertyName(e));

			validationServiceDependenciesMock.SetupGet(v => v.PropertyNameHelper).Returns(propertyNameHelperMock.Object);

			var greaterThanEvaluatorMock = new Mock<IConditionEvaluator<CanBeValidatedStub>>();

			var conditionEvaluatorFactoryMock = new Mock<IConditionEvaluatorFactory>();
			conditionEvaluatorFactoryMock.Setup(
				f =>
					f.CreateGreaterThanEvaluator(It.IsAny<Func<CanBeValidatedStub, int>>(), It.IsAny<Func<CanBeValidatedStub, int>>()))
				.Returns(greaterThanEvaluatorMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ConditionEvaluatorFactory)
				.Returns(conditionEvaluatorFactoryMock.Object);

			_validationRuleMock = new Mock<IValidationRule<CanBeValidatedStub>>();
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<CanBeValidatedStub>())).Returns(true);
			_validationRuleMock.SetupGet(r => r.Properties).Returns(new HashSet<string>()).Verifiable();

			var validationRuleFactoryMock = new Mock<IValidationRuleFactory>();
			validationRuleFactoryMock.Setup(
				f =>
					f.Create(It.IsAny<IConditionEvaluator<CanBeValidatedStub>>(), It.IsAny<Severity>(), It.IsAny<string>(),
						It.IsAny<string>()))
				.Returns(_validationRuleMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ValidationRuleFactory).Returns(validationRuleFactoryMock.Object);

			_notificationServiceMock = new Mock<INotificationService>();
			_notificationServiceMock.Setup(s => s.TryRemoveNotification(It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();

			_validationService = new ValidationService<CanBeValidatedStub>(validationServiceDependenciesMock.Object);
		}

		[Test]
		public void It_subscribes_to_the_ValidationTrigger_event()
		{
			var validateMock = new Mock<IValidate<CanBeValidatedStub>>();

			_validationService.Register(validateMock.Object);

			_validationService.AddAbsoluteRule(new Mock<IHaveId>().Object, stub => stub.NumberInt, Condition.MustBeGreaterThan, 0, Severity.Error, "Test");

			validateMock.Raise(v => v.RowValidationTrigger += null, new CanBeValidatedStub(), "NumberInt");
			
			_validationRuleMock.VerifyGet(r => r.Properties);
		}
	}

	[TestFixture]
	public class DescribeAbsoluteRuleAddition
	{
		private IValidationService<CanBeValidatedStub> _validationService;
		private Mock<INotificationService> _notificationServiceMock;
		private Mock<IValidationRule<CanBeValidatedStub>> _validationRuleMock;
		private Mock<IPropertyNameHelper> _propertyNameHelperMock;
		private string _message;
		private Mock<IConditionEvaluatorFactory> _conditionEvaluatorFactoryMock;
		private Mock<IValidationRuleFactory> _validationRuleFactoryMock;
		private Mock<IConditionEvaluator<CanBeValidatedStub>> _greaterThanEvaluatorMock;
		private Func<CanBeValidatedStub, int> _getter1;
		private Func<CanBeValidatedStub, int> _getter2;


		[SetUp]
		public void BeforeEach()
		{
			var validationServiceDependenciesMock = new Mock<IValidationServiceDependencies>();

			_propertyNameHelperMock = new Mock<IPropertyNameHelper>();
			_propertyNameHelperMock.Setup(h => h.GetPropertyName(It.IsAny<Expression<Func<CanBeValidatedStub, int>>>()))
				.Returns<Expression<Func<CanBeValidatedStub, int>>>(e => new PropertyNameHelper().GetPropertyName(e)).Verifiable();

			validationServiceDependenciesMock.SetupGet(v => v.PropertyNameHelper).Returns(_propertyNameHelperMock.Object);

			_greaterThanEvaluatorMock = new Mock<IConditionEvaluator<CanBeValidatedStub>>();

			_conditionEvaluatorFactoryMock = new Mock<IConditionEvaluatorFactory>();
			_conditionEvaluatorFactoryMock.Setup(
				f =>
					f.CreateGreaterThanEvaluator(It.IsAny<Func<CanBeValidatedStub, int>>(), It.IsAny<Func<CanBeValidatedStub, int>>()))
				.Returns(_greaterThanEvaluatorMock.Object)
				.Callback<Func<CanBeValidatedStub, int>, Func<CanBeValidatedStub, int>>((func, func1) =>
				{
					_getter1 = func;
					_getter2 = func1;
				});

			validationServiceDependenciesMock.SetupGet(d => d.ConditionEvaluatorFactory)
				.Returns(_conditionEvaluatorFactoryMock.Object);

			_validationRuleMock = new Mock<IValidationRule<CanBeValidatedStub>>();
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<CanBeValidatedStub>())).Returns(true);
			_validationRuleMock.SetupGet(r => r.Properties).Returns(new HashSet<string>()).Verifiable();

			_validationRuleFactoryMock = new Mock<IValidationRuleFactory>();
			_validationRuleFactoryMock.Setup(
				f =>
					f.Create(It.IsAny<IConditionEvaluator<CanBeValidatedStub>>(), It.IsAny<Severity>(), It.IsAny<string>(),
						It.IsAny<string>()))
				.Returns(_validationRuleMock.Object).Callback<IConditionEvaluator<CanBeValidatedStub>, Severity, string, string[]>((evaluator, severity, arg3, arg4) => _message = arg3);

			validationServiceDependenciesMock.SetupGet(d => d.ValidationRuleFactory).Returns(_validationRuleFactoryMock.Object);

			_notificationServiceMock = new Mock<INotificationService>();
			_notificationServiceMock.Setup(s => s.TryRemoveNotification(It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();

			_validationService = new ValidationService<CanBeValidatedStub>(validationServiceDependenciesMock.Object);

			var validateMock = new Mock<IValidate<CanBeValidatedStub>>();

			_validationService.Register(validateMock.Object);

			_validationService.AddAbsoluteRule(new Mock<IHaveId>().Object, stub => stub.NumberInt, Condition.MustBeGreaterThan, 0, Severity.Error, "Test{0}");
		}

		[Test]
		public void It_gets_propertyName_from_Expression()
		{
			// Verify call to the PropertyNameHelper
			_propertyNameHelperMock.Verify(v => v.GetPropertyName<CanBeValidatedStub, int>(stub => stub.NumberInt));
		}

		[Test]
		public void It_gets_ConditionEvaluator_from_the_factory()
		{
			_conditionEvaluatorFactoryMock.Verify(f => f.CreateGreaterThanEvaluator(It.IsAny<Func<CanBeValidatedStub, int>>(), It.IsAny<Func<CanBeValidatedStub, int>>()), Times.Once);

			Assert.AreEqual(1, _getter1(new CanBeValidatedStub{NumberInt = 1}));
			Assert.AreEqual(0, _getter2(new CanBeValidatedStub()));
		}

		[Test]
		public void It_gets_the_ValidationRule_from_the_IValidationRuleFactory()
		{
			// Verify call to the ValidationRuleFactory with the right arguments
			_validationRuleFactoryMock.Setup(
				f => f.Create(_greaterThanEvaluatorMock.Object, Severity.Error, _message, "NumberInt"));
		}

		[Test]
		public void It_creates_a_formatted_validationmessage()
		{
			Assert.AreEqual("TestNumberInt", _message);
		}
	}

	[TestFixture]
	public class DescribeRuleEvaluation
	{
		private IValidationService<CanBeValidatedStub> _validationService;
		private Mock<INotificationService> _notificationServiceMock;
		private Mock<IValidationRule<CanBeValidatedStub>> _validationRuleMock;
		private Mock<IValidate<CanBeValidatedStub>> _validateMock;
		private Guid _validationRuleId;
		private Guid _validateId;
		private string _validationRuleMessage;

		[SetUp]
		public void BeforeEach()
		{
			var validationServiceDependenciesMock = new Mock<IValidationServiceDependencies>();

			var propertyNameHelperMock = new Mock<IPropertyNameHelper>();
			propertyNameHelperMock.Setup(h => h.GetPropertyName(It.IsAny<Expression<Func<CanBeValidatedStub, int>>>()))
				.Returns<Expression<Func<CanBeValidatedStub, int>>>(e => new PropertyNameHelper().GetPropertyName(e));

			validationServiceDependenciesMock.SetupGet(v => v.PropertyNameHelper).Returns(propertyNameHelperMock.Object);

			var greaterThanEvaluatorMock = new Mock<IConditionEvaluator<CanBeValidatedStub>>();

			var conditionEvaluatorFactoryMock = new Mock<IConditionEvaluatorFactory>();
			conditionEvaluatorFactoryMock.Setup(
				f =>
					f.CreateGreaterThanEvaluator(It.IsAny<Func<CanBeValidatedStub, int>>(), It.IsAny<Func<CanBeValidatedStub, int>>()))
				.Returns(greaterThanEvaluatorMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ConditionEvaluatorFactory)
				.Returns(conditionEvaluatorFactoryMock.Object);

			_validationRuleMock = new Mock<IValidationRule<CanBeValidatedStub>>();
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<CanBeValidatedStub>())).Returns(false);
			_validationRuleId = Guid.NewGuid();
			_validationRuleMock.SetupGet(r => r.Id).Returns(_validationRuleId);
			_validationRuleMessage = "Test";
			_validationRuleMock.SetupGet(r => r.Message).Returns(_validationRuleMessage);

			var validationRuleFactoryMock = new Mock<IValidationRuleFactory>();
			validationRuleFactoryMock.Setup(
				f =>
					f.Create(It.IsAny<IConditionEvaluator<CanBeValidatedStub>>(), It.IsAny<Severity>(), It.IsAny<string>(),
						It.IsAny<string>()))
				.Returns(_validationRuleMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ValidationRuleFactory).Returns(validationRuleFactoryMock.Object);

			_notificationServiceMock = new Mock<INotificationService>();
			_notificationServiceMock.Setup(s => s.TryRemoveNotification(It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();
			_notificationServiceMock.Setup(s => s.AddNotification(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

			validationServiceDependenciesMock.SetupGet(d => d.NotificationService).Returns(_notificationServiceMock.Object);

			_validationService = new ValidationService<CanBeValidatedStub>(validationServiceDependenciesMock.Object);

			_validateMock = new Mock<IValidate<CanBeValidatedStub>>();
			_validateId = Guid.NewGuid();
			_validateMock.As<IHaveId>().Setup(h => h.Id).Returns(_validateId);

			_validationService.Register(_validateMock.Object);

			_validationService.AddAbsoluteRule(new Mock<IHaveId>().Object, stub => stub.NumberInt, Condition.MustBeGreaterThan, 0, Severity.Error, "Test");
			_validationRuleMock.SetupGet(r => r.Properties).Returns(new HashSet<string>(new[] { "NumberInt" }));
		}

		[Test]
		public void It_does_not_evaluate_rule_if_trigger_is_not_relevant_based_on_property_name()
		{
			_validateMock.Raise(v => v.RowValidationTrigger += null, new CanBeValidatedStub(), "WhollyDifferentProperty");

			_validationRuleMock.Verify(r => r.Evaluate(It.IsAny<CanBeValidatedStub>()), Times.Never);
		}

		[Test]
		public void It_adds_Notification_if_Rule_evaluates_to_false()
		{
			const string validationName = "Stub";
			var stub = new CanBeValidatedStub(validationName);
			_validateMock.Raise(v => v.RowValidationTrigger += null, stub, "NumberInt");

			// Test that AddNotification is called with right args
			_notificationServiceMock.Verify(s => s.AddNotification(_validationRuleId, stub.Id, validationName, _validationRuleMessage));
		}

		[Test]
		public void It__tries_to_remove_Rule_Notification_if_Rule_evaluates_to_true()
		{
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<CanBeValidatedStub>())).Returns(true);

			var stub = new CanBeValidatedStub();

			_validateMock.Raise(v => v.RowValidationTrigger += null, stub, "NumberInt");

			_notificationServiceMock.Verify(s => s.TryRemoveNotification(_validationRuleId, stub.Id));
		}
	}
}
