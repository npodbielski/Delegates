// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Events_Remove.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelegatesTest
{
    [TestClass]
    public class DelegateFactoryTests_Events_Remove
    {
        private readonly IService _interfaceImpl = new Service();
        private readonly Type _interfaceType = typeof(IService);
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);

        [TestMethod]
        public void EventRemove_ByTypes_Public()
        {
            var accessor = DelegateFactory.EventRemove<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            _testClassInstance.AddPublicEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokePublicEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByTypes_Internal()
        {
            var accessor = DelegateFactory.EventRemove<TestClass, TestClass.InternalEventArgs>("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.InternalEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            _testClassInstance.AddInternalEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokeInternalEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByTypes_Protected()
        {
            var accessor = DelegateFactory.EventRemove<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.ProtectedEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            _testClassInstance.AddProtectedEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokeProtectedEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByTypes_Private()
        {
            var accessor = DelegateFactory.EventRemove<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PrivateEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            _testClassInstance.AddPrivateEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokePrivateEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByTypes_NotExistingEvent()
        {
            var accessor = DelegateFactory.EventRemove<TestStruct, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventRemove_ByTypes_Incorrect_EventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                DelegateFactory.EventRemove<TestClass, TestClass.PublicEventArgs>("PrivateEvent");
            });
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Public()
        {
            var accessor = _testClassType.EventRemove<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            _testClassInstance.AddPublicEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokePublicEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Internal()
        {
            var accessor = _testClassType.EventRemove<TestClass.InternalEventArgs>("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.InternalEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            _testClassInstance.AddInternalEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokeInternalEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Protected()
        {
            var accessor = _testClassType.EventRemove<TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.ProtectedEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            _testClassInstance.AddProtectedEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokeProtectedEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Private()
        {
            var accessor = _testClassType.EventRemove<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PrivateEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            _testClassInstance.AddPrivateEventHandler(EventHandler);
            accessor(_testClassInstance, EventHandler);
            _testClassInstance.InvokePrivateEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_NotExistingEvent()
        {
            var accessor = _testStructType.EventRemove<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Incorrect_EventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                _testClassType.EventRemove<TestClass.PublicEventArgs>("PrivateEvent");
            });
        }

        [TestMethod]
        public void EventRemove_ByObjects_Public()
        {
            var addAccessor = _testClassType.EventAdd("PublicEvent");
            var removeAccessor = _testClassType.EventRemove("PublicEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokePublicEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_Internal()
        {
            var addAccessor = _testClassType.EventAdd("InternalEvent");
            var removeAccessor = _testClassType.EventRemove("InternalEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokeInternalEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_Protected()
        {
            var addAccessor = _testClassType.EventAdd("ProtectedEvent");
            var removeAccessor = _testClassType.EventRemove("ProtectedEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokeProtectedEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_Private()
        {
            var addAccessor = _testClassType.EventAdd("PrivateEvent");
            var removeAccessor = _testClassType.EventRemove("PrivateEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokePrivateEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_NotExistingEvent()
        {
            var accessor = _testStructType.EventRemove("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventRemove_Interface_ByObjects()
        {
            var addAccessor = _interfaceType.EventAdd("Event");
            var removeAccessor = _interfaceType.EventRemove("Event");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_interfaceImpl, EventAction);
            removeAccessor(_interfaceImpl, EventAction);
            _interfaceImpl.InvokeEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_Interface_ByObjectAndType()
        {
            var addAccessor = _interfaceType.EventAdd<EventArgs>("Event");
            var removeAccessor = _interfaceType.EventRemove<EventArgs>("Event");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_interfaceImpl, EventAction);
            removeAccessor(_interfaceImpl, EventAction);
            _interfaceImpl.InvokeEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_Interface_ByTypes()
        {
            var addAccessor = DelegateFactory.EventAdd<IService, EventArgs>("Event");
            var removeAccessor = DelegateFactory.EventRemove<IService, EventArgs>("Event");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_interfaceImpl, EventAction);
            removeAccessor(_interfaceImpl, EventAction);
            _interfaceImpl.InvokeEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_ByObjects()
        {
            var addAccessor = _testClassType.EventAdd("CusDelegEvent");
            var removeAccessor = _testClassType.EventRemove("CusDelegEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_ByObjectAndType()
        {
            var addAccessor = _testClassType.EventAdd<EventArgs>("CusDelegEvent");
            var removeAccessor = _testClassType.EventRemove<EventArgs>("CusDelegEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_ByTypeAndObject()
        {
            var addAccessor = DelegateFactory.EventAdd<TestClass>("CusDelegEvent");
            var removeAccessor = DelegateFactory.EventRemove<TestClass>("CusDelegEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(TestClass sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_ByTypesAndEventHandler()
        {
            var addAccessor = DelegateFactory.EventAdd<TestClass, EventArgs>("CusDelegEvent");
            var removeAccessor = DelegateFactory.EventRemove<TestClass, EventArgs>("CusDelegEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_ByTypes()
        {
            var addAccessor = DelegateFactory.EventAddCustomDelegate<TestClass, EventCustomDelegate>("CusDelegEvent");
            var removeAccessor =
                DelegateFactory.EventRemoveCustomDelegate<TestClass, EventCustomDelegate>("CusDelegEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            addAccessor(_testClassInstance, EventAction);
            removeAccessor(_testClassInstance, EventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_IncompatibleDelegate()
        {
            ArgumentException ae = null;
            try
            {
                DelegateFactory.EventRemoveCustomDelegate<TestClass, EventHandler<ConsoleCancelEventArgs>>(
                    "CusDelegEvent");
            }
            catch (ArgumentException e)
            {
                ae = e;
            }

            Assert.IsNotNull(ae);
            Assert.IsInstanceOfType(ae, typeof(ArgumentException));
        }
    }
}