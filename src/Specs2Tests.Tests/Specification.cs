using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Impl;
using Rhino.Mocks.Interfaces;

namespace Specs2Tests.Tests
{
    [TestFixture]
    public abstract class Specification
    {
        private MockRepository _mocks;

        [TestFixtureSetUp]
        public virtual void MainSetup()
        {
            DoSetup();
        }

        [TestFixtureTearDown]
        public virtual void MainTeardown()
        {
            DoTeardown();
        }

        protected virtual void DoSetup()
        {
            Before_Establish_context();
            Establish_context();
            After_Establish_context();

            ReplayAll();
            Because_of();
        }

        protected virtual void DoTeardown()
        {
            Before_Cleanup();
            Cleanup();
            After_Cleanup();
        }

        protected virtual void Before_Establish_context()
        {

        }

        protected virtual void Establish_context()
        {

        }

        protected virtual void After_Establish_context()
        {

        }

        protected virtual void Because_of()
        {

        }

        protected virtual void Before_Cleanup()
        {

        }

        protected virtual void Cleanup()
        {

        }

        protected virtual void After_Cleanup()
        {

        }

        protected virtual T CreateStub<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        protected virtual T CreateMock<T>() where T : class
        {
            return MockRepository.GenerateMock<T>();
        }

        protected T CreatePartialMock<T>() where T : class
        {
            if (_mocks == null)
                _mocks = new MockRepository();
            return _mocks.PartialMock<T>();
        }

        protected T CreatePartialMock<T>(params object[] args) where T : class
        {
            if (_mocks == null)
                _mocks = new MockRepository();
            return _mocks.PartialMock<T>(args);
        }

        protected void RaiseEvent(object mock, string eventName, object sender, EventArgs args)
        {
            new EventRaiser((IMockedObject)mock, eventName).Raise(sender, args);
        }

        protected void SpecNotImplemented()
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();

            SpecNotImplemented(caller.DeclaringType.Name + "." + caller.Name);
        }

        protected void SpecNotImplemented(string specName)
        {
            Console.WriteLine("Specification not implemented : " + specName);
            throw new NotImplementedException();
        }

        protected virtual void ReplayAll()
        {
            if (_mocks != null)
                _mocks.ReplayAll();
        }
    }
}