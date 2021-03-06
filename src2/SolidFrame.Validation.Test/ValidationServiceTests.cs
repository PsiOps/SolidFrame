﻿using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;
using SolidFrame.Validation.Logics;
using SolidFrame.Validation.Test.Stubs;
using SolidFrame.Validation.Types;
using System;
using System.Collections.Generic;

namespace SolidFrame.Validation.Test
{
	[TestFixture]
	public class DescribeRegistration
	{
		private IValidationService<ValidatableStub> _validationService;
		private Mock<INotificationService> _notificationServiceMock;
		private Mock<IValidationRule<ValidatableStub>> _validationRuleMock;

		[SetUp]
		public void BeforeEach()
		{
			var validationServiceDependenciesMock = new Mock<IValidationServiceDependencies>();

			var greaterThanEvaluatorMock = new Mock<IConditionEvaluator<ValidatableStub>>();

			var conditionEvaluatorFactoryMock = new Mock<IConditionEvaluatorFactory>();
			conditionEvaluatorFactoryMock.Setup(
				f =>
					f.CreateGreaterThanEvaluator(It.IsAny<Func<ValidatableStub, int>>(), It.IsAny<Func<ValidatableStub, int>>()))
				.Returns(greaterThanEvaluatorMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ConditionEvaluatorFactory)
				.Returns(conditionEvaluatorFactoryMock.Object);

			_validationRuleMock = new Mock<IValidationRule<ValidatableStub>>();
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(true);
			_validationRuleMock.SetupGet(r => r.Properties).Returns(new HashSet<string>()).Verifiable();

			var validationRuleFactoryMock = new Mock<IValidationRuleFactory>();
			validationRuleFactoryMock.Setup(
				f =>
					f.Create(It.IsAny<IConditionEvaluator<ValidatableStub>>(), It.IsAny<Severity>(), It.IsAny<string>(),
						It.IsAny<string>()))
				.Returns(_validationRuleMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ValidationRuleFactory).Returns(validationRuleFactoryMock.Object);

			_notificationServiceMock = new Mock<INotificationService>();
			_notificationServiceMock.Setup(s => s.TryRemoveNotification(It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();

			_validationService = new ValidationService<ValidatableStub>(validationServiceDependenciesMock.Object);
		}

		[Test]
		public void It_subscribes_to_the_ValidationTrigger_event()
		{
			var validateMock = new Mock<IValidate<ValidatableStub>>();

			_validationService.Register(validateMock.Object);

			_validationService.AddAbsoluteRule(new Mock<IHaveId>().Object, stub => stub.NumberInt, Condition.MustBeGreaterThan, 0, Severity.Error, "Test");

			validateMock.Raise(v => v.ValidationTrigger += null, new ValidatableStub(), "NumberInt");
			
			_validationRuleMock.VerifyGet(r => r.Properties);
		}
	}

	[TestFixture]
	public class DescribeAbsoluteRuleAddition
	{
		private IValidationService<ValidatableStub> _validationService;
		private Mock<INotificationService> _notificationServiceMock;
		private Mock<IValidationRule<ValidatableStub>> _validationRuleMock;
		private string _message;
		private Mock<IConditionEvaluatorFactory> _conditionEvaluatorFactoryMock;
		private Mock<IValidationRuleFactory> _validationRuleFactoryMock;
		private Mock<IConditionEvaluator<ValidatableStub>> _greaterThanEvaluatorMock;
		private Func<ValidatableStub, int> _getter1;
		private Func<ValidatableStub, int> _getter2;


		[SetUp]
		public void BeforeEach()
		{
			var validationServiceDependenciesMock = new Mock<IValidationServiceDependencies>();

			_greaterThanEvaluatorMock = new Mock<IConditionEvaluator<ValidatableStub>>();

			_conditionEvaluatorFactoryMock = new Mock<IConditionEvaluatorFactory>();
			_conditionEvaluatorFactoryMock.Setup(
				f =>
					f.CreateGreaterThanEvaluator(It.IsAny<Func<ValidatableStub, int>>(), It.IsAny<Func<ValidatableStub, int>>()))
				.Returns(_greaterThanEvaluatorMock.Object)
				.Callback<Func<ValidatableStub, int>, Func<ValidatableStub, int>>((func, func1) =>
				{
					_getter1 = func;
					_getter2 = func1;
				});

			validationServiceDependenciesMock.SetupGet(d => d.ConditionEvaluatorFactory)
				.Returns(_conditionEvaluatorFactoryMock.Object);

			_validationRuleMock = new Mock<IValidationRule<ValidatableStub>>();
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(true);
			_validationRuleMock.SetupGet(r => r.Properties).Returns(new HashSet<string>()).Verifiable();

			_validationRuleFactoryMock = new Mock<IValidationRuleFactory>();
			_validationRuleFactoryMock.Setup(
				f =>
					f.Create(It.IsAny<IConditionEvaluator<ValidatableStub>>(), It.IsAny<Severity>(), It.IsAny<string>(),
						It.IsAny<string>()))
				.Returns(_validationRuleMock.Object).Callback<IConditionEvaluator<ValidatableStub>, Severity, string, string[]>((evaluator, severity, arg3, arg4) => _message = arg3);

			validationServiceDependenciesMock.SetupGet(d => d.ValidationRuleFactory).Returns(_validationRuleFactoryMock.Object);

			_notificationServiceMock = new Mock<INotificationService>();
			_notificationServiceMock.Setup(s => s.TryRemoveNotification(It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();

			_validationService = new ValidationService<ValidatableStub>(validationServiceDependenciesMock.Object);

			var validateMock = new Mock<IValidate<ValidatableStub>>();

			_validationService.Register(validateMock.Object);

			_validationService.AddAbsoluteRule(new Mock<IHaveId>().Object, stub => stub.NumberInt, Condition.MustBeGreaterThan, 0, Severity.Error, "Test{0}");
		}

		[Test]
		public void It_gets_ConditionEvaluator_from_the_factory()
		{
			_conditionEvaluatorFactoryMock.Verify(f => f.CreateGreaterThanEvaluator(It.IsAny<Func<ValidatableStub, int>>(), It.IsAny<Func<ValidatableStub, int>>()), Times.Once);

			Assert.AreEqual(1, _getter1(new ValidatableStub{NumberInt = 1}));
			Assert.AreEqual(0, _getter2(new ValidatableStub()));
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
		private IValidationService<ValidatableStub> _validationService;
		private Mock<INotificationService> _notificationServiceMock;
		private Mock<IValidationRule<ValidatableStub>> _validationRuleMock;
		private Mock<IValidate<ValidatableStub>> _validateMock;
		private Guid _validationRuleId;
		private Guid _validateId;
		private string _validationRuleMessage;

		[SetUp]
		public void BeforeEach()
		{
			var validationServiceDependenciesMock = new Mock<IValidationServiceDependencies>();

			var greaterThanEvaluatorMock = new Mock<IConditionEvaluator<ValidatableStub>>();

			var conditionEvaluatorFactoryMock = new Mock<IConditionEvaluatorFactory>();
			conditionEvaluatorFactoryMock.Setup(
				f =>
					f.CreateGreaterThanEvaluator(It.IsAny<Func<ValidatableStub, int>>(), It.IsAny<Func<ValidatableStub, int>>()))
				.Returns(greaterThanEvaluatorMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ConditionEvaluatorFactory)
				.Returns(conditionEvaluatorFactoryMock.Object);

			_validationRuleMock = new Mock<IValidationRule<ValidatableStub>>();
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(false);
			_validationRuleId = Guid.NewGuid();
			_validationRuleMock.SetupGet(r => r.Id).Returns(_validationRuleId);
			_validationRuleMessage = "Test";
			_validationRuleMock.SetupGet(r => r.Message).Returns(_validationRuleMessage);

			var validationRuleFactoryMock = new Mock<IValidationRuleFactory>();
			validationRuleFactoryMock.Setup(
				f =>
					f.Create(It.IsAny<IConditionEvaluator<ValidatableStub>>(), It.IsAny<Severity>(), It.IsAny<string>(),
						It.IsAny<string>()))
				.Returns(_validationRuleMock.Object);

			validationServiceDependenciesMock.SetupGet(d => d.ValidationRuleFactory).Returns(validationRuleFactoryMock.Object);

			_notificationServiceMock = new Mock<INotificationService>();
			_notificationServiceMock.Setup(s => s.TryRemoveNotification(It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();
			_notificationServiceMock.Setup(s => s.AddNotification(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

			validationServiceDependenciesMock.SetupGet(d => d.NotificationService).Returns(_notificationServiceMock.Object);

			_validationService = new ValidationService<ValidatableStub>(validationServiceDependenciesMock.Object);

			_validateMock = new Mock<IValidate<ValidatableStub>>();
			_validateId = Guid.NewGuid();
			_validateMock.As<IHaveId>().Setup(h => h.Id).Returns(_validateId);

			_validationService.Register(_validateMock.Object);

			_validationService.AddAbsoluteRule(new Mock<IHaveId>().Object, stub => stub.NumberInt, Condition.MustBeGreaterThan, 0, Severity.Error, "Test");
			_validationRuleMock.SetupGet(r => r.Properties).Returns(new HashSet<string>(new[] { "NumberInt" }));
		}

		[Test]
		public void It_does_not_evaluate_rule_if_trigger_is_not_relevant_based_on_property_name()
		{
			_validateMock.Raise(v => v.ValidationTrigger += null, new ValidatableStub(), "WhollyDifferentProperty");

			_validationRuleMock.Verify(r => r.Evaluate(It.IsAny<ValidatableStub>()), Times.Never);
		}

		[Test]
		public void It_adds_Notification_if_Rule_evaluates_to_false()
		{
			const string validationName = "Stub";
			var stub = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub, "NumberInt");

			// Test that AddNotification is called with right args
			_notificationServiceMock.Verify(s => s.AddNotification(_validationRuleId, stub.Id, validationName, _validationRuleMessage));
		}

		[Test]
		public void It_tries_to_remove_Rule_Notification_if_Rule_evaluates_to_true()
		{
			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(true);

			var stub = new ValidatableStub();

			_validateMock.Raise(v => v.ValidationTrigger += null, stub, "NumberInt");

			_notificationServiceMock.Verify(s => s.TryRemoveNotification(_validationRuleId, stub.Id));
		}

		[Test]
		public void It_updates_HasErrors_property_based_on_prescence_of_rule_violations()
		{
			Assert.IsFalse(_validationService.HasErrors);

			const string validationName = "Stub";
			var stub = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub, "NumberInt");

			Assert.IsTrue(_validationService.HasErrors);

			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(true);

			_validateMock.Raise(v => v.ValidationTrigger += null, stub, "NumberInt");

			Assert.IsFalse(_validationService.HasErrors);
		}

		[Test]
		public void It_raises_HasErrorsChanged_on_first_rule_violation()
		{
			var eventRaised = false;

			_validationService.HasErrorsChanged += state => eventRaised = true;

			const string validationName = "Stub";
			var stub = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub, "NumberInt");

			Assert.IsTrue(eventRaised);
		}

		[Test]
		public void It_does_not_raise_HasErrorsChanged_when_rule_complies_and_no_violations_present()
		{
			var eventRaised = false;

			_validationService.HasErrorsChanged += state => eventRaised = true;

			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(true);

			const string validationName = "Stub";
			var stub = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub, "NumberInt");

			Assert.IsFalse(eventRaised);
		}

		[Test]
		public void It_does_not_raise_HasErrorsChanged_on_second_rule_validation()
		{
			bool eventRaised;

			_validationService.HasErrorsChanged += state => eventRaised = true;

			const string validationName = "Stub";
			var stub1 = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub1, "NumberInt");

			eventRaised = false;

			var stub2 = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub2, "NumberInt");

			Assert.IsFalse(eventRaised);
		}

		[Test]
		public void It_does_not_raise_HasErrorsChanged_when_violation_is_resolved_but_violations_remain()
		{
			bool eventRaised;

			_validationService.HasErrorsChanged += state => eventRaised = true;

			const string validationName = "Stub";
			var stub1 = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub1, "NumberInt");

			var stub2 = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub2, "NumberInt");

			eventRaised = false;

			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(true);

			_validateMock.Raise(v => v.ValidationTrigger += null, stub1, "NumberInt");

			Assert.IsFalse(eventRaised);
		}

		[Test]
		public void It_raises_HasErrorsChanged_when_last_violation_is_resolved()
		{
			bool eventRaised;

			_validationService.HasErrorsChanged += state => eventRaised = true;

			const string validationName = "Stub";
			var stub1 = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub1, "NumberInt");

			var stub2 = new ValidatableStub(validationName);
			_validateMock.Raise(v => v.ValidationTrigger += null, stub2, "NumberInt");

			_validationRuleMock.Setup(r => r.Evaluate(It.IsAny<ValidatableStub>())).Returns(true);

			_validateMock.Raise(v => v.ValidationTrigger += null, stub1, "NumberInt");

			eventRaised = false;

			_validateMock.Raise(v => v.ValidationTrigger += null, stub2, "NumberInt");

			Assert.IsTrue(eventRaised);
		}
	}
}
