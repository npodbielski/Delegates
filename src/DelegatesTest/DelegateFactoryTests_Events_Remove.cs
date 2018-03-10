// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Events_Remove.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace
#if NET35
        DelegatesTestNET35
#elif NET4
        DelegatesTestNET4
#elif NET45
        DelegatesTestNET45
#elif NET46
    DelegatesTestNET46
#elif PORTABLE
        DelegatesTestNETPortable
#elif NETCORE
        DelegatesTestNETCORE
#elif NETSTANDARD1_1
        DelegatesTestNETStandard11
#elif NETSTANDARD1_5
        DelegatesTestNETStandard15
#endif
{
    [TestClass]
    public class DelegateFactoryTests_Events_Remove
    {
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

            var testClassInstance = new TestClass();
            testClassInstance.AddPublicEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokePublicEvent();
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

            var testClassInstance = new TestClass();
            testClassInstance.AddInternalEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokeInternalEvent();
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

            var testClassInstance = new TestClass();
            testClassInstance.AddProtectedEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokeProtectedEvent();
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

            var testClassInstance = new TestClass();
            testClassInstance.AddPrivateEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokePrivateEvent();
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
            var accessor = typeof(TestClass).EventRemove<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPublicEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokePublicEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Internal()
        {
            var accessor = typeof(TestClass).EventRemove<TestClass.InternalEventArgs>("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.InternalEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddInternalEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokeInternalEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Protected()
        {
            var accessor = typeof(TestClass).EventRemove<TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.ProtectedEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddProtectedEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokeProtectedEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Private()
        {
            var accessor = typeof(TestClass).EventRemove<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PrivateEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPrivateEventHandler(EventHandler);
            accessor(testClassInstance, EventHandler);
            testClassInstance.InvokePrivateEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_NotExistingEvent()
        {
            var accessor = typeof(TestStruct).EventRemove<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventRemove_ByObjectAndType_Incorrect_EventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                typeof(TestClass).EventRemove<TestClass.PublicEventArgs>("PrivateEvent");
            });
        }

        [TestMethod]
        public void EventRemove_ByObjects_Public()
        {
            var testClassType = typeof(TestClass);
            var addAccessor = testClassType.EventAdd("PublicEvent");
            var removeAccessor = testClassType.EventRemove("PublicEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokePublicEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_Internal()
        {
            var testClassType = typeof(TestClass);
            var addAccessor = testClassType.EventAdd("InternalEvent");
            var removeAccessor = testClassType.EventRemove("InternalEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokeInternalEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_Protected()
        {
            var testClassType = typeof(TestClass);
            var addAccessor = testClassType.EventAdd("ProtectedEvent");
            var removeAccessor = testClassType.EventRemove("ProtectedEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokeProtectedEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_Private()
        {
            var testClassType = typeof(TestClass);
            var addAccessor = testClassType.EventAdd("PrivateEvent");
            var removeAccessor = testClassType.EventRemove("PrivateEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokePrivateEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_ByObjects_NotExistingEvent()
        {
            var accessor = typeof(TestStruct).EventRemove("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventRemove_Interface_ByObjects()
        {
            var interfaceType = typeof(IService);
            var addAccessor = interfaceType.EventAdd("Event");
            var removeAccessor = interfaceType.EventRemove("Event");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var interfaceImpl = new Service();
            addAccessor(interfaceImpl, EventAction);
            removeAccessor(interfaceImpl, EventAction);
            interfaceImpl.InvokeEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_Interface_ByObjectAndType()
        {
            var interfaceType = typeof(IService);
            var addAccessor = interfaceType.EventAdd<EventArgs>("Event");
            var removeAccessor = interfaceType.EventRemove<EventArgs>("Event");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var interfaceImpl = new Service();
            addAccessor(interfaceImpl, EventAction);
            removeAccessor(interfaceImpl, EventAction);
            interfaceImpl.InvokeEvent();
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

            var interfaceImpl = new Service();
            addAccessor(interfaceImpl, EventAction);
            removeAccessor(interfaceImpl, EventAction);
            interfaceImpl.InvokeEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var addAccessor = testClassType.EventAdd("CusDelegEvent");
            var removeAccessor = testClassType.EventRemove("CusDelegEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
            Assert.IsFalse(eventExecuted);
        }

        [TestMethod]
        public void EventRemove_CustomDelegate_ByObjectAndType()
        {
            var testClassType = typeof(TestClass);
            var addAccessor = testClassType.EventAdd<EventArgs>("CusDelegEvent");
            var removeAccessor = testClassType.EventRemove<EventArgs>("CusDelegEvent");
            Assert.IsNotNull(removeAccessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
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

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
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

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
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

            var testClassInstance = new TestClass();
            addAccessor(testClassInstance, EventAction);
            removeAccessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
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