using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace Specs2Tests.Tests
{
    [TestFixture]
    public class SpecParserTests : Specification
    {
        private SpecParser _parser;
        private ParsedSpec _result;

        [Test]
        public void Basic_parsing_of_one_scenario()
        {
            When_parsing_specs(
                @"
Scenario: Deposit money into an account
    Given an account
    And the account is not closed
    When I attempt to deposit money
    And some other stuff too
    Then the balance should increase by the amount of the deposit
    And it should tell the user, ""Deposit successful."" 
") ;
            Then_it_should_generate_test_methods_named_after_the_scenario_title(new[] {"Deposit_money_into_an_account"});
            Then_the_tests_should_call_the_helper_methods_from_the_test_method(
                "Deposit_money_into_an_account",
                new[]
                    {
                        "Given_an_account",
                        "Given_the_account_is_not_closed",
                        "When_I_attempt_to_deposit_money",
                        "When_some_other_stuff_too",
                        "Then_the_balance_should_increase_by_the_amount_of_the_deposit",
                        "Then_it_should_tell_the_user_Deposit_successful"
                    });
            Then_it_should_generate_helper_methods(
                new[]
                    {
                        "Given_an_account",
                        "Given_the_account_is_not_closed",
                        "When_I_attempt_to_deposit_money",
                        "When_some_other_stuff_too",
                        "Then_the_balance_should_increase_by_the_amount_of_the_deposit",
                        "Then_it_should_tell_the_user_Deposit_successful",
                    });
        }

        [Test]
        public void Multiple_scenarios()
        {
            When_parsing_specs(
                @"
Scenario: Deposit money into an account
    Given an account
    When I attempt to deposit money
    Then the balance should increase by the amount of the deposit

Scenario: Withdraw
    Given an account
    When I attempt to withdraw money
    Then the balance should decrease by the amount of the deposit
") ;
            Then_it_should_generate_test_methods_named_after_the_scenario_title(new[] {"Deposit_money_into_an_account", "Withdraw"});
            Then_the_tests_should_call_the_helper_methods_from_the_test_method(
                "Deposit_money_into_an_account",
                new[]
                    {
                        "Given_an_account",
                        "When_I_attempt_to_deposit_money",
                        "Then_the_balance_should_increase_by_the_amount_of_the_deposit",
                    });
            Then_the_tests_should_call_the_helper_methods_from_the_test_method(
                "Withdraw",
                new[]
                    {
                        "Given_an_account",
                        "When_I_attempt_to_withdraw_money",
                        "Then_the_balance_should_decrease_by_the_amount_of_the_deposit",
                    });

            // should group the givens, whens, and thens together 
            Then_it_should_generate_helper_methods(
                new[]
                    {
                        "Given_an_account",
                        "When_I_attempt_to_deposit_money",
                        "When_I_attempt_to_withdraw_money",
                        "Then_the_balance_should_increase_by_the_amount_of_the_deposit",
                        "Then_the_balance_should_decrease_by_the_amount_of_the_deposit",
                    });
        }

        [Test]
        public void Handling_special_characters_in_scenarios_and_methods()
        {
            When_parsing_specs(
                @"
Scenario: Foo-123!@#$%^&*(){}[]:;'""<,>.?x
    Given abc!@#$%^&*(){}[]:;'""<,>.?/123
") ;
            Then_it_should_generate_test_methods_named_after_the_scenario_title(new[] {"Foo_123_x"});
            Then_the_tests_should_call_the_helper_methods_from_the_test_method(
                "Foo_123_x",
                new[]
                    {
                        "Given_abc_123",
                    });
            Then_it_should_generate_helper_methods(
                new[]
                    {
                        "Given_abc_123",
                    }); 
        }

        private void When_parsing_specs(string specText)
        {
            _parser = new SpecParser();
            _result = _parser.Parse(specText);
        }

        private void Then_it_should_generate_test_methods_named_after_the_scenario_title(IList<string> expectedTestMethodNames)
        {
            _result.Scenarios.Count().ShouldEqual(expectedTestMethodNames.Count());

            for (var i = 0; i < _result.Scenarios.Count(); i++ )
            {
                _result.Scenarios[i].Name.ShouldEqual(expectedTestMethodNames[i]);
            }
        }

        private void Then_the_tests_should_call_the_helper_methods_from_the_test_method(string testName, IList<string> calledMethods)
        {
            var test = _result.Scenarios.Single(t => t.Name == testName);

            test.CalledMethods.Count().ShouldEqual(calledMethods.Count());

            for (var i = 0; i < test.CalledMethods.Count(); i++ )
            {
                test.CalledMethods[i].ShouldEqual(calledMethods[i]);
            }
        }

        private void Then_it_should_generate_helper_methods(IList<string> expectedHelperMethods)
        {
            _result.HelperMethods.Count().ShouldEqual(expectedHelperMethods.Count());

            for (var i = 0; i < _result.HelperMethods.Count(); i++ )
            {
                _result.HelperMethods[i].ShouldEqual(expectedHelperMethods[i]);
            }
        }
    }
}
